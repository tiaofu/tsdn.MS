using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
    ///<summary>
    ///通行证申请单
    ///</summary>
    [Table("PASSPORT_APPLICATIONFORM")]
    public class ApplicationForm
    {
        ///<summary>
        ///申请单Id 主键 
        ///</summary> 
        [Key, Column("APPLICATIONID", TypeName = "VARCHAR2")]
        public string ApplicationId { get; set; }

        ///<summary>
        ///申请单编号 
        ///</summary> 
        [Column("FORMNO", TypeName = "VARCHAR2"), Required]
        public string FormNo { get; set; }

        ///<summary>
        ///通行证类型ID 
        ///</summary> 
        [Column("TYPEID", TypeName = "VARCHAR2"), Required]
        public string TypeId { get; set; }

        ///<summary>
        ///申请类型 对应数据字典kind 6001 
        ///</summary> 
        [Column("APPLICATIONTYPE", TypeName = "VARCHAR2")]
        public string ApplicationType { get; set; }

        ///<summary>
        ///申请单位
        ///</summary> 
        [Column("APPLICATIONNAME", TypeName = "NVARCHAR2")]
        public string ApplicationName { get; set; }

        ///<summary>
        ///申请人姓名 
        ///</summary> 
        [Column("FORMNAME", TypeName = "NVARCHAR2")]
        public string FormName { get; set; }

        ///<summary>
        ///申请人身份证信息 
        ///</summary> 
        [Column("IDENTITYCARD", TypeName = "VARCHAR2")]
        public string IdentityCard { get; set; }

        ///<summary>
        ///移动电话 
        ///</summary> 
        [Column("MOBILEPHONE", TypeName = "VARCHAR2")]
        public string MobilePhone { get; set; }

        ///<summary>
        ///固定电话 
        ///</summary> 
        [Column("TELPHONE", TypeName = "VARCHAR2")]
        public string TelPhone { get; set; }

        ///<summary>
        ///生效开始日期 
        ///</summary> 
        [Column("EFFECTIVESTARTTIME", TypeName = "DATE")]
        public DateTime? EffectiveStartTime { get; set; }

        ///<summary>
        ///生效截止日期 
        ///</summary> 
        [Column("EFFECTIVEENDTIME", TypeName = "DATE")]
        public DateTime? EffectiveEndTime { get; set; }

        ///<summary>
        ///申请单状态 默认为0 0:待审核 10：审核未通过 20：待复核 30：已审核 
        ///</summary> 
        [Column("STATUS", TypeName = "NUMBER")]
        public int? Status { get; set; }

        ///<summary>
        ///申请单创建人 
        ///</summary> 
        [Column("CREATOR", TypeName = "VARCHAR2")]
        public string Creator { get; set; }

        ///<summary>
        ///申请单创建时间 
        ///</summary> 
        [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime? CreatedTime { get; set; }

        ///<summary>
        ///申请单修改时间 
        ///</summary> 
        [Column("MODIFIEDTIME", TypeName = "DATE")]
        public DateTime? ModifiedTime { get; set; }

        ///<summary>
        ///申请单修改人 
        ///</summary> 
        [Column("MODIFIER", TypeName = "VARCHAR2")]
        public string Modifier { get; set; }

        ///<summary>
        ///审核日期 
        ///</summary> 
        [Column("APPROVETIME", TypeName = "DATE")]
        public DateTime? ApproveTime { get; set; }

        ///<summary>
        ///审核人 
        ///</summary> 
        [Column("APPROVETOR", TypeName = "VARCHAR2")]
        public string Approvetor { get; set; }

        ///<summary>
        ///审核意见 
        ///</summary> 
        [Column("APPROVEREMARK", TypeName = "NVARCHAR2")]
        public string ApproveRemark { get; set; }

        ///<summary>
        ///复核日期 
        ///</summary> 
        [Column("REAPPROVETIME", TypeName = "DATE")]
        public DateTime? ReApproveTime { get; set; }

        ///<summary>
        ///复核人 
        ///</summary> 
        [Column("REAPPROVETOR", TypeName = "VARCHAR2")]
        public string ReApprovetor { get; set; }

        ///<summary>
        ///复核意见 
        ///</summary> 
        [Column("REAPPROVEREMARK", TypeName = "NVARCHAR2")]
        public string ReApproveRemark { get; set; }

        ///<summary>
        ///线路打印描述 
        ///</summary> 
        [Column("PRINTLINEREMARK", TypeName = "NVARCHAR2")]
        public string PrintLineRemark { get; set; }
    }
}

