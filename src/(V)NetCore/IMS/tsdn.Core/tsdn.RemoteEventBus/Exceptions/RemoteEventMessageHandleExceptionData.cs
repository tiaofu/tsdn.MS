/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Events.Bus.Exceptions;
using System;

namespace tsdn.RemoteEventBus.Exceptions
{
    [Serializable]
    public class RemoteEventMessageHandleExceptionData : HandledExceptionData
    {
        public string Topic { get; set; }

        public string Message { get; set; }

        public RemoteEventMessageHandleExceptionData(Exception exception, string topic, string message) : base(exception)
        {
            Topic = topic;
            Message = message;
        }
    }
}
