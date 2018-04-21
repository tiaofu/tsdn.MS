/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Collections.Generic;
using System.Reflection;

namespace tsdn.Reflection
{
    /// <summary>
    /// 程序集查找器接口
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// 返回应用程序使用的所有程序集
        /// </summary>
        /// <returns>程序集列表</returns>
        List<Assembly> GetAllAssemblies();
    }
}