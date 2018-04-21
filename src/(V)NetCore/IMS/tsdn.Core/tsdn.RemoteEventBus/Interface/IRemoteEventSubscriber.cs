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
    /// 事件订阅接口
    /// </summary>
    public interface IRemoteEventSubscriber : IDisposable, ISingletonDependency
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="topics">需要订阅的主题</param>
        /// <param name="handler">事件处理器</param>
        void Subscribe(IEnumerable<string> topics, Action<string, string> handler);

        /// <summary>
        /// 异步订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <param name="handler">处理器</param>
        /// <returns>Task</returns>
        Task SubscribeAsync(IEnumerable<string> topics, Action<string, string> handler);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topics">主题</param>
        void Unsubscribe(IEnumerable<string> topics);

        /// <summary>
        /// 异步取消订阅
        /// </summary>
        /// <param name="topics">主题</param>
        /// <returns>Task</returns>
        Task UnsubscribeAsync(IEnumerable<string> topics);

        /// <summary>
        /// 取消所有订阅
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 异步取消所有订阅
        /// </summary>
        /// <returns>Task</returns>
        Task UnsubscribeAllAsync();
    }
}
