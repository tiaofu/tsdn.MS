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
    /// ISS系统参数配置
    /// </summary>

    [ConfigType(Group = "ISSConfig", GroupCn = "ISS参数配置", ImmediateUpdate = true, FunctionType = "后台参数配置")]
    public class ISSConfig: ConfigOption
    {
        /// <summary>
        /// ISS大小车判定
        /// </summary>
        [Config(Name = "ISS大小车判定标识", DefaultValue = "学", Required = false, Title = "ISS大小车判定标识")]
        public static string ISSForceSmall { get; set; }
    }
}
