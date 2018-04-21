/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Events.Bus;
using System;

namespace tsdn.RemoteEventBus
{
    [Serializable]
    public sealed class RemoteEventTopicData : EventData, IRemoteEventData, IHasTopicRemoteEventData
    {
        public object Data { get; set; }

        public string DataType { get; set; }

        public string Topic { get; set; }

        private RemoteEventTopicData()
        {
            Data = new object();
        }

        public RemoteEventTopicData(string type) : this()
        {
            DataType = type;
        }

        public RemoteEventTopicData(string type, object data) : this()
        {
            DataType = type;
            Data = data;
        }
    }
}
