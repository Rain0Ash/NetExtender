using System;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Times;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityJWTTimeService<TId, TUser, TRole> : TimeProviderAdapter, IIdentityJWTTimeService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}