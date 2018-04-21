/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Collections.Generic;
using tsdn.Common.Config;

namespace tsdn.ConfigHandler
{
    /// <summary>
    /// 违法审核引导页分组配置
    /// </summary>
    

    [ConfigType(Group = "IllegalApproveGroupConfig", GroupCn = "违法审核引导页分组配置", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalApproveGroupConfig : ConfigOption
    {
        /// <summary>
        /// 违法行为分组 0:标准代码  1：自定义代码
        /// </summary>
        [Config(Name = "违法行为分组", ValueType = ConfigValueType.Select, DefaultValue = "0", DataSource = "tsdn.Service,tsdn.ConfigHandler.IllegalApproveGroupConfig.GetIllegalCodeGroupDs", Title = "引导页一级按照标准违法代码分组还是违法编号分组，默认标准代码")]
        public static string IllegalCodeGroup { get; set; }

        /// <summary>
        /// 违法行为分组数据
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetIllegalCodeGroupDs()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = 0,
                Text = "默认(标准违法代码)"
            });
            list.Add(new
            {
                Value = 1,
                Text = "自定义违法编号"
            });
            return list;
        }
    }
}
