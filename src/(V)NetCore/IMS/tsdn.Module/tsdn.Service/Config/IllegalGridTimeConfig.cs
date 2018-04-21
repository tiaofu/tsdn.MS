/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Config;

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 违法模块列表时间范围配置
    /// </summary>
    
    [ConfigType(Group = "IllegalGridTimeConfig", GroupCn = "违法模块列表时间范围", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalGridTimeConfig : ConfigOption
    {
        [Config(Name = "包含今天", DefaultValue = false, Title = "列表日期显示最近X天还是前X天")]
        public static bool ContainNow { get; set; }
    }
}
