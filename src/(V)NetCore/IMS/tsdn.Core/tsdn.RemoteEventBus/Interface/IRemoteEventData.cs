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
using tsdn.Events.Bus;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件数据
    /// </summary>
    public interface IRemoteEventData : IEventData
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        object Data { get; set; }
    }
}
