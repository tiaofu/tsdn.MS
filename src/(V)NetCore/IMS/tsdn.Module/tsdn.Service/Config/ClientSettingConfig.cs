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
    /// 视频客户端配置
    /// </summary>

    [ConfigType(Group = "ClientSettingConfig", GroupCn = "视频客户端配置", ImmediateUpdate = true, FunctionType = "系统管理")]
    public class ClientSettingConfig : ConfigOption
    {
        /// <summary>
        /// 最大路数
        /// </summary>
        [Config(Name = "最大路数", DefaultValue = "4,6", Title = "用于控制客户端权限的视频最大路数")]
        public static string MaxNum { get; set; }


        /// <summary>
        /// 匹配客户端关键字
        /// </summary>
        [Config(Name = "匹配客户端关键字", DefaultValue = "指挥室", Title = "用于推送至与此关键字相关联的所有客户端")]
        public static string KeyWord { get; set; }
    }
}
