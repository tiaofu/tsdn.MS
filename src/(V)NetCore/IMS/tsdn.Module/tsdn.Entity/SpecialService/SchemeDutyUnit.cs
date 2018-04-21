using tsdn.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
	///<summary>
	///方案值勤组关系表
	///</summary>
	[Table("SS_SCHEMEDUTYUNIT")]
    [MapsTo(typeof(SchemeDutyUnitPostViewModel))]
	public class SchemeDutyUnit
    {
	    ///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("SDID", TypeName = "VARCHAR2")]
        public string SDID { get; set; }

        ///<summary>
        ///方案信息 
        ///</summary> 
        [Column("SSID", TypeName = "VARCHAR2")]
        public string SSID { get; set; }

        ///<summary>
        ///组类型， 对应数据字典值 
        ///</summary> 
        [Column("GROUPTYPE", TypeName = "VARCHAR2")]
        public string GroupType { get; set; }
	      
		///<summary>
	    ///负责人 
	    ///</summary> 
	    [Column("LEADER", TypeName = "VARCHAR2")]
        public string Leader { get; set; }
	      
		///<summary>
	    ///职责信息 
	    ///</summary> 
	    [Column("TASKINFO", TypeName = "VARCHAR2")]
        public string TaskInfo { get; set; }
	      
		///<summary>
	    /// Remark
		///</summary> 
	    [Column("REMARK", TypeName = "VARCHAR2")]
        public string Remark { get; set; }
	      
	}

    /// <summary>
    /// 方案字组与岗位信息视图
    /// </summary>
    [MapsFrom(typeof(SchemeDutyUnit))]
    public class SchemeDutyUnitPostViewModel : SchemeDutyUnit
    {
        /// <summary>
        /// 岗位信息
        /// </summary>
        public List<Post> Posts;
    }
}

