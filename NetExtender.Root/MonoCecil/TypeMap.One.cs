using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NetExtender.Cecil
{
    public abstract partial class TypeMap<TKey, TValue>
    {
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        private sealed class One : TypeMap<TKey, TValue>
        {
            private const Int32 count = 1;
            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return count;
                }
            }

            public override TValue First
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Single;
                }
            }

            private readonly KeyValuePair<TKey, TValue> _single;
            public override TValue Single
            {
                get
                {
                    return _single.Value;
                }
            }

            public override TValue SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Single;
                }
            }

            public override IEnumerable<TKey> Keys
            {
                get
                {
                    yield return _single.Key;
                }
            }

            public override IEnumerable<TValue> Values
            {
                get
                {
                    yield return _single.Value;
                }
            }

            public One(KeyValuePair<TKey, TValue> single)
            {
                _single = single;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TKey? key)
            {
                return key is not null && EqualityComparer<TKey>.Default.Equals(_single.Key, key);
            }

            public override Boolean TryGetValue(TKey? key, [MaybeNullWhen(false)] out TValue value)
            {
                if (key is not null && EqualityComparer<TKey>.Default.Equals(_single.Key, key))
                {
                    value = _single.Value;
                    return true;
                }

                value = default;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            public override KeyValuePair<TKey, TValue> this[Int32 index]
            {
                get
                {
                    return index switch
                    {
                        0 => _single,
                        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
                    };
                }
            }

            public override TValue this[TKey key]
            {
                get
                {
                    if (key is null)
                    {
                        throw new ArgumentNullException(nameof(key));
                    }

                    return EqualityComparer<TKey>.Default.Equals(_single.Key, key) ? _single.Value : throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
                }
            }
        }
    }
}