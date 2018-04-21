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
    /// 违法审核违法类型修改配置
    /// </summary>


    [ConfigType(Group = "IllegalApproveConfig", GroupCn = "违法审核-违法编号修改配置", ImmediateUpdate = true, CustomPage = "/Punish/IllegalHandler/ApproveConfig", FunctionType = "交通违法")]
    public class IllegalApproveConfig : ConfigOption
    {
        /// <summary>
        /// 可修改的违法编号
        /// </summary>
        [Config(Name = "可修改的违法编号")]
        public static string IllegalType { get; set; }

        /// <summary>
        /// 修改后的违法编号
        /// </summary>
        [Config(Name = "修改后的违法编号")]
        public static string ChangeIllegalType { get; set; }
    }
}
