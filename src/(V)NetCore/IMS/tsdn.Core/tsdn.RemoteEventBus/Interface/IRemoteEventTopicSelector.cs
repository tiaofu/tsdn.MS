/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件主题选择器接口
    /// </summary>
    public interface IRemoteEventTopicSelector
    {
        /// <summary>
        /// 根据事件选择主题
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        string SelectTopic(IRemoteEventData eventData);

        void SetMapping<T>(string topic) where T : IRemoteEventData;
    }
}
