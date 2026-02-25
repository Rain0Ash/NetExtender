using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Cecil
{
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
    public abstract partial class TypeMap<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IReadOnlyList<KeyValuePair<TKey, TValue>> where TKey : class
    {
        public static TypeMap<TKey, TValue> Empty
        {
            get
            {
                return Zero.Instance;
            }
        }

        public const Int32 Boundary = 3;

        public abstract Int32 Count { get; }
        public abstract TValue First { get; }
        public abstract TValue Single { get; }
        public abstract TValue? SingleOrDefault { get; }

        public abstract IEnumerable<TKey> Keys { get; }
        public abstract IEnumerable<TValue> Values { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TypeMap<TKey, TValue> Create(KeyValuePair<TKey, TValue> single)
        {
            return single.Key is not null ? new One(single) : Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeMap<TKey, TValue> Create(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second)
        {
            if (first.Key is null)
            {
                return Create(second);
            }

            if (second.Key is null)
            {
                return Create(first);
            }

            return new Two(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeMap<TKey, TValue> Create(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second, KeyValuePair<TKey, TValue> third)
        {
            if (first.Key is null)
            {
                return Create(second, third);
            }

            if (second.Key is null)
            {
                return Create(first, third);
            }

            if (third.Key is null)
            {
                return Create(first, second);
            }

            return new Three(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeMap<TKey, TValue> Create(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second, KeyValuePair<TKey, TValue> third, KeyValuePair<TKey, TValue> fourth)
        {
            if (first.Key is null)
            {
                return Create(second, third, fourth);
            }

            if (second.Key is null)
            {
                return Create(first, third, fourth);
            }

            if (third.Key is null)
            {
                return Create(first, second, fourth);
            }

            if (fourth.Key is null)
            {
                return Create(first, second, third);
            }

            Dictionary<TKey, TValue> map = new Dictionary<TKey, TValue>
            {
                [first.Key] = first.Value,
                [second.Key] = second.Value,
                [third.Key] = third.Value,
                [fourth.Key] = fourth.Value
            };

            return new Multi(map);
        }

        internal static TypeMap<TKey, TValue> Create(IEnumerable<KeyValuePair<TKey, TValue>>? source)
        {
            return source.CanHaveCount() ? new Builder<IEnumerator<KeyValuePair<TKey, TValue>>>(source.GetEnumerator()).Build() : Empty;
        }

        internal static TypeMap<TKey, TValue> Create(Dictionary<TKey, TValue>? map)
        {
            if (map is null || map.Count <= 0)
            {
                return Empty;
            }

            return map.Count switch
            {
                1 or 2 or 3 => new FastBuilder<Dictionary<TKey, TValue>.Enumerator>(map.GetEnumerator()).Build(),
                _ => new Multi(map)
            };
        }

        internal static TypeMap<TKey, TValue> Create(ImmutableDictionary<TKey, TValue>? map)
        {
            return map is not null && map.Count > 0 ? new FastBuilder<ImmutableDictionary<TKey, TValue>.Enumerator>(map.GetEnumerator()).Build() : Empty;
        }

        internal static TypeMap<TKey, TValue> Create(ImmutableDictionary<TKey, TValue>.Builder? map)
        {
            return map is not null && map.Count > 0 ? new FastBuilder<ImmutableDictionary<TKey, TValue>.Enumerator>(map.GetEnumerator()).Build() : Empty;
        }

        internal static TypeMap<TKey, TValue> Create<T, TEnumerator>(TEnumerator? enumerator, Func<T, TValue> selector) where TEnumerator : IEnumerator<KeyValuePair<TKey, T>>
        {
            return enumerator is not null ? new FastBuilder<T, TEnumerator>(enumerator, selector).Build() : Empty;
        }

        public abstract Boolean Contains(TKey? key);

        Boolean IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return Contains(key);
        }

        public abstract Boolean TryGetValue(TKey? key, [MaybeNullWhen(false)] out TValue value);

        public abstract Enumerator GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract KeyValuePair<TKey, TValue> this[Int32 index] { get; }
        public abstract TValue this[TKey key] { get; }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
#if NET8_0_OR_GREATER
            private FrozenDictionary<TKey, TValue>.Enumerator Internal;
#else
            private Dictionary<TKey, TValue>.Enumerator Internal;
#endif
            private Int32 _index = Int32.MinValue;
            private TypeMap<TKey, TValue> Map { get; } = Zero.Instance;

            public readonly KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return _index switch
                    {
                        -1 or -2 => throw new InvalidOperationException(),
                        Int32.MinValue => Internal.Current,
                        _ => Map[_index]
                    };
                }
            }

            readonly Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

#if NET8_0_OR_GREATER
            public Enumerator(FrozenDictionary<TKey, TValue>.Enumerator enumerator)
            {
                Internal = enumerator;
            }
#else
            public Enumerator(Dictionary<TKey, TValue>.Enumerator enumerator)
            {
                Internal = enumerator;
            }
#endif
            internal Enumerator(TypeMap<TKey, TValue> map)
            {
                Internal = default;
                Map = map;
                _index = -1;
            }

            public Boolean MoveNext()
            {
                if (_index == Int32.MinValue)
                {
                    return Internal.MoveNext();
                }

                Int32 index = _index;
                if (index < -1 || ++index >= Map.Count)
                {
                    _index = -2;
                    return false;
                }

                _index = index;
                return true;
            }

            public void Reset()
            {
                if (_index == Int32.MinValue)
                {
                    Internal.TryReset();
                    return;
                }

                _index = -1;
            }

            public void Dispose()
            {
                if (_index == Int32.MinValue)
                {
                    Internal.TryDispose();
                    return;
                }

                _index = -1;
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct Builder<TEnumerator> : IDisposable where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable
        {
            private TEnumerator Enumerator;

            private KeyValuePair<TKey?, TValue> First = default;
            private KeyValuePair<TKey?, TValue> Second = default;
            private KeyValuePair<TKey?, TValue> Third = default;

            private readonly Int32 Count
            {
                get
                {
                    Int32 count = 0;

                    if (First.Key is not null)
                    {
                        count++;
                    }

                    if (Second.Key is not null)
                    {
                        count++;
                    }

                    if (Third.Key is not null)
                    {
                        count++;
                    }

                    return count;
                }
            }

            public Builder(TEnumerator enumerator)
            {
                Enumerator = enumerator;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public TypeMap<TKey, TValue> Build()
            {
                Dictionary<TKey, TValue>? map;

                while (Enumerator.MoveNext())
                {
                    KeyValuePair<TKey, TValue> item = Enumerator.Current;

                    if (item.Key is null)
                    {
                        continue;
                    }

                    if (First.Key is null || EqualityComparer<TKey>.Default.Equals(First.Key, item.Key))
                    {
                        First = item!;
                        continue;
                    }

                    if (Second.Key is null || EqualityComparer<TKey>.Default.Equals(Second.Key, item.Key))
                    {
                        Second = item!;
                        continue;
                    }

                    if (Third.Key is null || EqualityComparer<TKey>.Default.Equals(Third.Key, item.Key))
                    {
                        Third = item!;
                        continue;
                    }

                    map = new Dictionary<TKey, TValue>
                    {
                        [First.Key] = First.Value,
                        [Second.Key] = Second.Value,
                        [Third.Key] = Third.Value,
                        [item.Key] = item.Value
                    };

                    goto set;
                }

                Dispose();

                return Count switch
                {
                    0 => Empty,
                    1 => new One(First!),
                    2 => new Two(First!, Second!),
                    3 => new Three(First!, Second!, Third!),
                    _ => throw new NeverOperationException()
                };

                set:
                while (Enumerator.MoveNext())
                {
                    (TKey key, TValue value) = Enumerator.Current;
                    map[key] = value;
                }

                Dispose();
                return new Multi(map);
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct FastBuilder<TEnumerator> where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private TEnumerator Enumerator;

            private KeyValuePair<TKey?, TValue> First = default;
            private KeyValuePair<TKey?, TValue> Second = default;
            private KeyValuePair<TKey?, TValue> Third = default;

            private readonly Int32 Count
            {
                get
                {
                    Int32 count = 0;

                    if (First.Key is not null)
                    {
                        count++;
                    }

                    if (Second.Key is not null)
                    {
                        count++;
                    }

                    if (Third.Key is not null)
                    {
                        count++;
                    }

                    return count;
                }
            }

            public FastBuilder(TEnumerator enumerator)
            {
                Enumerator = enumerator;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public TypeMap<TKey, TValue> Build()
            {
                Dictionary<TKey, TValue>? map;

                while (Enumerator.MoveNext())
                {
                    KeyValuePair<TKey, TValue> item = Enumerator.Current;

                    if (item.Key is null)
                    {
                        continue;
                    }

                    if (First.Key is null)
                    {
                        First = item!;
                        continue;
                    }

                    if (Second.Key is null)
                    {
                        Second = item!;
                        continue;
                    }

                    if (Third.Key is null)
                    {
                        Third = item!;
                        continue;
                    }

                    map = new Dictionary<TKey, TValue>
                    {
                        [First.Key] = First.Value,
                        [Second.Key] = Second.Value,
                        [Third.Key] = Third.Value,
                        [item.Key] = item.Value
                    };

                    goto set;
                }

                Dispose();

                return Count switch
                {
                    0 => Empty,
                    1 => new One(First!),
                    2 => new Two(First!, Second!),
                    3 => new Three(First!, Second!, Third!),
                    _ => throw new NeverOperationException()
                };

                set:
                while (Enumerator.MoveNext())
                {
                    (TKey key, TValue value) = Enumerator.Current;
                    map[key] = value;
                }

                Dispose();
                return new Multi(map);
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct FastBuilder<T, TEnumerator> where TEnumerator : IEnumerator<KeyValuePair<TKey, T>>
        {
            private TEnumerator Enumerator;
            private readonly Func<T, TValue> Selector;

            private KeyValuePair<TKey?, TValue> First = default;
            private KeyValuePair<TKey?, TValue> Second = default;
            private KeyValuePair<TKey?, TValue> Third = default;

            private readonly Int32 Count
            {
                get
                {
                    Int32 count = 0;

                    if (First.Key is not null)
                    {
                        count++;
                    }

                    if (Second.Key is not null)
                    {
                        count++;
                    }

                    if (Third.Key is not null)
                    {
                        count++;
                    }

                    return count;
                }
            }

            public FastBuilder(TEnumerator enumerator, Func<T, TValue> selector)
            {
                Enumerator = enumerator;
                Selector = selector;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public TypeMap<TKey, TValue> Build()
            {
                Dictionary<TKey, TValue>? map;

                while (Enumerator.MoveNext())
                {
                    KeyValuePair<TKey, T> item = Enumerator.Current;

                    if (item.Key is null)
                    {
                        continue;
                    }

                    if (First.Key is null)
                    {
                        First = new KeyValuePair<TKey, TValue>(item.Key, Selector(item.Value))!;
                        continue;
                    }

                    if (Second.Key is null)
                    {
                        Second = new KeyValuePair<TKey, TValue>(item.Key, Selector(item.Value))!;
                        continue;
                    }

                    if (Third.Key is null)
                    {
                        Third = new KeyValuePair<TKey, TValue>(item.Key, Selector(item.Value))!;
                        continue;
                    }

                    map = new Dictionary<TKey, TValue>
                    {
                        [First.Key] = First.Value,
                        [Second.Key] = Second.Value,
                        [Third.Key] = Third.Value,
                        [item.Key] = Selector(item.Value)
                    };

                    goto set;
                }

                Dispose();

                return Count switch
                {
                    0 => Empty,
                    1 => new One(First!),
                    2 => new Two(First!, Second!),
                    3 => new Three(First!, Second!, Third!),
                    _ => throw new NeverOperationException()
                };

                set:
                while (Enumerator.MoveNext())
                {
                    (TKey key, T value) = Enumerator.Current;
                    map[key] = Selector(value);
                }

                Dispose();
                return new Multi(map);
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }
        }
    }
}