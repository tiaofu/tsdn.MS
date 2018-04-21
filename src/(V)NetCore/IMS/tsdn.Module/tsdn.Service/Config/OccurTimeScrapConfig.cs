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
    /// 违法入库时间异常废弃
    /// 违法入库相比入库时间(ims服务器系统时间)严重滞后或超前 则废弃
    /// </summary>


    [ConfigType(Group = "OccurTimeScrapConfig", GroupCn = "违法入库时间异常废弃", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class OccurTimeScrapConfig : ConfigOption
    {
        /// <summary>
        /// 违法数据入库时间滞后或超前废弃,废弃理由9998
        /// </summary>
        [Config(Name = "是否开启违法时间异常废弃", DefaultValue = true, Title = @"违法数据在入库时进行校验违法时间与系统时间的时间差,若是滞后或超前幅度满足下面的阀值，则废弃")]
        public static bool AutoScrapForOccerTime { get; set; }

        /// <summary>
        /// 违法时间小于系统时间开启违法阻断
        /// </summary>
        [Config(Name = "违法时间滞后系统时间阀值(分钟)", DefaultValue = 120, ValidateRule = "min=0 max=50000 digits=true", Title = @"违法时间滞后系统时间达到该阀值就废弃，最大值50000")]
        public static int OccerTimeLessThanCurrentTime { get; set; }

        /// <summary>
        /// 违法时间大于系统时间开启违法阻断
        /// </summary>
        [Config(Name = "违法时间超前系统时间阀值(分钟)", DefaultValue = 2, ValidateRule = "min=0 max=50000 digits=true", Title = @"违法时间超出系统时间达到该阀值就废弃，最大值50000")]
        public static int OccerTimeGreaterThanCurrentTime { get; set; }





    }
}
