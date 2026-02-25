using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Utilities.Types;

namespace NetExtender.Cecil
{
    public abstract partial class TypeSet
    {
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        private sealed class One : TypeSet
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

            public override MonoCecilType First
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Single;
                }
            }

            public override MonoCecilType Single { get; }

            public override MonoCecilType SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Single;
                }
            }

            public One(MonoCecilType single)
            {
                Single = single;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(Type? item)
            {
                return item is not null && Single == item;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TypeReference? item)
            {
                return item is not null && Single == item;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(MonoCecilType? item)
            {
                return item is not null && Single == item;
            }

            public override Boolean TryGetValue(Type? equalValue, out Type actualValue)
            {
                if (equalValue is null)
                {
                    actualValue = equalValue!;
                    return false;
                }

                if (Single == equalValue)
                {
                    actualValue = Single.Type ?? equalValue;
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

                if (Single == equalValue)
                {
                    actualValue = Single;
                    return true;
                }

                actualValue = equalValue;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType? Search(Type? type)
            {
                return type is not null && Single == type ? Single : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Where(Func<MonoCecilType, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                return predicate(Single) ? this : Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                return predicate(Single, argument) ? this : Empty;
            }

            public override Boolean IsSubsetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> set)
                {
                    return set.Contains(Single);
                }

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean IsSubsetOf(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean IsSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> set)
                {
                    return set.Contains(Single);
                }

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean IsProperSubsetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? From(other).ToHashSet();

                if (!set.Contains(Single))
                {
                    return false;
                }

                Int32 items = set.Count;

                if (set.Contains(null!))
                {
                    items--;
                }

                return items > count;
            }

            public override Boolean IsProperSubsetOf(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                IReadOnlySet<MonoCecilType> set = From(other).ToHashSet();

                if (!set.Contains(Single))
                {
                    return false;
                }

                Int32 items = set.Count;

                if (set.Contains(null!))
                {
                    items--;
                }

                return items > count;
            }

            public override Boolean IsProperSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? other.ToHashSet();

                if (!set.Contains(Single))
                {
                    return false;
                }

                Int32 items = set.Count;

                if (set.Contains(null!))
                {
                    items--;
                }

                return items > count;
            }

            public override Boolean IsSupersetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single != item)
                    {
                        return false;
                    }
                }

                return true;
            }

            public override Boolean IsSupersetOf(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single != item)
                    {
                        return false;
                    }
                }

                return true;
            }

            public override Boolean IsSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single != item)
                    {
                        return false;
                    }
                }

                return true;
            }

            public override Boolean IsProperSupersetOf(IEnumerable<Type>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            public override Boolean Overlaps(IEnumerable<Type>? other)
            {
                return IsSubsetOf(other);
            }

            public override Boolean Overlaps(IEnumerable<TypeReference>? other)
            {
                return IsSubsetOf(other);
            }

            public override Boolean Overlaps(IEnumerable<MonoCecilType>? other)
            {
                return IsSubsetOf(other);
            }

            public override Boolean SetEquals(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> { Count: count } set)
                {
                    return set.Contains(Single);
                }

                Boolean found = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        found = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found;
            }

            public override Boolean SetEquals(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                Boolean found = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        found = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found;
            }

            public override Boolean SetEquals(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> { Count: count } set)
                {
                    return set.Contains(Single);
                }

                Boolean found = false;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (Single == item)
                    {
                        found = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found;
            }

            public override TypeSet Add(MonoCecilType? item)
            {
                return item is not null && !Contains(item) ? new Two(Single, item) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second)
            {
                if (first is null || Contains(first))
                {
                    return Add(second);
                }

                if (second is null || Contains(second) || first == second)
                {
                    return new Two(Single, first);
                }

                return new Three(Single, first, second);
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third)
            {
                if (first is null || Contains(first))
                {
                    return Add(second, third);
                }

                if (second is null || Contains(second) || first == second)
                {
                    return Add(first, third);
                }

                if (third is null || Contains(third) || first == third || second == third)
                {
                    return new Three(Single, first, second);
                }

                return new Multi(new HashSet<MonoCecilType>(count + 3) { Single, first, second, third });
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Add(TypeSet? set)
            {
                return Union(set);
            }

            private IEnumerable<MonoCecilType> YieldUnion(IEnumerable<MonoCecilType> other)
            {
                yield return Single;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    yield return item;
                }
            }

            public override TypeSet Union(IEnumerable<Type>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(YieldUnion(From(other))) : this;
            }

            public override TypeSet Union(IEnumerable<TypeReference>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(YieldUnion(From(other))) : this;
            }

            public override TypeSet Union(IEnumerable<MonoCecilType>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(YieldUnion(other)) : this;
            }

            public override TypeSet Union(TypeSet? set)
            {
                if (set is null)
                {
                    return this;
                }

                return set.Count switch
                {
                    0 => this,
                    1 => Add(set[0]),
                    2 => Add(set[0], set[1]),
                    3 => Add(set[0], set[1], set[2]),
                    _ => Create(new HashSet<MonoCecilType?>(set) { Single })
                };
            }

            public override TypeSet Intersect(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                return IsSubsetOf(other) ? this : Empty;
            }

            public override TypeSet Intersect(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                return IsSubsetOf(other) ? this : Empty;
            }

            public override TypeSet Intersect(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                return IsSubsetOf(other) ? this : Empty;
            }

            public override TypeSet Intersect(TypeSet? set)
            {
                if (set is null || set.Count <= 0)
                {
                    return Empty;
                }

                return set.Contains(Single) ? this : Empty;
            }

            public override TypeSet Except(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                return IsSubsetOf(other) ? Empty : this;
            }

            public override TypeSet Except(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                return IsSubsetOf(other) ? Empty : this;
            }

            public override TypeSet Except(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                return IsSubsetOf(other) ? Empty : this;
            }

            public override TypeSet Except(TypeSet? set)
            {
                if (set is null || set.Count <= 0)
                {
                    return this;
                }

                return set.Contains(Single) ? Empty : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(Type? item)
            {
                return item is not null && Single == item ? Empty : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeReference? item)
            {
                return item is not null && Single == item ? Empty : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(MonoCecilType? item)
            {
                return item is not null && Single == item ? Empty : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeSet? set)
            {
                return set is not null && set.Contains(Single) ? Empty : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Clear()
            {
                return Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType[] ToArray()
            {
                return new[] { Single };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override MonoCecilType[] ToArray(MonoCecilType? @new)
            {
                return @new is not null ? new [] { Single, @new } : ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override List<MonoCecilType> ToList()
            {
                return new List<MonoCecilType>(count + 1) { Single };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            public override MonoCecilType this[Int32 index]
            {
                get
                {
                    return index switch
                    {
                        0 => Single,
                        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
                    };
                }
            }
        }
    }
}