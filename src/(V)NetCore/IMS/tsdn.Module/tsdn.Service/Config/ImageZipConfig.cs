/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Config;

namespace tsdn.ConfigHandler.Config
{
    /// <summary>
    /// 图片处理配置
    /// </summary>
    

    [ConfigType(Group = "ImageZipConfig", GroupCn = "图片视频转发配置", ImmediateUpdate = true, FunctionType = "系统管理")]
    public class ImageZipConfig : ConfigOption
    {
        /// <summary>
        /// 图片处理接口地址
        /// </summary>
        [Config(Name = "图片处理接口地址", Required = false)]
        public static string ImageHandleApi { get; set; }

        /// <summary>
        /// 视频处理接口地址
        /// </summary>
        [Config(Name = "视频处理接口地址", Required = false)]
        public static string VideoHandleApi { get; set; }
    }
}