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
using tsdn.Reflection;

namespace tsdn.RemoteEventBus
{
    /// <summary>
    /// 事件总线初始化配置接口
    /// </summary>
    public interface IRemoteEventBusConfiguration : ISingletonDependency
    {
        /// <summary>
        /// Ioc容器解析器
        /// </summary>
        IIocManager IocManger { get; }

        /// <summary>
        /// 类型查找
        /// </summary>
        ITypeFinder TypeFinder { get; }
    }
}