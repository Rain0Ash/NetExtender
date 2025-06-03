// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Algorithms.Interfaces;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityJWTAsymmetricAlgorithm<out TId, out TUser, TRole> : IIdentityJWTAlgorithm<TId, TUser, TRole>, IJWTAsymmetricAlgorithm where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}