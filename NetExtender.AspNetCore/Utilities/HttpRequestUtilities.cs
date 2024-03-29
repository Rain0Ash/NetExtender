// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HttpRequestUtilities
    {
        public static Boolean IsLocalHost(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ConnectionInfo connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress is not null)
            {
                return connection.LocalIpAddress is not null ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress) : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return connection.RemoteIpAddress is null && connection.LocalIpAddress is null;
        }

        public static String? GetRequestId(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.TryGetValue("X-Request-ID", out StringValues id) ? id.ToString() : null;
        }

        public static String? GetUserAgent(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.TryGetValue("User-Agent", out StringValues agent) ? agent.ToString() : null;
        }

        public static String GetAbsoluteUri(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new StringBuilder(100)
                .Append(request.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }
    }
}