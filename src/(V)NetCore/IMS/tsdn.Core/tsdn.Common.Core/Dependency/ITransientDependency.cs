/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
namespace tsdn.Dependency
{
    /// <summary>
    /// 所有接口的依赖接口，每次创建新实例
    /// </summary>
    /// <remarks>
    /// 用于Autofac自动注册时，查找所有依赖该接口的实现。
    /// 实现自动注册功能
    /// </remarks>
    public interface ITransientDependency
    {
    }
}
