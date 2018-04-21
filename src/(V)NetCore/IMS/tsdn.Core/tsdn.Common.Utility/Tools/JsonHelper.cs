/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: json帮助
 *********************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace tsdn.Utility
{
    /// <summary>
    /// json 帮助类
    /// </summary>
    public static class JsonHelper
    {
        public static JToken ToJson(this string input)
        {
            using (TextReader tr = new StringReader(input))
            {
                return tr.ToJson();
            }
        }

        public static JToken ToJson(this TextReader tr)
        {
            using (JsonReader jr = new JsonTextReader(tr))
            {
                JToken jt = (JToken)Newtonsoft.Json.JsonSerializer.Create().Deserialize(jr);
                return jt;
            }
        }

        public static T ToJson<T>(this string input)
        {
            return input.ToJson().ToObject<T>();
        }

        public static T ToJson<T>(this TextReader tr)
        {
            return tr.ToJson().ToObject<T>();
        }

        /// <summary>
        /// 将.net对象转为json格式的字符串 （Json.net开源库）
        /// zhaoxh
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="dateTimeFormatString">可选参数，datetime格式占位符，yyyy年MM月dd日 或 yyyy-MM-dd HH:mm:ss 等</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string dateTimeFormatString = "yyyy-MM-dd HH:mm:ss")
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;//
            settings.Converters = new List<JsonConverter>() { new IsoDateTimeConverter() { DateTimeFormat = dateTimeFormatString } };
            string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
            return json;
        }

        public static string ToJson(this object obj, bool IgnoreNullValue, string dateTimeFormatString = "yyyy-MM-dd HH:mm:ss")
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = IgnoreNullValue?NullValueHandling.Ignore: NullValueHandling.Include;
            settings.Converters = new List<JsonConverter>() { new IsoDateTimeConverter() { DateTimeFormat = dateTimeFormatString } };
            string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
            return json;
        }

        /// <summary>
        ///  DataContractJsonSerializer 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeserializeFromString<T>(string str) where T : class
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                MemoryStream ms = new MemoryStream(buffer, 0, buffer.Length);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return ser.ReadObject(ms) as T;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}