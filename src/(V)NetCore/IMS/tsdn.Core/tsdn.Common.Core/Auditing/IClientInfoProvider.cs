/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Auditing
{
    /// <summary>
    /// 客户端信息接口
    /// </summary>
    public interface IClientInfoProvider
    {
        /// <summary>
        /// 浏览器信息
        /// </summary>
        UserAgentInfo BrowserInfo { get; }

        /// <summary>
        /// 客户端Ip地址
        /// </summary>
        string ClientIpAddress { get; }
    }
}