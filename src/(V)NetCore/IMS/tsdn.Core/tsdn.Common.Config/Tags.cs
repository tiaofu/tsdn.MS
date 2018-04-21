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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Common.Config
{
    ///<summary>
    ///标签定义表
    ///</summary>
    [Table("COMMON_TAGS")]
    public class Tags
    {
        ///<summary>
        ///标签ID 
        ///</summary> 
        [Key, Column("TAGGUID", TypeName = "VARCHAR2")]
        public string TagGUID { get; set; }

        ///<summary>
        ///标签类型 
        ///</summary> 
        [Column("TAGTYPE", TypeName = "VARCHAR2"), Required]
        public string TagType { get; set; }

        ///<summary>
        ///关联表主键 
        ///</summary> 
        [Column("SOURCEID", TypeName = "VARCHAR2"), Required]
        public string SourceId { get; set; }

        ///<summary>
        ///标签名称 层级标签通过/进行分割(名称不能含有特殊字符) 
        ///</summary> 
        [Column("TAGNAME", TypeName = "NVARCHAR2"), Required]
        public string TagName { get; set; }

        ///<summary>
        ///标签热度 
        ///</summary> 
        [Column("TAGHEAT", TypeName = "NUMBER")]
        public int? TagHeat { get; set; }

        ///<summary>
        ///创建日期 
        ///</summary> 
        [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime? CreatedTime { get; set; }

        ///<summary>
        ///创建人(用户代码) 
        ///</summary> 
        [Column("CREATOR", TypeName = "VARCHAR2")]
        public string Creator { get; set; }

        ///<summary>
        ///修改日期 
        ///</summary> 
        [Column("MODIFIEDTIME", TypeName = "DATE")]
        public DateTime? ModifiedTime { get; set; }

        ///<summary>
        ///修改人(用户代码) 
        ///</summary> 
        [Column("MODIFIER", TypeName = "VARCHAR2")]
        public string Modifier { get; set; }
    }
}
