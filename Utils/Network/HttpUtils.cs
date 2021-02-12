// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;

namespace NetExtender.Utils.Network
{
    public static class HttpUtils
    {
        public static Boolean IsClientErrorCode(this HttpStatusCode code)
        {
            return code >= HttpStatusCode.BadRequest && code < HttpStatusCode.InternalServerError;
        }
    }
}