/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.RemoteEventBus.Events
{
    public class RemoteEventBusPublishingEvent: RemoteEventBusPublishEvent
    {
        public RemoteEventBusPublishingEvent(IRemoteEventData eventData)
            : base(eventData)
        {

        }
    }
}
