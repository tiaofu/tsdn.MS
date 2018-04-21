using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
	///<summary>
	///特勤方案值勤组
	///</summary>
	[Table("SS_DUTYUNIT")]
	public class DutyUnit
    {
	    ///<summary>
	    ///特勤任务组ID 
	    ///</summary> 
	    [Key,Column("DUID", TypeName = "VARCHAR2")]
        public string DUID { get; set; }

        ///<summary>
	    ///方案信息 
	    ///</summary> 
	    [Column("SSID", TypeName = "VARCHAR2")]
        public string SSID { get; set; }

        ///<summary>
        ///特勤方案组类型，对应数据字典组类型 
        ///</summary> 
        [Column("GROUPTYPE", TypeName = "NVARCHAR2")]
        public string GroupType { get; set; }
	      
		///<summary>
	    ///负责人 
	    ///</summary> 
	    [Column("LEADER", TypeName = "VARCHAR2")]
        public string Leader { get; set; }
	      
		///<summary>
	    ///值勤职责说明 
	    ///</summary> 
	    [Column("TASKINFO", TypeName = "NVARCHAR2")]
        public string TaskInfo { get; set; }
	      
		///<summary>
	    ///备注 
	    ///</summary> 
	    [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

