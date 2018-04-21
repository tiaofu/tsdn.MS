/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Common.Config
{
    /// <summary>
    /// 系统基础配置信息
    /// </summary>

    [ConfigType(Group = "SystemConfig", GroupCn = "系统基础配置", ImmediateUpdate = true, FunctionType = "基础配置")]
    public class SystemConfig : ConfigOption
    {
        /// <summary>
        /// 系统页面标题
        /// </summary>
        [Config(Name = "系统页面标题", DefaultValue = "天津市电子警察综合管理应用平台")]
        public static string SystemTitle { get; set; }


        /// <summary>
        /// 系统页面标题
        /// </summary>
        [Config(Name = "区域编码", DefaultValue = "120000")]
        public static string AreaCode { get; set; }


        /// <summary>
        /// 系统静态资源文件版本管理
        /// </summary>
        [Config(Name = "静态资源版本", DefaultValue = "20160322195917")]
        public static string StaticVersion { get; set; }

        /// <summary>
        /// 车牌归属
        /// </summary>
        [Config(Name = "车牌归属", Title = "控制号牌选择控件优先显示归属,多个，分隔", Required = false)]
        public static string PlateBelong { get; set; }

        /// <summary>
        /// Excel导出条数 最大值
        /// </summary>
        [Config(Name = "Excel导出条数", Required = true, DefaultValue = 20000, ValidateRule = "min=1 max=1000000 digits=true")]
        public static int MaxExport { get; set; }

        /// <summary>
        /// 是否显示异常信息
        /// </summary>
        [Config(Name = "是否显示异常", DefaultValue = true)]
        public static bool ShowException { get; set; }

        /// <summary>
        /// 白天时间范围 起始时间 （用于判断白天晚上登陆）
        /// </summary>
        [Config(Name = "白天时间开始", DefaultValue = "07:00:00", Required = true, ValidateRule = "time=true")]
        public static string DayTimeBegin { get; set; }

        /// <summary>
        /// 白天时间范围 结束时间 （用于判断白天晚上登陆）
        /// </summary>
        [Config(Name = "白天时间结束", DefaultValue = "19:00:00", Required = true, ValidateRule = "time=true")]
        public static string DayTimeEnd { get; set; }

        /// <summary>
        /// 系统环境
        /// </summary>
        [Config(Name = "是否公安网", DefaultValue = false,Required =true)]
        public static bool IsPolice { get; set; }



    }
}