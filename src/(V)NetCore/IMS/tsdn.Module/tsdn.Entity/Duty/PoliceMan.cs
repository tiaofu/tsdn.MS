/*
 * Model:警员信息
 * Desctiption:警员信息
 * Author:王玲
 * Created: 2017/01/14 16:40:29  
 * Copyright：武汉中科通达高新技术股份有限公司
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tsdn.PIS.Entities
{
    ///<summary>
    ///警员信息
    ///</summary>
    [Table("POLICE_POLICEMAN")]
    public class PoliceMan
    {
        ///<summary>
        ///人员ID,主键 
        ///</summary> 
        [Key, Column("POLICEMANID", TypeName = "VARCHAR2")]
        public string PoliceManID { get; set; }

        ///<summary>
        ///人员姓名 
        ///</summary> 
        [Column("POLICENAME", TypeName = "NVARCHAR2"), Required]
        public string PoliceName { get; set; }

        ///<summary>
        ///性别 
        ///</summary> 
        [Column("GENDER", TypeName = "VARCHAR2")]
        public string Gender { get; set; }

        ///<summary>
        ///出生日期 
        ///</summary> 
        [Column("BIRTHDAY", TypeName = "DATE")]
        public DateTime? Birthday { get; set; }

        ///<summary>
        ///身份证号码 
        ///</summary> 
        [Column("SID", TypeName = "NVARCHAR2")]
        public string SID { get; set; }

        ///<summary>
        ///编号 
        ///</summary> 
        [Column("POLICENO", TypeName = "VARCHAR2")]
        public string PoliceNO { get; set; }

        ///<summary>
        ///移动电话 
        ///</summary> 
        [Column("PHONE", TypeName = "VARCHAR2")]
        public string Phone { get; set; }

        ///<summary>
        ///工作单位 
        ///</summary> 
        [Column("DEPARTMENTID", TypeName = "VARCHAR2")]
        public string DepartmentID { get; set; }

        ///<summary>
        ///工作年限 
        ///</summary> 
        [Column("WORKYEAR", TypeName = "NUMBER")]
        public int? WorkYear { get; set; }

        ///<summary>
        ///录入时间 
        ///</summary> 
        [Column("CREATEDTIME", TypeName = "DATE")]
        public DateTime? CreatedTime { get; set; }

        ///<summary>
        ///录入人 
        ///</summary> 
        [Column("CREATOR", TypeName = "VARCHAR2")]
        public string Creator { get; set; }

        ///<summary>
        ///人员类型 kind=99 
        ///</summary> 
        [Column("POLICETYPE", TypeName = "VARCHAR2")]
        public string PoliceType { get; set; }

        ///<summary>
        ///驾驶技能 
        ///</summary> 
        [Column("DRIVINGSKILL", TypeName = "NVARCHAR2")]
        public string DrivingSkill { get; set; }

        ///<summary>
        ///警衔 kind=81 
        ///</summary> 
        [Column("POLICERANK", TypeName = "VARCHAR2")]
        public string PoliceRank { get; set; }

        ///<summary>
        ///职务 
        ///</summary> 
        [Column("DUTY", TypeName = "VARCHAR2")]
        public string Duty { get; set; }

        ///<summary>
        ///修改人 
        ///</summary> 
        [Column("MODIFIER", TypeName = "VARCHAR2")]
        public string Modifier { get; set; }

        ///<summary>
        ///修改时间 
        ///</summary> 
        [Column("MODIFIEDTIME", TypeName = "DATE")]
        public DateTime? ModifiedTime { get; set; }

        ///<summary>
        ///家庭住址 
        ///</summary> 
        [Column("HOMEADDRESS", TypeName = "NVARCHAR2")]
        public string HomeAddress { get; set; }

        ///<summary>
        ///微信号 
        ///</summary> 
        [Column("WEIXINNO", TypeName = "NVARCHAR2")]
        public string WeixinNO { get; set; }

        ///<summary>
        ///学历 
        ///</summary> 
        [Column("EDUCATION", TypeName = "VARCHAR2")]
        public string Education { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        [NotMapped]
        public string PoliceTypeName { get; set; }

        /// <summary>
        /// 系统用户code
        /// </summary>
        [Column("SYSUSERCODE", TypeName = "VARCHAR2")]
        public string SysUserCode { get; set; }

        [NotMapped]
        public string BoPoMofo { get; set; }

        /// <summary>
        /// 是否排班
        /// </summary>
        [NotMapped]
        public bool IsOnDuty { get; set; }
    }
}
