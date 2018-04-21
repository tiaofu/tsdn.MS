/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.RemoteEventBus.RabbitMQ
{
    /// <summary>
    /// MQ配置信息
    /// </summary>
    public class RabbitMQSetting
    {
        /// <summary>
        /// mq连接url
        /// 格式：amqp://username:password@ip:port/
        /// amqp://admin:admin@127.0.0.1:5672/
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 自动还原
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; }

        /// <summary>
        /// 最初容量
        /// </summary>
        public int InitialSize { get; set; }

        /// <summary>
        /// 连接池最大数
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 交换器exchange名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 队列前缀 建议以程序名称ip端口命名 eg:tsdn.Schedule.192.168.0.1:92
        /// </summary>
        public string QueuePrefix { get; set; }

        /// <summary>
        /// 队列是否持久化
        /// </summary>
        public bool Durable { get; set; }

        public RabbitMQSetting()
        {
            AutomaticRecoveryEnabled = true;
            InitialSize = 0;
            MaxSize = 10;
            Durable = true;
            ExchangeName = "tsdn.Exchange.RemoteEventBus";
            QueuePrefix = "tsdn.RemoteEventBus.Queue.";
        }
    }
}
