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
using Microsoft.AspNetCore.Hosting.Server.Features;
using tsdn.Core.Net;
using System.Runtime.InteropServices;

namespace tsdn.Auditing
{
    /// <summary>
    /// 应用程序和服务器信息
    /// </summary>
    public class DefaultAppInfoProvider : IAppInfoProvider, ISingletonDependency
    {
        private readonly IServerAddressesFeature serverAddressesFeature;

        public DefaultAppInfoProvider(IServerAddressesFeature _serverAddressesFeature)
        {
            serverAddressesFeature = _serverAddressesFeature;
        }

        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public string FrameWorkVersion
        {
            get
            {
                return RuntimeInformation.FrameworkDescription;
            }
        }

        public string ApplicationBasePath
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public string OSVersion
        {
            get
            {
                return RuntimeInformation.OSDescription;
            }
        }

        /// <summary>
        /// 操作系统位数
        /// </summary>
        public string OSBit
        {
            get
            {
                return RuntimeInformation.OSArchitecture.ToString();
            }
        }

        public string IpAddress
        {
            get
            {
                return HostHelper.GetIpAddressV4();
            }
        }

        public string[] Ports
        {
            get
            {
                var ports = new string[serverAddressesFeature.Addresses.Count];
                int i = 0;
                foreach (var item in serverAddressesFeature.Addresses)
                {
                    var arr = item.Split(':');
                    ports[i] = arr[arr.Length - 1];
                    i++;
                }
                return ports;
            }
        }
    }
}
