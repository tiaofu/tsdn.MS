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
    /// 缓存服务Redis配置信息
    /// </summary>
    [ConfigType(Group = "RedisConfig", GroupCn = "缓存服务Redis配置", ImmediateUpdate = false, FunctionType = "系统管理")]
    public class RedisConfig : ConfigOption
    {
        /// <summary>
        /// MQ主机IP
        /// </summary>
        [Config(Name = "主机地址", ValidateRule = "ipv4=true")]
        public static string Host { get; set; }

        /// <summary>
        /// MQ连接端口
        /// </summary>
        [Config(Name = "端口", DefaultValue = "6379", ValidateRule = "min=0 digits=true")]
        public static int Port { get; set; }

        [Config(Name = "redis服务地址", DefaultValue = "", Required = false)]
        public static string RedisClustorAddresses { get; set; }
    }
}