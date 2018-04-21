using tsdn.RealTime;
using tsdn.RemoteEventBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace tsdn.Common.SignalR
{
    /// <summary>
    /// 处理MQ分发的SignalR消息，推送到客户端
    /// </summary>
    [RemoteEventHandler(ForType = "MQ.SignalR", ForTopic = "SignalR.#")]
    public class MessageHandler : IRemoteEventHandler
    {
        private readonly IMessagePush push;
        private readonly ILogger logger;

        public MessageHandler(IMessagePush _push, ILogger<MessageHandler> _logger)
        {
            push = _push;
            logger = _logger;
        }

        public void HandleEvent(RemoteEventArgs eventArgs)
        {
            logger.LogInformation($"从MQ中接收到消息{JsonConvert.SerializeObject(eventArgs)}，开始推送到客户端");
            var jobject = eventArgs.EventData.Data as JObject;
            MessageRecord record = jobject["Record"].ToObject<MessageRecord>();
            var groups = jobject["Groups"].ToObject<string[]>();
            push.SendAsync(record, groups);
        }
    }
}
