/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: EF查询拓展 EF参数查询拓展方法服务
 *********************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Linq.Dynamic.Core;
using tsdn.Utility;
namespace tsdn.Common.Utility
{
    /// <summary>
    /// 功能说明：EF动态查询
    /// 创建人：杜冬军
    /// 创建日期：2016-06-16
    /// 版本：V1.0
    /// </summary>
    public static class EFPagination
    {
        private static System.Reflection.BindingFlags BindFlags = System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public;

        private static Type DefaultType = typeof(string);
        /// <summary>
        /// EF单实体查询封装 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Query">IQueryable对象</param>
        /// <param name="gridParam">过滤条件</param>
        /// <returns>查询结果</returns>
        public static PaginationResult<T> PageQuery<T>(this IQueryable<T> Query, QueryCondition gridParam)
        {
            //查询条件
            EFFilter filter = GetParameterSQL<T>(gridParam);
            var query = Query.Where(filter.Filter, filter.ListArgs.ToArray());
            //查询结果
            PaginationResult<T> result = new PaginationResult<T>();
            if (gridParam.IsPagination)
            {
                int PageSize = gridParam.PageSize;
                int PageIndex = gridParam.PageIndex < 0 ? 0 : gridParam.PageIndex;
                //获取排序信息
                string sort = GetSort(gridParam, typeof(T).FullName);
                result.Data = query.OrderBy(sort).Skip(PageIndex * PageSize).Take(PageSize).ToList<T>();
                if (gridParam.IsCalcTotal)
                {
                    result.Total = query.Count();
                    result.TotalPage = Convert.ToInt32(Math.Ceiling(result.Total * 1.0 / PageSize));
                }
                else
                {
                    result.Total = result.Data.Count();
                }
            }
            else
            {
                result.Data = query.ToList();
                result.Total = result.Data.Count();
            }
            return result;
        }

        /// <summary>
        /// 根据过滤条件统计总数 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Query">IQueryable对象</param>
        /// <param name="gridParam">过滤条件</param>
        /// <returns>符合条件总数</returns>
        public static int Count<T>(this IQueryable<T> Query, QueryCondition gridParam)
        {
            //查询条件
            EFFilter filter = GetParameterSQL<T>(gridParam);
            var query = Query.Where(filter.Filter, filter.ListArgs.ToArray());
            if (gridParam.IsPagination)
            {
                if (gridParam.IsCalcTotal)
                {
                    return query.Count();
                }
            }
            return 0;
        }

        /// <summary>
        /// EF连接查询封装 
        /// </summary>

        /// <typeparam name="DestinationType">目标实体类型</typeparam>
        /// <param name="Query">IQueryable对象</param>
        /// <param name="gridParam">过滤条件</param>
        /// <returns>查询结果</returns>
        public static PaginationResult<DestinationType> PageQuery<DestinationType>(this IQueryable<object> Query, QueryCondition gridParam)
        {
            if (string.IsNullOrEmpty(gridParam.SortField))
            {
                gridParam.SortField = EFTypeDescriptionCache.GetPrimaryKeyField(typeof(DestinationType).FullName);
            }
            return Mapper.Map<PaginationResult<DestinationType>>(Query.PageQuery(gridParam));
        }

        /// <summary>
        /// 分页查询拓展
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Query">数据集</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns>数据</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> Query, int PageIndex, int PageSize) where T : class
        {
            return Query.Skip(PageIndex * PageSize).Take(PageSize);
        }

        #region "私有方法"
        /// <summary>
        /// 获取排序字符串
        /// </summary>
        /// <param name="gridParam">过滤条件</param>
        /// <param name="entityTypeFullName">实体类型名称</param>
        /// <returns></returns>
        private static string GetSort(QueryCondition gridParam, string entityTypeFullName)
        {
            string SortField = gridParam.SortField;
            string SortOrder = gridParam.SortOrder;
            string sortSQL = string.Empty;
            if (string.IsNullOrEmpty(SortField))
            {
                sortSQL = EFTypeDescriptionCache.GetPrimaryKeyField(entityTypeFullName);
            }
            else
            {
                SortOrder = string.IsNullOrEmpty(SortOrder) ? "ASC" : SortOrder;

                //修改支持多字段排序 逗号分隔
                string[] arrSortField = SortField.Split(',');
                //判断排序方向
                string[] arrSortOrder = SortOrder.Split(',');
                for (int i = 0, length = arrSortField.Length, OrderLength = arrSortOrder.Length - 1; i < length; i++)
                {
                    sortSQL += string.Format(" {0} {1}{2}", arrSortField[i], i <= OrderLength ? arrSortOrder[i] : "asc", i < length - 1 ? "," : " ");
                }
            }
            return sortSQL;
        }

