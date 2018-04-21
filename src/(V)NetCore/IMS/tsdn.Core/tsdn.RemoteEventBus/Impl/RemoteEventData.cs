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
    public sealed class RemoteEventData : EventData, IRemoteEventData
    {
        public object Data { get; set; }

        public string DataType { get; set; }

        private RemoteEventData()
        {
            Data = new object();
        }

        public RemoteEventData(string type) : this()
        {
            DataType = type;
        }

        public RemoteEventData(string type, object data) : this()
        {
            DataType = type;
            Data = data;
        }
    }
}
