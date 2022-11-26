// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class AccessRestrictionMiddleware : AsyncMiddleware
    {
        public const Int32 AllowStatusCode = 0;
        public const Int32 DefaultRestrictionStatusCode = (Int32) HttpStatusCode.Forbidden;

        protected Func<HttpContext, Int32> AccessCondition { get; }

        protected AccessRestrictionMiddleware(RequestDelegate next)
            : base(next)
        {
            AccessCondition = Access;
        }
        
        public AccessRestrictionMiddleware(RequestDelegate next, Func<HttpContext, Boolean>? access)
            : this(next, access, DefaultRestrictionStatusCode)
        {
        }

        public AccessRestrictionMiddleware(RequestDelegate next, Func<HttpContext, Boolean>? access, HttpStatusCode reject)
            : this(next, access, (Int32) reject)
        {
        }

        public AccessRestrictionMiddleware(RequestDelegate next, Func<HttpContext, Boolean>? access, Int32 reject)
            : base(next)
        {
            AccessCondition = access is null ? Access : context => access(context) ? AllowStatusCode : reject;
        }

        public AccessRestrictionMiddleware(RequestDelegate next, Func<HttpContext, HttpStatusCode>? access)
            : base(next)
        {
            AccessCondition = access is null ? Access : context =>
            {
                HttpStatusCode code = access(context);
                return code >= HttpStatusCode.BadRequest ? (Int32) code : AllowStatusCode;
            };
        }

        public AccessRestrictionMiddleware(RequestDelegate next, Func<HttpContext, Int32>? condition)
            : base(next)
        {
            AccessCondition = condition ?? Access;
        }

        protected virtual Int32 Access(HttpContext context)
        {
            return AllowStatusCode;
        }
        
        public override async Task InvokeAsync(HttpContext context)
        {
            Int32 status = AccessCondition(context);
            if (status > AllowStatusCode)
            {
                context.Response.StatusCode = status;
                return;
            }

            await Next(context);
        }
    }
}