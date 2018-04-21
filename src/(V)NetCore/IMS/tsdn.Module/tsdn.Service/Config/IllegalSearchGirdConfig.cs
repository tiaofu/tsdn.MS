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
    /// 违法查询列表分页模式
    /// </summary>
    

    [ConfigType(Group = "IllegalSearchGirdConfig", GroupCn = "违法查询列表分页模式", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalSearchGirdConfig : ConfigOption
    {
        /// <summary>
        /// 分页模式
        /// </summary>
        [Config(Name = "分页模式", ValueType = ConfigValueType.Select, DefaultValue = "0", DataSource = "tsdn.Service,tsdn.ConfigHandler.IllegalSearchGirdConfig.GetModeDs")]
        public static string PageMode { get; set; }

        /// <summary>
        /// 获取分页模式下拉数据
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetModeDs()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = 0,
                Text = "默认(滚动加载)"
            });
            list.Add(new
            {
                Value = 1,
                Text = "传统列表分页"
            });
            return list;
        }
    }
}
