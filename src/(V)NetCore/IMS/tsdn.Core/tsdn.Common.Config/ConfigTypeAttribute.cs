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

namespace tsdn.Common.Config
{

    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigTypeAttribute : Attribute
    {
        /// <summary>
        /// 分组类型-用来区分数据
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        public string FunctionType { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupCn { get; set; }

        /// <summary>
        /// 自定义页面地址，不为空的化则跳转到自定义配置页面
        /// </summary>
        public string CustomPage { get; set; }

        /// <summary>
        /// 是否立即进行服务间同步配置，并且立即应用配置，默认为是
        /// </summary>
        private bool _immediateUpdate = true;

        /// <summary>
        /// 是否立即进行服务间同步配置，并且立即应用配置，默认为是  为是会发送MQ消息
        /// </summary>
        public bool ImmediateUpdate
        {
            get
            {
                return _immediateUpdate;
            }
            set
            {
                _immediateUpdate = value;
            }
        }
    }
}
