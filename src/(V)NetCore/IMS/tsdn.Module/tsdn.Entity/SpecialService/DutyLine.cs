using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
	///<summary>
	///
	///</summary>
	[Table("SS_DUTYLINE")]
	public class DutyLine
    {
	    ///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("SDLID", TypeName = "VARCHAR2")]
        public string SDLID { get; set; }
	      
		///<summary>
	    ///特勤方案信息 
	    ///</summary> 
	    [Column("SSID", TypeName = "VARCHAR2")]
        public string SSID { get; set; }
	      
		///<summary>
	    ///线路信息 
	    ///</summary> 
	    [Column("LINEINFO", TypeName = "CLOB")]
        public string LineInfo { get; set; }
	      
		///<summary>
	    ///线路优先级，特勤任务中分为 主线路以及备用线路 
	    ///</summary> 
	    [Column("LEVEL", TypeName = "CHAR")]
        public string Level { get; set; }
	      
		///<summary>
	    ///备注信息 
	    ///</summary> 
	    [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

