using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityJWTSecret<TId, TUser, TRole> : IIdentityJWTSecret<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IJWTSecret Internal { get; }

        public JWTKey Key
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Key;
            }
        }

        public JWTKeys Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Keys;
            }
        }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        public IdentityJWTSecret(IJWTSecret secret)
        {
            Internal = secret ?? throw new ArgumentNullException(nameof(secret));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKeys IJWTSecret.Get()
        {
            return Internal.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKey IGetter<JWTKey>.Get()
        {
            return ((IGetter<JWTKey>) Internal).Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKeys IGetter<JWTKeys>.Get()
        {
            return ((IGetter<JWTKeys>) Internal).Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(JWTKey other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(JWTKeys other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IJWTSecret? other)
        {
            return Internal.Equals(other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<JWTKey> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public sealed class IdentityJWTSecret<TId, TUser, TRole, TSecret> : IIdentityJWTSecret<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : IJWTSecret, new()
    {
        private TSecret Internal { get; } = new TSecret();

        public JWTKey Key
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Key;
            }
        }

        public JWTKeys Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Keys;
            }
        }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKeys IJWTSecret.Get()
        {
            return Internal.Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKey IGetter<JWTKey>.Get()
        {
            return ((IGetter<JWTKey>) Internal).Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        JWTKeys IGetter<JWTKeys>.Get()
        {
            return ((IGetter<JWTKeys>) Internal).Get();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(JWTKey other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(JWTKeys other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(IJWTSecret? other)
        {
            return Internal.Equals(other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<JWTKey> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}