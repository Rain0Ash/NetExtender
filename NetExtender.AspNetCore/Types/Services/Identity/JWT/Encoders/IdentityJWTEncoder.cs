// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTAlgorithmEncoder<TId, TUser, TRole> : JWTEncoder, IIdentityJWTEncoder<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTAlgorithmEncoder(IIdentityJWTUrlEncoder<TId, TUser, TRole> encoder, IIdentityJWTAlgorithm<TId, TUser, TRole> algorithm, IIdentityJWTSerializer<TId, TUser, TRole> serializer)
            : base(encoder, algorithm, serializer)
        {
        }
    }
    
    public class IdentityJWTAlgorithmFactoryEncoder<TId, TUser, TRole> : JWTEncoder, IIdentityJWTEncoder<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityJWTAlgorithmFactoryEncoder(IIdentityJWTUrlEncoder<TId, TUser, TRole> encoder, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> algorithm, IIdentityJWTSerializer<TId, TUser, TRole> serializer)
            : base(encoder, algorithm, serializer)
        {
        }
    }
}