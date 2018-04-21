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
using System;
using System.Collections.Generic;
using System.Linq;

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 勤务排班配置
    /// </summary>
    [ConfigType(Group = "AttendanceConfig", GroupCn = "勤务排班配置", ImmediateUpdate = true, FunctionType = "勤务管理")]
    public class AttendanceConfig : ConfigOption
    {
        /// <summary>
        /// 白班时间区间
        /// </summary>
        [Config(Name = "白班时间区间", DefaultValue = "09:00-17:00")]
        public static string DayTime { get; set; }

        /// <summary>
        /// 晚班时间区间
        /// </summary>
        [Config(Name = "晚班时间区间", DefaultValue = "19:00-23:00")]
        public static string NightTime { get; set; }

        /// <summary>
        /// 保存前校验
        /// </summary>
        /// <param name="value">配置值</param>
        /// <returns>校验结果</returns>
        public override VerifyResult BeforeSave(OptionViewModel value)
        {
            VerifyResult result = new VerifyResult();
            result.IsSusscess = false;
            var DayTimeConfig = value.ListOptions.FirstOrDefault(e => e.Key == "DayTime");
            var NightTimeConfig = value.ListOptions.FirstOrDefault(e => e.Key == "NightTime");
            string tip = "时间区间格式不正确,应为 HH:mm-HH:mm 如09:00-12:00 或者 14:00-23:00 并且分只能是 00 或者30 即半个小时为间隔";
            List<Options> list = new List<Options>();
            list.Add(DayTimeConfig);
            list.Add(NightTimeConfig);
            foreach (var option in list)
            {
                if (option != null)
                {
                    string[] TimeArea = option.Value.Split('-');
                    if (TimeArea.Length != 2)
                    {
                        result.ErrorMessage = tip;
                        return result;
                    }
                    else
                    {
                        foreach (var item in TimeArea)
                        {
                            try
                            {
                                var time = DateTime.ParseExact(item, "HH:mm", System.Globalization.CultureInfo.CurrentCulture);
                                if (time.Minute != 0 && time.Minute != 30)
                                {
                                    result.ErrorMessage = "时间必须以半个小时为间隔 如09:00 或者09:30";
                                    return result;
                                }
                            }
                            catch
                            {
                                result.ErrorMessage = tip;
                                return result;
                            }
                        }
                        if (TimeArea[1].CompareTo(TimeArea[0]) < 0)
                        {
                            result.ErrorMessage = "截止时间必须大于起始时间";
                            return result;
                        }
                    }
                }
                else
                {
                    result.IsSusscess = false;
                    result.ErrorMessage = "【时间区间】配置项为空";
                }
            }
            result.IsSusscess = true;
            return result;
        }
    }
}
