using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;

namespace NetExtender.Cecil
{
    public abstract partial class TypeMap<TKey, TValue>
    {
        private sealed class Zero : TypeMap<TKey, TValue>
        {
            public static TypeMap<TKey, TValue> Instance { get; } = new Zero();

            private const Int32 count = 0;
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
                    throw new InvalidOperationNoElementsException();
                }
            }

            public override TValue Single
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new InvalidOperationNoElementsException();
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

            public override IReadOnlyCollection<TKey> Keys
            {
                get
                {
                    return Array.Empty<TKey>();
                }
            }

            public override IReadOnlyCollection<TValue> Values
            {
                get
                {
                    return Array.Empty<TValue>();
                }
            }

            private Zero()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TKey? key)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue(TKey? key, [MaybeNullWhen(false)] out TValue value)
            {
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
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
            }

            public override TValue this[TKey key]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if (key is null)
                    {
                        throw new ArgumentNullException(nameof(key));
                    }

                    throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
                }
            }
        }
    }
}