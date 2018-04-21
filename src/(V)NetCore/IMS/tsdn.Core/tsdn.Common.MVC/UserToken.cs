/*
 * Model: 用户访问令牌
 * Desctiption: 描述
 * Author: 杜冬军
 * Created: 2018/03/21 11:45:19 
 * Copyright：武汉中科通达高新技术股份有限公司
 */

using tsdn.Common.Core;
using Newtonsoft.Json;
using System;

namespace tsdn.Common.MVC
{
    /// <summary>
    /// 用户认证令牌
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public string UserGUID { get; set; }
     
        /// <summary>
        /// 令牌生成时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="UserGUID">当前用户</param>
        public UserToken(string UserGUID)
        {
            this.UserGUID = UserGUID;
            Time = DateTime.Now;
        }

        public UserToken()
        {
            Time = DateTime.Now;
        }
    }

    /// <summary>
    /// 用户令牌帮助类
    /// </summary>
    public class TokenHelper
    {
        /// <summary>
        /// 获取用户令牌
        /// </summary>
        /// <param name="UserGUID">当前用户</param>
        /// <returns>用户令牌</returns>
        public static string CreateToken(string UserGUID)
        {
            string token = JsonConvert.SerializeObject(new UserToken(UserGUID));
            return DESEncrypt.Encrypt(token);
        }

        /// <summary>
        /// 获取用户令牌
        /// </summary>
        /// <param name="token">用户认证令牌</param>
        /// <returns>用户令牌</returns>
        public static UserToken RestoreToken(string token)
        {
            return JsonConvert.DeserializeObject<UserToken>(DESEncrypt.Decrypt(token));
        }

        /// <summary>
        /// 验证token有效性
        /// </summary>
        /// <param name="token">用户认证令牌</param>
        /// <returns></returns>
        public static bool VerifyToken(string token, out UserToken userToken, out string errorMessage)
        {
            userToken = RestoreToken(token);
            if (userToken == null || string.IsNullOrEmpty(userToken.UserGUID))
            {
                errorMessage = "token格式错误,未包含用户id";
                return false;
            }
            //验证令牌是否过期,默认有效期60分钟
            if ((DateTime.Now - userToken.Time).Minutes > 60)
            {
                errorMessage = "Token超时失效！";
                return false;
            }
            //if (!string.IsNullOrEmpty(userToken.AppKey))
            //{
                //ApiToken ctoken = CacheHelper.Get<ApiToken>(userToken.AppKey, CacheRegion.IMS_TOKEN);
                //if (ctoken == null || string.IsNullOrEmpty(ctoken.UserGUID) || ctoken.Disabled == true)
                //{
                //    errorMessage = "token失效,AppKey不存在";
                //    return false;
                //}
            //}
            errorMessage = "";
            return true;
        }
    }
}