﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.IO;

namespace System.Net.Http
{
    public struct FileParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数的名称</param>
        /// <param name="fileStream">调用方会自动释放</param>
        /// <param name="filename"></param>
        /// <param name="contentType"></param>
        public FileParameter(string name, Stream fileStream, string filename, string contentType)
        {
            Writer = s =>
            {
                var buffer = new byte[1024];
                using (fileStream)
                {
                    int readCount;
                    while ((readCount = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        s.Write(buffer, 0, readCount);
                    }
                }
            };
            FileName = filename;
            ContentType = contentType;
            Name = name;
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 读写操作流
        ///  返回的是写入的字节流长度
        /// </summary>
        public Action<Stream> Writer;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName;
        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType;
    }
}
