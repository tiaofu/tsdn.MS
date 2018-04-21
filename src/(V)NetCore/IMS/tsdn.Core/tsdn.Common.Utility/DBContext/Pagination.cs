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
using System.Collections;
using Newtonsoft.Json;
using System.Data;
using System.ComponentModel;
using tsdn.Utility;
using tsdn.Common.Dapper.SqlGenerator;
namespace tsdn.Common.Utility
{

    /// <summary>
    /// 功能说明：结合miniui特性封装列表查询类
    /// 创建人：杜冬军
    /// 创建日期：2015-11-26
    /// 版本：V1.1
    /// 修改历史: 2015-12-04 修改日期解析时由于前台传过来的值大于了最大日期和最小日期导致查询报错BUG
    /// </summary>
    public class Pagination
    {
        #region "通过QueryCondition转换成实际查询的过滤条件"

        /// <summary>
        /// 通过界面查询条件 转换成SQL查询的过滤条件 
        /// </summary>
        /// <param name="gridParam">界面的过滤条件</param>
        /// <param name="htArgs">参数化查询</param>
        /// <returns>List[0] 过滤SQL  List[1] 排序SQL</returns>
        public static List<string> GetFilter(QueryCondition gridParam, out IDictionary<string, object> outHtArgs)
        {
            //获取参数化查询SQL
            IDictionary<string, object> htArgs = new Dictionary<string, object>();
            string SortField = gridParam.SortField;
            string SortOrder = gridParam.SortOrder;

            string filter = GetParameterSQL(gridParam.FilterList, htArgs);
            if (string.IsNullOrEmpty(filter))
            {
                filter = " 1=1";
            }

            //处理固定参数
            FixedParameter(gridParam.FixedFilterList, htArgs);

            string sortSQL = string.Empty;
            if (string.IsNullOrEmpty(SortField))
            {
                sortSQL = "";
            }
            else
            {
                SortOrder = String.IsNullOrEmpty(SortOrder) == true ? "ASC" : SortOrder;

                //修改支持多字段排序 逗号分隔
                string[] arrSortField = SortField.Split(',');
                //判断排序方向
                string[] arrSortOrder = SortOrder.Split(',');
                sortSQL = " ORDER BY ";
                for (int i = 0, length = arrSortField.Length, OrderLength = arrSortOrder.Length - 1; i < length; i++)
                {
                    sortSQL += string.Format(" {0} {1}{2}", arrSortField[i], i <= OrderLength ? arrSortOrder[i] : "asc", i < length - 1 ? "," : " ");
                }
            }
            List<string> list = new List<string>();
            list.Add(filter);
            list.Add(sortSQL);
            outHtArgs = htArgs;
            return list;
        }
        #endregion
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="connectionString">数据库连接字符串,默认值为null 即表示使用默认连接字符串</param>
        /// <returns>
        /// ht["data"] DataTable
        /// ht["total"] 总条数
        /// </returns>
        public static PaginationResult<TEntity> QueryBase<TEntity>(string sql, QueryCondition gridParam, string connectionString = null)
        {
            try
            {
                PaginationResult<TEntity> result = new PaginationResult<TEntity>();
                List<SqlQuery> ListSqlQuery = GetSqlQuery(sql, gridParam, connectionString);
                int total = 0;
                int PageSize = gridParam.PageSize;
                using (CommonDbContext db = new CommonDbContext())
                {
                    result.Data = db.SqlQuery(ListSqlQuery[0].GetSql(), ListSqlQuery[0].Param).Read<TEntity>().ToList();
                    if (gridParam.IsCalcTotal)
                        result.Total = db.SqlQuery(ListSqlQuery[1].GetSql(), ListSqlQuery[1].Param).ReadFirst<int>();
                    else
                        result.Total = total;
                    result.TotalPage = Convert.ToInt32(Math.Ceiling(result.Total * 1.0 / PageSize));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="connectionString">数据库连接字符串,默认值为null 即表示使用默认连接字符串</param>
        /// <returns>
        /// ht["data"] DataTable
        /// ht["total"] 总条数
        /// </returns>
        public static List<SqlQuery> GetSqlQuery(string sql, QueryCondition gridParam, string connectionString = null)
        {
            try
            {
                int PageSize = gridParam.PageSize;
                int PageIndex = gridParam.PageIndex;
                IDictionary<string, object> htArgs = new Dictionary<string, object>();
                List<string> listFilter = GetFilter(gridParam, out htArgs);

                string filter = listFilter[0];
                string sortSQL = listFilter[1];
                //过滤条件
                if (String.IsNullOrEmpty(sql))
                {
                    throw new ArgumentNullException("调用函数QueryBase时参数sql为空");
                }


                Hashtable result = new Hashtable();
                string newSql = string.Empty;

                List<SqlQuery> listResult = new List<SqlQuery>();
                string CPQueryTotal = string.Empty;
                if (gridParam.IsPagination)
                {
                    PageIndex = PageIndex < 0 ? 0 : PageIndex;
                    newSql = "SELECT * FROM (SELECT T.*,ROWNUM AS LIMIT_ROWNUMBER__ FROM ({1}  {0}) T where ROWNUM <= {3} AND {4}) B WHERE LIMIT_ROWNUMBER__>{2}";
                    newSql = String.Format(newSql, sortSQL, sql, PageIndex * PageSize, (PageIndex + 1) * PageSize, filter);
                }
                else
                {
                    newSql = string.Format("SELECT T.* FROM ({1}) T where {2} {0}", sortSQL, sql, filter);
                }
                if (gridParam.IsCalcTotal)
                {
                    CPQueryTotal = string.Format("SELECT COUNT(1) FROM ({0}) A WHERE {1}", sql, filter);
                }
                var newSqlQ = new SqlQuery();
                newSqlQ.SqlBuilder.Append(newSql);
                newSqlQ.SetParam(htArgs);
                var TotalQ = new SqlQuery();
                TotalQ.SqlBuilder.Append(CPQueryTotal);
                TotalQ.SetParam(htArgs);
                listResult.Add(newSqlQ);
                listResult.Add(TotalQ);
                return listResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "私有方法"
        /// <summary>
        /// like查询时替换特殊字符
        /// </summary>
        /// <param name="strValue">值</param>
        /// <returns>替换后的字符串</returns>
        private static string ReplaceReg(string strValue, bool IsAutoPercent, out bool isContainSpecialChar)
        {
            isContainSpecialChar = false;
            if (strValue.Contains("'"))
            {
                strValue = strValue.Replace("'", "''");
            }
            if (strValue.Contains("_"))
            {
                isContainSpecialChar = true;
                strValue = strValue.Replace("_", @"\_");
            }
            if (IsAutoPercent && strValue.Contains("%"))
            {
                isContainSpecialChar = true;
                strValue = strValue.Replace("%", @"\%");
            }
            //if (isContainSpecialChar)
            //{
            //    strValue += " escape('\\')";
            //}
            return strValue;
        }

        /// <summary>
        /// 通过查询条件,获取参数化查询SQL
        /// </summary>
        /// <param name="filterList">过滤添加列表</param>
        /// <param name="htArgs">参数列表</param>
        /// <returns>参数化查询SQL</returns>
        private static string GetParameterSQL(List<Filter> filterList, IDictionary<string, object> htArgs)
        {
            if (filterList != null && filterList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                string DateParamValue = string.Empty;
                string DateParamName = string.Empty;
                DateTime dateTime;
                //操作符
                string strOperator = string.Empty;
                foreach (var item in filterList)
                {
                    #region "匹配符替换"
                    //字段名称为空则跳过
                    if (string.IsNullOrEmpty(item.FieldName))
                    {
                        continue;
                    }
                    //匹配枚举，防止SQL注入
                    Operator operatorEnum = (Operator)Enum.Parse(typeof(Operator), item.Operator, true);

                    //跳过字段值为空的
                    if (operatorEnum != Operator.Null && operatorEnum != Operator.NotNull && string.IsNullOrEmpty(item.FieldValue))
                    {
                        continue;
                    }
                    strOperator = operatorEnum.GetDescription();
                    if (item.IgnoreCase && !item.IsDateTime)
                    {
                        //2016-07-19添加查询时忽略大小写比较
                        item.FieldValue = item.FieldValue.ToLower();
                        item.FieldName = string.Format("lower({0})", item.FieldName);
                    }
                    switch (operatorEnum)
                    {
                        //等于,不等于，小于，大于，小于等于，大于等于
                        case Operator.EQ:
                        case Operator.NE:
                        case Operator.GT:
                        case Operator.GE:
                        case Operator.LT:
                        case Operator.LE:
                            if (item.IsDateTime)
                            {
                                //2015-12-04修改日期字符串解析异常 比如10000-01-01之类的超过了最大最小时间
                                if (DateTime.TryParse(item.FieldValue, out dateTime))
                                {
                                    //DateParamName = string.Format("{0}{1}", item.FieldName, DateParamCount);
                                    //htArgs[DateParamName] = item.FieldValue;

                                    DateParamValue = DateTime.Parse(item.FieldValue).ToString("yyyy-MM-dd HH:mm:ss");
                                    if (!item.FieldValue.Contains("00:00:00") && dateTime.ToString("HH:mm:ss") == "00:00:00")
                                    {
                                        if (operatorEnum == Operator.LE)
                                        {
                                            DateParamValue = dateTime.ToString("yyyy-MM-dd") + " 23:59:59";
                                        }
                                    }
                                    sb.AppendFormat(" AND {0} {1} to_date('{2}','yyyy-mm-dd hh24:mi:ss')", item.FieldName, strOperator, DateParamValue);
                                    //DateParamCount++;
                                }
                            }
                            else
                            {

                                if (operatorEnum == Operator.EQ)
                                {
                                    sb.AppendFormat(" AND (");
                                    //新增多字段匹配功能
                                    var arrFiledMany = item.FieldName.Split(',');
                                    string paramName = GetSqlParamName(arrFiledMany[0]);
                                    htArgs[paramName] = item.FieldValue;
                                    for (int i = 0, len = arrFiledMany.Length; i < len; i++)
                                    {
                                        sb.AppendFormat("  {0} {1} :{2}", arrFiledMany[i], strOperator, paramName);

                                        if ((i + 1) != len)
                                        {
                                            sb.AppendFormat(" OR ");
                                        }
                                    }
                                    sb.AppendFormat(" ) ");
                                }
                                else
                                {
                                    string paramName = GetSqlParamName(item.FieldName);
                                    htArgs[paramName] = item.FieldValue;
                                    sb.AppendFormat(" AND {0} {1} :{2}", item.FieldName, strOperator, paramName);
                                }
                            }
                            break;
                        case Operator.Like:
                        case Operator.NotLike:
                        case Operator.LLike:
                        case Operator.RLike:

                            sb.AppendFormat(" AND (");
                            //新增多字段匹配功能
                            var arrFiled = item.FieldName.Split(',');
                            for (int i = 0, len = arrFiled.Length; i < len; i++)
                            {
                                bool isContainSpecialChar = false;
                                if (!item.IsAutoPercent)
                                {
                                    sb.AppendFormat("  {0} {1} '{2}'", arrFiled[i], strOperator, ReplaceReg(item.FieldValue, item.IsAutoPercent, out isContainSpecialChar));
                                }
                                else
                                {
                                    if (operatorEnum == Operator.Like || operatorEnum == Operator.NotLike)
                                    {
                                        sb.AppendFormat(" {0} {1} '%{2}%'", arrFiled[i], strOperator, ReplaceReg(item.FieldValue, item.IsAutoPercent, out isContainSpecialChar));
                                    }
                                    else if (operatorEnum == Operator.LLike)
                                    {
                                        sb.AppendFormat(" {0} {1} '%{2}'", arrFiled[i], strOperator, ReplaceReg(item.FieldValue, item.IsAutoPercent, out isContainSpecialChar));
                                    }
                                    else if (operatorEnum == Operator.RLike)
                                    {
                                        sb.AppendFormat(" {0} {1} '{2}%'", arrFiled[i], strOperator, ReplaceReg(item.FieldValue, item.IsAutoPercent, out isContainSpecialChar));
                                    }
                                }
                                if (isContainSpecialChar)
                                {
                                    sb.AppendFormat(" escape('\\')");
                                }
                                if ((i + 1) != len)
                                {
                                    sb.AppendFormat(" OR ");
                                }
                            }
                            sb.AppendFormat(" ) ");
                            break;
                        case Operator.Null:
                        case Operator.NotNull:
                            sb.AppendFormat(" AND {0} {1}", item.FieldName, strOperator);
                            break;
                        case Operator.In:
                        case Operator.NotIn:
                            sb.AppendFormat(" AND {0} {1} ('{2}')", item.FieldName, strOperator, item.FieldValue.Replace("'", "''").Replace(",", "','"));
                            break;
                    }
                    #endregion
                }
                if (sb.ToString().Length > 0)
                {
                    return sb.ToString().Substring(4, sb.Length - 4);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 固定参数替换,一般用于联动查询,选择公司显示公司下的用户
        /// </summary>
        /// <param name="FixedFilterList">固定参数列表</param>
        /// <param name="htArgs">参数列表</param>
        private static void FixedParameter(List<FixedFilter> FixedFilterList, IDictionary<string, object> htArgs)
        {
            if (FixedFilterList != null && FixedFilterList.Count > 0)
            {
                //添加参数化查询
                FixedFilterList.ForEach((item) =>
                {
                    if (item.IsDateTime)
                    {
                        htArgs[item.ParamName] = DateTime.Parse(item.ParamValue);
                    }
                    else
                    {
                        htArgs[item.ParamName] = item.ParamValue;
                    }
                });
            }
        }

        /// <summary>
        /// 处理参数中的特殊字符 eg  a.Name 做为oracle参数 :a.Name 会报错，处理后为 aName
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>处理后的参数名</returns>
        private static string GetSqlParamName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            else
            {
                return name.Replace(".", "");
            }
        }
        #endregion
    }

    /// <summary>
    /// SQL查询匹配符
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        EQ = 0,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("<>")]
        NE = 1,

        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        GT = 2,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=")]
        GE = 3,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        LT = 4,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=")]
        LE = 5,

        /// <summary>
        /// 自动在值前后面加上%
        /// </summary>
        [Description("like")]
        Like = 6,

        /// <summary>
        /// 自动在值前面加上%
        /// </summary>
        [Description("like")]
        LLike = 7,

        /// <summary>
        /// 自动在值后面加上%
        /// </summary>
        [Description("like")]
        RLike = 8,


        /// <summary>
        /// 不类似
        /// </summary>
        [Description("not like")]
        NotLike = 9,

        /// <summary>
        /// 为空
        /// </summary>
        [Description("is null")]
        Null = 10,

        /// <summary>
        /// 不为空
        /// </summary>
        [Description("is not null")]
        NotNull = 11,


        /// <summary>
        /// 包含
        /// </summary>
        [Description("in")]
        In = 12,

        /// <summary>
        /// 不包含
        /// </summary>
        [Description("not in")]
        NotIn = 13
    }

    public class OperatorUtil
    {
        public static string HandlerOperatorName(string oper)
        {
            Operator operatorEnum = (Operator)Enum.Parse(typeof(Operator), oper, true);
            string operatorName = string.Empty;
            switch (operatorEnum)
            {
                case Operator.EQ:
                    operatorName = "等于";
                    break;
                case Operator.GE:
                    operatorName = "大于等于";
                    break;
                case Operator.GT:
                    operatorName = "大于";
                    break;
                case Operator.In:
                    operatorName = "包含";
                    break;
                case Operator.LE:
                    operatorName = "小于等于";
                    break;
                case Operator.Like:
                    operatorName = "模糊查询";
                    break;
                case Operator.LLike:
                    operatorName = "半模糊查询（%条件）";
                    break;
                case Operator.LT:
                    operatorName = "小于";
                    break;
                case Operator.NE:
                    operatorName = "不等于";
                    break;
                case Operator.NotIn:
                    operatorName = "不包含";
                    break;
                case Operator.NotLike:
                    operatorName = "不类似";
                    break;
                case Operator.NotNull:
                    operatorName = "不为空";
                    break;
                case Operator.Null:
                    operatorName = "为空";
                    break;
                case Operator.RLike:
                    operatorName = "半模糊查询（条件%）";
                    break;
            }
            return operatorName;
        }
    }
}
