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
    ///  定义所有事件数据类的接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件发生时间
        /// </summary>
        string ReportedTime { get; set; }

        /// <summary>
        /// 事件源
        /// </summary>
        object EventSource { get; set; }
    }
}
