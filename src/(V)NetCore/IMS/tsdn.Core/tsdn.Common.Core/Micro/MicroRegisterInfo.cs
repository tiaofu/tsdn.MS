/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace tsdn.Common.Micro
{
    /// <summary>
    /// 注册节点信息
    /// </summary>
    internal class MicroRegisterInfo
    {
        /// <summary>
        /// App描述文档
        /// </summary>
        public MicroAppInfo app { get; set; }

        /// <summary>
        /// 主机列表
        /// </summary>
        public List<MicroHostInfo> hosts { get; set; }

        /// <summary>
        /// 接口列表
        /// </summary>
        public List<MicroApiInfo> paths { get; set; }
    }

    /// <summary>
    /// App应用程序信息
    /// </summary>
    internal class MicroAppInfo
    {
        /// <summary>
        /// 运行实例标示
        /// </summary>
        public string idtf { get; set; }

        /// <summary>
        ///  当前应用的编号，注册应用时分配的
        /// </summary>
        public string no { get; set; }

        /// <summary>
        /// 当前应用的标题，应和注册应用时的标题一致
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 应用版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 标签，多个标签用逗号分隔
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// 目标对象的描述信息
        /// </summary>
        public string summary { get; set; }
    }

    /// <summary>
    /// 虚拟主机描述，当前应用所在主机的地址和端口,需要保证能够被注册中心访问
    /// </summary>
    internal class MicroHostInfo
    {
        /// <summary>
        /// 主机名称，可以重复，名称相同的主机，自动构成集群
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 主机定位串，根据 scheme 不同，有不同的解释，目前仅仅支持 {IP}[:{Port}] 形式的主机
        /// </summary>
        public string host { get; set; }

        /// <summary>
        /// 支持的协议
        /// </summary>
        public string scheme { get; set; }

        /// <summary>
        /// 目标对象的描述信息
        /// </summary>
        public string summary { get; set; }

        public MicroHostInfo()
        {
            scheme = "http";
        }
    }

    /// <summary>
    /// 服务提供的接口信息
    /// </summary>
    public class MicroApiInfo
    {
        /// <summary>
        /// 支持的协议
        /// </summary>
        public string scheme { get; set; }

        /// <summary>
        /// 标示服务的唯一路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 服务的实现版本
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 服务的特征码，应该根据实现来计算，相同的实现应该有相同的特征码。 相同的 path 应该具有相同的实现。因此，相同的 path 应该有相同的特征码
        /// </summary>
        public string feature { get; set; }

        /// <summary>
        /// 标签，多个标签用逗号分隔
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// 目标对象的描述信息
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 支持方法 get,post..
        /// </summary>
        public string method { get; set; }
    }
}
