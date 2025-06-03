// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net.Mail;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Phone;
using NetExtender.Types.Strings;

namespace NetExtender.AspNetCore.Identity
{
    public abstract class IdentityUser<TUser, TRole, TService> : IdentityUser<TRole>, IUserInfo where TRole : IEquatable<TRole> where TUser : IdentityUser<StringId, TUser, TRole, TService> where TService : class, IIdentityUserService<StringId, TUser, TRole>
    {
        protected internal TService Service { get; init; }

        IIdentityUserService? IUserInfo.Service
        {
            get
            {
                return Service;
            }
            init
            {
                Service = value switch
                {
                    null => throw new ArgumentNullException(nameof(Service)),
                    TService service => service,
                    _ => throw new ArgumentException($"Service must be of type '{typeof(TService).Name}'.", nameof(value))
                };
            }
        }

        protected IdentityUser()
        {
            Service = null!;
        }

        protected IdentityUser(TService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }

    public class IdentityUser<TRole> : Microsoft.AspNetCore.Identity.IdentityUser, IUser<StringId, TRole>, IUnsafeUserInfo where TRole : IEquatable<TRole>
    {
        Object IUserInfo.Id
        {
            get
            {
                return Id;
            }
        }
        
        public new StringId Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }

        public virtual String? Login
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
        
        public HashSet<TRole> Roles { get; } = new HashSet<TRole>();

        IReadOnlySet<TRole> IUserInfo<StringId, TRole>.Roles
        {
            get
            {
                return Roles;
            }
        }

        ISet<TRole> IUser<StringId, TRole>.Roles
        {
            get
            {
                return Roles;
            }
        }
        
        public new MailAddress? Email
        {
            get
            {
                return base.Email is { } email ? new MailAddress(email) : null;
            }
            set
            {
                base.Email = value?.Address;
            }
        }

        public Phone? Phone
        {
            get
            {
                return PhoneNumber is not null ? NetExtender.Types.Phone.Phone.Parse(PhoneNumber) : null;
            }
            set
            {
                PhoneNumber = value?.ToString();
            }
        }

        public virtual Boolean IsConfirm
        {
            get
            {
                return EmailConfirmed;
            }
            set
            {
                EmailConfirmed = value;
            }
        }

        public IdentityUser.UserConfirmation Confirmation
        {
            get
            {
                return new IdentityUser.UserConfirmation(this);
            }
        }

        IdentityUser.Confirmation IUserInfo.Confirmation
        {
            get
            {
                return Confirmation;
            }
        }

        String? IUnsafeUserInfo.Password
        {
            get
            {
                return PasswordHash;
            }
            set
            {
                PasswordHash = value;
            }
        }

        public IdentityUser.UserPassword Password
        {
            get
            {
                return new IdentityUser.UserPassword(this);
            }
        }

        IdentityUser.Password IUserInfo.Password
        {
            get
            {
                return Password;
            }
        }

        DateTimeOffset? IUnsafeUserInfo.Lockout
        {
            get
            {
                return LockoutEnd;
            }
            set
            {
                LockoutEnd = value;
            }
        }

        Boolean IUnsafeUserInfo.IsLockout
        {
            get
            {
                return LockoutEnabled;
            }
        }

        Int32 IUnsafeUserInfo.AccessFails
        {
            get
            {
                return AccessFailedCount;
            }
            set
            {
                AccessFailedCount = value;
            }
        }

        public IdentityUser.UserRestriction Restriction
        {
            get
            {
                return new IdentityUser.UserRestriction(this);
            }
        }

        IdentityUser.Restriction IUserInfo.Restriction
        {
            get
            {
                return Restriction;
            }
        }

        StringId IEntity<StringId>.Get()
        {
            return Id;
        }
    }

    public abstract class IdentityUser<TId, TUser, TRole, TService> : IdentityUser<TId, TRole>, IUserInfo where TId : struct, IEquatable<TId> where TUser : IdentityUser<TId, TUser, TRole, TService> where TRole : IEquatable<TRole> where TService : class, IIdentityUserService<TId, TUser, TRole>
    {
        protected internal TService Service { get; init; }

        IIdentityUserService? IUserInfo.Service
        {
            get
            {
                return Service;
            }
            init
            {
                Service = value switch
                {
                    null => throw new ArgumentNullException(nameof(Service)),
                    TService service => service,
                    _ => throw new ArgumentException($"Service must be of type '{typeof(TService).Name}'.", nameof(value))
                };
            }
        }

        protected IdentityUser()
        {
            Service = null!;
        }
        
        protected IdentityUser(TService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }

    public class IdentityUser<TId, TRole> : Microsoft.AspNetCore.Identity.IdentityUser<TId>, IUser<TId, TRole>, IUnsafeUserInfo where TId : struct, IEquatable<TId> where TRole : IEquatable<TRole>
    {
        Object IUserInfo.Id
        {
            get
            {
                return Id;
            }
        }

        public virtual String? Login
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
        
        public HashSet<TRole> Roles { get; } = new HashSet<TRole>();

        IReadOnlySet<TRole> IUserInfo<TId, TRole>.Roles
        {
            get
            {
                return Roles;
            }
        }

        ISet<TRole> IUser<TId, TRole>.Roles
        {
            get
            {
                return Roles;
            }
        }
        
        public new MailAddress? Email
        {
            get
            {
                return base.Email is { } email ? new MailAddress(email) : null;
            }
            set
            {
                base.Email = value?.Address;
            }
        }

        public Phone? Phone
        {
            get
            {
                return PhoneNumber is not null ? NetExtender.Types.Phone.Phone.Parse(PhoneNumber) : null;
            }
            set
            {
                PhoneNumber = value?.ToString();
            }
        }

        public virtual Boolean IsConfirm
        {
            get
            {
                return EmailConfirmed;
            }
            set
            {
                EmailConfirmed = value;
            }
        }

        public IdentityUser.UserConfirmation Confirmation
        {
            get
            {
                return new IdentityUser.UserConfirmation(this);
            }
        }

        IdentityUser.Confirmation IUserInfo.Confirmation
        {
            get
            {
                return Confirmation;
            }
        }

        String? IUnsafeUserInfo.Password
        {
            get
            {
                return PasswordHash;
            }
            set
            {
                PasswordHash = value;
            }
        }

        public IdentityUser.UserPassword Password
        {
            get
            {
                return new IdentityUser.UserPassword(this);
            }
        }

        IdentityUser.Password IUserInfo.Password
        {
            get
            {
                return Password;
            }
        }

        DateTimeOffset? IUnsafeUserInfo.Lockout
        {
            get
            {
                return LockoutEnd;
            }
            set
            {
                LockoutEnd = value;
            }
        }

        Boolean IUnsafeUserInfo.IsLockout
        {
            get
            {
                return LockoutEnabled;
            }
        }

        Int32 IUnsafeUserInfo.AccessFails
        {
            get
            {
                return AccessFailedCount;
            }
            set
            {
                AccessFailedCount = value;
            }
        }

        public IdentityUser.UserRestriction Restriction
        {
            get
            {
                return new IdentityUser.UserRestriction(this);
            }
        }

        IdentityUser.Restriction IUserInfo.Restriction
        {
            get
            {
                return Restriction;
            }
        }
        
        TId IEntity<TId>.Get()
        {
            return Id;
        }
    }
}