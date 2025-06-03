// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Http;
using NetExtender.JWT;
using NetExtender.Types.Monads.Result;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IJWTIdentityService<out TId, in TUser, TRole, TResponse> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public BusinessResult<TResponse> JWT(ReadOnlySpan<Char> jwt);
        public BusinessResult<TResponse> JWT(String jwt);
        public BusinessResult<TResponse> JWT(JWTToken jwt);
        public BusinessResult<TResponse> JWT(HttpContext context);
        public String CreateAccessToken(TUser user, DateTime expire);
    }
}