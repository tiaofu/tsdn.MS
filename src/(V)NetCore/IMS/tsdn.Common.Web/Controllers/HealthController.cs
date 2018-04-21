/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Microsoft.AspNetCore.Mvc;
using tsdn.Common.Core;
using Microsoft.AspNetCore.Authorization;

namespace tsdn.Common.Web.Controllers
{
    /// <summary>
    /// 健康检测接口
    /// </summary>
    [AllowAnonymous]
    public class HealthController : Controller
    {
        /// <summary>
        /// 检测程序Http服务是否正常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public ApiResult<string> ApiServer()
        {
            return new ApiResult<string>();
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public string Decrypt(string text)
        {
            return DESEncrypt.Decrypt(text);
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public string Encrypt(string text)
        {
            return DESEncrypt.Encrypt(text);
        }
    }
}
