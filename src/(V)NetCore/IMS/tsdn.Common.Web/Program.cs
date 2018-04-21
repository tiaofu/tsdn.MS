/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: application run
 *********************************************************/
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace tsdn.Common.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //增加commandline支持
            var config = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Config"))
                    .AddXmlFile("Config.config", optional: true)
                    .AddXmlFile("Config_Local.config", optional: true)
                    .AddCommandLine(args).Build();
            string serverUrls = config.GetValue<string>("server.urls");

            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseStartup<Startup>()
                        .UseUrls(serverUrls)
                        .Build();
            host.Run();
        }
    }
}
