using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;

namespace tsdn.Common.MVC
{
    /// <summary>
    /// Api控制器基类，用于实现权限认证
    /// </summary>
    //[Route("[controller]")]
    public class ApiController : Controller
    {
        /// <summary>
        /// 当前登录的用户属性
        /// </summary>
        protected UserInfo CurrentUserInfo { get; set; }

        /// <summary>
        /// 当前主机地址
        /// </summary>
        protected string UrlScheme { get; set; }

        /// <summary>
        /// 认证token
        /// </summary>
        protected string token { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!AllowAnonymousCall(context))
            {
                CurrentUserInfo = GetUserInfo(context.HttpContext.Request);
            }
            UrlScheme = Request.Scheme + "://" + Request.Host.ToString();
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 是否允许匿名调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool AllowAnonymousCall(ActionExecutingContext context)
        {
            if (context.Controller.GetType().GetTypeInfo()
                .GetCustomAttributes<AllowAnonymousAttribute>().Count() > 0)
            {
                return true;
            }
            if (((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo
                .GetCustomAttribute<AllowAnonymousAttribute>() != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private UserInfo GetUserInfo(HttpRequest request)
        {
            UserInfo userInfo = new UserInfo()
            {
                UserCode = "admin",
                UserName = "admin"
            };
            StringValues values;
            if (Request.Headers.TryGetValue("Authorization", out values))
            {
                string strAuthHeader = values.ToString();

                string authScheme = "Basic ";
                if (!string.IsNullOrEmpty(strAuthHeader) && strAuthHeader.StartsWith(authScheme, StringComparison.CurrentCultureIgnoreCase))
                {
                    token = strAuthHeader.Substring(authScheme.Length);
                }
            }
            return userInfo;
        }
    }
}
