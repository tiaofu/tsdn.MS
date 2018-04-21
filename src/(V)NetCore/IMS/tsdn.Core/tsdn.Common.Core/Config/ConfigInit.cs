/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace tsdn.Common.Core
{
    /// <summary>
    /// 配置文件信息初始化,config.json会覆盖appsettings.json
    /// 下次获取最新又覆盖了本地修改的相关配置问题，通过添加本地配置文件格式为app_Local.config开解决此问题
    /// </summary>
    public class ConfigInit
    {
        /// <summary>
        /// 本地配置文件地址
        /// </summary>
        private static readonly string ConfigLocalPath = FileHelper.GetAbsolutePath("Config/Config_Local.config");

        /// <summary>
        /// 获取本地化配置文件地址
        /// </summary>
        /// <returns>本地化配置文件地址</returns>
        public static string GetLocalConfigPath()
        {
            return ConfigLocalPath;
        }
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        public static void InitConfig(IConfiguration Configuration)
        {
            SysConfig.DefaultConnection = DESEncrypt.Decrypt(Configuration.GetConnectionString("DefaultConnection"));
            SysConfig.MicroServiceOption = Configuration.GetSection("MicroService").Get<MicroServiceOption>();
            SysConfig.StaticDirectory = Configuration.GetSection("StaticDirectory").Get<List<StaticFilesOptions>>();
        }
    }
}
