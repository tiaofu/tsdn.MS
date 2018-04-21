/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Core;
using tsdn.Common.Utility;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System;
using tsdn.Dependency;

namespace tsdn.Common.Config
{
    /// <summary>
    /// 系统配置接口
    /// </summary>
    public interface IConfigService : ITransientDependency
    {
        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns>所有配置项</returns>
        List<Options> GetAllOptions(string GroupType = "");

        /// <summary>
        /// 获取所有本地化配置分组信息
        /// </summary>
        /// <param name="GroupType">配置大类</param>
        /// <returns>配置分组信息</returns>
        List<Options> GetAllLocalOptions(string GroupType);
    }

    public class ConfigService : IConfigService
    {
        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns>所有配置项</returns>
        public List<Options> GetAllOptions(string GroupType = "")
        {
            List<Options> dbConfigList = null;
            using (CommonDbContext db = new CommonDbContext())
            {
                if (!string.IsNullOrEmpty(GroupType))
                {
                    dbConfigList = db.Set<Options>().FindAll(e => e.OptionType == GroupType).ToList();
                }
                else
                {
                    dbConfigList = db.Set<Options>().FindAll().ToList();
                }
            }
            List<Options> localConfigList = GetAllLocalOptions(GroupType);
            if (localConfigList != null)
            {
                //用本地化配置覆盖数据库配置
                foreach (var item in localConfigList)
                {
                    var dbConfig = dbConfigList.FirstOrDefault(e => e.OptionType == item.OptionType && e.Key == item.Key);
                    if (dbConfig == null)
                    {
                        dbConfig = new Options
                        {
                            OptionType = item.OptionType,
                            Key = item.Key
                        };
                        dbConfigList.Add(dbConfig);
                    }
                    LocalConfigHelper.OverriedDbConfig(dbConfig, item);
                }
            }
            return dbConfigList;
        }

        /// <summary>
        /// 获取所有本地化业务参数配置
        /// </summary>
        /// <param name="GroupType">配置项大类</param>
        /// <returns>所有配置项</returns>
        public List<Options> GetAllLocalOptions(string GroupType)
        {
            return LocalConfigHelper.GetLocalOptions(GroupType);
        }
    }

    /// <summary>
    /// 本地化配置
    /// </summary>
    public class LocalConfigHelper
    {
        /// <summary>
        /// 获取指定配置项
        /// </summary>
        /// <param name="GroupType">配置组</param>
        /// <param name="Key">配置项</param>
        /// <returns>指定配置项</returns>
        public static Options GetOptionByKey(string GroupType, string Key)
        {
            //var dbConfig = XmlCommand.From("ConfigManager:GetOptionByKey", new { OptionType = GroupType, Key = Key }).ToSingle<Options>();
            //var localConfig = GetLocalOptionsByKey(GroupType, Key);
            //OverriedDbConfig(dbConfig, localConfig);
            //return dbConfig;
            return null;
        }

        /// <summary>
        /// 获取所有本地化业务参数配置
        /// </summary>
        /// <param name="GroupType">配置项大类</param>
        /// <returns>所有配置项</returns>
        public static List<Options> GetLocalOptions(string GroupType)
        {
            string localPath = ConfigInit.GetLocalConfigPath();
            if (File.Exists(localPath))
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(localPath);
                string xmlPath = "//CustomLocalParams/BusinessParams";
                if (!string.IsNullOrEmpty(GroupType))
                {
                    xmlPath += "[@key='" + GroupType + "']";
                }
                var nodes = doc.SelectNodes(xmlPath);
                if (nodes != null)
                {
                    List<Options> list = new List<Options>();
                    Options op = null;
                    XmlAttribute attr = null;
                    foreach (XmlNode node in nodes)
                    {

                        attr = node.Attributes["key"];
                        string OptionType = attr != null ? attr.Value : string.Empty;
                        var childNodes = node.ChildNodes;
                        if (childNodes != null)
                        {
                            foreach (XmlNode childNode in childNodes)
                            {
                                if (childNode.Attributes == null)
                                {
                                    continue;
                                }
                                attr = childNode.Attributes["key"];
                                string key = attr != null ? attr.Value : string.Empty;
                                if (!string.IsNullOrEmpty(key))
                                {
                                    op = new Options();
                                    op.OptionType = OptionType;
                                    op.Key = key;
                                    op.Value = childNode.InnerText;
                                    list.Add(op);
                                }
                            }
                        }
                    }
                    return list;
                }
            }
            return new List<Options>();
        }

        /// <summary>
        /// 获取所有本地化业务参数配置
        /// </summary>
        /// <param name="GroupType">配置项大类</param>
        /// <param name="Key">配置项小类</param>
        /// <returns>指定配置项</returns>
        public static Options GetLocalOptionsByKey(string GroupType, string Key)
        {
            string localPath = ConfigInit.GetLocalConfigPath();
            if (File.Exists(localPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(localPath);
                string xmlPath = "//CustomLocalParams/BusinessParams[@key='" + GroupType + "']/param[@key='" + Key + "']";
                var node = doc.SelectSingleNode(xmlPath);
                if (node != null)
                {
                    Options op = null;
                    XmlAttribute attr = null;
                    attr = node.Attributes["key"];
                    string key = attr != null ? attr.Value : string.Empty;
                    if (!string.IsNullOrEmpty(key))
                    {
                        op = new Options();
                        op.Key = key;
                        op.Value = node.InnerText;
                    }
                    return op;
                }
            }
            return null;
        }

        /// <summary>
        /// 本地化配置覆盖数据库配置
        /// </summary>
        /// <param name="dbConifg">数据库配置</param>
        /// <param name="LocalConfig">本地化配置</param>
        public static void OverriedDbConfig(Options dbConifg, Options LocalConfig)
        {
            if (dbConifg != null && LocalConfig != null)
            {
                dbConifg.DBValue = dbConifg.Value;
                dbConifg.LocalValue = LocalConfig.Value;
                dbConifg.Value = LocalConfig.Value;
                dbConifg.ConfigFrom = "local";
            }
        }
    }
}