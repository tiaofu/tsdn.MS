/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tsdn.RemoteEventBus
{
    public static class ConfigurationExtensions
    {
        public static IApplicationBuilder UseRemoteEventBus(this IApplicationBuilder app, Action<IRemoteEventBusConfiguration> action)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IRemoteEventBusConfiguration>();
            action(configuration);
            return app;
        }

        public static IRemoteEventBusConfiguration AutoSubscribe(this IRemoteEventBusConfiguration configuration)
        {
            var topics = new List<string>();
            configuration.TypeFinder.Find(type => Attribute.IsDefined(type, typeof(RemoteEventHandlerAttribute), false) && typeof(IRemoteEventHandler).IsAssignableFrom(type))
            .ToList().ForEach(type =>
                {
                    var attribute = Attribute.GetCustomAttribute(type, typeof(RemoteEventHandlerAttribute)) as RemoteEventHandlerAttribute;
                    if (!string.IsNullOrWhiteSpace(attribute.ForTopic) && !topics.Contains(attribute.ForTopic))
                    {
                        topics.Add(attribute.ForTopic);
                    }
                });
            var _remoteEventBus = configuration.IocManger.Resolve<IRemoteEventBus>();
            _remoteEventBus.Subscribe(topics);
            return configuration;
        }
    }
}
