/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Auditing;
using tsdn.Common.Core;
using tsdn.Common.Module;
using tsdn.Dependency;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace tsdn.Common.Micro
{
    /// <summary>
    /// 微服务注册
    /// </summary>
    public class MicroRegister : IAfterRunConfigure
    {
        private readonly IAppInfoProvider appInfo;

        private readonly ILogger logger;

        private readonly IIocManager iocManager;

        public MicroRegister(IAppInfoProvider appInfo, ILogger<MicroRegister> logger, IIocManager iocManager)
        {
            this.appInfo = appInfo;
            this.logger = logger;
            this.iocManager = iocManager;
        }

        public int Order
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Configure()
        {
            MicroRegisterInfo info = new MicroRegisterInfo();
            info.app = GetAppInfo();
            info.hosts = GetHostInfo();
            info.paths = GetApiInfo();

            string url = SysConfig.MicroServiceOption.Cloud.RegistCenterUri + "/micro/UDDI/Register";
            tsdnHttpRequest request = new tsdnHttpRequest();
            request.AddressUrl = url;
            request.Method = HttpMethod.Post;
            request.Body = info;
            bool result = request.SendAsync<bool>().Result;
            if (result)
            {
                logger.LogInformation($"向注册中心({SysConfig.MicroServiceOption.Cloud.RegistCenterUri})注册节点成功");
                if (SysConfig.MicroServiceOption.Type == RegisterType.IMS)
                {
                    var menuRegister = iocManager.Resolve<IMenuRegister>(RegisterType.IMS.ToString());
                    menuRegister.RegisterMenu();
                }
            }
        }

        /// <summary>
        /// 获取应用程序信息
        /// </summary>
        /// <returns></returns>
        private MicroAppInfo GetAppInfo()
        {
            var option = SysConfig.MicroServiceOption.Application;
            MicroAppInfo appInfo = new MicroAppInfo();
            appInfo.idtf = option.Name;
            appInfo.no = option.No;
            appInfo.title = option.Title;
            appInfo.summary = option.Remark;
            appInfo.version = option.Version;
            return appInfo;
        }

        /// <summary>
        /// 获取主机信息
        /// </summary>
        /// <returns>服务器主机信息</returns>
        private List<MicroHostInfo> GetHostInfo()
        {
            var option = SysConfig.MicroServiceOption.Application;
            List<MicroHostInfo> list = new List<MicroHostInfo>();
            MicroHostInfo hostInfo = null;
            foreach (string port in appInfo.Ports)
            {
                hostInfo = new MicroHostInfo();
                hostInfo.name = SysConfig.MicroServiceOption.Application.Name;
                hostInfo.host = $"{appInfo.IpAddress}:{port}";
                list.Add(hostInfo);
            }

            if (SysConfig.MicroServiceOption.ImsMenu != null)
            {
                foreach (string port in appInfo.Ports)
                {
                    hostInfo = new MicroHostInfo();
                    hostInfo.scheme = "menu";
                    hostInfo.name = SysConfig.MicroServiceOption.Application.Name;
                    hostInfo.host = $"{appInfo.IpAddress}:{port}";
                    list.Add(hostInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取系统api接口信息
        /// </summary>
        /// <returns></returns>
        private List<MicroApiInfo> GetApiInfo()
        {
            tsdnHttpRequest request = new tsdnHttpRequest();
            request.AddressUrl = $"http://{appInfo.IpAddress}:{appInfo.Ports[0]}/swagger/v1/swagger.json";
            var jobject = request.SendAsync<JObject>().Result;
            var Paths = jobject["paths"].ToObject<IDictionary<string, PathDetail>>();
            var list = new List<MicroApiInfo>();
            OperationDetail detail = null;
            string method = string.Empty;
            foreach (string path in Paths.Keys)
            {
                var item = Paths[path];
                detail = GetOperationDetail(item, out method);
                if (detail == null)
                {
                    continue;
                }
                list.Add(new MicroApiInfo
                {
                    scheme = "http",
                    path = path,
                    method = method,
                    tags = detail.Tags != null ? string.Join(",", detail.Tags) : "",
                    summary = detail.Summary
                });
            }
            if (SysConfig.MicroServiceOption.ImsMenu != null)
            {
                foreach (var item in SysConfig.MicroServiceOption.ImsMenu)
                {
                    if (string.IsNullOrEmpty(item.ParentCode) || string.IsNullOrEmpty(item.FunctionUrl))
                    {
                        continue;
                    }
                    list.Add(new MicroApiInfo
                    {
                        scheme = "menu",
                        path = "/" + SysConfig.MicroServiceOption.Application.Name + item.FunctionUrl,
                        feature = item.FunctionUrl,
                        tags = "",
                        summary = item.FunctionName
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private OperationDetail GetOperationDetail(PathDetail item, out string method)
        {
            if (item.Get != null)
            {
                method = "get";
                return item.Get;
            }
            if (item.Post != null)
            {
                method = "post";
                return item.Post;
            }
            if (item.Put != null)
            {
                method = "put";
                return item.Put;
            }
            if (item.Delete != null)
            {
                method = "delete";
                return item.Delete;
            }
            if (item.Patch != null)
            {
                method = "patch";
                return item.Patch;
            }
            if (item.Options != null)
            {
                method = "options";
                return item.Options;
            }
            if (item.Head != null)
            {
                method = "head";
                return item.Head;
            }
            method = "";
            return null;
        }
    }
}
