// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.Identities
{
    public sealed class DefaultIdentityUser<TUser, TKey> : ClaimsPrincipal where TKey : IEquatable<TKey> where TUser : IdentityUser<TKey>
    {
        public HttpContext HttpContext { get; }

        public UserManager<TUser> Manager { get; }

        private TUser? _current;

        public new TUser? Current
        {
            get
            {
                if (_current is not null)
                {
                    return _current;
                }

                if (!IsSignedIn)
                {
                    return null;
                }

                UserManager<TUser> manager = HttpContext.RequestServices.GetRequiredService<UserManager<TUser>>();

                String? id = manager.GetUserId(HttpContext.User);
                TKey uid = typeof(TKey) == typeof(Guid) ? (TKey) (Object) Guid.Parse(id) : (TKey) Convert.ChangeType(id, typeof(TKey));

                try
                {
                    _current = manager.Users.Single(user => user.Id.Equals(uid));
                    return _current;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Boolean IsSignedIn
        {
            get
            {
                return HttpContext.User?.Identity?.IsAuthenticated ?? false;
            }
        }

        public DefaultIdentityUser(IHttpContextAccessor accessor, UserManager<TUser> manager)
        {
            HttpContext = accessor?.HttpContext ?? throw new ArgumentNullException(nameof(accessor));
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
            AddIdentity(new ClaimsIdentity(HttpContext.User.Identity));
            AddIdentities(HttpContext.User.Identities);
        }
    }
}