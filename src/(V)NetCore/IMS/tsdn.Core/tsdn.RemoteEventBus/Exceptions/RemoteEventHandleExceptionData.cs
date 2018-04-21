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
    public class RemoteEventHandleExceptionData : HandledExceptionData
    {
        public RemoteEventArgs EventArgs { get; set; }

        public RemoteEventHandleExceptionData(Exception exception, RemoteEventArgs eventArgs) : base(exception)
        {
            EventArgs = eventArgs;
        }
    }
}
