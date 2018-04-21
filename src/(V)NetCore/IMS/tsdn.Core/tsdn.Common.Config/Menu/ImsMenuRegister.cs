/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Core;
using tsdn.Common.Micro;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace tsdn.Common.Config.Menu
{
    /// <summary>
    /// Ims菜单注册
    /// </summary>
    public class ImsMenuRegister : IMenuRegister
    {
        private readonly ILogger logger;

        public ImsMenuRegister(ILogger<ImsMenuRegister> logger)
        {
            this.logger = logger;
        }

        public void RegisterMenu()
        {
            if (string.IsNullOrEmpty(MicroServiceConfig.ImsHost))
            {
                logger.LogError(new Exception("向IMS注册功能菜单异常，IMS服务地址未配置"), "请到IMS->参数配置->微服务配置中进行“IMS服务地址”的配置");
            }
            else
            {
                string url = $"http://{MicroServiceConfig.ImsHost}/api/Common/MicroService/RegisterFunction";
                tsdnHttpRequest request = new tsdnHttpRequest();
                request.AddressUrl = url;
                request.Method = HttpMethod.Post;
                var pData = new
                {
                    App = SysConfig.MicroServiceOption.Application,
                    Menus = SysConfig.MicroServiceOption.ImsMenu
                };
                request.Body = pData;
                var result = request.SendAsync<ApiResult<bool>>().Result;
                if (result.Code.IsSuccessCode())
                {
                    logger.LogInformation($"向{url}成功注册了功能菜单{JsonConvert.SerializeObject(pData)}");
                }
                else
                {
                    logger.LogError(result.Exception, $"向{url}注册功能菜单出现异常，{result.Message}");
                }
            }
        }
    }
}
