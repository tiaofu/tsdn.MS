/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: ef管理
 *********************************************************/
using System;
using System.Collections.Generic;

namespace tsdn.Common
{
    /// <summary>
    /// 类型管理
    /// </summary>
    public class TypeManager
    {
        /// <summary>
        /// 使用实例
        /// </summary>
        public static readonly TypeManager Instance = new TypeManager();

        private readonly ISet<Type> _typeSet = new HashSet<Type>();

        public ISet<Type> TypeSet
        {
            get
            {
                return _typeSet;
            }
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterType(Type type)
        {
            TypeSet.Add(type);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterType<T>() where T: class, new()
        {
            RegisterType(typeof(T));
        }
    }
}
