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
using System.Xml.Serialization;
using System.ComponentModel;

namespace tsdn.Common.Utility
{
    /// <summary>
    /// 表示*.config文件中的一个XmlCommand配置项。
    /// </summary>
    [XmlType("XmlCommand")]
    [Serializable]
    public class XmlCommandItem
    {
        /// <summary>
        /// 命令的名字，这个名字将在XmlCommand.From时被使用。
        /// </summary>
        [XmlAttribute("Name")]
        public string CommandName;

        /// <summary>
        /// 命令所引用的所有参数集合
        /// </summary>
        [XmlArrayItem("Parameter")]
        public List<XmlCmdParameter> Parameters = new List<XmlCmdParameter>();

        /// <summary>
        /// 命令的文本。是一段可运行的SQL脚本或存储过程名称。
        /// </summary>
        [XmlElement]
        public MyCDATA CommandText;

        /// <summary>
        /// 获取或设置在终止执行命令的尝试并生成错误之前的等待时间。 
        /// </summary>
        [DefaultValue(30)]
        [XmlAttribute]
        public int Timeout = 30;
    }

    /// <summary>
    /// XmlCommand的命令参数。
    /// </summary>
    [Serializable]
    public class XmlCmdParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [XmlAttribute]
        public string Name;

        /// <summary>
        /// 参数值的长度。
        /// </summary>
        [DefaultValue(0)]
        [XmlAttribute]
        public int Size;
    }
}
