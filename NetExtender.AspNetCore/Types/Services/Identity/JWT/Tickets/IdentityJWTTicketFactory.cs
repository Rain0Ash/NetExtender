using System;
using System.Security.Principal;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTTicketFactory<TId, TUser, TRole> : IdentityJWTTicketFactory<TId, TUser, TRole, IIdentity> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
    
    public class IdentityJWTTicketFactory<TId, TUser, TRole, TIdentity> : TicketFactory<TIdentity>, IIdentityJWTTicketFactory<TId, TUser, TRole, TIdentity> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TIdentity : IIdentity
    {
    }
}