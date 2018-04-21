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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace tsdn.Common
{
    /// <summary>
    /// EF初始化-进行实体读取
    /// </summary>
    public class EFInitializer
    {           
        private static bool s_inited = false;
        /// <summary>
		/// 读取配置,寻找符合规则的实体
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
        public static void UnSafeInit()
        {
            if (s_inited)
            {
                throw new InvalidOperationException("请不要多次调用UnSafeInit方法!");
            }
            string strSearchPattern = "tsdn.Entity.dll";
            if (!string.IsNullOrEmpty(strSearchPattern))
            {
                string[] SearchPatterns = strSearchPattern.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<Assembly> assemblies = new List<Assembly>();
                for (int i = 0; i < SearchPatterns.Length; i++)
                {
                    foreach (string file in Directory.GetFiles(BinDirectory, SearchPatterns[i], SearchOption.TopDirectoryOnly))
                    {
                        var assemblyFullName = Assembly.Load(File.ReadAllBytes(file)).FullName;
                        assemblies.Add(Assembly.Load(assemblyFullName));
                    }
                }

                foreach (Assembly assembly in assemblies)
                {
                    foreach (Type type in assembly.GetExportedTypes())
                    {
                        // 查找所有含有Table属性的实体
                        if (type.GetCustomAttribute<TableAttribute>() != null)
                        {
                            if (type.IsNested == false)
                            {
                                // 数据实体类型要求提供【无参构造函数】。
                                if (type.GetConstructor(Type.EmptyTypes) == null)
                                {
                                    throw new InvalidProgramException(string.Format("类型 {0} 没有定义无参的构造函数 。", type));
                                }
                                // 保留符合所有条件的数据实体类型
                                TypeManager.Instance.RegisterType(type);
                                //缓存相关信息
                                EFTypeDescriptionCache.RegisterType(type);
                            }
                        }
                    }
                }
            }
            s_inited = true;
        }

        public static string BinDirectory
        {
            get
            {
                return AppContext.BaseDirectory; 
            }
        }
    }
    /// <summary>
    /// EF实体相关属性缓存
    /// </summary>
    public class EFTypeDescriptionCache
    {
        public static Dictionary<string, DbMapInfo> MemberDict = new Dictionary<string, DbMapInfo>();

        /// <summary>
        /// 新增实体类型
        /// </summary>
        /// <param name="entityType"></param>
        public static void RegisterType(Type entityType)
        {
            DbMapInfo info = null;
            string typename = entityType.FullName;
            if (!MemberDict.TryGetValue(typename, out info))
            {
                var tableAttr = entityType.GetCustomAttribute<TableAttribute>();
                string tableName = string.IsNullOrEmpty(tableAttr.Name) ? entityType.Name : tableAttr.Name;
                info = new DbMapInfo(tableName, entityType.Name, entityType);
                MemberDict[typename] = info;
            }
        }

        /// <summary>
        /// 获取实体的主键字段
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>主键字段</returns>
        public static string GetPrimaryKeyField(Type entityType)
        {
            return GetPrimaryKeyField(entityType.FullName);
        }

        /// <summary>
        /// 获取实体的主键字段
        /// </summary>
        /// <param name="entityTypeFullName">实体类型全称</param>
        /// <returns>主键字段</returns>
        public static string GetPrimaryKeyField(string entityTypeFullName)
        {
            DbMapInfo info = null;
            if (MemberDict.TryGetValue(entityTypeFullName, out info))
            {
                ColumnMapInfo colinfo = info.ColumnMapInfos.First(e => e.IsPK);
                if (colinfo == null)
                {
                    throw new ArgumentException(string.Format("实体{0}不存在主键字段", entityTypeFullName));
                }
                return colinfo.PropName;
            }
            else
            {
                throw new ArgumentException(string.Format("实体{0}不存在", entityTypeFullName));
            }
        }
    }

    public class DbMapInfo
    {
        private static BindingFlags s_flag = BindingFlags.Instance | BindingFlags.Public;

        public string DbName { get; private set; }

        public string NetName { get; private set; }

        public List<ColumnMapInfo> ColumnMapInfos { get; private set; }

        public Dictionary<string, ColumnMapInfo> CacheColumnMapInfos { get; private set; }

        public DbMapInfo(string dbName, string netName, Type type)
        {
            DbName = dbName;
            NetName = netName;
            PropertyInfo[] properties = type.GetProperties(s_flag);
            ColumnMapInfos = new List<ColumnMapInfo>();
            foreach (PropertyInfo prop in properties)
            {
                KeyAttribute attrKey = prop.GetCustomAttribute<KeyAttribute>();
                ColumnAttribute attrColumn = prop.GetCustomAttribute<ColumnAttribute>();
                NotMappedAttribute attrIgnore = prop.GetCustomAttribute<NotMappedAttribute>();
                bool iskey = attrKey != null;
                string dbColName = attrIgnore != null ? string.Empty : ((attrColumn != null && !string.IsNullOrEmpty(attrColumn.Name)) ? attrColumn.Name : prop.Name);
                ColumnMapInfos.Add(new ColumnMapInfo(dbColName, prop.Name, prop.PropertyType, iskey));
            }
            CacheColumnMapInfos = ColumnMapInfos.ToDictionary(e => e.PropName, e => e);
        }
    }

    public class ColumnMapInfo
    {
        /// <summary>
        /// 映射数据库列名
        /// </summary>
        public string DbColName { get; private set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropName { get; private set; }

        public Type PropType { get; private set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPK { get; private set; }

        public ColumnMapInfo(string dbColName, string propName, Type propType, bool isPK)
        {
            DbColName = dbColName;
            PropName = propName;
            PropType = propType;
            IsPK = isPK;
        }
    }
}
