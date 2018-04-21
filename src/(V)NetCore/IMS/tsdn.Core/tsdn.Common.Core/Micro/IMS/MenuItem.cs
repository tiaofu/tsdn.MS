/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Collections.Generic;

namespace tsdn.Common.Micro.IMS
{
    /// <summary>
    /// 功能菜单
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 功能代码
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 功能代码
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string FunctionIcon { get; set; }

        /// <summary>
        /// 页面地址
        /// </summary>
        public string FunctionUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FunctionType { get; set; } = "1";

        ///<summary>
        ///说明 
        ///</summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ActionItem> ListAction { get; set; }
    }

    /// <summary>
    /// 动作点定义
    /// </summary>
    public class ActionItem
    {
        ///<summary>
        ///说明 
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///动作名称 
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///动作代码 
        ///</summary>
        public string Code { get; set; }
    }
}
