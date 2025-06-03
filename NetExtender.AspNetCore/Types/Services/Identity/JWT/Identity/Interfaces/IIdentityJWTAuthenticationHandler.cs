using System;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityJWTAuthenticationHandler<out TId, out TUser, TRole> : IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}