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

namespace tsdn.Common
{
    /// <summary>
    /// 分页查询结果
    /// </summary>
    /// <typeparam name="T">泛型实体</typeparam>
    public class PaginationResult<T>
    {
        /// <summary>
        /// 查询结果数据
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 过滤条件
    /// </summary>
    public class EFFilter
    {
        /// <summary>
        /// 过滤条件where字符串
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public List<object> ListArgs { get; set; }
    }
    /// <summary>
    /// 列表页面进行查询的参数
    /// </summary>
    public class QueryCondition
    {
        private int pageSize;

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value < 0)
                {
                    IsPagination = false;
                }
                else
                {
                    IsPagination = true;
                }
                pageSize = value;
            }
        }

        /// <summary>
        /// 当前页码 从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// 升序或者降序
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// 是否进行分页查询
        /// </summary>
        public bool IsPagination { get; set; }

        private bool isCalcTotal = true;

        /// <summary>
        /// 分页查询时是否从数据库查询总条数
        /// </summary>
        public bool IsCalcTotal
        {
            get
            {
                return isCalcTotal;
            }
            set
            {
                isCalcTotal = value;
            }
        }

        /// <summary>
        /// 过滤条件集合
        /// </summary>
        public List<Filter> FilterList { get; set; }

        /// <summary>
        /// 固定的过滤条件,用于参数化查询
        /// </summary>
        public List<FixedFilter> FixedFilterList { get; set; }

        /// <summary>
        /// 用于拼接复杂查询条件
        /// </summary>
        /// <example>
        ///{ OrFilterList:[
        ///    [{FieldName:'PlateNo', FieldValue:'津A12345', Operator:'eq', IsDateTime:false},{FieldName:'PlateColor',FieldValue:'0',Operator:'eq',IsDateTime:false}],
        ///     [{FieldName:'PlateNo', FieldValue:'津A22345', Operator:'eq', IsDateTime:false},{FieldName:'PlateColor',FieldValue:'1',Operator:'eq',IsDateTime:false}]
        ///   ]
        /// }
        /// 最终SQL  (PlateNo='津A12345' and PlateColor='0') or (PlateNo='津A22345' and PlateColor='1'))
        /// </example>
        //public List<List<Filter>> OrFilterList { get; set; }
    }

    /// <summary>
    /// 固定的参数化查询SQL
    /// </summary>
    public class FixedFilter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string ParamValue { get; set; }

        /// <summary>
        /// 字段是否为DateTime类型,如果为DateTime类型则需将FieldValue转换成DateTime进行参数化查询
        /// </summary>
        public bool IsDateTime { get; set; }

    }

    /// <summary>
    /// 列表页面进行查询的过滤条件
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 过滤条件字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 过滤条件值
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// 过滤类型 Like = > ....
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 字段是否为DateTime类型,如果为DateTime类型则需将FieldValue转换成DateTime进行参数化查询
        /// </summary>
        public bool IsDateTime { get; set; }


        /// <summary>
        /// 字段查询时是否忽略大小写比较
        /// </summary>
        public bool IgnoreCase { get; set; }


        private bool isAutoPercent = true;

        /// <summary>
        /// 字段是否为自动添加%匹配,对字符串类型查询时IsAutoPercent为true则会在字符串前后加字符串进行匹配 为false则不进行处理，主要处理通行记录时车牌号码查询记录过大速度慢
        /// </summary>
        public bool IsAutoPercent
        {
            get { return isAutoPercent; }
            set
            {
                isAutoPercent = value;
            }
        }
    }

    public static class QueryConditionExtend
    {
        public static string HandleSortField(string SortField)
        {
            SortField = SortField.Replace(" asc", "").Replace(" desc", "").Trim();
            if (SortField.EndsWith(","))
            {
                SortField = SortField.Substring(0, SortField.Length - 1);
            }
            return SortField;
        }

        /// <summary>
        /// 处理jqgrid 层级列表传输的查询条件排序字段不符合规则
        /// </summary>
        /// <param name="Condition">查询条件</param>
        public static void HandleSortField(this QueryCondition Condition)
        {
            if (Condition != null && !string.IsNullOrEmpty(Condition.SortField))
            {
                Condition.SortField = HandleSortField(Condition.SortField);
            }
        }
    }
}
