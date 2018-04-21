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

namespace tsdn.Common.Config
{
    /// <summary>
    /// 配置项属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConfigAttribute : Attribute
    {
        /// <summary>
        /// 对应数据库的键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 每项值中文说明,表单前面的说明
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 每项值中文说明,表单前面的说明
        /// </summary>
        public ConfigValueType ValueType { get; set; }

        /// <summary>
        /// 下拉选择数据源，通过反射调用方法获取 返回value text类型数据
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 日期项 对应格式化显示，默认为年-月-日
        /// </summary>
        public string FormatDate { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 默认为必填
        /// </summary>
        private bool _required = true;
        /// <summary>
        /// 值是否必填
        /// </summary>
        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                _required = value;
            }
        }

        /// <summary>
        /// 字段前端校验规则-对应jquery.validate的校验规则
        /// </summary>
        public string ValidateRule { get; set; }

        /// <summary>
        /// 界面鼠标放上去Title
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// 每项数值类型
    /// </summary>
    public enum ConfigValueType
    {
        /// <summary>
        /// 整形
        /// </summary>
        Number = 1,

        /// <summary>
        /// 字符串
        /// </summary>
        String = 2,

        /// <summary>
        /// 布尔型
        /// </summary>
        Bool = 3,

        /// <summary>
        /// 密码
        /// </summary>
        Password = 4,

        /// <summary>
        /// 多行文本
        /// </summary>
        TextArea = 5,

        /// <summary>
        /// 下拉框
        /// </summary>
        Select = 6,

        /// <summary>
        /// 多选下拉框
        /// </summary>
        MultiSelect = 7,

        /// <summary>
        /// 日期
        /// </summary>
        DateTime = 8
    }
}
