﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: Excel批量导入数据，业务类型
 *********************************************************/
using tsdn.Common.Core;
using System.Collections.Concurrent;

namespace tsdn.Utility.Excel
{
    /// <summary>
    /// Excel导入数据对应类型枚举
    /// </summary>
    public enum ExcelImportType
    {
        Assets = 0
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExcelImporMapper
    {
        /// <summary>
        /// 业务类型模板文件路径缓存
        /// </summary>
        private static ConcurrentDictionary<ExcelImportType, string> _fileMappingDict = null;

        /// <summary>
        /// 根据业务类型获取模版文件路径
        /// </summary>
        /// <param name="type">业务类型</param>
        /// <returns>模版文件路径</returns>
        public static string GetTemplatePath(ExcelImportType type)
        {
            InitMapping();
            return _fileMappingDict[type];
        }


        /// <summary>
        /// 初始化模版文件路径缓存
        /// </summary>
        private static void InitMapping()
        {
            if (_fileMappingDict == null)
            {
                _fileMappingDict = new ConcurrentDictionary<ExcelImportType, string>();
                _fileMappingDict.TryAdd(ExcelImportType.Assets, FileHelper.GetAbsolutePath("/Template/Excel/设备批量注册.xls"));
            }
        }
    }
}
