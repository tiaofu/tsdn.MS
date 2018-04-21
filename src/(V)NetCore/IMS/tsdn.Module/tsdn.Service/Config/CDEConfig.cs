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
    /// CDE系统参数配置
    /// </summary>

    [ConfigType(Group = "CDEConfig", GroupCn = "CDE参数配置", ImmediateUpdate = true, FunctionType = "后台参数配置")]
    public class CDEConfig: ConfigOption
    {
        /// <summary>
        /// CDE状态检测间隔
        /// </summary>
        [Config(Name = "CDE状态检测间隔（分钟）", DefaultValue = "15", Required = false, ValidateRule = "min=0 max=300 digits=true", Title = "按照配置，定时检测状态")]
        public static int StateInsPectInterval { get; set; }

        /// <summary>
        /// CDE状态检测间隔
        /// </summary>
        [Config(Name = "CDE延迟下载时间（分钟）", DefaultValue = "1440", Required = false, ValidateRule = "min=0 digits=true", Title = "按照配置，延迟导出数据")]
        public static int DownloadDelayTime { get; set; }

        /// <summary>
        /// CDE状态检测间隔
        /// </summary>
        [Config(Name = "CDE导出范围（最近N天）", DefaultValue = "5", Required = false, ValidateRule = "min=0 max=500 digits=true", Title = "CDE导出范围（最近N天）")]
        public static int ExportDays { get; set; }
    }
}
