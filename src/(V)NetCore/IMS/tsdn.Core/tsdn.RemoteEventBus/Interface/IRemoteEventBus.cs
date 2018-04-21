/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Dependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件总线接口
    /// </summary>
    public interface IRemoteEventBus : IDisposable, ISingletonDependency
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="message">消息</param>
        void MessageHandle(string topic, string message);

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventData">消息</param>
        void Publish(IRemoteEventData eventData);

        /// <summary>
        /// 发布指定主题事件
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="eventData">事件</param>
        void Publish(string topic, IRemoteEventData eventData);

        /// <summary>
        /// 异步发布事件
        /// </summary>
        /// <param name="eventData">事件</param>
        /// <returns></returns>
        Task PublishAsync(IRemoteEventData eventData);

        /// <summary>
        /// 异步发布指定主题事件
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="eventData">事件</param>
        /// <returns></returns>
        Task PublishAsync(string topic, IRemoteEventData eventData);

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topic">主题</param>
        void Subscribe(string topic);

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topic">主题</param>
        void Subscribe(string topic, Action<string, string> action);

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topics">主题</param>
        void Subscribe(IEnumerable<string> topics);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        Task SubscribeAsync(string topic);

        Task SubscribeAsync(IEnumerable<string> topics);

        void Unsubscribe(string topic);

        void Unsubscribe(IEnumerable<string> topics);

        /// <summary>
        /// 异步取消指定主题订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <returns></returns>
        Task UnsubscribeAsync(string topic);

        /// <summary>
        /// 异步取消指定主题订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <returns></returns>
        Task UnsubscribeAsync(IEnumerable<string> topics);

        /// <summary>
        /// 取消所有订阅
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 异步取消所有订阅
        /// </summary>
        /// <returns></returns>
        Task UnsubscribeAllAsync();
    }
}