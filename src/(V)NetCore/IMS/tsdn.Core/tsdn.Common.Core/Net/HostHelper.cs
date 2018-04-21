/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Net;
using System.Net.Sockets;

namespace tsdn.Core.Net
{
    /// <summary>
    /// 服务器信息读取
    /// </summary>
    public class HostHelper
    {
        /// <summary>
        /// 获取本机的IPV4地址
        /// </summary>
        /// <returns>本机的IPV4地址</returns>
        public static string GetIpAddressV4()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            }
            return "127.0.0.1";
        }
    }
}
