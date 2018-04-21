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
    /// 微服务配置
    /// </summary>
    

    [ConfigType(Group = "MicroServiceConfig", GroupCn = "微服务配置", ImmediateUpdate = true, FunctionType = "系统管理")]
    public class MicroServiceConfig : ConfigOption
    {
        /// <summary>
        /// 服务中心地址
        /// </summary>
        [Config(Name = "服务中心地址", Required = false, Title = "微服务中心服务器地址 IP:Port")]
        public static string DataCenterIP { get; set; }

        /// <summary>
        /// 身份认证服务地址
        /// </summary>
        [Config(Name = "身份认证服务地址", Required = false, Title = "身份认证服务地址 IP:Port")]
        public static string AuthenticationDataCenterIP { get; set; }
    }
}