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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace System.Net.Http
{
    /// <summary>
    /// 代理规则拓展
    /// </summary>
    public static class ProxyExtend
    {
        private static ProxyOptionGroup options = null;

        /// <summary>
        /// 初始化代理转发规则
        /// </summary>
        /// <param name="_options"></param>
        public static void InitProxyRule(ProxyOptionGroup _options)
        {
            options = _options;
        }

        /// <summary>
        /// 找到转发匹配规则
        /// </summary>
        /// <param name="options">规则组</param>
        /// <param name="path">请求路径</param>
        /// <returns>规则项</returns>
        public static ProxyOptions MatchProxyRule(this string path)
        {           
            return MatchProxyRule(path, options);
        }

        /// <summary>
        /// 找到转发匹配规则
        /// </summary>
        /// <param name="options">规则组</param>
        /// <param name="path">请求路径</param>
        /// <returns>规则项</returns>
        public static ProxyOptions MatchProxyRule(this string path, ProxyOptionGroup options)
        {
            ProxyOptions op = null;
            if (options.Excludes != null && options.Excludes.Exists(e => path.StartsWith(e, StringComparison.CurrentCultureIgnoreCase)))
            {
                return op;
            }
            foreach (var item in options.Options)
            {
                if (item.Excludes != null && item.Excludes.Exists(e => path.StartsWith(e, StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }
                if (item.MatchReg.IsMatch(path))
                {
                    op = item;
                    break;
                }
            }
            return op;
        }
    }
}
