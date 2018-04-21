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

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 首页数据显示权限配置
    /// </summary>


    [ConfigType(Group = "DataDisplayConfig", GroupCn = "首页数据显示权限配置", ImmediateUpdate = true, FunctionType = "系统管理")]
    public class DataDisplayConfig: ConfigOption
    {
        /// <summary>
        /// 首页数据显示权限配置
        /// </summary>
        [Config(Name = "请选择首页数据显示配置", Required = false, ValueType = ConfigValueType.Select, DataSource = "tsdn.Service,tsdn.ConfigHandler.Config.DataDisplayConfig.GetAllDataDisplay", Title = "请选择首页数据显示配置")]
        public static string DataDisplay { get; set; }
        /// <summary>
        /// 获取所有显示配置
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetAllDataDisplay()
        {
            List<dynamic> listR = new List<dynamic>() { new { Value = 0, Text = "管辖单位" }, new { Value = 1, Text = "处理单位" } };
            return listR;
        }
    }
}
