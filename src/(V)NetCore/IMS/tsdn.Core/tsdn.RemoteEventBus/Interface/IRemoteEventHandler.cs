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

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    public interface IRemoteEventHandler : ITransientDependency
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventArgs">事件参数</param>
        void HandleEvent(RemoteEventArgs eventArgs);
    }
}
