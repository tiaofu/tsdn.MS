/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Events.Bus.Handlers;

namespace tsdn.Events.Bus.Factories
{
    /// <summary>
    /// 事件处理工厂接口，用于释放和获取事件处理
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器
        /// </summary>
        /// <returns>事件处理器</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// 释放事件处理.
        /// </summary>
        /// <param name="handler">需要释放的事件处理器</param>
        void ReleaseHandler(IEventHandler handler);
    }
}