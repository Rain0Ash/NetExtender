// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public readonly struct JWTKeys : IEquatableStruct<JWTKeys>, IJWTSecret, IEquatable<Memory<Byte>>, IEquatable<ReadOnlyMemory<Byte>>, IEquatable<Byte[]>
    {
        public static explicit operator JWTKey(JWTKeys value)
        {
            return value.Primary;
        }
        
        public static implicit operator ImmutableHashSet<JWTKey>?(JWTKeys value)
        {
            return value.Keys;
        }

        public static implicit operator JWTKeys(Memory<Byte>[]? value)
        {
            return value is not null ? new JWTKeys(value) : default;
        }

        public static implicit operator JWTKeys(ReadOnlyMemory<Byte>[]? value)
        {
            return value is not null ? new JWTKeys(value) : default;
        }

        public static implicit operator JWTKeys(Byte[]?[]? value)
        {
            return value is not null ? new JWTKeys(value) : default;
        }

        public static implicit operator JWTKeys(String?[]? value)
        {
            return value is not null ? new JWTKeys(value) : default;
        }

        public static implicit operator JWTKeys(JWTKey[]? value)
        {
            return value is not null ? new JWTKeys(value) : default;
        }

        public static Boolean operator ==(JWTKeys first, JWTKeys second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(JWTKeys first, JWTKeys second)
        {
            return !(first == second);
        }

        public static JWTKeys operator +(JWTKeys first, JWTKey second)
        {
            return !second.IsEmpty ? first.With(second, first.Primary.IsEmpty) : first;
        }

        public static JWTKeys operator -(JWTKeys first, JWTKey second)
        {
            if (first.Keys is null || first.IsEmpty || !first.Keys.Contains(second))
            {
                return first;
            }

            JWTKeys result = new JWTKeys(first.Keys.Remove(second));
            return result.Has(first.Primary) ? result.SetPrimary(first.Primary) : result.SetPrimary();
        }

        public static JWTKeys operator +(JWTKeys first, JWTKeys second)
        {
            if (first.IsEmpty)
            {
                return second;
            }

            if (second.IsEmpty)
            {
                return first;
            }

            ImmutableHashSet<JWTKey> set = first.Keys ?? ImmutableHashSet<JWTKey>.Empty;
            return new JWTKeys(set.Union(second.Keys ?? ImmutableHashSet<JWTKey>.Empty), first.Primary.IsEmpty ? second.Primary : first.Primary);
        }

        public static JWTKeys operator -(JWTKeys first, JWTKeys second)
        {
            if (first.IsEmpty || second.IsEmpty)
            {
                return first;
            }

            ImmutableHashSet<JWTKey> set = first.Keys ?? ImmutableHashSet<JWTKey>.Empty;
            JWTKeys result = new JWTKeys(set.Except(second.Keys ?? ImmutableHashSet<JWTKey>.Empty));
            return result.Has(first.Primary) ? result.SetPrimary(first.Primary) : result.SetPrimary();
        }

        private ImmutableHashSet<JWTKey>? Keys { get; }

        JWTKeys IJWTSecret.Keys
        {
            get
            {
                return this;
            }
        }

        private readonly JWTKey _primary;
        public JWTKey Primary
        {
            get
            {
                return _primary;
            }
            init
            {
                _primary = Keys?.Contains(value) is true ? value : throw new ArgumentException("Keys must contains primary key.", nameof(value));
            }
        }

        public JWTKey Key
        {
            get
            {
                return Primary;
            }
        }

        public Int32 Count
        {
            get
            {
                if (Keys is null || Keys.IsEmpty)
                {
                    return 0;
                }
                
                Int32 count = 0;
                foreach (JWTKey key in Keys)
                {
                    if (!key.IsEmpty)
                    {
                        ++count;
                    }
                }
                
                return count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                if (Keys is null || Keys.IsEmpty)
                {
                    return true;
                }

                foreach (JWTKey key in Keys)
                {
                    if (!key.IsEmpty)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public JWTKeys(Byte[] key)
            : this(key is not null ? (JWTKey) key : throw new ArgumentNullException(nameof(key)))
        {
        }

        public JWTKeys(String key)
            : this(key is not null ? (JWTKey) key : throw new ArgumentNullException(nameof(key)))
        {
        }

        public JWTKeys(JWTKey key)
        {
            _primary = !key.IsEmpty ? key : default;
            Keys = ImmutableHashSet<JWTKey>.Empty.AddIf(_primary, !_primary.IsEmpty);
        }

        public JWTKeys(params Memory<Byte>[]? keys)
            : this((IEnumerable<Memory<Byte>>?) keys)
        {
        }

        public JWTKeys(IEnumerable<Memory<Byte>>? keys)
        {
            Keys = keys?.Select(static key => new JWTKey(key)).Where(static key => !key.IsEmpty).ToImmutableHashSet();
            _primary = Keys?.FirstOrDefault() ?? default;
        }

        public JWTKeys(params ReadOnlyMemory<Byte>[]? keys)
            : this((IEnumerable<ReadOnlyMemory<Byte>>?) keys)
        {
        }

        public JWTKeys(IEnumerable<ReadOnlyMemory<Byte>>? keys)
        {
            Keys = keys?.Select(static key => new JWTKey(key)).Where(static key => !key.IsEmpty).ToImmutableHashSet();
            _primary = Keys?.FirstOrDefault() ?? default;
        }

        public JWTKeys(params Byte[]?[]? keys)
            : this((IEnumerable<Byte[]?>?) keys)
        {
        }

        public JWTKeys(IEnumerable<Byte[]?>? keys)
        {
            Keys = keys?.WhereNotNull(static key => key is { Length: > 0 }).Select(static key => new JWTKey(key)).Where(static key => !key.IsEmpty).ToImmutableHashSet();
            _primary = Keys?.FirstOrDefault() ?? default;
        }

        public JWTKeys(params String?[]? keys)
            : this((IEnumerable<String?>?) keys)
        {
        }

        public JWTKeys(IEnumerable<String?>? keys)
        {
            Keys = keys?.WhereNotNull(static key => key is { Length: > 0 }).Select(static key => new JWTKey(key)).Where(static key => !key.IsEmpty).ToImmutableHashSet();
            _primary = Keys?.FirstOrDefault() ?? default;
        }

        public JWTKeys(params JWTKey[]? keys)
            : this((IEnumerable<JWTKey>?) keys)
        {
        }

        public JWTKeys(IEnumerable<JWTKey>? keys)
        {
            Keys = keys?.Where(static key => !key.IsEmpty).ToImmutableHashSet();
            _primary = Keys?.FirstOrDefault() ?? default;
        }

        private JWTKeys(ImmutableHashSet<JWTKey>? keys)
            : this(keys, default)
        {
        }

        private JWTKeys(ImmutableHashSet<JWTKey>? keys, JWTKey primary)
        {
            Keys = keys;
            _primary = primary;
        }

        JWTKey IGetter<JWTKey>.Get()
        {
            return Primary;
        }

        JWTKeys IGetter<JWTKeys>.Get()
        {
            return this;
        }

        JWTKeys IJWTSecret.Get()
        {
            return this;
        }

        public Boolean Has(JWTKey key)
        {
            return Equals(key);
        }

        public Boolean Has(JWTKeys keys)
        {
            if (Keys is null || keys.Keys is null)
            {
                return ReferenceEquals(Keys, keys.Keys);
            }

            return Keys.Intersect(keys.Keys).Count == keys.Keys.Count;
        }

        private JWTKeys SetPrimary()
        {
            return new JWTKeys(Keys, Keys?.FirstOrDefault() ?? default);
        }

        public JWTKeys SetPrimary(JWTKey key)
        {
            return new JWTKeys(Keys) { Primary = key };
        }

        public JWTKeys With(JWTKey key)
        {
            if (key.IsEmpty)
            {
                return this;
            }
            
            ImmutableHashSet<JWTKey> keys = Keys ?? ImmutableHashSet<JWTKey>.Empty;
            return new JWTKeys(keys.Add(key)) { Primary = Primary };
        }

        public JWTKeys With(JWTKey key, Boolean primary)
        {
            if (key.IsEmpty)
            {
                return this;
            }

            ImmutableHashSet<JWTKey> keys = Keys ?? ImmutableHashSet<JWTKey>.Empty;
            return new JWTKeys(keys.Add(key)) { Primary = primary ? key : Primary };
        }

        public override Int32 GetHashCode()
        {
            if (Keys is null)
            {
                return 0;
            }
            
            HashCode code = new HashCode();
            code.AddRange(Keys);
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
                Memory<Byte> key => Equals(key),
                ReadOnlyMemory<Byte> key => Equals(key),
                IJWTSecret key => Equals(key),
                _ => false
            };
        }

        public Boolean Equals(Memory<Byte> other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            return !other.IsEmpty ? Keys.Contains(other) : IsEmpty;
        }

        public Boolean Equals(Span<Byte> other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            if (other.IsEmpty)
            {
                return IsEmpty;
            }

            foreach (JWTKey key in Keys)
            {
                if (key.Equals(other))
                {
                    return true;
                }
            }
            
            return false;
        }

        public Boolean Equals(ReadOnlyMemory<Byte> other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            return !other.IsEmpty ? Keys.Contains(other) : IsEmpty;
        }

        public Boolean Equals(ReadOnlySpan<Byte> other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            if (other.IsEmpty)
            {
                return IsEmpty;
            }

            foreach (JWTKey key in Keys)
            {
                if (key.Equals(other))
                {
                    return true;
                }
            }
            
            return false;
        }

        public Boolean Equals(Byte[]? other)
        {
            if (Keys is null)
            {
                return other is null || other.Length <= 0;
            }

            if (other is null || other.Length <= 0)
            {
                return IsEmpty;
            }

            return Keys.Contains(other);
        }

        public Boolean Equals(JWTKey other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            return !other.IsEmpty ? Keys.Contains(other) : IsEmpty;
        }

        public Boolean Equals(JWTKeys other)
        {
            if (Keys is null)
            {
                return other.IsEmpty;
            }

            if (other.Keys is null || other.IsEmpty)
            {
                return IsEmpty;
            }

            if (IsEmpty)
            {
                return other.IsEmpty;
            }

            return Primary == other.Primary && Keys.SymmetricExcept(other.Keys).IsEmpty;
        }

        public Boolean Equals(IJWTSecret? other)
        {
            return other is not null ? Equals(other.Keys) : IsEmpty;
        }

        public override String? ToString()
        {
            return Keys?.IsEmpty is false ? String.Join("; ", Keys) : null;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(Keys);
        }

        IEnumerator<JWTKey> IEnumerable<JWTKey>.GetEnumerator()
        {
            if (Keys is null)
            {
                yield break;
            }

            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (JWTKey key in Keys)
            {
                if (!key.IsEmpty)
                {
                    yield return key;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public struct Enumerator : IEnumerator<JWTKey>
        {
            private ImmutableHashSet<JWTKey>.Enumerator Internal;

            public JWTKey Current
            {
                get
                {
                    return Internal.Current;
                }
            }

            Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(ImmutableHashSet<JWTKey>? set)
                : this((set ?? ImmutableHashSet<JWTKey>.Empty).GetEnumerator())
            {
            }

            public Enumerator(ImmutableHashSet<JWTKey>.Enumerator enumerator)
            {
                Internal = enumerator;
            }

            public Boolean MoveNext()
            {
                while (Internal.MoveNext())
                {
                    if (!Internal.Current.IsEmpty)
                    {
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                Internal.Reset();
            }

            public void Dispose()
            {
                Internal.Dispose();
            }
        }
    }
}