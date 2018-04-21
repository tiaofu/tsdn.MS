﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using NPOI.SS.UserModel;

namespace tsdn.Utility.Excel
{
    /// <summary>
    /// DataGrid信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public string Align { get; set; }

        /// <summary>
        /// 标题头
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 绑定列
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 内容对齐方式
        /// </summary>
        public ICellStyle Style { get; set; }

        /// <summary>
        /// 是否超链接列
        /// </summary>
        public bool IsLink { get; set; }

        /// <summary>
        /// 是否在数据集中存在对应列
        /// </summary>
        public bool IsMapDT { get; set; }
    }
}