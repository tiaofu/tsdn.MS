/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 数据对接配置
 *********************************************************/
using tsdn.Common.Config;
using tsdn.Common.Utility;
using tsdn.Entity;
using System.Collections.Generic;
using System.Linq;

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 数据对接配置
    /// </summary>
    [ConfigType(Group = "DataInterchangeConfig", GroupCn = "过车数据对接配置", ImmediateUpdate = true, FunctionType = "后台参数配置")]
    public class DataInterchangeConfig : ConfigOption
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [Config(Name = "请选择设备名称", Required = false, ValueType = ConfigValueType.MultiSelect, DataSource = "tsdn.Service,tsdn.ConfigHandler.Config.DataInterchangeConfig.GetAllFrontendDevice", Title = "请选择设备名称")]
        public static string DeviceId { get; set; }
        /// <summary>
        /// 获取路面设备
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetAllFrontendDevice()
        {
            string sql = "select DEVICEID,DEVICENAME from frontenddevice where functiontype like '09%' or functiontype='0200'";
            List<FrontendDevice> list = null;
            using (CommonDbContext db = new CommonDbContext())
            {
                list = db.SqlQuery(sql).Read<FrontendDevice>().ToList();
            }
            List<dynamic> listR = new List<dynamic>();
            foreach (var item in list)
            {
                listR.Add(new
                {
                    Value = item.DeviceId,
                    Text = item.DeviceName
                });
            }
            return listR;
        }
    }
}
