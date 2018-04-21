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
using tsdn.Common.Config;

namespace tsdn.ConfigHandler
{
    /// <summary>
    /// 违法审核布局配置
    /// </summary>


    [ConfigType(Group = "IllegalApproveLayout", GroupCn = "违法审核布局配置", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalApproveLayout : ConfigOption
    {
        [Config(Name = "布局结构", ValueType = ConfigValueType.Select, DefaultValue = "LeftRight", DataSource = "tsdn.Service,tsdn.ConfigHandler.IllegalApproveLayout.GetLayout")]
        public static string Layout { get; set; }

        [Config(Name = "打开方式", ValueType = ConfigValueType.Select, DefaultValue = "In", DataSource = "tsdn.Service,tsdn.ConfigHandler.IllegalApproveLayout.GetOpenType")]
        public static string OpenType { get; set; }


        /// <summary>
        /// 获取布局结构
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetLayout()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = "UpDown",
                Text = "上下结构"
            });
            list.Add(new
            {
                Value = "LeftRight",
                Text = "左右结构"
            });
            return list;
        }

        /// <summary>
        /// 打开方式
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetOpenType()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = "In",
                Text = "layer弹出"
            });
            list.Add(new
            {
                Value = "Out",
                Text = "新开窗口"
            });
            return list;
        }
    }
}
