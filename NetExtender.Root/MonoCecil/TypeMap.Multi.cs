using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Cecil
{
    public abstract partial class TypeMap<TKey, TValue>
    {
        private sealed class Multi : TypeMap<TKey, TValue>
        {
#if NET8_0_OR_GREATER
            private FrozenDictionary<TKey, TValue> Map { get; }
#else
            private Dictionary<TKey, TValue> Map { get; }
#endif
            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Map.Count;
                }
            }

            public override TValue First
            {
                get
                {
                    foreach (KeyValuePair<TKey, TValue> item in Map)
                    {
                        return item.Value;
                    }

                    throw new InvalidOperationNoElementsException();
                }
            }

            public override TValue Single
            {
                get
                {
                    KeyValuePair<TKey, TValue>? result = null;

                    foreach (KeyValuePair<TKey, TValue> item in Map)
                    {
                        if (result is not null)
                        {
                            throw new InvalidOperationMoreThanOneElementException();
                        }

                        result = item;
                    }

                    return result is { Value: var value } ? value : throw new InvalidOperationNoElementsException();
                }
            }

            public override TValue? SingleOrDefault
            {
                get
                {
                    return Map is { Count: 1 } ? Single : default;
                }
            }

            public override IEnumerable<TKey> Keys
            {
                get
                {
                    return Map.Keys;
                }
            }

            public override IEnumerable<TValue> Values
            {
                get
                {
                    return Map.Values;
                }
            }

            internal Multi(Dictionary<TKey, TValue> map)
            {
#if NET8_0_OR_GREATER
                Map = map.ToFrozenDictionary();
#else
                map.TrimExcess();
                Map = map;
#endif
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TKey? key)
            {
                return key is not null && Map.ContainsKey(key);
            }

            public override Boolean TryGetValue(TKey? key, [MaybeNullWhen(false)] out TValue value)
            {
                if (key is not null)
                {
                    return Map.TryGetValue(key, out value);
                }

                value = default;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(Map.GetEnumerator());
            }

            [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
            public override KeyValuePair<TKey, TValue> this[Int32 index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    Int32 count = 0;
                    foreach (KeyValuePair<TKey, TValue> item in Map)
                    {
                        if (count++ == index)
                        {
                            return item;
                        }
                    }

                    throw new NeverOperationException();
                }
            }

            public override TValue this[TKey key]
            {
                get
                {
                    return key is not null ? Map[key] : throw new ArgumentNullException(nameof(key));
                }
            }
        }
    }
}