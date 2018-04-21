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

namespace tsdn.Utility.Excel
{
    /// <summary>
    /// 定义调用各业务接口返回结果的格式
    /// </summary>
    public class ExcelApiResult
    {
        /// <summary>
        /// 执行是否成功：true false
        /// </summary>
        /// <remarks></remarks>
        public bool HasError { get; set; }

   
        /// <summary>
        /// 执行返回消息
        /// </summary>
        /// <remarks></remarks>
        public string Message { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 返回的主要内容.
        /// </summary>
        public List<Dictionary<string, object>> Result { get; set; }
    }
}
