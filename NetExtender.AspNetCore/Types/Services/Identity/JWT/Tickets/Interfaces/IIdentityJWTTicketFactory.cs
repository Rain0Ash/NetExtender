using System;
using System.Security.Principal;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityJWTTicketFactory<out TId, out TUser, TRole, in TIdentity> : IIdentityJWTTicketFactory<TId, TUser, TRole>, ITicketFactory<TIdentity> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity
    {
    }
    
    public interface IIdentityJWTTicketFactory<out TId, out TUser, TRole> : ITicketFactory, IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}