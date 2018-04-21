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
using Autofac;
using tsdn.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using tsdn.Auditing;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using tsdn.Common.Core;
using System.Net;

namespace tsdn.Common.Module
{
    /// <summary>
    /// http服务启动后配置
    /// </summary>
    public class AfterRunConfigureModule : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return int.MaxValue;
            }
        }

        public void Configure()
        {
            IIocManager iocManager = IocManager.Instance;
            IAppInfoProvider appInfo = iocManager.Resolve<IAppInfoProvider>();
            ILogger logger = iocManager.Resolve<ILogger<AfterRunConfigureModule>>();
            IEnumerable<IAfterRunConfigure> configures = iocManager.Resolve<IEnumerable<IAfterRunConfigure>>();
            Task.Factory.StartNew(() =>
            {
                tsdnHttpRequest request = new tsdnHttpRequest();
                string name = SysConfig.MicroServiceOption.Application.Name;
                request.AddressUrl = $"http://{appInfo.IpAddress}:{appInfo.Ports[0]}/api/{name}/Health/ApiServer";
                int i = 0;
                while (true)
                {
                    try
                    {
                        var responeMessage = request.SendAsync().Result;
                        if (responeMessage.StatusCode == HttpStatusCode.OK)
                        {
                            if (configures != null && configures.Count() > 0)
                            {
                                foreach (var item in configures.OrderBy(e => e.Order))
                                {
                                    try
                                    {
                                        item.Configure();
                                    }
                                    catch (Exception e)
                                    {
                                        logger.LogError(e.InnerException != null ? e.InnerException : e, $"调用类型{item.GetType().ToString()}的Configure方法异常");
                                    }
                                }
                            }
                            break;
                        }
                    }
                    catch { }
                    if (i > 30)
                    {
                        logger.LogError("程序启动异常，30s内http服务还未启动成功");
                        break;
                    }
                    i++;
                    Task.Delay(100).Wait();
                }
            });
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<AfterRunConfigureModule>().AsSelf().SingleInstance();
            var baseType = typeof(IAfterRunConfigure);
            builder.RegisterTypes(typeFinder.Find(
                t => baseType.IsAssignableFrom(t) && t != baseType)).AsImplementedInterfaces();
        }
    }
}
