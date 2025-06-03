// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    public static class JWTBuilderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder AddClaim(this JWT.Algorithms.JWT.Builder builder, JWTClaim claim, Object value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (claim.GetDescription() is not { } name)
            {
                throw new EnumUndefinedOrNotSupportedException<JWTClaim>(claim, nameof(claim), null);
            }

            return builder.AddClaim(name, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder AddClaim<T>(this JWT.Algorithms.JWT.Builder builder, JWTClaim claim, T value) where T : notnull
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AddClaim(builder, claim, (Object) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder AddClaim<T>(this JWT.Algorithms.JWT.Builder builder, String claim, T value) where T : notnull
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (claim is null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return builder.AddClaim(claim, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder AddClaims(this JWT.Algorithms.JWT.Builder builder, IEnumerable<KeyValuePair<String, Object>> claims)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (claims is null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            return claims.Aggregate(builder, static (builder, claim) => builder.AddClaim(claim.Key, claim.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder ExpirationTime(this JWT.Algorithms.JWT.Builder builder, Int64 time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.ExpirationTime, time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder ExpirationTime(this JWT.Algorithms.JWT.Builder builder, DateTime time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.ExpirationTime, Math.Round(time.SinceEpoch().TotalSeconds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Issuer(this JWT.Algorithms.JWT.Builder builder, String issuer)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (issuer is null)
            {
                throw new ArgumentNullException(nameof(issuer));
            }

            return builder.AddClaim(JWTClaim.Issuer, issuer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Subject(this JWT.Algorithms.JWT.Builder builder, String subject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (subject is null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            return builder.AddClaim(JWTClaim.Subject, subject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Audience(this JWT.Algorithms.JWT.Builder builder, String audience)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (audience is null)
            {
                throw new ArgumentNullException(nameof(audience));
            }

            return builder.AddClaim(JWTClaim.Audience, audience);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder NotBefore(this JWT.Algorithms.JWT.Builder builder, DateTime time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.NotBefore, Math.Round(time.SinceEpoch().TotalSeconds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder NotBefore(this JWT.Algorithms.JWT.Builder builder, Int64 time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.NotBefore, time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder IssuedAt(this JWT.Algorithms.JWT.Builder builder, Int64 time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.IssuedAt, time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder IssuedAt(this JWT.Algorithms.JWT.Builder builder, DateTime time)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddClaim(JWTClaim.IssuedAt, Math.Round(time.SinceEpoch().TotalSeconds));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Id(this JWT.Algorithms.JWT.Builder builder, Guid id)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Id(id.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Id(this JWT.Algorithms.JWT.Builder builder, Int64 id)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Id(id.ToString(CultureInfo.InvariantCulture));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder Id(this JWT.Algorithms.JWT.Builder builder, String id)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return builder.AddClaim(JWTClaim.JwtId, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder GivenName(this JWT.Algorithms.JWT.Builder builder, String name)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.AddClaim(JWTClaim.GivenName, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder FamilyName(this JWT.Algorithms.JWT.Builder builder, String name)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.AddClaim(JWTClaim.FamilyName, name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JWT.Algorithms.JWT.Builder MiddleName(this JWT.Algorithms.JWT.Builder builder, String name)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return builder.AddClaim(JWTClaim.MiddleName, name);
        }
    }
}
