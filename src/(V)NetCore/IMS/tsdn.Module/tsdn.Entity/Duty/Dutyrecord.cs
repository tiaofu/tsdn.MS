using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
	///<summary>
	///考核记录信息表
	///</summary>
	[Table("DUTY_DUTYRECORD")]
	public class  DutyRecord
    {
	///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("DUTYRECORDID", TypeName = "VARCHAR2")]
        public string DutyRecordId { get; set; }
	      
		///<summary>
	    ///岗位ID 
	    ///</summary> 
	    [Column("POSTID", TypeName = "VARCHAR2")]
        public string PostId { get; set; }
	      
		///<summary>
	    ///警员ID 
	    ///</summary> 
	    [Column("POLICEMANID", TypeName = "VARCHAR2")]
        public string PolicemanId { get; set; }
	      
		///<summary>
	    ///考核时间 
	    ///</summary> 
	    [Column("CHECKTIME", TypeName = "DATE")]
        public DateTime?  CheckTime { get; set; }
	      
		///<summary>
	    ///状态 是否到岗 1 到岗 2 未到岗 3 离岗 
	    ///</summary> 
	    [Column("STATUS", TypeName = "CHAR")]
        public string Status { get; set; }
	      
		///<summary>
	    ///备注 
	    ///</summary> 
	    [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

