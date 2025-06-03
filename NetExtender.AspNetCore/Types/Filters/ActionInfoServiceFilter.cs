// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.AspNetCore.Types.Services;
using NetExtender.AspNetCore.Types.Services.Interfaces;
using NetExtender.Utilities.AspNetCore.Types;

namespace NetExtender.AspNetCore.Filters
{
    public class ActionInfoServiceFilter<TId, TUser, TRole> : ActionInfoServiceFilter, IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        protected ActionInfoServiceFilter()
        {
        }
        
        public ActionInfoServiceFilter(IActionInfoService? service)
            : base(service)
        {
        }
    }

    public class ActionInfoServiceFilter : ActionInfoService, IAsyncActionFilter
    {
        private readonly IUnsafeActionInfoService _service;
        protected IActionInfoService Service
        {
            get
            {
                return _service;
            }
        }

        protected ActionInfoServiceFilter()
        {
            _service = this;
        }

        public ActionInfoServiceFilter(IActionInfoService? service)
        {
            _service = service is null ? this : service.Unsafe ?? throw new NotSupportedException($"Service '{service.GetType()}' is not supported.");
        }

        protected virtual void Set(String? action, ActionDescriptor? descriptor)
        {
            _service.Set(Current = action, Descriptor = descriptor);
        }
        
        public virtual async Task OnActionExecutionAsync(ActionExecutingContext? context, ActionExecutionDelegate? next)
        {
            Set(context?.RouteData.Values["action"]?.ToString(), context?.ActionDescriptor);
            await next.Execute();
        }
    }
}