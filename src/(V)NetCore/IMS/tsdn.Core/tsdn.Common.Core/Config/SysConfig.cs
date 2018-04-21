/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Micro.IMS;
using System.Collections.Generic;

namespace tsdn.Common.Core
{
    /// <summary>
    /// 缓存系统所有配置信息，以键值对形式存在
    /// </summary>
    /// <example>
    /// 获取连接字符串 SysConfig.DefaultConnection
    /// </example>
    public class SysConfig
    {
        /// <summary>
        /// 数据库连接字符串信息
        /// </summary>
        public static string DefaultConnection { get; set; }

        /// <summary>
        /// 微服务配置信息
        /// </summary>
        public static MicroServiceOption MicroServiceOption { get; set; }

        /// <summary>
        /// 静态目录配置
        /// </summary>
        public static List<StaticFilesOptions> StaticDirectory { get; set; }
    }

    /// <summary>
    /// 静态资源浏览配置
    /// </summary>
    public class StaticFilesOptions
    {
        /// <summary>
        /// 以程序目录开始  磁盘相对路径
        /// </summary>
        public string PhysicalRelativePath { get; set; }

        /// <summary>
        /// url起始路径
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// 是否开启目录浏览
        /// </summary>
        public bool EnableDirectoryBrowsing { get; set; } = false;
    }

    /// <summary>
    /// 注册类型
    /// </summary>
    public enum RegisterType
    {
        /// <summary>
        /// 交警集成平台
        /// </summary>
        IMS = 0,

        /// <summary>
        /// 公安大数据平台
        /// </summary>
        XAI = 1
    }

    /// <summary>
    /// 微服务的配置信息
    /// </summary>
    public class MicroServiceOption
    {
        /// <summary>
        /// 注册类型 
        /// </summary>
        public RegisterType Type { get; set; }

        /// <summary>
        /// 代理配置
        /// </summary>
        public List<ProxyOption> Proxy { get; set; }

        /// <summary>
        /// 应用程序相关信息
        /// </summary>
        public MicroServiceApplicationOption Application { get; set; }

        /// <summary>
        /// 注册相关信息
        /// </summary>
        public MicroServiceCloudOption Cloud { get; set; }

        public List<MenuItem> ImsMenu { get; set; }
    }

    public class MicroServiceApplicationOption
    {
        /// <summary>
        /// 应用程序Code
        /// </summary>
        public string No
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// 微服务应用程序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 微服务标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 微服务描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 应用程序版本号
        /// </summary>
        public string Version { get; set; }
    }

    /// <summary>
    /// 代理规则
    /// </summary>
    public class ProxyOption
    {
        /// <summary>
        /// 代理url正则
        /// </summary>
        public string UrlReg { get; set; }

        /// <summary>
        /// 代理转发url
        /// </summary>
        public string ProxyPass { get; set; }

        /// <summary>
        /// 排除Url
        /// </summary>
        public string[] Excludes { get; set; }
    }

    /// <summary>
    /// 注册相关信息
    /// </summary>
    public class MicroServiceCloudOption
    {
        /// <summary>
        /// 注册中心节点
        /// </summary>
        public string RegistCenterUri { get; set; }
    }

}
