// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTAlgorithmDecoder<TId, TUser, TRole> : JWTDecoder.Decoder, IIdentityJWTDecoder<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTAlgorithmDecoder(IIdentityJWTUrlEncoder<TId, TUser, TRole> url, IIdentityJWTAlgorithm<TId, TUser, TRole> algorithm, IIdentityJWTSerializer<TId, TUser, TRole> serializer, IIdentityJWTValidator<TId, TUser, TRole>? validator)
            : base(url, algorithm, serializer, validator)
        {
        }
    }

    public class IdentityJWTAlgorithmFactoryDecoder<TId, TUser, TRole> : JWTDecoder.Decoder, IIdentityJWTDecoder<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTAlgorithmFactoryDecoder(IIdentityJWTUrlEncoder<TId, TUser, TRole> url, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> algorithm, IIdentityJWTSerializer<TId, TUser, TRole> serializer, IIdentityJWTValidator<TId, TUser, TRole>? validator)
            : base(url, algorithm, serializer, validator)
        {
        }
    }
}