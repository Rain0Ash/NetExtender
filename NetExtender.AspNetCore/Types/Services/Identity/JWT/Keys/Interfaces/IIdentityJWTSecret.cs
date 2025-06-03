using System;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityJWTSecret<out TId, out TUser, TRole> : IJWTSecret, IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}