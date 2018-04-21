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
using System.Threading.Tasks;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件发布接口
    /// </summary>
    public interface IRemoteEventPublisher: IDisposable, ISingletonDependency
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="topic">消息主题</param>
        /// <param name="remoteEventData">消息数据</param>
        void Publish(string topic, IRemoteEventData remoteEventData);

        /// <summary>
        /// 异步发布消息
        /// </summary>
        /// <param name="topic">消息主题</param>
        /// <param name="remoteEventData">消息数据</param>
        /// <returns>Task</returns>
        Task PublishAsync(string topic, IRemoteEventData remoteEventData);
    }
}
