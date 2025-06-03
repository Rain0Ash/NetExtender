// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    public readonly struct JWTKey : IEquatableStruct<JWTKey>, IJWTSecret, IEquatable<Memory<Byte>>, IEquatable<ReadOnlyMemory<Byte>>, IEquatable<IEnumerable<Byte>>, IEquatable<Byte[]>
    {
        public static implicit operator ReadOnlySpan<Byte>(JWTKey value)
        {
            return value.Key.Span;
        }
        
        public static implicit operator ReadOnlyMemory<Byte>(JWTKey value)
        {
            return value.Key;
        }

        public static explicit operator Byte[](JWTKey value)
        {
            return value.Array ?? value.Key.ToArray();
        }

        public static implicit operator JWTKey(Memory<Byte> value)
        {
            return new JWTKey(value);
        }

        public static implicit operator JWTKey(ReadOnlyMemory<Byte> value)
        {
            return new JWTKey(value);
        }

        public static implicit operator JWTKey(Byte[]? value)
        {
            return value is not null ? new JWTKey(value) : default;
        }

        public static implicit operator JWTKey(String? value)
        {
            return !String.IsNullOrEmpty(value) ? Storage.GetValue(value, static key => new JWTKey(key).Array!) : default;
        }

        public static Boolean operator ==(JWTKey first, JWTKey second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(JWTKey first, JWTKey second)
        {
            return !(first == second);
        }
        
        private static ConditionalWeakTable<String, Byte[]> Storage { get; } = new ConditionalWeakTable<String, Byte[]>();

        private ReadOnlyMemory<Byte> Key { get; }

        JWTKey IJWTSecret.Key
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this;
            }
        }

        JWTKeys IJWTSecret.Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new JWTKeys(this);
            }
        }

        private Byte[]? Array { get; }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return IsEmpty ? 0 : 1;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Key.IsEmpty;
            }
        }

        public JWTKey(Memory<Byte> key)
            : this((ReadOnlyMemory<Byte>) key)
        {
        }

        public JWTKey(ReadOnlyMemory<Byte> key)
        {
            Key = key;
            Array = null;
        }

        public JWTKey(Byte[] key)
            : this(key is not null ? (ReadOnlyMemory<Byte>) key : throw new ArgumentNullException(nameof(key)))
        {
            Array = key;
        }

        public JWTKey(String key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            Array = Algorithms.JWT.Encoding.GetBytes(key);
            Key = Array;
        }

        JWTKey IGetter<JWTKey>.Get()
        {
            return this;
        }

        JWTKeys IGetter<JWTKeys>.Get()
        {
            return new JWTKeys(this);
        }

        JWTKeys IJWTSecret.Get()
        {
            return new JWTKeys(this);
        }

        public override Int32 GetHashCode()
        {
            if (Key.IsEmpty)
            {
                return 0;
            }

            HashCode code = new HashCode();
            code.AddBytes(Key.Span);
            return code.ToHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                JWTKeys keys => Equals(keys),
                JWTKey key => Equals(key),
                Byte[] key => Equals(key),
                IEnumerable<Byte> key => Equals(key),
                Memory<Byte> key => Equals(key),
                ReadOnlyMemory<Byte> key => Equals(key),
                IJWTSecret key => Equals(key),
                _ => false
            };
        }

        public Boolean Equals(Memory<Byte> other)
        {
            return Equals(other.Span);
        }

        public Boolean Equals(Span<Byte> other)
        {
            return Key.SequenceEqual(other);
        }

        public Boolean Equals(ReadOnlyMemory<Byte> other)
        {
            return Equals(other.Span);
        }

        public Boolean Equals(ReadOnlySpan<Byte> other)
        {
            return Key.SequenceEqual(other);
        }

        public Boolean Equals(IEnumerable<Byte>? other)
        {
            return Key.SequenceEqual(other);
        }

        public Boolean Equals(Byte[]? other)
        {
            return Equals((ReadOnlySpan<Byte>) other);
        }

        public Boolean Equals(JWTKey other)
        {
            return Key.SequenceEqual(other.Key);
        }

        public Boolean Equals(JWTKeys other)
        {
            return other.Has(this);
        }

        public Boolean Equals(IJWTSecret? other)
        {
            return other is not null ? Equals(other.Keys) : IsEmpty;
        }

        public override String? ToString()
        {
            return !Key.IsEmpty ? Convert.ToBase64String(Key.Span) : default;
        }

        IEnumerator<JWTKey> IEnumerable<JWTKey>.GetEnumerator()
        {
            if (!IsEmpty)
            {
                yield return this;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (!IsEmpty)
            {
                yield return this;
            }
        }
    }
}