/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.MVC;
using tsdn.Utility.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using tsdn.Core.Net;
using tsdn.Common.Config;
using System.IO;
using tsdn.Utility;
using tsdn.Common.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace tsdn.Common.Web.Controllers
{
    /// <summary>
    /// Excel导入导出接口
    /// </summary>
    [Route("api/Common/[controller]")]
    [SwaggerHidden]
    public class ExcelController : ApiController
    {
        private readonly IEnumerable<ExcelImport> AllImports;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allImports"></param>
        public ExcelController(IEnumerable<ExcelImport> allImports)
        {
            AllImports = allImports;
        }

        /// <summary>
        /// 通用导出EXECL接口
        /// </summary>
        /// <param name="info">列表参数信息</param>
        /// <returns>内存流信息</returns>
        [HttpPost]
        [Route("[action]")]
        public ApiResult<ExcelDownloadInfo> Export([FromBody]ExcelInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            ApiResult<ExcelDownloadInfo> result = new ApiResult<ExcelDownloadInfo>();
            result.Result = new ExcelDownloadInfo();
            string fileExt = info.GetFileExt();
            if (string.IsNullOrEmpty(info.FileName))
            {
                info.FileName = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + fileExt;
            }
            else
            {
                if (!info.FileName.EndsWith(fileExt))
                {
                    info.FileName = info.FileName + fileExt;
                }
            }
            //mimeType
            string mineType = MimeHelper.GetMineType(info.FileName);

            if (!info.IsExportSelectData)
            {
                //设置最大导出条数
                info.Condition = info.Condition == null ? new QueryCondition() : info.Condition;
                info.Condition.PageSize = SystemConfig.MaxExport;
                if (string.IsNullOrEmpty(info.Api))
                {
                    throw new ArgumentNullException(nameof(info.Api));
                }
                if (!info.Api.StartsWith(Request.Scheme))
                {
                    info.Api = $"{Request.Scheme}://{Request.Host}{info.Api}";
                }
            }
            string[] arrPath = GetTempFilePath(UrlScheme, info.FileName);
            using (MemoryStream ms = info.ExportExeclStream(token))
            {
                using (FileStream fs = new FileStream(arrPath[0], FileMode.CreateNew))
                {
                    ms.WriteTo(fs);
                }
            }
            result.Result.Name = info.FileName;
            result.Result.Url = arrPath[1];
            return result;
        }

        /// <summary>
        /// 导出Excel模版
        /// </summary>
        /// <param name="type">业务类型</param>
        /// <param name="FunctionCode">对应功能模块Code</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DownLoadTemplate(ExcelImportType type, string FunctionCode = "")
        {
            if (AllImports == null)
            {
                throw new ArgumentNullException("系统不存在Excel批量导入业务处理模块");
            }
            var handler = AllImports.FirstOrDefault(e => e.Type == type);
            if (handler == null)
            {
                throw new Exception("未找到“" + type.ToString() + "”相应处理模块");
            }

            string path = ExcelImporMapper.GetTemplatePath(type);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    if (handler.DowloadNeedUserInfo)
                    {
                        await handler.GetExportTemplate(path, ms, CurrentUserInfo);
                    }
                    else
                    {
                        await handler.GetExportTemplate(path, ms);
                    }
                    ms.Position = 0;
                    return File(ms, MimeHelper.GetMineType(path), Path.GetFileName(path));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("未找到“" + type.ToString() + "”对应模版文件");
            }
        }

        /// <summary>
        /// 导入Excel模版
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ImportTemplate(ExcelImportType type, string FunctionCode = "")
        {
            ImportResult result = new ImportResult();
            try
            {
                if (AllImports == null)
                {
                    throw new ArgumentNullException("系统不存在Excel批量导入业务处理模块");
                }
                var handler = AllImports.FirstOrDefault(e => e.Type == type);
                if (handler == null)
                {
                    throw new Exception("未找到“" + type.ToString() + "”相应处理模块");
                }
                //文件
                var file = Request.Form.Files[0];
                using (MemoryStream ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    ms.Position = 0;
                    result = await handler.ImportTemplate(ms, file.FileName, CurrentUserInfo);
                }
                if (result.IsSuccess)
                {
                    //是否获取详细数据，决定后台是否返回 result.ExtraInfo
                    string ReturnDetailData = Request.Query["ReturnDetailData"];
                    if (string.IsNullOrEmpty(ReturnDetailData) || ReturnDetailData != "1")
                    {
                        result.ExtraInfo = null;
                    }
                }
                else
                {
                    //设置错误模版http路径
                    result.Message = $"{Request.Scheme}://{Request.Host}{result.Message}";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return Content(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// 获取文件临时保存路径 
        /// [0] 本地绝对路径 TempFiles\年-月-日\Guid\aa.xxx
        /// [1] URL下载地址
        /// </summary>
        /// <param name="urlScheme">host</param>
        /// <param name="fileName">文件名</param>
        /// <returns>文件路径</returns>
        private static string[] GetTempFilePath(string urlScheme, string fileName)
        {
            string fileExt = Path.GetExtension(fileName);
            string uuid = GuidHelper.GetSeqGUID();
            string tempFileName = $"{GuidHelper.GetSeqGUID()}{fileExt}";
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            string[] arrPath = new string[2];
            var dir = FileHelper.GetDirectoryPath($"TempFiles\\{time}", true);
            dir = FileHelper.GetDirectoryPath($"TempFiles\\{time}\\{uuid}", true);
            arrPath[0] = $"{dir}\\{fileName}";
            arrPath[1] = $"{urlScheme}/TempFiles/{time}/{uuid}/{fileName}";
            return arrPath;
        }
    }
}
