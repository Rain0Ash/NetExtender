// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HttpResponceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetStatusCode<T>(this T response, HttpStatusCode code) where T : HttpResponse
        {
            return SetStatusCode(response, (Int32) code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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