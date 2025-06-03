// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTValidator<TId, TUser, TRole> : JWTValidator, IIdentityJWTValidator<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTValidator(IIdentityJWTUrlEncoder<TId, TUser, TRole> url, IIdentityJWTTimeService<TId, TUser, TRole> time, IIdentityJWTSerializer<TId, TUser, TRole> serializer, IdentityJWTValidationInfo<TId, TUser, TRole>? validation)
            : base(url ?? throw new ArgumentNullException(nameof(url)), time, serializer, validation)
        {
        }
    }
}