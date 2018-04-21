/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Autofac;
using System;

namespace tsdn.RemoteEventBus.RabbitMQ
{
    public static class ConfigurationExtensions
    {
        public static IRemoteEventBusConfiguration UseRabbitMQ(this IRemoteEventBusConfiguration configuration, Action<RabbitMQSetting> configureAction)
        {
            var setting = new RabbitMQSetting();
            configureAction(setting);
            configuration.UseRabbitMQ(setting);
            return configuration;
        }

        public static IRemoteEventBusConfiguration UseRabbitMQ(this IRemoteEventBusConfiguration configuration, RabbitMQSetting setting)
        {
            var settingParam = new TypedParameter(typeof(RabbitMQSetting), setting);
            configuration.IocManger.Resolve<IRemoteEventPublisher>(settingParam);
            configuration.IocManger.Resolve<IRemoteEventSubscriber>(settingParam);
            return configuration;
        }
    }
}