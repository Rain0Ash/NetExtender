// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using NetExtender.Utils.Network;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class HttpResponceUtils
    {
        public static T SetStatusCode<T>(this T response, HttpStatusCode code) where T : HttpResponse
        {
            return SetStatusCode(response, code.StatusCode());
        }

        public static T SetStatusCode<T>(this T response, Int32 code) where T : HttpResponse
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.StatusCode = code;
            return response;
        }
    }
}