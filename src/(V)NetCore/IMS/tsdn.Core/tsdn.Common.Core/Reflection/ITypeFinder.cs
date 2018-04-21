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

namespace tsdn.Reflection
{
    /// <summary>
    /// 类型查找接口
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 查找指定类型
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        Type[] Find(Func<Type, bool> predicate);

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        Type[] FindAll();
    }
}