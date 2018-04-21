using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
	///<summary>
	///岗位警车信息表
	///</summary>
	[Table("DUTY_POSTEQUIP")]
	public class  Postequip
    {
	///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("POSTEQUIPID", TypeName = "VARCHAR2")]
        public string Postequipid { get; set; }
	      
		///<summary>
	    ///岗位ID 
	    ///</summary> 
	    [Column("POSTID", TypeName = "VARCHAR2")]
        public string Postid { get; set; }
	      
		///<summary>
	    ///警用装备ID 
	    ///</summary> 
	    [Column("EQUIPMENTID", TypeName = "VARCHAR2")]
        public string Equipmentid { get; set; }
	      
		///<summary>
	    /// Creater
		///</summary> 
	    [Column("CREATER", TypeName = "VARCHAR2")]
        public string Creater { get; set; }
	      
		///<summary>
	    /// Createdtime
		///</summary> 
	    [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime?  Createdtime { get; set; }
	      
		///<summary>
	    /// Updater
		///</summary> 
	    [Column("UPDATER", TypeName = "VARCHAR2")]
        public string Updater { get; set; }
	      
		///<summary>
	    /// Updatetime
		///</summary> 
	    [Column("UPDATETIME", TypeName = "DATE")]
        public DateTime?  Updatetime { get; set; }
	      
		///<summary>
	    ///备注 
	    ///</summary> 
	    [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

