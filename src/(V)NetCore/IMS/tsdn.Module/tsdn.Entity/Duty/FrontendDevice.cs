using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
    ///<summary>
    ///前端设备表代替原来的设备点位关联表和设备扩展表。每一个前端设备，都应至少和一个资产进行关联。
    ///</summary>
    [Table("FRONTENDDEVICE")]
    public class FrontendDevice
    {
        ///<summary>
        ///设备ID 
        ///</summary> 
        [Key, Column("DEVICEID")]
        public string DeviceId { get; set; }

        ///<summary>
        ///关联资产主键 
        ///</summary> 
        [Column("ASSETSID")]
        public string AssetsId { get; set; }

        ///<summary>
        ///设备名称，默认和关联的资产同名（只有一个关联时） 
        ///</summary> 
        [Column("DEVICENAME")]
        public string DeviceName { get; set; }

        ///<summary>
        ///设备编号，用于和前端设备进行匹配 
        ///</summary> 
        [Column("DEVICENO")]
        public string DeviceNo { get; set; }

        ///<summary>
        ///来源类型,设备的来源：新购设备、返厂换新、升级换新、… 
        ///</summary> 
        [Column("SOURCEKIND")]
        public string SourceKind { get; set; }

        ///<summary>
        ///功能类型，例如卡口相机，电子警察相机，信号机，诱导屏，工控机 
        ///</summary> 
        [Column("FUNCTIONTYPE")]
        public string FunctionType { get; set; }

        ///<summary>
        ///关联点位ID 
        ///</summary> 
        [Column("SPOTTINGID")]
        public string SpottingId { get; set; }

        ///<summary>
        ///经度坐标值 
        ///</summary> 
        [Column("LONGITUDE")]
        public double? Longitude { get; set; }

        ///<summary>
        ///纬度坐标值 
        ///</summary> 
        [Column("LATITUDE")]
        public double? Latitude { get; set; }

        ///<summary>
        ///连接信息，详情参考后续说明 
        ///</summary> 
        [Column("CONNECTIONINFO")]
        public string ConnectionInfo { get; set; }

        ///<summary>
        ///位置信息，卡口、电子警察、信号机、诱导屏、区间测速设备，关联信息各不相同，需要自定义 
        ///</summary> 
        [Column("SPOTTINGINFO")]
        public string SpottingInfo { get; set; }

        ///<summary>
        ///扩展字段1，根据FUNCTIONTYPE不同而有不同的解释。例如，有六合一代码的区间测速设别， 此字段表示六合一代码 
        ///</summary> 
        [Column("EXT1")]
        public string Ext1 { get; set; }

        ///<summary>
        ///扩展字段2，根据FUNCTIONTYPE不同而有不同的解释。为方便查询，扩展字段的值统一使用"实际字段名称"=实际值的形式存储 
        ///</summary> 
        [Column("EXT2")]
        public string Ext2 { get; set; }

        ///<summary>
        ///扩展字段3，根据FUNCTIONTYPE不同而有不同的解释。 
        ///</summary> 
        [Column("EXT3")]
        public string Ext3 { get; set; }

        ///<summary>
        ///扩展字段，根据FUNCTIONTYPE不同而有不同的解释。 
        ///</summary> 
        [Column("EXT")]
        public string Ext { get; set; }

        ///<summary>
        ///备注 
        ///</summary> 
        [Column("REMARK")]
        public string Remark { get; set; }

        ///<summary>
        ///IP地址 
        ///</summary> 
        [Column("IP")]
        public string Ip { get; set; }

        ///<summary>
        ///待审核、已审核,正常、故障、调试、拆改、启停用 
        ///</summary> 
        [Column("YWSTATUS")]
        public string YwStatus { get; set; }

        ///<summary>
        ///故障的分类：人工发现的，系统自测的网络故障+数据异常 
        ///</summary> 
        [Column("GZSTATUS")]
        public int? GzStatus { get; set; }

        ///<summary>
        ///核心版对接编号 
        ///</summary> 
        [Column("CORENO")]
        public string CoreNo { get; set; }

        ///<summary>
        ///审核状态(0:未审核, 1:审核通过, 2:审核未通过), 默认为未审核状态
        ///</summary> 
        [Column("APPROVESTATUS")]
        public int? ApproveStatus { get; set; }

        ///<summary>
        ///审核人用户Code
        ///</summary> 
        [Column("APPROVETOR")]
        public string Approvetor { get; set; }

        ///<summary>
        ///审核时间
        ///</summary> 
        [Column("APPROVETIME")]
        public DateTime? ApproveTime { get; set; }

        ///<summary>
        ///审核备注
        ///</summary> 
        [Column("APPROVEINFO")]
        public string ApproveInfo { get; set; }
    }
}
