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
    /// 外地违法统计配置
    /// </summary>
    
    [ConfigType(Group = "IllegalCountConfig", GroupCn = "外地违法统计", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalCountConfig:ConfigOption
    {
        [Config(Name = "号牌省份开头", DefaultValue = "鄂", Required = true, Title = "号牌省份开头")]
        public static string Province { get; set; }

        [Config(Name = "号牌城市开头", DefaultValue = "鄂A", Required = true, Title = "号牌城市开头")]
        public static string City { get; set; }

        [Config(Name = "已上传违法状态码", DefaultValue = "1004", Required = true, Title = "已上传字典值")]
        public static string Status { get; set; }

        [Config(Name = "是否执行定时统计违法任务", DefaultValue = false, Required = true, Title = "是否执行定时统计违法任务")]
        public static bool ExcuteTask { get; set; }
    }
}
