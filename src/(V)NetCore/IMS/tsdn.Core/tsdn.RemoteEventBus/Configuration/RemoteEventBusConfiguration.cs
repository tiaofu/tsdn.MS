/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Reflection;
using tsdn.Dependency;

namespace tsdn.RemoteEventBus.RabbitMQ
{
    /// <summary>
    /// 事件总线初始化配置
    /// </summary>
    public class RemoteEventBusConfiguration : IRemoteEventBusConfiguration
    {
        private readonly IIocManager _iocManger;
        private readonly ITypeFinder _typeFinder; 

        public RemoteEventBusConfiguration(IIocManager iocManger, ITypeFinder typeFinder)
        {
            _iocManger = iocManger;
            _typeFinder = typeFinder;
        }

        public virtual IIocManager IocManger
        {
            get
            {
                return _iocManger;
            }
        }

        public virtual ITypeFinder TypeFinder
        {
            get
            {
                return _typeFinder;
            }
        }
    }
}
