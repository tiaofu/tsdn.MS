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
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using tsdn.Common.Dapper.SqlGenerator;
using Dapper;
using System.Data;

namespace tsdn.Common.Utility
{
    /// <summary>
    /// 表示对*.config配置的XmlCommand命令封装。
    /// </summary>
    public sealed class XmlCommand : IDbExecute
    {
        private SqlQuery _query = new SqlQuery();

        /// <summary>
        /// 创建新的XmlCommand对象。
        /// </summary>
        /// <param name="name">命令名字</param>
        public XmlCommand(string name) : this(name, null)
        {
        }

        /// <summary>
        /// 创建新的XmlCommand对象。
        /// </summary>
        /// <param name="name">命令名字</param>
        /// <param name="argsObject">匿名对象表示的参数</param>
        public XmlCommand(string name, object argsObject) : this(name, argsObject, null)
        {
        }

        /// <summary>
        /// 创建新的XmlCommand对象。
        /// </summary>
        /// <param name="name">命令名字</param>
        /// <param name="argsObject">匿名对象表示的参数</param>
        /// <param name="replaces">替换的关键字字典</param>
        public XmlCommand(string name, object argsObject, Dictionary<string, string> replaces)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            XmlCommandItem command = XmlCommandManager.GetCommand(name);
            if (command == null)
                throw new ArgumentOutOfRangeException("name", string.Format("指定的XmlCommand名称 {0} 不存在。", name));

            // 根据XML的定义以及传入参数，生成IDictionary
            IDictionary<string, object> parameters = GetParameters(command, argsObject);

            // 创建CPQuery实例
            StringBuilder commandText = new StringBuilder(command.CommandText);
            if (replaces != null)
                foreach (KeyValuePair<string, string> kvp in replaces)
                    commandText.Replace(kvp.Key, kvp.Value);

            _query.SetParam(parameters);
            _query.SqlBuilder.Append(commandText.ToString());
        }

        /// <summary>
        /// 返回新的XmlCommand对象实例。
        /// </summary>
        /// <param name="name">命令名字</param>
        /// <returns>新的XmlCommand对象实例</returns>
        public static XmlCommand From(string name)
        {
             return new XmlCommand(name);
        }

        /// <summary>
        /// 返回新的XmlCommand对象实例。
        /// </summary>
        /// <param name="name">命令名字</param>
        /// <param name="argsObject">匿名对象表示的参数</param>
        /// <returns>新的XmlCommand对象实例</returns>
        public static XmlCommand From(string name, object argsObject)
        {
            return new XmlCommand(name, argsObject);
        }

        /// <summary>
        /// 返回新的XmlCommand对象实例。
        /// </summary>
        /// <param name="name">命令名字</param>
        /// <param name="argsObject">匿名对象表示的参数</param>
        /// <param name="replaces">替换的关键字字典</param>
        /// <returns></returns>
        public static XmlCommand From(string name, object argsObject, Dictionary<string, string> replaces)
        {
            return new XmlCommand(name, argsObject, replaces);
        }

        /// <summary>
        /// 根据XmlCommandItem对象，返回OracleParameter对象数组
        /// </summary>
        /// <param name="command">XmlCommandItem对象实例</param>
        /// <param name="argsObject">匿名对表示的数据库参数</param>
        /// <returns>OracleParameter对象数组</returns>
        private IDictionary<string, object> GetParameters(XmlCommandItem command, object argsObject)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (argsObject == null || command.Parameters.Count == 0)
                return result;
            PropertyInfo[] properties = argsObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // 赋值。
            foreach (PropertyInfo pInfo in properties)
            {
                string name = ":" + pInfo.Name;
                XmlCmdParameter p = command.Parameters.FirstOrDefault(x => string.Compare(x.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
                // 如果传入了在XML中没有定义的参数项，则会抛出异常。               
                if (p == null)
                    throw new ArgumentException(string.Format("传入的参数对象中，属性 {0} 没有在XML定义对应的参数名。", pInfo.Name));
                result.Add(p.Name, pInfo.FastGetValue(argsObject) ?? DBNull.Value);
            }
            return result;
        }

        private Regex _pagingRegex = new Regex(@"\)\s*as\s*rowindex\s*,", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// 基本的分页信息。
        /// </summary>
        public class PagingInfo
        {
            /// <summary>
            /// 分页序号，从0开始计数
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 分页大小
            /// </summary>
            public int PageSize { get; set; }
            /// <summary>
            /// 从相关查询中获取到的符合条件的总记录数
            /// </summary>
            public int TotalRecords { get; set; }


            /// <summary>
            /// 计算总页数
            /// </summary>
            /// <returns>总页数</returns>
            public int CalcPageCount()
            {
                if (this.PageSize == 0 || this.TotalRecords == 0)
                    return 0;

                return (int)Math.Ceiling((double)this.TotalRecords / (double)this.PageSize);
            }
        }

        #region IDbExecute 成员

        /// <summary>
        /// 执行命令,并返回影响函数
        /// </summary>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery()
        {
            using (CommonDbContext db = new CommonDbContext())
            {
               return db.Connection.ExecuteScalar<int>(_query.GetSql(), _query.Param);
            }           
        }
        /// <summary>
        /// 执行命令,返回第一行,第一列的值,并将结果转换为T类型
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <returns>结果集的第一行,第一列</returns>
        public T ExecuteScalar<T>()
        {
            using (CommonDbContext db = new CommonDbContext())
            {
                return db.Connection.ExecuteScalar<T>(_query.GetSql(), _query.Param);
            }
        }                
        /// <summary>
        /// 执行命令,将结果集转换为实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体集合</returns>
        public List<T> ToList<T>() where T : class, new()
        {
            using (CommonDbContext db = new CommonDbContext())
            {
                return db.Connection.Query<T>(_query.GetSql(), _query.Param).ToList();
            }           
        }
        /// <summary>
        /// 执行命令,将结果集转换为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体</returns>
        public T ToSingle<T>() where T : class, new()
        {
            using (CommonDbContext db = new CommonDbContext())
            {
                return db.Connection.Query<T>(_query.GetSql(), _query.Param).FirstOrDefault();
            }
        }

        public DataSet FillDataSet()
        {            
            throw new NotImplementedException();
        }
      
        public DataTable FillDataTable()
        {
            throw new NotImplementedException();
        }

        public List<T> FillScalarList<T>()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
