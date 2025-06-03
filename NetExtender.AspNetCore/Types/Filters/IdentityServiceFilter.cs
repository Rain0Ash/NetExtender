using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NetExtender.AspNetCore.Identity;
using NetExtender.AspNetCore.Identity.Attributes;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.AspNetCore.Types;

namespace NetExtender.AspNetCore.Filters
{
    public abstract class IdentityServiceFilter<TId, TUser, TRole> : IdentityUserService<TId, TUser, TRole>, IAsyncActionFilter where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
    {
        private static ConcurrentDictionary<Type, IdentityAttributeCollection> ControllerStorage
        {
            get
            {
                return IdentityServiceFilter.ControllerStorage;
            }
        }

        private static ConcurrentDictionary<(Type, String?), IdentityAttributeCollection> FastStorage
        {
            get
            {
                return IdentityServiceFilter.FastStorage;
            }
        }

        private static ConcurrentDictionary<MethodInfo, IdentityAttributeCollection> RealStorage
        {
            get
            {
                return IdentityServiceFilter.RealStorage;
            }
        }

        protected static IdentityAttributeCollection NoIdentity
        {
            get
            {
                return IdentityServiceFilter.NoIdentity;
            }
        }

        protected static IdentityAttributeCollection AuthorizeIdentity
        {
            get
            {
                return IdentityServiceFilter.AuthorizeIdentity;
            }
        }
        
        private readonly IUnsafeIdentityUserService<TId, TUser, TRole> _service;
        protected IIdentityUserService<TId, TUser, TRole> Service
        {
            get
            {
                return _service;
            }
        }
        
        protected IdentityServiceFilter()
        {
            _service = this;
        }
        
        protected IdentityServiceFilter(TRole? @default)
            : base(@default)
        {
            _service = this;
        }
        
        protected IdentityServiceFilter(ImmutableHashSet<TRole>? @default)
            : base(@default)
        {
            _service = this;
        }
        
        protected IdentityServiceFilter(IIdentityUserService<TId, TUser, TRole>? service)
            : base(service?.Unsafe?.NoUserRole)
        {
            _service = service is null ? this : service.Unsafe ?? throw new NotSupportedException($"Service '{service.GetType()}' is not supported.");
        }
        
        protected virtual void Set(TId? id)
        {
            _service.Set(Id = id);
        }
        
        protected virtual void Set(TUser? user)
        {
            _service.Set(User = user);
        }

        protected IdentityAttributeCollection Identity(Type type, String action)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (FastStorage.TryGetValue((type, action), out IdentityAttributeCollection? attribute))
            {
                return attribute;
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
            IdentityAttributeCollection controller = ControllerStorage.GetOrAdd(type, IdentityAttributeCollection.From);
            return type.GetMethod(action, binding) is { } method ? FastStorage[(type, action)] = RealStorage[method] = IdentityAttributeCollection.From(method).FromController(controller) : NoIdentity;
        }

        protected virtual IdentityAttributeCollection Identity(ActionExecutingContext? context)
        {
            if (context?.RouteData.Values["action"]?.ToString() is not { Length: > 0 } action)
            {
                return NoIdentity;
            }

            Type type = context.Controller.GetType();
            return Identity(type, action);
        }

        public virtual async Task OnActionExecutionAsync(ActionExecutingContext? context, ActionExecutionDelegate? next)
        {
            if (context is null)
            {
                await next.Execute();
                return;
            }
            
            IdentityAttributeCollection identity = Identity(context);
            TokenInfo? token = Token(context, out IIdentityException? exception);

            if (exception?.Known is IdentityException.Known.IsExpired)
            {
                if (identity.IsNoIdentity)
                {
                    await next.Execute();
                    return;
                }

                await context.Exception<IdentityTokenExpiredException>();
                return;
            }

            if (exception is not null)
            {
                await context.Exception(exception.Exception);
                return;
            }

            if (token is null)
            {
                if (identity.IsNoIdentity)
                {
                    await next.Execute();
                    return;
                }
                
                await context.Exception<IdentityNoTokenException>();
                return;
            }

            if (await FindAsync(token.Id) is not { } user)
            {
                await context.Exception<IdentityNoUserException>();
                return;
            }
            
            Set(user);
        }
        
        protected abstract TokenInfo? Token(String? token);

        protected virtual TokenInfo? Token(ActionExecutingContext? context, out IIdentityException? exception)
        {
            exception = null;
            if (context is null)
            {
                return null;
            }

            try
            {
                return Token(context.Authorization());
            }
            catch (BusinessException business) when (business is IIdentityException jwt)
            {
                exception = jwt;
                return null;
            }
            catch (JWTException jwt)
            {
                exception = IdentityException.From(jwt);
                return null;
            }
            catch (Exception unknown)
            {
                exception = new IdentityUnknownException(null, unknown);
                return null;
            }
        }
        
        protected record TokenInfo(TId Id, TRole[]? Role);
    }

    internal static class IdentityServiceFilter
    {
        internal static ConcurrentDictionary<Type, IdentityAttributeCollection> ControllerStorage { get; } = new ConcurrentDictionary<Type, IdentityAttributeCollection>();
        internal static ConcurrentDictionary<(Type, String?), IdentityAttributeCollection> FastStorage { get; } = new ConcurrentDictionary<(Type, String?), IdentityAttributeCollection>();
        internal static ConcurrentDictionary<MethodInfo, IdentityAttributeCollection> RealStorage { get; } = new ConcurrentDictionary<MethodInfo, IdentityAttributeCollection>();
        internal static IdentityAttributeCollection NoIdentity { get; } =  new IdentityAttributeCollection(new NoIdentityAttribute());
        internal static IdentityAttributeCollection AuthorizeIdentity { get; } =  new IdentityAttributeCollection(new AuthorizeIdentityAttribute());
    }
}