// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity
{
    public class IdentityJWTUrlEncoder<TId, TUser, TRole> : JWTUrlEncoder, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}