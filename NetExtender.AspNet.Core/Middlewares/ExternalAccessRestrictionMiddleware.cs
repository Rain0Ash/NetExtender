// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using NetExtender.Utilities.AspNetCore.Types;

namespace NetExtender.AspNet.Core.Middlewares
{
    public class ExternalAccessRestrictionMiddleware : AccessRestrictionMiddleware
    {
        protected Int32 RestrictionStatusCode { get; }
        
        public ExternalAccessRestrictionMiddleware(RequestDelegate next)
            : this(next, DefaultRestrictionStatusCode)
        {
        }

        public ExternalAccessRestrictionMiddleware(RequestDelegate next, HttpStatusCode code)
            : this(next, (Int32) code)
        {
        }

        public ExternalAccessRestrictionMiddleware(RequestDelegate next, Int32 code)
            : base(next)
        {
            RestrictionStatusCode = code;
        }

        protected override Int32 Access(HttpContext context)
        {
            return context.Request.IsLocalHost() ? AllowStatusCode : RestrictionStatusCode;
        }
    }
}