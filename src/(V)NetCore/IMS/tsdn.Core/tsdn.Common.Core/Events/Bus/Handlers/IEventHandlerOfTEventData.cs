/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Events.Bus.Handlers
{
    /// <summary>
    /// 事件处理类的接口<see cref="TEventData"/>.
    /// </summary>
    /// <typeparam name="TEventData">事件处理数据</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// 事件处理实现的方法
        /// </summary>
        /// <param name="eventData">事件数据</param>
        void HandleEvent(TEventData eventData);
    }
}
