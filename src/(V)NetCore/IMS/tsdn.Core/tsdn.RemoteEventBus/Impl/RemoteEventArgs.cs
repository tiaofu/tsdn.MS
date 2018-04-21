/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Events.Bus;
using System;

namespace tsdn.RemoteEventBus
{
    public class RemoteEventArgs : EventArgs, IEventData
    {
        public IRemoteEventData EventData { get; set; }

        public string Topic { get; set; }

        public string Message { get; set; }

        public bool Suspended { get; set; }

        public string ReportedTime { get; set; }

        public object EventSource { get; set; }

        public RemoteEventArgs(IRemoteEventData eventData, string topic, string message)
        {
            EventData = eventData;
            Message = message;
            Topic = topic;
            ReportedTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
