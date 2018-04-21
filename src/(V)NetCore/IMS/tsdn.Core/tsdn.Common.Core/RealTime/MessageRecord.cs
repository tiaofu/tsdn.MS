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

namespace tsdn.RealTime
{
    /// <summary>
    /// 消息记录格式
    /// </summary>
    public class MessageRecord
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageRecord()
        {
            PushTime = DateTime.Now;
        }

        public DateTime PushTime { get; set; }
        public string MsgType { get; set; }
        public object Message { get; set; }
    }
}
