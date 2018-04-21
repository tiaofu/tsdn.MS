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
    /// 登录自定义配置
    /// </summary>

    [ConfigType(Group = "LoginConfig", GroupCn = "登录页配置", CustomPage = "/SysManage/LoginConfig/Index", FunctionType = "系统管理")]
    public class LoginConfig: ConfigOption
    {
        /// <summary>
        /// 白天模板
        /// </summary>
        [Config(Name = "白天模板", DefaultValue = "", Required = false)]
        public static string DayTimeBlackModel { get; set; }
        /// <summary>
        /// 白天皮肤
        /// </summary>
        [Config(Name = "皮肤", DefaultValue = "", Required = false)]
        public static string DayTimeBlackSkin { get; set; }
        /// <summary>
        /// 黑夜模板
        /// </summary>
        [Config(Name = "黑夜模板", DefaultValue = "", Required = false)]
        public static string NightBlackModel{ get; set; }

        /// <summary>
        /// 白天皮肤
        /// </summary>
        [Config(Name = "蓝色皮肤", DefaultValue = "", Required = false)]
        public static string DayTimeBlueSkin { get; set; }
        /// <summary>
        /// 白天蓝色模板
        /// </summary>
        [Config(Name = "白天蓝色模板", DefaultValue = "", Required = false)]
        public static string DayTimeBlueModel { get; set; }

        /// <summary>
        /// 黑夜蓝色模板
        /// </summary>
        [Config(Name = "黑夜蓝色模板", DefaultValue = "", Required = false)]
        public static string NightBlueModel { get; set; }

        /// <summary>
        /// 深蓝色皮肤
        /// </summary>
        [Config(Name = "深蓝色皮肤", DefaultValue = "", Required = false)]
        public static string DayTimedarkBlueSkin { get; set; }
        /// <summary>
        /// 深蓝色白天模板
        /// </summary>
        [Config(Name = "白天深蓝色模板", DefaultValue = "", Required = false)]
        public static string DayTimedarkBlueModel { get; set;}
        /// <summary>
        /// 深蓝色黑夜模板
        /// </summary>
        [Config(Name = "黑夜深蓝色模板", DefaultValue = "", Required = false)]
        public static string NightdarkBlueModel { get; set; }
    }
}
