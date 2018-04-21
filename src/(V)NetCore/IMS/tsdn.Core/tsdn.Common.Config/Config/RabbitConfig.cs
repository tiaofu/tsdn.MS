/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Common.Config
{
    /// <summary>
    /// MQ消息队列配置信息
    /// </summary>
    [ConfigType(Group = "RabbitMQConfig", GroupCn = "消息队列配置", ImmediateUpdate = false, FunctionType = "系统管理")]
    public class RabbitMQConfig : ConfigOption
    {
        /// <summary>
        /// 交换器名称
        /// </summary>
        [Config(Name = "交换器名称", DefaultValue = "tsdn.Exchange.TianjinEPP")]
        public static string ExchangeName { get; set; }

        /// <summary>
        /// MQ主机IP
        /// </summary>
        [Config(Name = "主机地址", ValidateRule = "ipv4=true")]
        public static string Host { get; set; }

        /// <summary>
        /// MQ连接用户名
        /// </summary>
        [Config(Name = "用户名")]
        public static string UserName { get; set; }

        /// <summary>
        /// MQ连接密码
        /// </summary>
        [Config(Name = "密码", ValueType = ConfigValueType.Password)]
        public static string Password { get; set; }

        /// <summary>
        /// MQ连接端口
        /// </summary>
        [Config(Name = "端口", DefaultValue = "5672", ValidateRule = "min=0 digits=true")]
        public static int Port { get; set; }

        /// <summary>
        /// 队列是否持久化
        /// </summary>
        [Config(Name = "是否持久化", DefaultValue = true)]
        public static bool Durable { get; set; }

    }
}