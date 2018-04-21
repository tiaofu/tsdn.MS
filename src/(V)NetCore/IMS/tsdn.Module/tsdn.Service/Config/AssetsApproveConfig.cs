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
    /// 违法审核配置信息
    /// </summary>  
    [ConfigType(Group = "AssetsApproveConfig", GroupCn = "路口设备审核配置", ImmediateUpdate = true, FunctionType = "运维管理")]
    public class AssetsApproveConfig: ConfigOption
    {
        /// <summary>
        /// 设备是否开启审核
        /// </summary>
        [Config(Name = "设备是否开启审核", DefaultValue = "false", Title = "用于控制前端设备是否需要审核")]
        public static bool IsNeedApprove { get; set; }


        /// <summary>
        /// 路口是否开启审核
        /// </summary>
        [Config(Name = "路口是否开启审核", DefaultValue = "false", Title = "用于控制路口是否需要审核")]
        public static bool SpottingIsNeedApprove { get; set; }
    }
}
