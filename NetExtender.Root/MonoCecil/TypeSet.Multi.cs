using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Cecil
{
    public abstract partial class TypeSet
    {
        private sealed class Multi : TypeSet
        {
#if NET8_0_OR_GREATER
            private FrozenSet<MonoCecilType> Set { get; }
#else
            private HashSet<MonoCecilType> Set { get; }
#endif
            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Set.Count;
                }
            }

            public override MonoCecilType First
            {
                get
                {
                    foreach (MonoCecilType item in Set)
                    {
                        return item;
                    }

                    throw new InvalidOperationNoElementsException();
                }
            }

            public override MonoCecilType Single
            {
                get
                {
                    MonoCecilType? result = null;

                    foreach (MonoCecilType item in Set)
                    {
                        if (result is not null)
                        {
                            throw new InvalidOperationMoreThanOneElementException();
                        }

                        result = item;
                    }

                    return result ?? throw new InvalidOperationNoElementsException();
                }
            }

            public override MonoCecilType? SingleOrDefault
            {
                get
                {
                    return Set is { Count: 1 } ? Single : null;
                }
            }

            internal Multi(HashSet<MonoCecilType> set)
            {
                set.Remove(null!);
#if NET8_0_OR_GREATER
                Set = set.ToFrozenSet();
#else
                set.TrimExcess();
                Set = set;
#endif
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(Type? item)
            {
                return item is not null && Set.Contains(MonoCecilType.From(item));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TypeReference? item)
            {
                return item is not null && Set.Contains(MonoCecilType.From(item)!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(MonoCecilType? item)
            {
                return item is not null && Set.Contains(item);
            }

            public override Boolean TryGetValue(Type? equalValue, out Type actualValue)
            {
                if (equalValue is null)
                {
                    actualValue = equalValue!;
                    return false;
                }

                if (Set.TryGetValue(MonoCecilType.From(equalValue), out MonoCecilType? result))
                {
                    actualValue = result.Type ?? equalValue;
                    return true;
                }

                actualValue = equalValue;
                return false;
            }

            public override Boolean TryGetValue(MonoCecilType? equalValue, out MonoCecilType actualValue)
            {
                if (equalValue is null)
                {
                    actualValue = equalValue!;
                    return false;
                }

                if (Set.TryGetValue(equalValue, out MonoCecilType? result))
                {
                    actualValue = result;
                    return true;
                }

                actualValue = equalValue;
                return false;
            }

            [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
            public override MonoCecilType? Search(Type? type)
            {
                if (type is null)
                {
                    return null;
                }

                foreach (MonoCecilType item in Set)
                {
                    if (item == type)
                    {
                        return item;
                    }
                }

                return null;
            }

            [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
            public override TypeSet Where(Func<MonoCecilType, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Int32 count = Set.Count;
                MonoCecilType[] buffer = ArrayPool<MonoCecilType>.Shared.Rent(count);

                try
                {
                    Int32 matches = 0;
                    foreach (MonoCecilType item in Set)
                    {
                        if (predicate(item))
                        {
                            buffer[matches++] = item;
                        }
                    }

                    if (matches == count)
                    {
                        return this;
                    }

                    static TypeSet New(ReadOnlySpan<MonoCecilType> buffer)
                    {
                        HashSet<MonoCecilType> result = new HashSet<MonoCecilType>(buffer.Length);

                        foreach (MonoCecilType item in buffer)
                        {
                            result.Add(item);
                        }

                        return new Multi(result);
                    }

                    return matches switch
                    {
                        0 => Empty,
                        1 => new One(buffer[0]),
                        2 => new Two(buffer[0], buffer[1]),
                        3 => new Three(buffer[0], buffer[1], buffer[2]),
                        _ => New(buffer.Slice(0, matches))
                    };
                }
                finally
                {
                    ArrayPool<MonoCecilType>.Shared.Return(buffer, true);
                }
            }

            [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
            public override TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Int32 count = Set.Count;
                MonoCecilType[] buffer = ArrayPool<MonoCecilType>.Shared.Rent(count);

                try
                {
                    Int32 matches = 0;
                    foreach (MonoCecilType item in Set)
                    {
                        if (predicate(item, argument))
                        {
                            buffer[matches++] = item;
                        }
                    }

                    if (matches == count)
                    {
                        return this;
                    }

                    static TypeSet New(ReadOnlySpan<MonoCecilType> buffer)
                    {
                        HashSet<MonoCecilType> result = new HashSet<MonoCecilType>(buffer.Length);

                        foreach (MonoCecilType item in buffer)
                        {
                            result.Add(item);
                        }

                        return new Multi(result);
                    }

                    return matches switch
                    {
                        0 => Empty,
                        1 => new One(buffer[0]),
                        2 => new Two(buffer[0], buffer[1]),
                        3 => new Three(buffer[0], buffer[1], buffer[2]),
                        _ => New(buffer.Slice(0, matches))
                    };
                }
                finally
                {
                    ArrayPool<MonoCecilType>.Shared.Return(buffer, true);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<Type>? other)
            {
                return Set.IsSubsetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<TypeReference>? other)
            {
                return Set.IsSubsetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                return Set.IsSubsetOf(other ?? Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<Type>? other)
            {
                return Set.IsProperSubsetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<TypeReference>? other)
            {
                return Set.IsProperSubsetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                return Set.IsProperSubsetOf(other ?? Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<Type>? other)
            {
                return Set.IsSupersetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<TypeReference>? other)
            {
                return Set.IsSupersetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                return Set.IsSupersetOf(other ?? Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<Type>? other)
            {
                return Set.IsProperSupersetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other)
            {
                return Set.IsProperSupersetOf(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                return Set.IsProperSupersetOf(other ?? Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<Type>? other)
            {
                return Set.Overlaps(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<TypeReference>? other)
            {
                return Set.Overlaps(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<MonoCecilType>? other)
            {
                return Set.Overlaps(other ?? Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<Type>? other)
            {
                return Set.SetEquals(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<TypeReference>? other)
            {
                return Set.SetEquals(other is not null ? From(other) : Array.Empty<MonoCecilType>());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<MonoCecilType>? other)
            {
                return Set.SetEquals(other ?? Array.Empty<MonoCecilType>());
            }

            public override TypeSet Add(MonoCecilType? item)
            {
                if (item is null || Set.Contains(item))
                {
                    return this;
                }

                HashSet<MonoCecilType> set = new HashSet<MonoCecilType>(Set);
                return set.Add(item) ? new Multi(set) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second)
            {
                if (first is null)
                {
                    return Add(second);
                }

                if (second is null)
                {
                    return Add(first);
                }

                HashSet<MonoCecilType> set = new HashSet<MonoCecilType>(Set);
                return set.Add(first) || set.Add(second) ? new Multi(set) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third)
            {
                if (first is null)
                {
                    return Add(second, third);
                }

                if (second is null)
                {
                    return Add(first, third);
                }

                if (third is null)
                {
                    return Add(first, second);
                }

                HashSet<MonoCecilType> set = new HashSet<MonoCecilType>(Set);
                return set.Add(first) || set.Add(second) || set.Add(third) ? new Multi(set) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Add(TypeSet? set)
            {
                return Union(set);
            }

            public override TypeSet Union(IEnumerable<Type>? other)
            {
                return other.CanHaveCount() ? Create(Set.Union(From(other))) : this;
            }

            public override TypeSet Union(IEnumerable<TypeReference>? other)
            {
                return other.CanHaveCount() ? Create(Set.Union(From(other))) : this;
            }

            public override TypeSet Union(IEnumerable<MonoCecilType>? other)
            {
                return other.CanHaveCount() ? Create(Set.Union(other)) : this;
            }

            public override TypeSet Union(TypeSet? set)
            {
                return set is not null && set.Count > 0 ? Create(Set.Union(set)) : this;
            }

            public override TypeSet Intersect(IEnumerable<Type>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Intersect(From(other))) : Empty;
            }

            public override TypeSet Intersect(IEnumerable<TypeReference>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Intersect(From(other))) : Empty;
            }

            public override TypeSet Intersect(IEnumerable<MonoCecilType>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Intersect(other)) : Empty;
            }

            public override TypeSet Intersect(TypeSet? set)
            {
                return set is not null && set.Count > 0 ? Create(Set.Intersect(set)) : Empty;
            }

            public override TypeSet Except(IEnumerable<Type>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Except(From(other))) : this;
            }

            public override TypeSet Except(IEnumerable<TypeReference>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Except(From(other))) : this;
            }

            public override TypeSet Except(IEnumerable<MonoCecilType>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(Set.Except(other)) : this;
            }

            public override TypeSet Except(TypeSet? set)
            {
                return set is not null && set.Count > 0 ? Create(Set.Except(set)) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(Type? item)
            {
                return Remove(MonoCecilType.From(item));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeReference? item)
            {
                return Remove(MonoCecilType.From(item));
            }

            public override TypeSet Remove(MonoCecilType? item)
            {
                if (item is null || !Set.Contains(item))
                {
                    return this;
                }

                return Create(EnumerableBaseUtilities.Where(Set, item, static (except, item) => item != except));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeSet? set)
            {
                return Except(set);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Clear()
            {
                return Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType[] ToArray()
            {
                return Set.ToArray();
            }

            internal override MonoCecilType[] ToArray(MonoCecilType? @new)
            {
                if (@new is null)
                {
                    return ToArray();
                }

                MonoCecilType[] result = new MonoCecilType[Set.Count + 1];
                Set.CopyTo(result, 0);
                result[Set.Count] = @new;
                return result;
            }

            public override List<MonoCecilType> ToList()
            {
                List<MonoCecilType> result = new List<MonoCecilType>(Set.Count + 1);
                result.AddRange(Set);
                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(Set.GetEnumerator());
            }

            [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
            public override MonoCecilType this[Int32 index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    Int32 count = 0;
                    foreach (MonoCecilType item in Set)
                    {
                        if (count++ == index)
                        {
                            return item;
                        }
                    }

                    throw new NeverOperationException();
                }
            }
        }
    }
}