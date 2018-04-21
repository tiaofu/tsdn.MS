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

namespace tsdn.Events.Bus.Exceptions
{
    /// <summary>
    ///事件异常处理对象
    /// </summary>
    public class HandledExceptionData : ExceptionData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">异常对象</param>
        public HandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}