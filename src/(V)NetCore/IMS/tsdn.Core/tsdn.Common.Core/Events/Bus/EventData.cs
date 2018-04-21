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

namespace tsdn.Events.Bus
{
    /// <summary>
    /// IEventData实现 
    /// <see cref="IEventData"/>
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// 时间发生时间
        /// </summary>
        public string ReportedTime { get; set; }

        /// <summary>
        /// 事件源 (可选项).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected EventData()
        {
            ReportedTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}