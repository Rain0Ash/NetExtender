using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityJWTIdentityFactory<out TId, out TUser, TRole, out TIdentity, in TPayload> : IIdentityJWTIdentityFactory<TId, TUser, TRole, TIdentity>, IIdentityFactory<TIdentity, TPayload> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity where TPayload : IEnumerable<KeyValuePair<String, Object?>>
    {
    }
    
    public interface IIdentityJWTIdentityFactory<out TId, out TUser, TRole, out TIdentity> : IIdentityJWTIdentityFactory<TId, TUser, TRole>, IIdentityFactory<TIdentity> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity
    {
    }
    
    public interface IIdentityJWTIdentityFactory<out TId, out TUser, TRole> : IIdentityFactory, IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}