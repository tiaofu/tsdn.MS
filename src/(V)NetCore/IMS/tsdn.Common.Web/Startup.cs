/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: startup config
 *********************************************************/
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using tsdn.Common.Core;
using tsdn.Common.MVC;
using tsdn.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using tsdn.Common.Config;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using tsdn.Dependency;
using tsdn.RemoteEventBus;
using tsdn.RemoteEventBus.RabbitMQ;
using Microsoft.AspNetCore.Hosting.Server.Features;
using tsdn.Common.SignalR;
using tsdn.Auditing;
using Autofac;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using tsdn.Common.SwaggerExtend;
using tsdn.Common.Module;

namespace tsdn.Common.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                //.SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfigInit.InitConfig(Configuration);
            EFInitializer.UnSafeInit();
            XmlCommandManager.UnSafeInit();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //替换IOC插件为Autofac
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //添加Http代理插件
            services.AddProxy(options =>
            {
                options.PrepareRequest = (originalRequest, message) =>
                {
                    message.Headers.Add("X-Forwarded-Host", originalRequest.Host.Host);
                    return Task.FromResult(0);
                };
            });
            services.AddSignalR();
            //.AddRedis(config =>
            //{
            //    config.Options = ConfigurationOptions.Parse($"{RedisConfig.Host}:{RedisConfig.Port}");
            //});
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                // 路由参数在此处仍然是有效的，比如添加一个版本号
                options.UseCentralRoutePrefix(new RouteAttribute($"api/{SysConfig.MicroServiceOption.Application.Name}/[controller]"));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddCors();
            //添加GZip响应压缩插件
            services.AddResponseCompression();
            //swagger接口描述
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = $"{SysConfig.MicroServiceOption.Application.Title}接口描述文档"
                });
                options.OperationFilter<AddAuthTokenHeaderParameter>();
                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string[] files = Directory.GetFiles(basePath, "tsdn.*.xml");
                foreach (var path in files)
                {
                    options.IncludeXmlComments(path);
                }
                options.DocumentFilter<DocumentFilter>();
            });
            return IocManager.Instance.Initialize(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
            }
            //参数配置初始化
            var configManager = IocManager.Instance.Resolve<IConfigManager>();
            configManager.Init();
            //设置默认启动页
            app.UseDefaultFiles();
            //允许跨域请求
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            //事件总线
            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            var appParam = new TypedParameter(typeof(IServerAddressesFeature), serverAddressesFeature);
            var appInfo = IocManager.Instance.Resolve<IAppInfoProvider>(appParam);
            app.UseRemoteEventBus(options =>
            {
                options.UseRabbitMQ(setting =>
                {
                    setting.Durable = RabbitMQConfig.Durable;
                    setting.QueuePrefix = $"tsdn.{SysConfig.MicroServiceOption.Application.Name}.{appInfo.IpAddress}:{appInfo.Ports[0]}";
                    setting.ExchangeName = RabbitMQConfig.ExchangeName;
                    setting.Url = $"amqp://{RabbitMQConfig.UserName}:{RabbitMQConfig.Password}@{RabbitMQConfig.Host}:{RabbitMQConfig.Port}/";
                });
                //自动绑定订阅者
                options.AutoSubscribe();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{SysConfig.MicroServiceOption.Application.Title}接口描述文档V1");
            });
            app.UseResponseCompression();
            //loggerFactory.AddConsole();
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/signalR");
            });

            if (SysConfig.StaticDirectory != null)
            {
                foreach (var item in SysConfig.StaticDirectory)
                {
                    var path = FileHelper.GetDirectoryPath(item.PhysicalRelativePath, true);
                    app.UseFileServer(new FileServerOptions()
                    {
                        FileProvider = new PhysicalFileProvider(path),
                        RequestPath = new PathString(item.RequestPath),
                        EnableDirectoryBrowsing = item.EnableDirectoryBrowsing
                    });
                }
            }
            app.UseMvc();
            //放在UseMvc之后,静态资源目录配置
            app.UseStaticFiles(new StaticFileOptions
            {
                //设置不限制content-type
                //ServeUnknownFileTypes = true
            });
            //启动Http代理插件
            var proxyRule = GetProxyOption();
            ProxyExtend.InitProxyRule(proxyRule);
            if (proxyRule.Options != null &&
                proxyRule.Options.Count > 0)
            {
                app.RunProxy(proxyRule);
            }

            //系统启动后配置
            var completeModule = IocManager.Instance.Resolve<AfterRunConfigureModule>();
            completeModule.Configure();
        }

        #region "私有方法"
        /// <summary>
        /// 获取http代理启动参数
        /// </summary>
        /// <returns>http代理参数</returns>
        private ProxyOptionGroup GetProxyOption()
        {
            ProxyOptionGroup pGroup = new ProxyOptionGroup();
            pGroup.Options = new List<ProxyOptions>();
            if (SysConfig.MicroServiceOption.Proxy == null)
            {
                SysConfig.MicroServiceOption.Proxy = new List<ProxyOption>();
            }
            pGroup.Options.Add(new ProxyOptions
            {
                Uri = new Uri($"http://{MicroServiceConfig.ImsHost}"),
                MatchReg = new Regex("/api")
            });
            pGroup.Options.Add(new ProxyOptions
            {
                Uri = new Uri(SysConfig.MicroServiceOption.Cloud.RegistCenterUri),
                MatchReg = new Regex("/map/scene")
            });
            foreach (var item in SysConfig.MicroServiceOption.Proxy)
            {
                pGroup.Options.Add(new ProxyOptions
                {
                    Uri = new Uri(item.ProxyPass),
                    MatchReg = new Regex(item.UrlReg),
                    Excludes = item.Excludes == null ? null : item.Excludes.ToList()
                });
            }
            pGroup.Excludes = new List<string> {
                $"/api/{SysConfig.MicroServiceOption.Application.Name}/",
                "/api/Common/ConfigOptions",
                "/api/Common/Excel"
            };
            return pGroup;
        }
        #endregion
    }
}
