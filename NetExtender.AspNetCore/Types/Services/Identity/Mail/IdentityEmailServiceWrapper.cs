// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading.Tasks;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityEmailServiceWrapper<TId, TUser, TRole> : IIdentityEmailService<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IEmailSender<TUser> Sender { get; }
        
        public IdentityEmailServiceWrapper(IEmailSender<TUser> sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        public Task SendConfirmationLinkAsync(TUser user, String email, String link)
        {
            return Sender.SendConfirmationLinkAsync(user, email, link);
        }

        public Task SendPasswordResetLinkAsync(TUser user, String email, String link)
        {
            return Sender.SendPasswordResetLinkAsync(user, email, link);
        }

        public Task SendPasswordResetCodeAsync(TUser user, String email, String code)
        {
            return Sender.SendPasswordResetCodeAsync(user, email, code);
        }
    }
}
#endif