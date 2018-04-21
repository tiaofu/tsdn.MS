using tsdn.RealTime;
using System.Threading.Tasks;

namespace tsdn.Common.SignalR
{
    /// <summary>
    /// 消息推送接口
    /// </summary>
    public interface IMessagePush
    {
        /// <summary>
        /// 立即发送消息给组
        /// </summary>
        /// <param name="record">消息</param>
        /// <param name="Groups">组</param>
        Task SendAsync(MessageRecord record, string[] Groups);

        /// <summary>
        /// 立即发送消息给所有人
        /// </summary>
        /// <param name="record">消息</param>
        Task SendAllAsync(MessageRecord record);

        /// <summary>
        /// 发送消息到MQ，由MQ分发后再推送到客户端
        /// </summary>
        /// <param name="record">消息</param>
        /// <param name="Groups">组</param>
        Task SendToRabbitMQAsync(MessageRecord record, string[] Groups);
    }
}
