/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Collections.Generic;
using tsdn.Common.Config;

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 违法审核配置信息
    /// </summary>


    [ConfigType(Group = "IllegalHandlerConfig", GroupCn = "交通违法配置", ImmediateUpdate = false, FunctionType = "交通违法")]
    public class IllegalHandlerConfig : ConfigOption
    {
        /// <summary>
        /// 审核数据范围
        /// </summary>
        [Config(Name = "审核X天违法(根据违法审核界面时间顺序配置)", DefaultValue = "5", ValidateRule = "min=0 digits=true", Title = "审核X天违法(根据违法审核界面时间顺序配置)")]
        public static int DefaultInterval { get; set; }

        /// <summary>
        /// 违法查询界面默认显示数据
        /// </summary>
        [Config(Name = "查询X天违法(根据违法查询界面时间顺序配置)", DefaultValue = "2", ValidateRule = "min=0 digits=true", Title = "查询X天违法(根据违法查询界面时间顺序配置)")]
        public static int DefaultSearchInterval { get; set; }

        /// <summary>
        /// 限行违法修改
        /// </summary>
        [Config(Name = "限行违法修改", DefaultValue = "false", Title = "用于控制审核数据时是否允许修改限行违法数据")]
        public static bool ModifyXX { get; set; }


        /// <summary>
        /// 常规违法修改
        /// </summary>
        [Config(Name = "常规违法修改", DefaultValue = "true", Title = "用于控制审核数据时是否允许修改常规违法数据")]
        public static bool ModifyCG { get; set; }

        /// <summary>
        /// 违法查询是否显示原始数据
        /// </summary>
        [Config(Name = "违法查询是否显示原始数据", DefaultValue = "true", Title = "用于控制违法查询是否显示业务数据，原始数据切换按钮")]
        public static bool ShowOriginalData { get; set; }

        /// <summary>
        /// 违法查询是否显示原始数据
        /// </summary>
        [Config(Name = "开启强制二审", DefaultValue = "false", Title = "用于控制违法审核是否需要进行强制二审，开启后一审完必须经过二审才能上传")]
        public static bool MustSecondApprove { get; set; }

        /// <summary>
        /// 违法类型修改
        /// </summary>
        [Config(Name = "违法编号修改", DefaultValue = "false", Title = "用于控制违法审核是否允许修改违法编号")]
        public static bool ModifyIllegalType { get; set; }

        /// <summary>
        /// x天待二审状态数据自动变成已通过
        /// </summary>
        [Config(Name = "x天待二审数据自动变成已通过", DefaultValue = "5", ValidateRule = "min=0 digits=true", Title = "强制开启二审后，对于待二审数据X天未做处理，自动变成已通过，为0则不自动通过")]
        public static int SecondApproveDaysOverdue { get; set; }

        /// <summary>
        /// 审核展开显示前N条数据
        /// </summary>
        [Config(Name = "审核展开显示前N条数据", DefaultValue = "10", ValidateRule = "min=5 max=500 digits=true", Title = "用于控制违法审核展开违法类型显示的路口取前多少条数据")]
        public static int NavigationTopN { get; set; }

        /// <summary>
        /// 常规违法修改
        /// </summary>
        [Config(Name = "x天未审核自动沉淀数据", DefaultValue = "4", ValidateRule = "min=0 digits=true", Title = "用于控制几天之内未审核的违法数据做过期处理，为0则不过期")]
        public static int DaysOverdue { get; set; }

        /// <summary>
        /// 相同违法行为去重时间间隔（分钟）
        /// </summary>
        [Config(Name = "相同违法去重间隔(分钟)", DefaultValue = "1", ValidateRule = "min=0 max=1440 digits=true", Title = "同车同路口同行为去重时间间隔")]
        public static int SameIllegalInterval { get; set; }

        /// <summary>
        /// 任意违法行为去重时间间隔（分钟）
        /// </summary>
        [Config(Name = "任意违法去重间隔(分钟)", DefaultValue = "1", ValidateRule = "min=0 max=1440 digits=true", Title = "同车同路口任意行为去重时间间隔")]
        public static int IllegalInterval { get; set; }

        /// <summary>
        /// 违法审核界面是否显示设备条件
        /// </summary>
        [Config(Name = "违法审核是否显示设备", DefaultValue = "true", Title = "违法审核界面是否显示设备")]
        public static bool ShowAssets { get; set; }

        /// <summary>
        /// 违法查询界面是否包含采集单位
        /// </summary>
        [Config(Name = "违法查询是否包含采集单位", DefaultValue = "true", Title = "违法查询界面包含采集单位")]
        public static bool IsContainCollectDp { get; set; }

        /// <summary>
        /// 是否允许图片拉伸
        /// </summary>
        [Config(Name = "是否允许图片拉伸", DefaultValue = "false", Title = "是否允许图片拉伸")]
        public static bool IsImageStretch { get; set; }

        /// <summary>
        /// 违法审核图片放大模式
        /// </summary>
        [Config(Name = "图片放大模式", ValueType = ConfigValueType.Select, DefaultValue = "0", DataSource = "tsdn.Service,tsdn.ConfigHandler.Config.IllegalHandlerConfig.GetImageZoneDs", Title = "违法审核界面是否显示设备")]
        public static string ImageZoneType { get; set; }

        /// <summary>
        /// 废弃违法是否保存入库
        /// </summary>
        [Config(Name = "废弃违法是否入库", DefaultValue = "false", Title = "废弃违法是否入库")]
        public static bool IsSaveWhenScrap { get; set; }

        /// <summary>
        /// 相同违法数据废弃是否入库
        /// </summary>
        [Config(Name = "相同违法数据废弃是否入库", DefaultValue = "false", Title = "相同违法数据废弃是否入库")]
        public static bool IsSaveSameIllegal { get; set; }

        /// <summary>
        /// 无效车牌是否保存入库
        /// </summary>
        [Config(Name = "无效车牌违法是否入库", DefaultValue = "true", Title = "无效车牌违法是否入库")]
        public static bool IsSaveUnknownPlateNo { get; set; }

        /// <summary>
        /// 违法审核的违法时间排序方式
        /// </summary>
        [Config(Name = "违法审核的违法时间排序方式", ValueType = ConfigValueType.Select, DefaultValue = "DESC", DataSource = "tsdn.Service,tsdn.ConfigHandler.Config.IllegalHandlerConfig.GetSortType", Title = "违法审核的违法时间排序方式")]
        public static string IllegalVehicleSortType { get; set; }

        /// <summary>
        /// 设备停用违法废弃理由
        /// </summary>
        [Config(Name = "设备停用违法废弃理由", Required = false, ValueType = ConfigValueType.String, DefaultValue = "", Title = "设备停用违法废弃理由，为空表示禁用废弃")]
        public static string AssetsDisableSNo { get; set; }

        /// <summary>
        /// 路口停用违法废弃理由
        /// </summary>
        [Config(Name = "路口停用违法废弃理由", Required = false, ValueType = ConfigValueType.String, DefaultValue = "", Title = "路口停用违法废弃理由,为空表示禁用废弃")]
        public static string SpottingDisableSNo { get; set; }

        /// <summary>
        /// 违法审核是否开启套牌车检查
        /// </summary>
        [Config(Name = "违法审核是否开启套牌车检查", DefaultValue = "false", Title = "违法审核是否开启套牌车检查")]
        public static bool IsOpenDeckChekc { get; set; }

        /// <summary>
        /// 违法审核列表检测几分钟一条违法为正常
        /// </summary>
        [Config(Name = "违法审核列表检测几分钟一条违法为正常", DefaultValue = 5, ValidateRule = "min=1 max=1440 digits=true",Title = "违法审核列表检测几分钟一条违法为正常")]
        public static int IllegalDetectionInterval { get; set; }

        /// <summary>
        /// 获取违法审核图片放大模式
        /// </summary>
        /// <returns></returns>
        public static List<dynamic> GetImageZoneDs()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = 0,
                Text = "默认(图片悬浮放大)"
            });
            list.Add(new
            {
                Value = 1,
                Text = "图片区域内放大"
            });
            return list;
        }
        public static List<dynamic> GetSortType()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Value = "DESC",
                Text = "降序"
            });
            list.Add(new
            {
                Value = "ASC",
                Text = "升序"
            });
            return list;
        }
    }
}
