﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件处理属性标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RemoteEventHandlerAttribute : Attribute
    {
        /// <summary>
        /// 需要处理的事件类型
        /// </summary>
        public string ForType { get; set; }

        /// <summary>
        /// 订阅的主题
        /// </summary>
        public string ForTopic { get; set; }

        /// <summary>
        /// 多个Handler处理的顺序，值小的先处理
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否阻止其他Handler继续处理本事件当本Handler处理时发生异常
        /// </summary>
        public bool SuspendWhenException { get; set; }

        /// <summary>
        /// 是否只处理该主题下的该类型的事件（不处理其他主题下该类型的事件）
        /// </summary>
        public bool OnlyHandleThisTopic { get; set; }
    }
}
