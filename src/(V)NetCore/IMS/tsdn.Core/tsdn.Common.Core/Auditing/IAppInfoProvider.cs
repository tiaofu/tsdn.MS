﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Auditing
{
    /// <summary>
    /// 应用程序和服务器信息接口
    /// </summary>
    public interface IAppInfoProvider
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        string MachineName { get; }

        /// <summary>
        /// NET框架版本
        /// </summary>
        string FrameWorkVersion { get; }

        /// <summary>
        /// 应用程序根目录
        /// </summary>
        string ApplicationBasePath { get; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        string OSVersion { get; }

        /// <summary>
        /// 操作系统位数
        /// </summary>
        string OSBit { get; }

        /// <summary>
        /// 客户端Ip地址
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// 应用程序绑定端口
        /// </summary>
        string[] Ports { get; }
    }
}