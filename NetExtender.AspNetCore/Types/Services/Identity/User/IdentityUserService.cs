// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public abstract class IdentityUserService<TId, TUser, TRole> : IUnsafeIdentityUserService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
    {
        private ImmutableHashSet<TRole> NoUserRole { get; }
        
        ImmutableHashSet<TRole> IUnsafeIdentityUserService<TId, TUser, TRole>.NoUserRole
        {
            get
            {
                return NoUserRole;
            }
        }

        public virtual TId? Id
        {
            get
            {
                return User?.Id;
            }
            protected set
            {
                User = value is { } id ? Find(id) : null;
            }
        }

        public abstract TUser? User { get; protected set; }

        public IReadOnlySet<TRole> Roles
        {
            get
            {
                return User?.Roles ?? NoUserRole;
            }
        }

        protected IdentityUserService()
            : this(null)
        {
        }

        protected IdentityUserService(TRole? @default)
            : this(@default is not null ? ImmutableHashSet<TRole>.Empty.Add(@default) : null)
        {
        }

        private protected IdentityUserService(ImmutableHashSet<TRole>? @default)
        {
            NoUserRole = @default ?? ImmutableHashSet<TRole>.Empty;
        }

        void IUnsafeIdentityUserService<TId, TUser, TRole>.Set(TId? id)
        {
            Id = id;
        }

        void IUnsafeIdentityUserService<TId, TUser, TRole>.Set(TUser? user)
        {
            User = user;
        }

        TId? IEntity<TId?>.Get()
        {
            return Id;
        }

        public virtual TUser New()
        {
            return new TUser { Service = this };
        }
        
        public virtual Boolean HasAccess(TRole? role)
        {
            return role is not null ? Roles.Contains(role) : ReferenceEquals(Roles, NoUserRole);
        }

        public virtual Boolean HasAccess(params TRole[]? roles)
        {
            return roles is { Length: > 0 } ? roles.Any(HasAccess) : ReferenceEquals(Roles, NoUserRole);
        }

        public TUser? Find()
        {
            return Id is { } id ? Find(id) : null;
        }

        public abstract TUser? Find(TId id);

        public ValueTask<TUser?> FindAsync(TId id)
        {
            return FindAsync(id, CancellationToken.None);
        }

        public ValueTask<TUser?> FindAsync()
        {
            return FindAsync(CancellationToken.None);
        }

        public ValueTask<TUser?> FindAsync(CancellationToken token)
        {
            return Id is { } id ? FindAsync(id, token) : ValueTask.FromResult(default(TUser));
        }
        
        public abstract ValueTask<TUser?> FindAsync(TId id, CancellationToken token);
        
        public abstract TUser? Find(String login);

        public ValueTask<TUser?> FindAsync(String login)
        {
            return FindAsync(login, CancellationToken.None);
        }
        
        public abstract ValueTask<TUser?> FindAsync(String login, CancellationToken token);
        
        public abstract TUser? Find(MailAddress address);

        public ValueTask<TUser?> FindAsync(MailAddress address)
        {
            return FindAsync(address, CancellationToken.None);
        }
        
        public abstract ValueTask<TUser?> FindAsync(MailAddress address, CancellationToken token);

        public abstract TUser? FromContext(HttpContext context);
    }
}