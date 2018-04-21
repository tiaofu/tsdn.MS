/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tsdn.RemoteEventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ事件订阅器
    /// </summary>
    public class RabbitMQRemoteEventSubscriber : IRemoteEventSubscriber
    {
        private readonly ConcurrentDictionary<string, IModel> _dictionary;
        private readonly List<IConnection> _connectionsAcquired;
        private readonly PooledObjectFactory _factory;
        /// <summary>
        /// RabbitMQ设置信息
        /// </summary>
        private readonly RabbitMQSetting _rabbitMQSetting;
        private bool _disposed;

        public RabbitMQRemoteEventSubscriber(RabbitMQSetting rabbitMQSetting)
        {
            _factory = new PooledObjectFactory(rabbitMQSetting);
            _dictionary = new ConcurrentDictionary<string, IModel>();
            _connectionsAcquired = new List<IConnection>();
            _rabbitMQSetting = rabbitMQSetting;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="topics">需要订阅的主题</param>
        /// <param name="handler">事件处理器</param>
        public void Subscribe(IEnumerable<string> topics, Action<string, string> handler)
        {
            var existsTopics = topics.ToList().Where(p => _dictionary.ContainsKey(p));
            if (existsTopics.Any())
            {
                throw new Exception(string.Format("主题 topics {0} 已经被订阅", string.Join(",", existsTopics)));
            }

            foreach (var topic in topics)
            {
                var connection = _factory.Create();
                _connectionsAcquired.Add(connection);
                try
                {
                    var channel = connection.CreateModel();
                    var queue = _rabbitMQSetting.QueuePrefix;
                    //var queue = _rabbitMQSetting.QueuePrefix + topic;
                    channel.ExchangeDeclare(_rabbitMQSetting.ExchangeName, "topic", true);
                    channel.QueueDeclare(queue, true, false, false, null);
                    channel.QueueBind(queue, _rabbitMQSetting.ExchangeName, topic);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (ch, ea) =>
                    {
                        handler(ea.RoutingKey, Encoding.UTF8.GetString(ea.Body));
                        //channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue, true, consumer);
                    _dictionary[topic] = channel;
                }
                finally
                {
                    _connectionsAcquired.Remove(connection);
                }
            }
        }

        /// <summary>
        /// 异步订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <param name="handler">处理器</param>
        /// <returns>Task</returns>
        public Task SubscribeAsync(IEnumerable<string> topics, Action<string, string> handler)
        {
            return Task.Factory.StartNew(() => Subscribe(topics, handler));
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topics">主题</param>
        public void Unsubscribe(IEnumerable<string> topics)
        {
            foreach (var topic in topics)
            {
                if (_dictionary.ContainsKey(topic))
                {
                    _dictionary[topic].Close();
                    _dictionary[topic].Dispose();
                }
            }
        }

        /// <summary>
        /// 异步取消订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <returns>Task</returns>
        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return Task.Factory.StartNew(() => Unsubscribe(topics));
        }

        /// <summary>
        /// 取消所有订阅
        /// </summary>
        public void UnsubscribeAll()
        {
            Unsubscribe(_dictionary.Select(p => p.Key));
        }

        /// <summary>
        /// 异步取消所有订阅
        /// </summary>
        /// <returns>Task</returns>
        public Task UnsubscribeAllAsync()
        {
            return Task.Factory.StartNew(UnsubscribeAll);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                UnsubscribeAll();
                foreach (var connection in _connectionsAcquired)
                {
                    _factory.Destroy(connection);
                }

                _disposed = true;
            }
        }
    }
}
