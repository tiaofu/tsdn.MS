/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using tsdn.RemoteEventBus.Exceptions;
using tsdn.Events.Bus.Handlers;
using Microsoft.Extensions.Logging;
using tsdn.Reflection;
using tsdn.Events.Bus;
using tsdn.Dependency;

namespace tsdn.RemoteEventBus.Impl
{
    public class AttributeRemoteEventHandler : IEventHandler<RemoteEventArgs>, ISingletonDependency
    {
        private readonly ILogger<AttributeRemoteEventHandler> _logger;
        private readonly ITypeFinder _typeFinder;
        private readonly IIocManager _iocManager;
        private readonly Dictionary<string, List<Tuple<Type, RemoteEventHandlerAttribute>>> _typeMapping;

        public IEventBus _eventBus { get; set; }

        public AttributeRemoteEventHandler(ITypeFinder typeFinder, IEventBus eventBus,
            ILogger<AttributeRemoteEventHandler> logger, IIocManager iocManager)
        {
            _logger = logger;
            _typeFinder = typeFinder;
            _eventBus = eventBus;
            _iocManager = iocManager;

            _typeMapping = new Dictionary<string, List<Tuple<Type, RemoteEventHandlerAttribute>>>();

            _typeFinder.Find(type => Attribute.IsDefined(type, typeof(RemoteEventHandlerAttribute), false) && typeof(IRemoteEventHandler).IsAssignableFrom(type))
                .ToList().ForEach(type =>
                {
                    var attribute = Attribute.GetCustomAttribute(type, typeof(RemoteEventHandlerAttribute)) as RemoteEventHandlerAttribute;
                    var key = attribute.ForType;
                    var item = new Tuple<Type, RemoteEventHandlerAttribute>(type, attribute);
                    if (_typeMapping.ContainsKey(key))
                    {
                        var list = _typeMapping[key];
                        list.Add(item);
                    }
                    else
                    {
                        _typeMapping.Add(key, new List<Tuple<Type, RemoteEventHandlerAttribute>>(new[] { item }));
                    }
                });
        }

        public void HandleEvent(RemoteEventArgs eventArgs)
        {
            var key = eventArgs.EventData.DataType;
            if (_typeMapping.ContainsKey(key))
            {
                var tuples = _typeMapping[key].OrderBy(p => p.Item2.Order).ToList();
                foreach (var tuple in tuples)
                {
                    if (tuple.Item2.OnlyHandleThisTopic && eventArgs.Topic != tuple.Item2.ForTopic)
                    {
                        continue;
                    }

                    try
                    {
                        var handler = (IRemoteEventHandler)_iocManager.Resolve(tuple.Item1);
                        handler.HandleEvent(eventArgs);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception occurred when handle remoteEventArgs", ex);

                        _eventBus.Trigger(this, new RemoteEventHandleExceptionData(ex, eventArgs));

                        if (tuple.Item2.SuspendWhenException)
                        {
                            eventArgs.Suspended = true;
                        }
                    }

                    if (eventArgs.Suspended)
                    {
                        break;
                    }
                }
            }
        }
    }
}