        /// <summary>
        /// 通过查询条件,获取参数化查询SQL
        /// </summary>
        /// <param name="gridParam">过滤条件</param>
        /// <returns>过滤条件字符</returns>
        private static EFFilter GetParameterSQL<T>(QueryCondition gridParam)
        {
            EFFilter result = new EFFilter();
            //参数值集合
            List<object> listArgs = new List<object>();
            string filter = "1=1";

            #region "处理动态过滤条件"
            if (gridParam.FilterList != null && gridParam.FilterList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                int paramCount = 0;
                DateTime dateTime;
                //操作符
                string strOperator = string.Empty;
                foreach (var item in gridParam.FilterList)
                {
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
                        item.FieldName = string.Format("{0}.ToLower()", item.FieldName);
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
                                if (DateTime.TryParse(item.FieldValue, out dateTime))
                                {
                                    if (!item.FieldValue.Contains("00:00:00") && dateTime.ToString("HH:mm:ss") == "00:00:00")
                                    {
                                        if (operatorEnum == Operator.LE)
                                        {
                                            listArgs.Add(DateTime.Parse(dateTime.ToString("yyyy-MM-dd") + " 23:59:59"));
                                        }
                                        else
                                        {
                                            listArgs.Add(dateTime);
                                        }
                                    }
                                    else
                                    {
                                        listArgs.Add(dateTime);
                                    }
                                    sb.AppendFormat(" AND {0} {1} @{2}", item.FieldName, strOperator, paramCount);
                                }
                            }
                            else
                            {
                                listArgs.Add(ConvertToType(item.FieldValue, GetPropType<T>(item.FieldName)));
                                sb.AppendFormat(" AND {0} {1} @{2}", item.FieldName, strOperator, paramCount);
                            }
                            paramCount++;
                            break;
                        case Operator.Like:
                        case Operator.NotLike:
                        case Operator.LLike:
                        case Operator.RLike:
                            listArgs.Add(item.FieldValue);
                            if (operatorEnum == Operator.Like)
                            {
                                sb.AppendFormat(" AND {0}.Contains(@{1})", item.FieldName, paramCount);
                            }
                            else if (operatorEnum == Operator.NotLike)
                            {
                                sb.AppendFormat(" AND !{0}.Contains(@{1})", item.FieldName, paramCount);
                            }
                            else if (operatorEnum == Operator.LLike)
                            {
                                sb.AppendFormat(" AND {0}.EndsWith(@{1})", item.FieldName, paramCount);
                            }
                            else if (operatorEnum == Operator.RLike)
                            {
                                sb.AppendFormat(" AND {0}.StartsWith(@{1})", item.FieldName, paramCount);
                            }
                            paramCount++;
                            break;
                        case Operator.Null:
                            listArgs.Add(item.FieldValue);
                            sb.AppendFormat(" AND {0}=null", item.FieldName);
                            paramCount++;
                            break;
                        case Operator.NotNull:
                            listArgs.Add(item.FieldValue);
                            sb.AppendFormat(" AND {0}!=null", item.FieldName);
                            paramCount++;
                            break;
                        case Operator.In:
                            sb.AppendFormat(" AND (");
                            foreach (var schar in item.FieldValue.Split(','))
                            {
                                listArgs.Add(schar);
                                sb.AppendFormat("{0}=@{1} or ", item.FieldName, paramCount);
                                paramCount++;
                            }
                            sb.Remove(sb.Length - 3, 3);
                            sb.AppendFormat(" )");
                            break;
                        case Operator.NotIn:
                            sb.AppendFormat(" AND (");
                            foreach (var schar in item.FieldValue.Split(','))
                            {
                                listArgs.Add(schar);
                                sb.AppendFormat("{0}!=@{1} and ", item.FieldName, paramCount);
                                paramCount++;
                            }
                            sb.Remove(sb.Length - 3, 3);
                            sb.AppendFormat(" )");
                            break;
                    }
                    if (sb.ToString().Length > 0)
                    {
                        filter = sb.ToString().Substring(4, sb.Length - 4);
                    }
                }
                #endregion

            }
            result.Filter = filter;
            result.ListArgs = listArgs;
            return result;
        }

        /// <summary>
        /// 获取实体属性的类型Type
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="propName">属性名称</param>
        /// <returns></returns>
        private static Type GetPropType<T>(string propName)
        {
            DbMapInfo dbInfo = null;
            Type result = DefaultType;
            Type entityType = typeof(T);
            if (EFTypeDescriptionCache.MemberDict.TryGetValue(entityType.FullName, out dbInfo))
            {
                ColumnMapInfo cmi = null;
                if (dbInfo.CacheColumnMapInfos.TryGetValue(propName, out cmi))
                {
                    result = cmi.PropType;
                }
            }
            else
            {
                var pInfo = entityType.GetProperty(propName, BindFlags);
                result = pInfo == null ? DefaultType : pInfo.PropertyType;
            }
            if (result.IsEnum)
            {
                result = Enum.GetUnderlyingType(result);
            }
            return result;
        }

        private static object ConvertToType(string value, Type type)
        {
            if ((type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                type = type.GetGenericArguments()[0];
            }
            if (type == typeof(bool))
            {
                if (value == "1" || value.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }
        #endregion
    }

}
