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
    /// 审核记录
    /// </summary>
    

    [ConfigType(Group = "IllegalHistoryApproveConfig", GroupCn = "审核记录", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalHistoryApproveConfig : ConfigOption
    {
        /// <summary>
        /// 可处理数据有效期
        /// </summary>
        [Config(Name = "可处理数据有效期(分钟)", Required = true, DefaultValue = 120, ValidateRule = "min=0 max=1000000 digits=true", Title ="控制审核记录可以处理最近多少分钟内的违法数据")]
        public static float OverLimitMinute { get; set; }
    }
}
