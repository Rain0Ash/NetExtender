// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    internal interface IUnsafeIdentityUserService<TId, TUser, TRole> : IIdentityUserService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public ImmutableHashSet<TRole> NoUserRole { get; }

        public void Set(TId? id);
        public void Set(TUser? user);
    }

    public interface IIdentityUserService<TId, TUser, TRole> : IIdentityUserService, IEntityId<TId?>, IIdentityService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        internal IUnsafeIdentityUserService<TId, TUser, TRole>? Unsafe
        {
            get
            {
                return this as IUnsafeIdentityUserService<TId, TUser, TRole>;
            }
        }
        
        public TUser? User { get; }
        public IReadOnlySet<TRole> Roles { get; }

        public TUser New();
        
        public Boolean HasAccess(TRole? role);
        public Boolean HasAccess(params TRole[]? roles);
        public TUser? Find();
        public TUser? Find(TId id);
        public ValueTask<TUser?> FindAsync();
        public ValueTask<TUser?> FindAsync(CancellationToken token);
        public ValueTask<TUser?> FindAsync(TId id);
        public ValueTask<TUser?> FindAsync(TId id, CancellationToken token);
        public TUser? Find(String login);
        public ValueTask<TUser?> FindAsync(String login);
        public ValueTask<TUser?> FindAsync(String login, CancellationToken token);
        public TUser? Find(MailAddress address);
        public ValueTask<TUser?> FindAsync(MailAddress address);
        public ValueTask<TUser?> FindAsync(MailAddress address, CancellationToken token);
        public TUser? FromContext(HttpContext context);
    }
    
    public interface IIdentityUserService
    {
    }
}