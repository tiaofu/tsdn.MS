﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System.Collections.Generic;
using System.Linq;

namespace System.Net.Http
{
    /// <summary>
    /// Http请求
    /// </summary>
    /// <example>
    ///  Post请求实例
    ///        string token = "F13CECD23F92526BC7220E8DC33AAD5E86AD3BE656C7E528D32395BA51847737E2B0C358032807E2AF9B653C9323DFB248B0AB00BD5CF1FDBAB993E25807A95EC86023F8F72746C2DF8FC045D0C0355E83E725DED84C89906321327C79A07A94B177F9C810EFA60482E801D6155C511C88667793715210E9";
    ///        var request = new tsdnHttpRequest()
    ///         {
    ///             AddressUrl = "http://192.168.0.135:8005/api/Security/Login/PostLoginIn",
    ///             Method = HttpMethod.Post,
    ///             Body = new { UserCode = "dudj1", Password = "c4ca4238a0b923820dcc509a6f75849b" },
    ///             Token = token
    ///         };
    ///         var xx = request.SendAsync<ApiResult<string>>().Result;
    ///  
    ///  Get请求实例
    ///        string token = "F13CECD23F92526BC7220E8DC33AAD5E86AD3BE656C7E528D32395BA51847737E2B0C358032807E2AF9B653C9323DFB248B0AB00BD5CF1FDBAB993E25807A95EC86023F8F72746C2DF8FC045D0C0355E83E725DED84C89906321327C79A07A94B177F9C810EFA60482E801D6155C511C88667793715210E9";
    ///        var request = new tsdnHttpRequest()
    ///         {
    ///             AddressUrl = "http://192.168.0.135:8005/api/Security/User/GetUserInfo",
    ///             Token = token
    ///         };
    ///         var xx = request.SendAsync<ApiResult<xx>>().Result;
    ///         
    /// </example>
    public class tsdnHttpRequest
    {
        public tsdnHttpRequest()
        {
            FileParameters = new List<FileParameter>();


        }

        /// <summary>
        /// 请求方式
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.Get;

        /// <summary>
        /// Uri 值
        /// </summary>
        public string AddressUrl { get; set; }     

        /// <summary>
        /// 不为空 会添加AuthenticationHeaderValue
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///   reqMessage 设置方法
        ///    如果当前的设置不能满足需求，可以通过这里再次设置
        /// </summary>
        public Action<HttpRequestMessage> RequestSet { get; set; }

        #region   请求的内容参数

        /// <summary>
        /// 文件参数列表
        /// </summary>
        public List<FileParameter> FileParameters { get; set; }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        public bool HasFile => FileParameters.Any();

        #endregion

        /// <summary>
        /// 自定义内容实体,Post等传入
        /// </summary>
        public object Body { get; set; }

        /// <summary>
        /// 配合Body使用
        /// application/json
        /// application/x-www-form-urlencoded
        /// </summary>
        public string MediaType { get; set; } = "application/json";
    }
}
