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
using System.Linq;
using System.Threading.Tasks;

namespace tsdn.Common.Micro
{
    /// <summary>
    /// PathDetail
    /// </summary>
    public class PathDetail
    {
        /// <summary>
        /// Put
        /// </summary>
        public OperationDetail Get { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OperationDetail Put { get; set; }
        /// <summary>
        /// Post
        /// </summary>
        public OperationDetail Post { get; set; }
        /// <summary>
        /// Delete
        /// </summary>
        public OperationDetail Delete { get; set; }
        /// <summary>
        /// Options
        /// </summary>
        public OperationDetail Options { get; set; }
        /// <summary>
        /// Head
        /// </summary>
        public OperationDetail Head { get; set; }
        /// <summary>
        /// Patch
        /// </summary>
        public OperationDetail Patch { get; set; }
    }
}
