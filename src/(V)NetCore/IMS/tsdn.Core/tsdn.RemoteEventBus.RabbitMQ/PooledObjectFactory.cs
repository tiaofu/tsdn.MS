/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Commons.Pool;
using RabbitMQ.Client;
using System;

namespace tsdn.RemoteEventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ连接池
    /// </summary>
    public class PooledObjectFactory : IPooledObjectFactory<IConnection>
    {
        private ConnectionFactory _connectionFactory;

        public PooledObjectFactory(RabbitMQSetting rabbitMQSetting)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMQSetting.Url),
                AutomaticRecoveryEnabled = true
            };
        }

        public IConnection Create()
        {
            return _connectionFactory.CreateConnection();
        }

        public void Destroy(IConnection obj)
        {
            obj.Dispose();
        }
    }
}
