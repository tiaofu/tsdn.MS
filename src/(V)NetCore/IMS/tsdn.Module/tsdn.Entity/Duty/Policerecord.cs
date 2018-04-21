using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
    ///<summary>
    ///警力备案信息表
    ///</summary>
    [Table("DUTY_POLICERECORD")]
    public class PoliceRecord
    {
        ///<summary>
        ///主键 
        ///</summary> 
        [Key, Column("POLICERECORDID", TypeName = "VARCHAR2")]
        public string PoliceRecordId { get; set; }

        ///<summary>
        ///名称 
        ///</summary> 
        [Column("NAME", TypeName = "NVARCHAR2")]
        public string Name { get; set; }

        ///<summary>
        ///报备信息，存储岗位等部署信息，需要考虑反序列化生成关系 
        ///</summary> 
        [Column("RECORD", TypeName = "VARCHAR2")]
        public string Record { get; set; }

        ///<summary>
        ///接收人 
        ///</summary> 
        [Column("RECEIVER", TypeName = "VARCHAR2")]
        public string Receiver { get; set; }

        ///<summary>
        /// Creater
        ///</summary> 
        [Column("CREATER", TypeName = "VARCHAR2")]
        public string Creater { get; set; }

        ///<summary>
        /// Createdtime
        ///</summary> 
        [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime? CreatedTime { get; set; }

        ///<summary>
        /// Updater
        ///</summary> 
        [Column("UPDATER", TypeName = "VARCHAR2")]
        public string Updater { get; set; }

        ///<summary>
        /// Updatetime
        ///</summary> 
        [Column("UPDATETIME", TypeName = "DATE")]
        public DateTime? UpdateTime { get; set; }

        ///<summary>
        /// 部门ID
        ///</summary> 
        [Column("DEPARTMENTID", TypeName = "VARCHAR2")]
        public string DepartmentId { get; set; }

        ///<summary>
        ///备注 
        ///</summary> 
        [Column("REMARK", TypeName = "NVARCHAR2")]
        public string Remark { get; set; }
    }
}

