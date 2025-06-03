// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityTimeService<out TId, out TUser, TRole> : IIdentityService<TId, TUser, TRole>, ITimeProvider where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
    }
}