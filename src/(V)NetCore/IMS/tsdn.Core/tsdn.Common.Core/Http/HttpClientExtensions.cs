/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Polly;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
    /// <summary>
    /// Represents the extensions for <see cref="HttpClient"/>.
    /// </summary>
    public static class HttpClientExtensions
    {
        private static HttpClient _client = new HttpClient(new HttpClientHandler
        {
            AllowAutoRedirect = false,
            UseCookies = false,
            UseProxy = false
        });

        #region "私有方法"
        private static HttpRequestMessage GetHttpRequestMessage(tsdnHttpRequest request)
        {
            var requestMessage = new HttpRequestMessage();
            var requestMethod = request.Method;
            if (requestMethod != HttpMethod.Get &&
                requestMethod != HttpMethod.Head &&
                requestMethod != HttpMethod.Trace
                && request.Body != null
                )
            {
                HttpContent content = null;
                if (request.MediaType == "application/json")
                {
                    content = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, request.MediaType);
                }
                else if (request.MediaType == "application/x-www-form-urlencoded")
                {
                    content = new FormUrlEncodedContent(request.Body as IEnumerable<KeyValuePair<string, string>>);
                }
                requestMessage.Content = content;
            }
            Uri uri = new Uri(request.AddressUrl, UriKind.RelativeOrAbsolute);
            var proxyOption = uri.PathAndQuery.MatchProxyRule();
            if (proxyOption != null)
            {
                uri = new Uri(UriHelper.BuildAbsolute(
                        proxyOption.Uri.Scheme,
                        new HostString(proxyOption.Uri.Authority),
                        proxyOption.Uri.AbsolutePath,
                        uri.AbsolutePath,
                        new QueryString(uri.Query).Add(new QueryString(proxyOption.Uri.Query)))
                    );
            }
            requestMessage.Headers.Host = uri.Authority;
            requestMessage.RequestUri = uri;
            requestMessage.Method = requestMethod;
            if (!string.IsNullOrEmpty(request.Token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", request.Token);
            }
            request.RequestSet?.Invoke(requestMessage);
            return requestMessage;
        }

        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static HttpClient CreateClient(tsdnHttpRequest request)
        {
            //_client.Timeout = TimeSpan.FromMilliseconds(request.TimeOutMilSeconds);
            //_client.DefaultRequestHeaders.Connection.Add("keep-alive");
            return _client;
        }

        /// <summary>
        ///  使用json格式化内容方法
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private static async Task<T> JsonFormat<T>(HttpResponseMessage resp)
        {
            var contentStr = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentStr);
        }
        #endregion

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns></returns>
        public async static Task<HttpResponseMessage> SendAsync(this tsdnHttpRequest request)
        {
            var client = CreateClient(request);
            var requestMessage = GetHttpRequestMessage(request);
            var httpRequestPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                                  .WaitAndRetryAsync(3, retryAttempt =>
                                  TimeSpan.FromSeconds(retryAttempt));
            try
            {
                return await httpRequestPolicy.ExecuteAsync(() =>
                    client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, CancellationToken.None));
            }
            catch (Exception e)
            {
                throw new Exception($"{request.Method.ToString()}请求{request.AddressUrl},参数{JsonConvert.SerializeObject(request.Body)}发生异常,{e.Message}", e);
            }
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <typeparam name="T">序列化类型</typeparam>
        /// <param name="request">请求对象</param>
        /// <returns></returns>
        public async static Task<T> SendAsync<T>(this tsdnHttpRequest request)
        {
            var r = await SendAsync(request);
            if (r.IsSuccessStatusCode)
            {
                return await JsonFormat<T>(r);
            }
            throw new Exception($"{request.Method.ToString()}请求{request.AddressUrl},参数{JsonConvert.SerializeObject(request.Body)}，服务器响应码{Convert.ToInt32(r.StatusCode)}({r.ReasonPhrase})");
        }
    }
}