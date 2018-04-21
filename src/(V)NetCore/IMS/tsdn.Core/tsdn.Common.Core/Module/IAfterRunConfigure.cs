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

namespace tsdn.Common.Module
{
    /// <summary>
    /// 服务启动后注册的服务
    /// </summary>
    public interface IAfterRunConfigure
    {
        int Order { get; }

        void Configure();
    }
}
