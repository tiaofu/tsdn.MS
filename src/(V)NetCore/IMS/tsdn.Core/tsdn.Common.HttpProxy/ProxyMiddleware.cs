/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Microsoft.AspNetCore.Proxy
{
    /// <summary>
    /// Proxy Middleware
    /// </summary>
    public class ProxyMiddleware
    {
        private const int DefaultWebSocketBufferSize = 4096;

        private readonly RequestDelegate _next;
        private readonly ProxyOptionGroup _options;
        private readonly ILogger _logger;

        private static readonly string[] NotForwardedWebSocketHeaders = new[] { "Connection", "Host", "Upgrade", "Sec-WebSocket-Key", "Sec-WebSocket-Version" };

        public ProxyMiddleware(RequestDelegate next, IOptions<ProxyOptionGroup> options, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<ProxyMiddleware>();
        }

        public Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress.ToString());
            var proxyOption = context.Request.Path.ToString().MatchProxyRule(_options);
            if (proxyOption == null)
            {
                return _next.Invoke(context);
            }
            else
            {
                var uri = new Uri(UriHelper.BuildAbsolute(
                        proxyOption.Uri.Scheme,
                        new HostString(proxyOption.Uri.Authority),
                        proxyOption.Uri.AbsolutePath,
                        context.Request.Path,
                        context.Request.QueryString.Add(new QueryString(proxyOption.Uri.Query))
                    )
                );
                return context.ProxyRequest(uri);
            }
        }
    }
}
