/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.Collections.Generic;
using tsdn.Common.Config;
using System.Linq;
using System.Text.RegularExpressions;

namespace tsdn.ConfigHandler
{
    /// <summary>
    /// 违法审核违法类型修改配置
    /// </summary>


    [ConfigType(Group = "IllegalSearchVehiceConfig", GroupCn = "违法审核车辆查询", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalSearchVehiceConfig : ConfigOption
    {
        /// <summary>
        /// 查车范围
        /// </summary>
        [Config(Name = "查车范围", ValueType = ConfigValueType.Select, DefaultValue = "0", DataSource = "tsdn.Service,tsdn.ConfigHandler.IllegalSearchVehiceConfig.GetSectionDs", Title = "可以进行车辆信息查询范围，默认本省外省车全部可查询")]
        public static string SearchSection { get; set; }

        /// <summary>
        /// 本省车正则表达式
        /// </summary>
        [Config(Name = "本省车正则", Required = false, Title = "如果范围选择为“本省”则需配置本省车的正则表达式，可参考数据字典号牌归类设置，eg：湖北省 正则：^鄂")]
        public static string NativeVehicleReg { get; set; }

        /// <summary>
        /// 使用缓存查询车辆信息
        /// </summary>
        [Config(Name = "使用缓存查询车辆信息", DefaultValue = false)]
        public static bool IsUseCache { get; set; }

        /// <summary>
        /// 外省车机动车所有人
        /// </summary>
        [Config(Name = "外地车机动车所有人", DefaultValue = "未知", Required = false, Title = "无法进行外地车查询，机动车所有人使用的默认值")]
        public static string OwnerName { get; set; }

        /// <summary>
        /// 外省车车辆状态
        /// </summary>
        [Config(Name = "外地车车辆状态", DefaultValue = "A", Required = false, Title = "无法进行外地车查询，车辆状态使用的默认值")]
        public static string VehicleStatus { get; set; }

        /// <summary>
        /// 外省车车辆类型
        /// </summary>
        [Config(Name = "外地车车辆类型", DefaultValue = "X99", Required = false, Title = "无法进行外地车查询，车辆类型使用的默认值")]
        public static string VehicleType { get; set; }

        /// <summary>
        /// 外省车使用性质
        /// </summary>
        [Config(Name = "外地车使用性质", DefaultValue = "A", Required = false, Title = "无法进行外地车查询，使用性质使用的默认值")]
        public static string UseProperty { get; set; }

        /// <summary>
        /// 参数配置项保存前处理逻辑
        /// </summary>
        /// <param name="value">当前保存的参数</param>
        public override VerifyResult BeforeSave(OptionViewModel value)
        {
            VerifyResult result = new VerifyResult();

            string SearchSection = value.ListOptions.Where(p => p.Key == "SearchSection").FirstOrDefault()?.Value;
            if (SearchSection == "1")
            {
                string NativeVehicleReg = value.ListOptions.Where(p => p.Key == "NativeVehicleReg").FirstOrDefault()?.Value;
                if (string.IsNullOrEmpty(NativeVehicleReg.Trim()))
                {
                    result.IsSusscess = false;
                    result.ErrorMessage = "范围为本省时，必须设置本省车正则，例如 湖北省 正则：^鄂";
                    return result;
                }
                else
                {
                    try
                    {
                        Regex regex = new Regex(NativeVehicleReg);
                    }
                    catch (Exception e)
                    {
                        result.IsSusscess = false;
                        result.ErrorMessage = "不是正确的正则表达式," + e.Message;
                        return result;
                    }
                }
                string OwnerName = value.ListOptions.Where(p => p.Key == "OwnerName").FirstOrDefault()?.Value;
                string VehicleStatus = value.ListOptions.Where(p => p.Key == "VehicleStatus").FirstOrDefault()?.Value;
                string VehicleType = value.ListOptions.Where(p => p.Key == "VehicleType").FirstOrDefault()?.Value;
                string UseProperty = value.ListOptions.Where(p => p.Key == "UseProperty").FirstOrDefault()?.Value;
                if (string.IsNullOrEmpty(OwnerName) || string.IsNullOrEmpty(VehicleStatus) || string.IsNullOrEmpty(VehicleType) || string.IsNullOrEmpty(UseProperty))
                {
                    result.IsSusscess = false;
                    result.ErrorMessage = "查车范围配置成“本省后”,【机动车所有人】【车辆状态】【车辆类型】【使用性质】必须填写，用于违法审核外省车查询默认值！";
                    return result;
                }
            }
            result.IsSusscess = true;
            return result;
        }

        /// <summary>
        /// 获取查车范围数据
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetSectionDs()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = 0,
                Text = "默认(本省,外省)"
            });
            list.Add(new
            {
                Value = 1,
                Text = "本省"
            });
            return list;
        }
    }
}
