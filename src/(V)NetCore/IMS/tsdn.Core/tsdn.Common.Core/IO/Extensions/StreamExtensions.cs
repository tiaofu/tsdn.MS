/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.IO;

namespace tsdn.IO.Extensions
{
    /// <summary>
    /// IO流拓展
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 将IO流转换成Byte数组
        /// </summary>
        /// <param name="stream">IO流</param>
        /// <returns>Byte数组</returns>
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
