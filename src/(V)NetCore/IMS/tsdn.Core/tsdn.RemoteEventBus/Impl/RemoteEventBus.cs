/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.RemoteEventBus.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tsdn.RemoteEventBus.Events;
using Microsoft.Extensions.Logging;
using tsdn.Events.Bus;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件总线实现
    /// </summary>
    public class RemoteEventBus : IRemoteEventBus
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;
        private readonly IRemoteEventPublisher _publisher;
        private readonly IRemoteEventSubscriber _subscriber;
        private readonly IRemoteEventTopicSelector _topicSelector;
        private readonly IRemoteEventSerializer _remoteEventSerializer;

        private bool _disposed;

        public RemoteEventBus(
            IEventBus eventBus,
            IRemoteEventPublisher publisher,
            IRemoteEventSubscriber subscriber,
            IRemoteEventTopicSelector topicSelector,
            IRemoteEventSerializer remoteEventSerializer,
            ILogger<RemoteEventBus> logger
        )
        {
            _eventBus = eventBus;
            _publisher = publisher;
            _subscriber = subscriber;
            _topicSelector = topicSelector;
            _remoteEventSerializer = remoteEventSerializer;
            _logger = logger;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventData">事件</param>
        public void Publish(IRemoteEventData eventData)
        {
            Publish(_topicSelector.SelectTopic(eventData), eventData);
        }

        public Task PublishAsync(IRemoteEventData eventData)
        {
            return PublishAsync(_topicSelector.SelectTopic(eventData), eventData);
        }

        public void Publish(string topic, IRemoteEventData eventData)
        {
            _eventBus.Trigger(this, new RemoteEventBusPublishingEvent(eventData));
            _publisher.Publish(topic, eventData);
            _logger.LogDebug($"Event published on topic {topic}");
            _eventBus.Trigger(this, new RemoteEventBusPublishedEvent(eventData));
        }

        public async Task PublishAsync(string topic, IRemoteEventData eventData)
        {
            await _eventBus.TriggerAsync(this, new RemoteEventBusPublishingEvent(eventData));
            await _publisher.PublishAsync(topic, eventData)
                .ContinueWith((s) => _logger.LogDebug($"Event published on topic {topic}"));
            await _eventBus.TriggerAsync(this, new RemoteEventBusPublishedEvent(eventData));
            await Task.FromResult(0);
        }

        public void Subscribe(string topic)
        {
            Subscribe(new[] { topic });
        }

        public Task SubscribeAsync(string topic)
        {
            return SubscribeAsync(new[] { topic });
        }

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topic">主题</param>
        public void Subscribe(string topic, Action<string, string> action)
        {
            _subscriber.Subscribe(new[] { topic }, action);
        }

        public void Subscribe(IEnumerable<string> topics)
        {
            _subscriber.Subscribe(topics, MessageHandle);
            _logger.LogDebug($"Subscribed topics {string.Join(",", topics)}");
        }

        public Task SubscribeAsync(IEnumerable<string> topics)
        {
            return _subscriber.SubscribeAsync(topics, MessageHandle)
                .ContinueWith((s) => _logger.LogDebug($"Subscribed topics {string.Join(",", topics)}"));
        }

        public void Unsubscribe(string topic)
        {
            Unsubscribe(new[] { topic });
        }

        public Task UnsubscribeAsync(string topic)
        {
            return UnsubscribeAsync(new[] { topic });
        }

        public void Unsubscribe(IEnumerable<string> topics)
        {
            _subscriber.Unsubscribe(topics);
            _logger.LogDebug($"Unsubscribed topics {string.Join(",", topics)}");
        }

        public Task UnsubscribeAsync(IEnumerable<string> topics)
        {
            return _subscriber.UnsubscribeAsync(topics)
                .ContinueWith((s) => _logger.LogDebug($"Unsubscribed topics {string.Join(",", topics)}"));
        }

        public virtual void MessageHandle(string topic, string message)
        {
            _logger.LogDebug($"Receive message on topic {topic}");
            try
            {
                var eventData = _remoteEventSerializer.Deserialize<RemoteEventData>(message);
                var eventArgs = new RemoteEventArgs(eventData, topic, message);
                _eventBus.Trigger(this, new RemoteEventBusHandlingEvent(eventArgs));
                _eventBus.Trigger(this, eventArgs);
                _eventBus.Trigger(this, new RemoteEventBusHandledEvent(eventArgs));
            }
            catch (Exception ex)
            {
                _logger.LogError("Consume remote message exception:" + ex.Message, ex);
                _eventBus.Trigger(this, new RemoteEventMessageHandleExceptionData(ex, topic, topic));
            }
        }

        public void UnsubscribeAll()
        {
            _subscriber.UnsubscribeAll();
            _logger.LogDebug($"Unsubscribed all topics");
        }

        public Task UnsubscribeAllAsync()
        {
            return _subscriber.UnsubscribeAllAsync()
                .ContinueWith((s) => _logger.LogDebug($"Unsubscribes all topics"));
            ;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                _subscriber?.Dispose();
                _publisher?.Dispose();

                _disposed = true;
            }
        }
    }
}