// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class HttpRequestUtils
    {
        public static Boolean IsLocalHost(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ConnectionInfo connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                return connection.LocalIpAddress != null ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress) : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return connection.RemoteIpAddress == null && connection.LocalIpAddress == null;
        }
        
        public static String? GetUserAgent(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.TryGetValue("User-Agent", out StringValues agent) ? agent.ToString() : null;
        }
    }
}