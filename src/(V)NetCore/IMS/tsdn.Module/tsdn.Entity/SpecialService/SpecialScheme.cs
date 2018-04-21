using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
	///<summary>
	///特勤方案信息表
	///</summary>
	[Table("SS_SPECIALSCHEME")]
	public class  SpecialScheme
    {
	///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("SSID", TypeName = "VARCHAR2")]
        public string SSID { get; set; }
	      
		///<summary>
	    ///特勤开始时间 
	    ///</summary> 
	    [Column("EFFECTSTARTTIME", TypeName = "DATE")]
        public DateTime?  EffectStarttime { get; set; }
	      
		///<summary>
	    ///特勤结束时间 
	    ///</summary> 
	    [Column("EFFECTENDTIME", TypeName = "DATE")]
        public DateTime?  EffectEndtime { get; set; }
	      
		///<summary>
	    ///特勤等级 
	    ///</summary> 
	    [Column("LEVEL", TypeName = "VARCHAR2")]
        public string Level { get; set; }
	      
		///<summary>
	    ///组织说明 
	    ///</summary> 
	    [Column("ORGANIZATIONDES", TypeName = "VARCHAR2")]
        public string OrganizationDes { get; set; }
	      
		///<summary>
	    ///领导组组长 
	    ///</summary> 
	    [Column("GROUPLEADER", TypeName = "VARCHAR2")]
        public string GroupLeader { get; set; }
	      
		///<summary>
	    ///副组长 
	    ///</summary> 
	    [Column("DEPUTYLEADER", TypeName = "VARCHAR2")]
        public string DeputyLeader { get; set; }
	      
		///<summary>
	    ///领导组成员 
	    ///</summary> 
	    [Column("GROUPMEMBERS", TypeName = "VARCHAR2")]
        public string GroupMembers { get; set; }
	      
		///<summary>
	    ///联系人 
	    ///</summary> 
	    [Column("LIAISON", TypeName = "VARCHAR2")]
        public string Liaison { get; set; }
	      
		///<summary>
	    ///车队出发时间 
	    ///</summary> 
	    [Column("TEAMDEPARTURETIME", TypeName = "DATE")]
        public DateTime?  TeamDepartureTime { get; set; }
	      
		///<summary>
	    ///车队到达时间 
	    ///</summary> 
	    [Column("TEAMARRIVETIME", TypeName = "DATE")]
        public DateTime?  TeamArriveTime { get; set; }
	      
		///<summary>
	    ///警力上岗时间 
	    ///</summary> 
	    [Column("ONWORKDATE", TypeName = "DATE")]
        public DateTime?  OnWorkDate { get; set; }
	      
		///<summary>
	    ///是否删除 1删除 0未删除 
	    ///</summary> 
	    [Column("ISDEL", TypeName = "CHAR")]
        public bool IsDdel { get; set; }
	      
		///<summary>
	    ///状态 未执行/执行中/完结 
	    ///</summary> 
	    [Column("STATUS", TypeName = "CHAR")]
        public string Status { get; set; }
	      
		///<summary>
	    ///创建时间 
	    ///</summary> 
	    [Column("CREATETIME", TypeName = "DATE")]
        public DateTime?  CreateTime { get; set; }
	      
		///<summary>
	    ///创建人 
	    ///</summary> 
	    [Column("CREATER", TypeName = "VARCHAR2")]
        public string Creater { get; set; }
	      
		///<summary>
	    ///更新人 
	    ///</summary> 
	    [Column("UPDATER", TypeName = "VARCHAR2")]
        public string Updater { get; set; }
	      
		///<summary>
	    ///更新时间 
	    ///</summary> 
	    [Column("UPDATETIME", TypeName = "DATE")]
        public DateTime?  UpdateTime { get; set; }
	      
		///<summary>
	    ///备注 
	    ///</summary> 
	    [Column("REMARK", TypeName = "VARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

