/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Commons.Pool;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace tsdn.RemoteEventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ事件发布器
    /// </summary>
    public class RabbitMQRemoteEventPublisher : IRemoteEventPublisher
    {
        /// <summary>
        /// 对象连接池，管理RabbitMQ连接
        /// </summary>
        private readonly IObjectPool<IConnection> _connectionPool;

        /// <summary>
        /// 序列化对象
        /// </summary>
        private readonly IRemoteEventSerializer _remoteEventSerializer;

        /// <summary>
        /// RabbitMQ设置信息
        /// </summary>
        private readonly RabbitMQSetting _rabbitMQSetting;

        private bool _disposed;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="poolManager">对象池管理</param>
        /// <param name="rabbitMQSetting">RabbitMQ设置信息</param>
        /// <param name="remoteEventSerializer">序列化对象</param>
        public RabbitMQRemoteEventPublisher(
            IPoolManager poolManager,
            RabbitMQSetting rabbitMQSetting,
            IRemoteEventSerializer remoteEventSerializer
            )
        {
            _remoteEventSerializer = remoteEventSerializer;
            _rabbitMQSetting = rabbitMQSetting;
            _connectionPool = poolManager.NewPool<IConnection>()
                                    .InitialSize(rabbitMQSetting.InitialSize)
                                    .MaxSize(rabbitMQSetting.MaxSize)
                                    .WithFactory(new PooledObjectFactory(rabbitMQSetting))
                                    .Instance();
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="topic">消息主题</param>
        /// <param name="remoteEventData">消息数据</param>
        public void Publish(string topic, IRemoteEventData remoteEventData)
        {
            var connection = _connectionPool.Acquire();
            try
            {
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(_rabbitMQSetting.ExchangeName, "topic", true);
                var body = Encoding.UTF8.GetBytes(_remoteEventSerializer.Serialize(remoteEventData));
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(_rabbitMQSetting.ExchangeName, topic, properties, body);
            }
            finally
            {
                _connectionPool.Return(connection);
            }
        }

        /// <summary>
        /// 异步发布消息
        /// </summary>
        /// <param name="topic">消息主题</param>
        /// <param name="remoteEventData">消息数据</param>
        /// <returns>Task</returns>
        public Task PublishAsync(string topic, IRemoteEventData remoteEventData)
        {
            return Task.Factory.StartNew(() =>
            {
                Publish(topic, remoteEventData);
            });
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connectionPool.Dispose();

                _disposed = true;
            }
        }
    }
}
