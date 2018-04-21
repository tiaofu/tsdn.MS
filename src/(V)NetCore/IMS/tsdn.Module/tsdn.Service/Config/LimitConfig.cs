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


namespace tsdn.ConfigHandler
{
    /// <summary>
    /// 限行比对配置 
    /// </summary>

    [ConfigType(Group = "LimitConfig", GroupCn = "限行比对配置", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class LimitConfig : ConfigOption
    {

        /// <summary>
        /// 查无此车是否比中
        /// </summary>
        [Config(Name = "查无此车是否比中（限行比中，准行不比中）", DefaultValue = "false", Required = true)]
        public static bool NoResultIsMatch { get; set; }

        /// <summary>
        /// 查询受限是否比中
        /// </summary>
        [Config(Name = "查询受限是否比中（限行比中，准行不比中）", DefaultValue = "false", Required = true)]
        public static bool LimitIsMatch { get; set; }
        
    }
}
