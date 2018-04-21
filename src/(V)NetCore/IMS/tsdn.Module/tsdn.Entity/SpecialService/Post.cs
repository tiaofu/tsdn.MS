using tsdn.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
    ///<summary>
    ///特勤岗位信息
    ///</summary>
    [Table("SS_POST")]
    public class Post
    {
        ///<summary>
        ///主键 
        ///</summary> 
        [Key, Column("SPID", TypeName = "VARCHAR2")]
        public string SPID { get; set; }

        ///<summary>
        ///子组类型 
        ///</summary> 
        [Column("SDID", TypeName = "VARCHAR2")]
        public string SDID { get; set; }

        ///<summary>
        ///负责人 
        ///</summary> 
        [Column("LEADER", TypeName = "VARCHAR2")]
        public string Leader { get; set; }

        ///<summary>
        ///负责单位 
        ///</summary> 
        [Column("DEPARTMENTID", TypeName = "VARCHAR2")]
        public string DepartmentId { get; set; }

        ///<summary>
        ///岗位类型 
        ///</summary> 
        [Column("POSTTYPE", TypeName = "VARCHAR2")]
        public string PostType { get; set; }

        ///<summary>
        ///需求警力人数 
        ///</summary> 
        [Column("NEEDPOLICECOUNT", TypeName = "NUMBER")]
        public double? NeedPoliceCount { get; set; }

        ///<summary>
        ///警员信息 多个逗号分割 
        ///</summary> 
        [Column("POLICE", TypeName = "CLOB")]
        public string Police { get; set; }

        ///<summary>
        ///警车信息 
        ///</summary> 
        [Column("POLICECAR", TypeName = "CLOB")]
        public string PoliceCar { get; set; }

        ///<summary>
        ///位置信息， 存储GPS信息，根据岗位类型区分 
        ///</summary> 
        [Column("POSITION", TypeName = "CLOB")]
        public string Position { get; set; }

        ///<summary>
	    ///位置信息说明 
	    ///</summary> 
	    [Column("POSITIONINFO", TypeName = "VARCHAR2")]
        public string PositionInfo { get; set; }

        ///<summary>
        ///任务信息 
        ///</summary> 
        [Column("TASKINFO", TypeName = "VARCHAR2")]
        public string TaskInfo { get; set; }

        ///<summary>
        /// Remark
        ///</summary> 
        [Column("REMARK", TypeName = "VARCHAR2")]
        public string Remark { get; set; }

    }
}

