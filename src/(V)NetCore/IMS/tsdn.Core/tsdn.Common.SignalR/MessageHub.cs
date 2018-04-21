using tsdn.Auditing;
using tsdn.RealTime;
using tsdn.RemoteEventBus;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace tsdn.Common.SignalR
{
    public class MessageHub : Hub
    {
        //private readonly ILogger logger;

        //private readonly IClientInfoProvider clientInfoProvider;

        private readonly IRemoteEventBus remoteEventBus;
        public MessageHub(IRemoteEventBus _remoteEventBus)
        {
            remoteEventBus = _remoteEventBus;
        }

        public override async Task OnConnectedAsync()
        {
            //logger.LogInformation($"客户端ConnectionId:{Context.ConnectionId}在{clientInfoProvider.ClientIpAddress}上连接上了WebSocket服务器{Context.Connection.LocalIpAddress.ToString()}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //logger.LogInformation($"客户端ConnectionId:{Context.ConnectionId}在{clientInfoProvider.ClientIpAddress}上断开连接");
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 加入到组
        /// </summary>
        /// <param name="groups">组</param>
        /// <returns></returns>
        public async Task JoinGroup(string[] groups)
        {
            if (groups != null)
            {
                foreach (string group in groups)
                {
                    if (string.IsNullOrEmpty(group))
                    {
                        continue;
                    }
                    await Groups.AddAsync(Context.ConnectionId, group);
                }
            }
        }

        /// <summary>
        /// 发送消息给组
        /// </summary>
        /// <param name="record">消息</param>
        /// <param name="Groups">组</param>
        public async Task Push(MessageRecord record, string[] Groups)
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
            await remoteEventBus.PublishAsync(eventDate);
        }
    }
}
