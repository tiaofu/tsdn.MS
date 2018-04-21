/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Common.Config
{
    ///<summary>
    ///系统参数配置选项表
    ///</summary>
    [Table("CONFIGURATION_OPTIONS")]
    public class Options
    {
        ///<summary>
        ///选项ID 
        ///</summary> 
        [Key]
        public string OptionId { get; set; }

        ///<summary>
        ///选项类型（字典表Kind 为 18 的类型，存放字典表主键编号） 
        ///</summary> 
        public string OptionType { get; set; }

        ///<summary>
        ///选项名称 
        ///</summary> 
        public string OptionName { get; set; }

        ///<summary>
        ///参数Key值 
        ///</summary> 
        public string Key { get; set; }

        ///<summary>
        ///选项值 
        ///</summary> 
        public string Value { get; set; }

        ///<summary>
        ///值类型（0:整形  1:字符串 2:布尔型 3:密码） 
        ///</summary> 
        public string Valuetype { get; set; }

        ///<summary>
        ///是否必填,默认为是
        ///</summary> 
        [NotMapped]
        public bool Required { get; set; }

        ///<summary>
        ///是否立即进行服务间同步配置，并且立即应用配置，默认为是  为是会发送MQ消息
        ///</summary> 
        [NotMapped]
        public bool ImmediateUpdate { get; set; }

        /// <summary>
        /// 字段校验规则
        /// </summary>
        [NotMapped]
        public string ValidateRule { get; set; }

        /// <summary>
        /// 鼠标悬浮Title
        /// </summary>
        [NotMapped]
        public string Title { get; set; }

        /// <summary>
        /// 下拉选择数据源，通过反射调用方法获取 返回value text类型数据
        /// </summary>
        [NotMapped]
        public object DataSource { get; set; }

        /// <summary>
        /// 日期项 对应格式化显示，默认为年-月-日
        /// </summary>
        [NotMapped]
        public string FormatDate { get; set; }

        /// <summary>
        /// 配置来源,db 数据库,local：配置文件覆盖
        /// </summary>
        [NotMapped]
        public string ConfigFrom { get; set; } = "db";

        /// <summary>
        /// 个性化配置值
        /// </summary>
        [NotMapped]
        public string LocalValue { get; set; }

        /// <summary>
        /// 数据库配置值
        /// </summary>
        [NotMapped]
        public string DBValue { get; set; }
    }

    /// <summary>
    /// 配置分组
    /// </summary>
    public class OptionGroup
    {
        /// <summary>
        /// 配置分组
        /// </summary>
        public string GroupType { get; set; }

        /// <summary>
        /// 分组中文
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 分组中文拼音
        /// </summary>
        public string GroupNamePinYin
        {
            get
            {
                return GroupName.GetBopomofo();
            }
        }

        /// <summary>
        /// 是否立即进行服务间同步配置，并且立即应用配置，默认为是  为是会发送MQ消息
        /// </summary>
        public bool ImmediateUpdate { get; set; }

        /// <summary>
        /// 自定义页面地址，不为空的化则跳转到自定义配置页面
        /// </summary>
        public string CustomPage { get; set; }
    }

    /// <summary>
    /// configViewModel
    /// </summary>
    public class OptionViewModel
    {
        public OptionGroup Group { get; set; }

        public List<Options> ListOptions { get; set; }

        /// <summary>
        /// 所属模块大类
        /// </summary>
        public string FunctionType { get; set; }

        /// <summary>
        /// 自定义标签列表
        /// </summary>
        public List<Tags> TagList { get; set; }
    }
}

