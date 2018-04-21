/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace tsdn.RemoteEventBus.Impl
{
    /// <summary>
    /// 基于Newtonsoft.Json的序列化实现
    /// </summary>
    public class JsonRemoteEventSerializer : IRemoteEventSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JsonRemoteEventSerializer()
        {
            settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver(),
            };
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}