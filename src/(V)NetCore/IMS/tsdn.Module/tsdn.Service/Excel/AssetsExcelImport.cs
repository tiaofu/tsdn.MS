/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Utility.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using tsdn.Common;
using System.Threading.Tasks;
using System.Linq;

namespace tsdn.Service.Excel
{
    /// <summary>
    /// 设备批量注册服务
    /// </summary>
    public class AssetsExcelImport : ExcelImport
    {
        /// <summary>
        /// 
        /// </summary>
        public override ExcelImportType Type => ExcelImportType.Assets;

        /// <summary>
        /// 
        /// </summary>
        public override Dictionary<string, ImportVerify> DictFields => new List<ImportVerify> {
            new ImportVerify{ ColumnName= "设备编号",FieldName="AssetsNo"},
            new ImportVerify{ ColumnName="设备名称",FieldName="AssetsName"},
            new ImportVerify{ ColumnName="设备代码",FieldName="PlateCode"},
            new ImportVerify{ ColumnName="设备类型",FieldName="AssetsType"},
            new ImportVerify{ ColumnName="设备厂商",FieldName="Manufacturer"},
            new ImportVerify{ ColumnName="设备状态",FieldName="Status"},
            new ImportVerify{ ColumnName="设备IP",FieldName="IP"},
            new ImportVerify{ ColumnName="所属项目",FieldName="ProjectID"},
            new ImportVerify{ ColumnName="管辖单位",FieldName="DepartmentId"},

            new ImportVerify{ ColumnName="所在路口",FieldName="SpottingID"},
            new ImportVerify{ ColumnName="关联方向",FieldName="DirectionName"},
            new ImportVerify{ ColumnName="监控车道",FieldName="Lane"},
            new ImportVerify{ ColumnName="设备型号",FieldName="AssetsModel"},
            new ImportVerify{ ColumnName="检测单位",FieldName="DetectionCompanyId"},
            new ImportVerify{ ColumnName="检测方式",FieldName="DetectionMode" },
            new ImportVerify{ ColumnName="备注",FieldName="Remark" },
            new ImportVerify{ ColumnName="厂商编号",FieldName="CustomassetsNo" }
        }.ToDictionary(e => e.ColumnName, e => e);


        /// <summary>
        ///返回对应的导出模版数据
        /// </summary>
        /// <param name="FilePath">模版的路径</param>
        /// <param name="s">响应流</param>
        /// <returns>模版MemoryStream</returns>
        public async override Task GetExportTemplate(string FilePath, Stream s)
        {
            await Task.Factory.StartNew(() =>
            {
                var sheet = NPOIHelper.GetFirstSheet(FilePath);
                int dataRowIndex = StartRowIndex + 1;
                //设置检测方式下拉选择
                string[] assestJCFS = new string[] { "测试1", "测试2" };
                NPOIHelper.SetHSSFValidation(sheet, assestJCFS, dataRowIndex, 14);
                sheet.Workbook.Write(s);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="extraInfo"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public override object SaveImportData(DataTable dt, Dictionary<string, object> extraInfo, UserInfo userInfo)
        {
            return dt;
        }
    }
}
