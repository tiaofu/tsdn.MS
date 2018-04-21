using tsdn.Dependency;
using tsdn.RealTime;
using tsdn.RemoteEventBus;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace tsdn.Common.SignalR
{
    /// <summary>
    /// 消息推送实现
    /// </summary>
    public class SignalrPush : IMessagePush, ISingletonDependency
    {
        private readonly IHubContext<MessageHub> hubContext;

        private readonly IRemoteEventBus remoteEventBus;

        public SignalrPush(IHubContext<MessageHub> _hubContext, IRemoteEventBus _remoteEventBus)
        {
            hubContext = _hubContext;
            remoteEventBus = _remoteEventBus;
        }

        /// <summary>
        /// 发送消息给组
        /// </summary>
        /// <param name="record">消息</param>
        /// <param name="Groups">组</param>
        public Task SendAsync(MessageRecord record, string[] Groups)
        {
            return Task.Factory.StartNew(() =>
            {
                if (Groups != null && Groups.Length > 0)
                {
                    hubContext.Clients.Groups(Groups).SendAsync("Push", record);
                }
                else
                {
                    hubContext.Clients.All.SendAsync("Push", record);
                }
            });
        }

        /// <summary>
        /// 发送消息给所有人
        /// </summary>
        /// <param name="record">消息</param>
        public Task SendAllAsync(MessageRecord record)
        {
            return SendAsync(record, null);
        }

        /// <summary>
        /// 发送消息到MQ，由MQ分发后再推送到客户端
        /// </summary>
        /// <param name="record">消息</param>
        /// <param name="Groups">组</param>
        public Task SendToRabbitMQAsync(MessageRecord record, string[] Groups)
        {
            var eventDate = new RemoteEventTopicData("MQ.SignalR")
            {
                Data = new
                {
                    Record = record,
                    Groups = Groups
                },
                Topic = $"SignalR.{record.MsgType}"
            };
            return remoteEventBus.PublishAsync(eventDate);
        }
    }
}
