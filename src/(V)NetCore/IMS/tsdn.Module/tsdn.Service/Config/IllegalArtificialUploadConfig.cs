/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Config;

namespace tsdn.ConfigHandler
{
    /// <summary>
    /// 违法数据骤减参数配置
    /// </summary>


    [ConfigType(Group = "IllegalArtificialUploadConfig", GroupCn = "违法人工录入配置", ImmediateUpdate = true, FunctionType = "交通违法")]
    public class IllegalArtificialUploadConfig : ConfigOption
    {

        /// <summary>
        /// ftp地址
        /// </summary>
        [Config(Name = "Ftp地址", Required = true, Title = "人工录入Ftp地址")]
        public static string FtpAddress { get; set; }

        /// <summary>
        /// ftp用户
        /// </summary>
        [Config(Name = "Ftp用户", Required = true, Title = "Ftp用户")]
        public static string FtpUser { get; set; }

        /// <summary>
        /// ftp密码
        /// </summary>
        [Config(Name = "Ftp密码", Required = true, Title = "ftp密码")]
        public static string FtpPwd { get; set; }

        /// <summary>
        /// 文件存储目录
        /// </summary>
        [Config(Name = "文件存储目录", DefaultValue = "", Required = false, Title = "文件存储目录")]
        public static string FtpDri { get; set; }

        /// <summary>
        /// Http图片映射地址
        /// </summary>
        [Config(Name = "Http图片映射地址", DefaultValue = "", Required = true, Title = "Http图片映射地址,eg:http://ip:port")]
        public static string HttpAddress { get; set; }

    }
}