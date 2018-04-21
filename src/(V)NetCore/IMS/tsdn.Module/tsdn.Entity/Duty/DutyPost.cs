using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
	///<summary>
	///岗位表
	///</summary>
	[Table("DUTY_POST")]
	public class  DutyPost
    {
	///<summary>
	    ///主键 
	    ///</summary> 
	    [Key,Column("POSTID", TypeName = "VARCHAR2")]
        public string PostId { get; set; }
	      
		///<summary>
	    ///岗位名称 
	    ///</summary> 
	    [Column("POSTNAME", TypeName = "VARCHAR2")]
        public string PostName { get; set; }
	      
		///<summary>
	    ///岗位类型 关联数据字典岗位类型 
	    ///</summary> 
	    [Column("POSTTYPE", TypeName = "VARCHAR2")]
        public string PostType { get; set; }
	      
		///<summary>
	    ///考核时间范围 多个时间段,存储json 
	    ///</summary> 
	    [Column("POSTTIMESPAN", TypeName = "VARCHAR2")]
        public string PostTimespan { get; set; }
	      
		///<summary>
	    ///存储岗位的坐标信息，用于绘制岗位巡逻的地图区域 
	    ///</summary> 
	    [Column("POSTPOINT", TypeName = "VARCHAR2")]
        public string PostPoint { get; set; }
	      
		///<summary>
	    ///岗位定位允许偏差,单位米 
	    ///</summary> 
	    [Column("POINTOFFSET", TypeName = "NUMBER")]
        public  double?  PointOffset { get; set; }
	      
		///<summary>
	    ///责任人 
	    ///</summary> 
	    [Column("POSTLIABLEPERSON", TypeName = "VARCHAR2")]
        public string PostLiableperson { get; set; }
	      
		///<summary>
	    ///负责部门 
	    ///</summary> 
	    [Column("POSTLIABLEDEP", TypeName = "VARCHAR2")]
        public string PostLiabledep { get; set; }
	      
		///<summary>
	    ///岗位有效期结束时间 
	    ///</summary> 
	    [Column("EFFECTENDTIME", TypeName = "DATE")]
        public DateTime?  EffectEndtime { get; set; }
	      
		///<summary>
	    ///岗位有效期开始时间 
	    ///</summary> 
	    [Column("EFFECTSTARTTIME", TypeName = "DATE")]
        public DateTime?  EffectStarttime { get; set; }
	      
		///<summary>
	    ///岗位警力要求，需配置的警力数量 
	    ///</summary> 
	    [Column("POLICEREQUIRE", TypeName = "NUMBER")]
        public  double?  PoliceRequire { get; set; }
	      
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
	    ///更信人 
	    ///</summary> 
	    [Column("UPDATER", TypeName = "VARCHAR2")]
        public string Updater { get; set; }
	      
		///<summary>
	    ///更新日期 
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

