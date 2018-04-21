/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.Data;
using System.IO;
using System.Net.Http;

namespace tsdn.Utility.Excel
{
    /// <summary>
    /// Execl操作帮助类
    /// </summary>
    public static class ExcelUtil
    {
        /// <summary>
        /// 拓展方法,生成EXECL
        /// </summary>
        /// <param name="info">EXECL相关信息</param>
        /// <param name="token">用户认证令牌</param>    
        /// <returns>Execl路径</returns>
        public static MemoryStream ExportExeclStream(this ExcelInfo info, string token)
        {
            //1.获取列表对应数据
            DataTable dt = GetGirdData(info, token);
            //2.创建文档
            MemoryStream ms = null;
            switch (info.FileFormat)
            {
                case ExportFileFormat.Excel:
                    //导出Excel
                    ms = NPOIHelper.Export(dt, info);
                    break;
                case ExportFileFormat.Word:
                    //导出Word
                    //ms = WordHelper.Export(dt, info, guid);
                    break;
                case ExportFileFormat.Pdf:
                    //导出Pdf
                    //ms = PdfHelper.Export(dt, info, guid);
                    break;
            }
            return ms;
        }


        /// <summary>
        /// 从WebAPI中获取列表数据
        /// </summary>
        /// <param name="info">EXECL相关信息</param>
        /// <param name="token">用户认证令牌</param>
        /// <returns></returns>
        private static DataTable GetGirdData(ExcelInfo info, string token)
        {
            if (info.IsExportSelectData)
            {
                if (info.Data == null)
                {
                    info.Data = new DataTable();
                }
                return info.Data;
            }
            try
            {
                HttpMethod method = HttpMethod.Get;
                if (info.Type.Equals(HttpMethod.Post.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    method = HttpMethod.Post;
                }
                tsdnHttpRequest request = new tsdnHttpRequest
                {
                    Method = method,
                    Token = token,
                    AddressUrl = info.Api,
                    Body = info.Condition
                };
                var responseJson = request.SendAsync<ExcelApiResult>().Result;
                if (!responseJson.HasError && responseJson.Message != "logout")
                {
                    return info.ConvertDataEx2Data(responseJson.Result);
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(info.ColumnInfoList[0].Field);
                    DataRow dr = dt.NewRow();
                    //接口报错
                    if (responseJson.HasError)
                    {
                        dr[0] = responseJson.Message;
                    }
                    if (responseJson.Message == "logout")
                    {
                        dr[0] = "登录超时,请刷新页面重试";
                    }
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
