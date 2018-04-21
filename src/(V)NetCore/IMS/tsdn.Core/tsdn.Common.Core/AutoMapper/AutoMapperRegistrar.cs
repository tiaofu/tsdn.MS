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
using AutoMapper;
using tsdn.Dependency;
using tsdn.Reflection;
using System;
using System.Reflection;

namespace tsdn.AutoMapper
{
    /// <summary>
    /// 注册AutoMapper实例
    /// </summary>
    public class AutoMapperRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            Action<IMapperConfigurationExpression> configurer = configuration =>
            {
                //初始化注册所有类型
                var types = typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsDefined(typeof(MapsFromAttribute)) ||
                           typeInfo.IsDefined(typeof(MapsToAttribute));
                });
                foreach (var type in types)
                {
                    foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
                    {
                        autoMapAttribute.CreateMap(configuration, type);
                    }
                }
            };
            Mapper.Initialize(configurer);
            builder.RegisterInstance(Mapper.Instance).As<IMapper>().SingleInstance();
        }
    }
}
