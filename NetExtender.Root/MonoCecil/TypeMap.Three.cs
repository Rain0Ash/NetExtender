using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;

namespace NetExtender.Cecil
{
    public abstract partial class TypeMap<TKey, TValue>
    {
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        private class Three : TypeMap<TKey, TValue>
        {
            private const Int32 count = 3;
            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return count;
                }
            }

            private readonly KeyValuePair<TKey, TValue> _first;
            private readonly KeyValuePair<TKey, TValue> _second;
            private readonly KeyValuePair<TKey, TValue> _third;

            public override TValue First
            {
                get
                {
                    return _first.Value;
                }
            }

            public override TValue Single
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new InvalidOperationMoreThanOneElementException();
                }
            }

            public override TValue? SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return default;
                }
            }

            public override IEnumerable<TKey> Keys
            {
                get
                {
                    yield return _first.Key;
                    yield return _second.Key;
                    yield return _third.Key;
                }
            }

            public override IEnumerable<TValue> Values
            {
                get
                {
                    yield return _first.Value;
                    yield return _second.Value;
                    yield return _third.Value;
                }
            }

            public Three(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second, KeyValuePair<TKey, TValue> third)
            {
                _first = first;
                _second = second;
                _third = third;
            }

            public override Boolean Contains(TKey? key)
            {
                if (key is null)
                {
                    return false;
                }

                return EqualityComparer<TKey>.Default.Equals(_first.Key, key) || EqualityComparer<TKey>.Default.Equals(_second.Key, key) || EqualityComparer<TKey>.Default.Equals(_third.Key, key);
            }

            public override Boolean TryGetValue(TKey? key, [MaybeNullWhen(false)] out TValue value)
            {
                if (key is null)
                {
                    value = default;
                    return false;
                }

                if (EqualityComparer<TKey>.Default.Equals(_first.Key, key))
                {
                    value = _first.Value;
                    return true;
                }

                if (EqualityComparer<TKey>.Default.Equals(_second.Key, key))
                {
                    value = _second.Value;
                    return true;
                }

                if (EqualityComparer<TKey>.Default.Equals(_third.Key, key))
                {
                    value = _third.Value;
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
                        0 => _first,
                        1 => _second,
                        2 => _third,
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

                    if (EqualityComparer<TKey>.Default.Equals(_first.Key, key))
                    {
                        return _first.Value;
                    }

                    if (EqualityComparer<TKey>.Default.Equals(_second.Key, key))
                    {
                        return _second.Value;
                    }

                    if (EqualityComparer<TKey>.Default.Equals(_third.Key, key))
                    {
                        return _third.Value;
                    }

                    throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
                }
            }
        }
    }
}