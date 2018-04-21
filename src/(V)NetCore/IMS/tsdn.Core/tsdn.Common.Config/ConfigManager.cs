/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Utility;
using tsdn.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace tsdn.Common.Config
{
    /// <summary>
    /// 配置管理,提供配置初始化，配置信息读取保存方法
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        private static string ConfigHandler = "ConfigHandler";

        /// <summary>
        /// 参数配置服务
        /// </summary>
        public IConfigService ConfigService { get; set; }

        /// <summary>
        /// 系统所有配置信息
        /// </summary>
        public IEnumerable<ConfigOption> AllConfig { get; set; }

        /// <summary>
        /// 标签服务
        /// </summary>
        public ITagService TagService { set; get; }

        /// <summary>
        /// 初始化系统参数配置信息
        /// </summary>
        public void Init()
        {
            //所有选项值
            List<Options> listOption = ConfigService.GetAllOptions();

            ConfigDescription desc = null;
            //代码现有配置项
            foreach (ConfigOption item in AllConfig)
            {
                //反射读取配置项ConfigTypeAttribute  ConfigAttribute 信息
                desc = ConfigDescriptionCache.GetTypeDiscription(item.GetType());

                //设置当前配置项的GroupType
                desc.GroupTypePropertyInfo.SetValue(item, ConvertScalar(desc.Group, desc.GroupTypePropertyInfo.PropertyType), null);

                //每项值信息
                List<Options> itemOptions = listOption.Where(e => e.OptionType.Equals(desc.Group, StringComparison.OrdinalIgnoreCase)).ToList();
                Options op = null;
                ConfigAttribute ca = null;
                foreach (PropertyInfo prop in desc.StaticPropertyInfo)
                {
                    op = itemOptions.FirstOrDefault(e1 => e1.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                    ca = desc.MemberDict[prop.Name];
                    if (op == null)
                    {
                        //设置默认值
                        prop.SetValue(null, ConvertScalar(ca.DefaultValue, prop.PropertyType), null);
                    }
                    else
                    {
                        prop.SetValue(null, ConvertScalar(op.Value, prop.PropertyType), null);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有配置信息
        /// </summary>
        /// <returns>所有配置信息</returns>
        public List<OptionViewModel> GetAllOption(string GroupType = "")
        {
            //所有选项值
            List<Options> listOption = ConfigService.GetAllOptions(GroupType);

            IEnumerable<Tags> listTags = TagService.GetTags(ConfigHandler);

            IEnumerable<ConfigOption> listConfigs = AllConfig;
            if (!string.IsNullOrEmpty(GroupType))
            {
                listConfigs = AllConfig.Where(e => e.GroupType.Equals(GroupType, StringComparison.OrdinalIgnoreCase));
            }

            ConfigDescription desc = null;

            //分组信息
            OptionGroup optionGroup = null;
            Options op = null;
            ConfigAttribute ca = null;

            List<OptionViewModel> result = new List<OptionViewModel>();
            OptionViewModel itemOptionViewModel = null;

            //代码现有配置项
            foreach (ConfigOption item in listConfigs)
            {
                //反射读取配置项ConfigTypeAttribute  ConfigAttribute 信息
                desc = ConfigDescriptionCache.GetTypeDiscription(item.GetType());

                itemOptionViewModel = new OptionViewModel();
                optionGroup = new OptionGroup { GroupType = desc.Group, GroupName = desc.GroupCn, CustomPage = desc.CustomPage };
                optionGroup.ImmediateUpdate = desc.ImmediateUpdate;
                itemOptionViewModel.Group = optionGroup;
                itemOptionViewModel.FunctionType = desc.FunctionType;
                itemOptionViewModel.ListOptions = new List<Options>();

                //每项值信息
                List<Options> itemOptions = listOption.Where(e => e.OptionType.Equals(desc.Group, StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (PropertyInfo prop in desc.StaticPropertyInfo)
                {
                    op = itemOptions.FirstOrDefault(e1 => e1.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                    ca = desc.MemberDict[prop.Name];

                    if (op == null)
                    {
                        op = new Options
                        {
                            OptionType = desc.Group,
                            OptionName = ca.Name,
                            Key = prop.Name,
                            Value = Convert.ToString(ca.DefaultValue)
                        };
                    }
                    //必填设置
                    op.Required = ca.Required;
                    //校验规则
                    op.ValidateRule = ca.ValidateRule;
                    //悬浮title
                    op.Title = ca.Title;
                    op.Valuetype = Convert.ToInt32(ca.ValueType).ToString();
                    op.OptionName = ca.Name;
                    op.DataSource = ca.DataSource == null ? null : JsonConvert.DeserializeObject(ca.DataSource);
                    op.FormatDate = ca.FormatDate;
                    itemOptionViewModel.ListOptions.Add(op);
                    itemOptionViewModel.TagList = listTags.Where(e => e.SourceId == itemOptionViewModel.Group.GroupType).ToList();
                }
                result.Add(itemOptionViewModel);
            }
            return result.OrderBy(e => e.Group.GroupType).ToList();
        }

        /// <summary>
        /// 获取指定项配置信息
        /// </summary>
        /// <param name="GroupType">分组项</param>
        /// <returns>所有配置信息</returns>
        public OptionViewModel GetOptionByGroup(string GroupType)
        {
            List<OptionViewModel> list = GetAllOption(GroupType);
            if (list != null && list.Count > 0)
            {
                OptionViewModel item = list[0];
                item.TagList = TagService.GetTags(ConfigHandler, GroupType);
                return list[0];
            }
            return null;
        }


        /// <summary>
        /// 获取指定项配置信息
        /// </summary>
        /// <param name="GroupType">分组项</param>
        /// <returns>所有配置信息</returns>
        public Options GetOptionByGroupAndKey(string GroupType, string key)
        {
            List<OptionViewModel> list = GetAllOption(GroupType);
            if (list != null && list.Count > 0)
            {
                return list[0].ListOptions.FirstOrDefault(e => e.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="value">配置信息</param>
        /// <param name="AfterSave">是否调用保存后方法</param>
        public ApiResult<string> Save(OptionViewModel value, bool AfterSave = true)
        {
            //保存标签信息
            if (value.TagList == null || value.TagList.Count == 0)
            {
                //删除标签
                TagService.DeleteTags(ConfigHandler, value.Group.GroupType);
            }
            else
            {
                //保存标签
                TagService.SaveTags(value.TagList, ConfigHandler, value.Group.GroupType, "");
            }

            ApiResult<string> result = new ApiResult<string>();
            result.Code = ResultCode.Parameter_Error;
            string GroupType = value.Group.GroupType;
            if (value.Group == null || string.IsNullOrEmpty(GroupType) || value.ListOptions == null)
            {
                result.Message = "保存参数配置时发生参数空异常";
                return result;
            }
            //调用保存前处理事件
            ConfigOption curConfigOption = AllConfig.FirstOrDefault(e => e.GroupType.Equals(GroupType, StringComparison.OrdinalIgnoreCase));
            if (curConfigOption == null)
            {
                //如果没有找到匹配项
                result.Message = string.Format("当前保存配置信息{0}不对应后台的任务配置类", GroupType);
                return result;
            }
            VerifyResult vr = curConfigOption.BeforeSave(value);
            if (!vr.IsSusscess)
            {
                result.Message = vr.ErrorMessage;
                return result;
            }

            //保存数据
            using (CommonDbContext db = new CommonDbContext())
            {
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        //先删除后插入
                        //删除原有数据
                        db.Set<Options>().Delete(e => e.OptionType == GroupType, trans);
                        foreach (var item in value.ListOptions)
                        {
                            item.OptionId = GuidHelper.GetSeqGUID();
                        }
                        //保存数据
                        db.Set<Options>().BulkInsert(value.ListOptions, trans);
                        db.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw e;
                    }
                }
            }

            //对当前配置项进行赋值
            SetValue(curConfigOption, value.ListOptions, AfterSave);

            ////MQ消息发送
            //RabbitMQClient.SendMessage(MQRoutingKey.ConfigHandler, GroupType, ChangeType.Update);

            result.Code = ResultCode.Success;
            return result;
        }

        /// <summary>
        /// 保存时 对当前配置项进行赋值
        /// </summary>
        /// <param name="item">当前配置项</param>
        /// <param name="ListOptions">配置项值</param>
        /// <param name="AfterSave">是否调用保存后方法</param>
        public void SetValue(ConfigOption item, List<Options> ListOptions, bool AfterSave = true)
        {
            if (AfterSave)
            {
                //调用保存后处理事件
                item.AfterSave(ListOptions);
            }
            //获取本地化配置
            var localConfigList = ConfigService.GetAllLocalOptions(item.GroupType);
            var desc = ConfigDescriptionCache.GetTypeDiscription(item.GetType());
            Options option = null;
            foreach (PropertyInfo prop in desc.StaticPropertyInfo)
            {
                option = ListOptions.FirstOrDefault(e => e.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                if (option == null)
                {
                    //不存在该配置项，则清空当前值
                    prop.SetValue(null, ConvertScalar(null, prop.PropertyType), null);
                }
                else
                {
                    var localConfig = localConfigList.FirstOrDefault(e => e.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                    string optionValue = option.Value;
                    if (localConfig != null)
                    {
                        optionValue = localConfig.Value;
                    }
                    prop.SetValue(null, ConvertScalar(optionValue, prop.PropertyType), null);
                }
            }
        }

        private static Type GetRealPropertyType(Type propertyType)
        {
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return propertyType.GetGenericArguments()[0];
            }
            else
            {
                return propertyType;
            }
        }

        private static object ConvertScalar(object obj, Type propertyType)
        {
            if (obj == null || DBNull.Value.Equals(obj))
            {
                return propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;
            }
            if (obj.GetType() == propertyType)
                return obj;

            var realType = GetRealPropertyType(propertyType);
            return Convert.ChangeType(obj, realType);
        }
    }
}