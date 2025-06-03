// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Phone;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IUser<out TId, TRole> : IUserInfo<TId, TRole> where TId : struct, IEquatable<TId> where TRole : IEquatable<TRole>
    {
        [ProtectedPersonalData]
        public new String? Login { get; set; }
        
        [PersonalData]
        public new String? UserName { get; set; }
        
        [ProtectedPersonalData]
        public new MailAddress? Email { get; set; }

        public new String? SecurityStamp { get; set; }

        [ProtectedPersonalData]
        public new Phone? Phone { get; set; }

        public new ISet<TRole> Roles { get; }

        [PersonalData]
        public new IdentityUser.UserConfirmation Confirmation { get; }
        
        [PersonalData]
        public new IdentityUser.UserRestriction Restriction { get; }
        
        [ProtectedPersonalData]
        public new IdentityUser.UserPassword Password { get; }
    }

    public interface IUserInfo<out TId, TRole> : IUserInfo, IEntityId<TId> where TId : struct, IEquatable<TId> where TRole : IEquatable<TRole>
    {
        public new TId Id { get; }
        public IReadOnlySet<TRole> Roles { get; }
    }

    internal interface IUnsafeUserInfo : IUserInfo
    {
        [PersonalData]
        public Boolean IsConfirm { get; set; }
        
        [PersonalData]
        public Boolean EmailConfirmed { get; set; }

        [PersonalData]
        public Boolean PhoneNumberConfirmed { get; set; }

        [PersonalData]
        public Boolean TwoFactorEnabled { get; set; }

        public DateTimeOffset? Lockout { get; set; }
        public Boolean IsLockout { get; }
        public Int32 AccessFails { get; set; }

        // TODO : Banned
        [ProtectedPersonalData]
        public new String? Password { get; set; }
    }

    public interface IUserInfo
    {
        public IIdentityUserService? Service
        {
            get
            {
                return null;
            }
            [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
            init
            {
            }
        }

        [PersonalData]
        public Object Id { get; }
        
        [ProtectedPersonalData]
        public String? Login { get; }
        
        [PersonalData]
        public String? UserName { get; }
 
        [ProtectedPersonalData]
        public MailAddress? Email { get; }

        [ProtectedPersonalData]
        public Phone? Phone { get; }

        [PersonalData]
        public IdentityUser.Confirmation Confirmation { get; }

        [PersonalData]
        public IdentityUser.Restriction Restriction { get; }

        [ProtectedPersonalData]
        public IdentityUser.Password Password { get; }
        
        [PersonalData]
        public String? SecurityStamp { get; }

        [PersonalData]
        public String? ConcurrencyStamp { get; }
    }
}