// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class HttpContextUtils
    {
        public static Task RejectAsync([NotNull] this HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.SetStatusCode(HttpStatusCode.ServiceUnavailable);
            context.Abort();
            return Task.CompletedTask;
        }
    }
}