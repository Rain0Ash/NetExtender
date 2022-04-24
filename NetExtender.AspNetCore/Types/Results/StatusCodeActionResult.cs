// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace NetExtender.AspNetCore.Types.Results
{
    public class StatusCodeActionResult : ActionResult, IClientErrorActionResult
    {
        public HttpStatusCode StatusCode { get; }

        Int32? IStatusCodeActionResult.StatusCode
        {
            get
            {
                return (Int32) StatusCode;
            }
        }

        protected Action<ActionContext>? Execute { get; }

        public StatusCodeActionResult(HttpStatusCode status)
            : this(status, null)
        {
        }

        public StatusCodeActionResult(HttpStatusCode status, Action<ActionContext>? execute)
        {
            StatusCode = status;
            Execute = execute;
        }

        public override void ExecuteResult(ActionContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Execute?.Invoke(context);
            context.HttpContext.Response.StatusCode = (Int32) StatusCode;
        }
    }
}