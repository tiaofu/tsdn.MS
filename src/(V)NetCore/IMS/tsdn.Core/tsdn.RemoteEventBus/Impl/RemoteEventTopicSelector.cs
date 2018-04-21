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

namespace tsdn.RemoteEventBus
{
    public class RemoteEventTopicSelector : IRemoteEventTopicSelector, ISingletonDependency
    {
        public const string TOPIC_DEFAULT = "TOPIC_DEFAULT";

        private readonly Dictionary<Type, string> _mapping;

        public RemoteEventTopicSelector()
        {
            _mapping = new Dictionary<Type, string>();
        }

        public void SetMapping<T>(string topic) where T : IRemoteEventData
        {
            _mapping[typeof(T)] = topic;
        }

        public string SelectTopic(IRemoteEventData eventData)
        {
            if (eventData is IHasTopicRemoteEventData)
            {
                return (eventData as IHasTopicRemoteEventData).Topic;
            }
            foreach (var item in _mapping)
            {
                if (item.Key.IsAssignableFrom(eventData.GetType()))
                {
                    return item.Value;
                }
            }
            return TOPIC_DEFAULT;
        }
    }
}
