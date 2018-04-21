using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
	///<summary>
	///岗位警力排班表
	///</summary>
	[Table("DUTY_POSTPOLICEONDUTY")]
	public class  PostPoliceOnduty
    {
	///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("PPNID", TypeName = "VARCHAR2")]
        public string PPNId { get; set; }
	      
		///<summary>
	    ///排班编号 
	    ///</summary> 
	    [Column("PPNNO", TypeName = "VARCHAR2")]
        public string PPNNo { get; set; }
	      
		///<summary>
	    ///勤务类型 数据字典 eg:内勤，外勤 
	    ///</summary> 
	    [Column("PPNTYPE", TypeName = "VARCHAR2")]
        public string PPNType { get; set; }
	      
		///<summary>
	    ///排班重复类型 数据字典 eg:不重复,周重复,月重复,日重复 
	    ///</summary> 
	    [Column("PPNREPEATTYPE", TypeName = "VARCHAR2")]
        public string PPNRepeatType { get; set; }

        ///<summary>
        ///重复信息：重复类型为周重复 重复信息 1,3,5 代表每周一，三，五执行 
        ///</summary> 
        [Column("PPNREPEATINFO", TypeName = "VARCHAR2")]
        public string PPNRepeatInfo { get; set; }

        ///<summary>
        ///岗位ID 
        ///</summary> 
        [Column("POSTID", TypeName = "VARCHAR2")]
        public string PostId { get; set; }
	      
		///<summary>
	    ///警员ID 
	    ///</summary> 
	    [Column("POLICEMANID", TypeName = "VARCHAR2")]
        public string PoliceManid { get; set; }
	      
		///<summary>
	    ///值勤开始时间  
	    ///</summary> 
	    [Column("ONDUTYSTARTTIME", TypeName = "DATE")]
        public DateTime?  OndutyStarttime { get; set; }
	      
		///<summary>
	    ///值勤结束时间 
	    ///</summary> 
	    [Column("ONDUTYENDTIME", TypeName = "DATE")]
        public DateTime?  OndutyEndtime { get; set; }
	      
		///<summary>
	    ///岗位排班开始日期 
	    ///</summary> 
	    [Column("EFFECTSTARTTIME", TypeName = "DATE")]
        public DateTime?  EffectStarttime { get; set; }
	      
		///<summary>
	    ///排班结束日期 ，不设置永久有效 
	    ///</summary> 
	    [Column("EFFECTENDTIME", TypeName = "DATE")]
        public DateTime?  EffectEndtime { get; set; }
	      
		///<summary>
	    ///创建人 
	    ///</summary> 
	    [Column("CREATER", TypeName = "VARCHAR2")]
        public string Creater { get; set; }
	      
		///<summary>
	    ///创建时间 
	    ///</summary> 
	    [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime?  CreatedTime { get; set; }
	      
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
	    [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
	      
	}  
}

