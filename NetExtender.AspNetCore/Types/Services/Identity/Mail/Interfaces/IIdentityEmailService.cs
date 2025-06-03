// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityEmailService<out TId, TUser, TRole> : IIdentityService<TId, TUser, TRole>
#if NET8_0_OR_GREATER
        , Microsoft.AspNetCore.Identity.IEmailSender<TUser>
#endif
        where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
#if !NET8_0_OR_GREATER
        public Task SendConfirmationLinkAsync(TUser user, String email, String link);
        public Task SendPasswordResetLinkAsync(TUser user, String email, String link);
        public Task SendPasswordResetCodeAsync(TUser user, String email, String code);
#endif
    }
}