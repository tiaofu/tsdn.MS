/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Funcular.DotNetCore.IdGenerators;
using System;

namespace tsdn.Utility
{
    /// <summary>
    /// 唯一Id生成类
    /// </summary>
    public class GuidHelper
    {
        /// <summary>
        /// 分布式顺序唯一Id生成器
        /// </summary>
        private static Base36IdGenerator _generator = new Base36IdGenerator(11, 4, 5, null, "-", new[] { 15, 10, 5 });

        /// <summary>
        /// 生成顺序Id
        /// </summary>
        /// <returns>顺序Id</returns>
        public static string GetSeqGUID()
        {
            return _generator.NewId();
        }

        /// <summary>
        /// 获取空GUID值
        /// </summary>
        /// <returns>GUID</returns>
        public static string GetEmptyGUID()
        {
            return Guid.Empty.ToString("N");
        }
    }
}
