// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Types.Monads.Result;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Identity
{
    public abstract class IdentityJWTService<TId, TUser, TRole, TResponse> : IJWTIdentityService<TId, TUser, TRole, TResponse> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        protected IIdentityJWTSecret<TId, TUser, TRole> Secret { get; }
        protected IIdentityJWTEncoder<TId, TUser, TRole> Encoder { get; }
        protected IIdentityJWTDecoder<TId, TUser, TRole> Decoder { get; }

        protected IdentityJWTService(IIdentityJWTSecret<TId, TUser, TRole> secret, IIdentityJWTEncoder<TId, TUser, TRole> encoder, IIdentityJWTDecoder<TId, TUser, TRole> decoder)
        {
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
            Encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
            Decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
        }

        protected virtual BusinessResult<TResponse> Convert(String jwt)
        {
            if (jwt is null)
            {
                throw new ArgumentNullException(nameof(jwt));
            }

            return JsonConvert.DeserializeObject<TResponse>(jwt) ?? throw new NotSupportedException($"Can't deserialize JWT to '{typeof(TResponse).Name}'.");
        }

        public BusinessResult<TResponse> JWT(ReadOnlySpan<Char> jwt)
        {
            BusinessResult<String> token = Token(jwt);
            return token ? Convert(token) : token.Exception;
        }

        public BusinessResult<TResponse> JWT(String jwt)
        {
            BusinessResult<String> token = Token(jwt);
            return token ? Convert(token) : token.Exception;
        }

        public BusinessResult<TResponse> JWT(JWTToken jwt)
        {
            BusinessResult<String> token = Token(jwt);
            return token ? Convert(token) : token.Exception;
        }

        public BusinessResult<TResponse> JWT(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            BusinessResult<String> token = Token(context);
            return token ? JWT(token.Value) : token.Exception;
        }

        protected virtual BusinessResult<String> Token(ReadOnlySpan<Char> jwt)
        {
            return JWTToken.TryParse(jwt, out JWTToken token) ? Token(token) : jwt.Length <= 0 ? Token(default(JWTToken)) : new IdentityTokenBadFormatException();
        }

        protected virtual BusinessResult<String> Token(String jwt)
        {
            return Token((ReadOnlySpan<Char>) jwt);
        }

        protected virtual BusinessResult<String> Token(JWTToken jwt)
        {
            if (jwt.IsEmpty)
            {
                return new IdentityNoTokenException();
            }

            if (jwt.Scheme != JWTScheme.Bearer)
            {
                return new IdentityTokenInvalidSchemeException();
            }
            
            try
            {
                JWTKey secret = Secret.Key;
                return Decoder.Decode(jwt, secret);
            }
            catch (JWTNotYetValidException)
            {
                return new IdentityTokenNotYetValidException();
            }
            catch (JWTExpiredException)
            {
                return new IdentityTokenExpiredException();
            }
            catch (JWTVerifyException)
            {
                return new IdentityTokenVerifyException();
            }
        }

        protected abstract BusinessResult<String> Token(HttpContext context);

        public virtual String CreateAccessToken(TUser user, DateTime expire)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            return Encoder.Encode(Secret.Key, new IdentityJWTPayload<TId, TUser, TRole>(user, expire));
        }

        public virtual String CreateRefreshToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}