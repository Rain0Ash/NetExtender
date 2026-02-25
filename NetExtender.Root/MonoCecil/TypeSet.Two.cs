using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Cecil
{
    public abstract partial class TypeSet
    {
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        private class Two : TypeSet
        {
            private const Int32 count = 2;
            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return count;
                }
            }

            public override MonoCecilType First { get; }
            public MonoCecilType Second { get; }

            public override MonoCecilType Single
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new InvalidOperationMoreThanOneElementException();
                }
            }

            public override MonoCecilType? SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return null;
                }
            }

            public Two(MonoCecilType first, MonoCecilType second)
            {
                First = first;
                Second = second;
            }

            public override Boolean Contains(Type? item)
            {
                if (item is null)
                {
                    return false;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);
                return First.Equals(key) || Second.Equals(key);
            }

            public override Boolean Contains(TypeReference? item)
            {
                if (item is null)
                {
                    return false;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);
                return First.Equals(key) || Second.Equals(key);
            }

            public override Boolean Contains(MonoCecilType? item)
            {
                if (item is null)
                {
                    return false;
                }

                return First == item || Second == item;
            }

            public override Boolean TryGetValue(Type? equalValue, out Type actualValue)
            {
                if (equalValue is null)
                {
                    actualValue = equalValue!;
                    return false;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(equalValue);

                if (First.Equals(key))
                {
                    actualValue = First.Type ?? equalValue;
                    return true;
                }

                if (Second.Equals(key))
                {
                    actualValue = Second.Type ?? equalValue;
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

                if (First == equalValue)
                {
                    actualValue = First;
                    return true;
                }

                if (Second == equalValue)
                {
                    actualValue = Second;
                    return true;
                }

                actualValue = equalValue;
                return false;
            }

            public override MonoCecilType? Search(Type? type)
            {
                if (type is null)
                {
                    return null;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(type);
                return First.Equals(key) ? First : Second.Equals(key) ? Second : null;
            }

            public override TypeSet Where(Func<MonoCecilType, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Boolean first = false;
                Boolean second = false;

                if (predicate(First))
                {
                    first = true;
                }

                if (predicate(Second))
                {
                    second = true;
                }

                if (first && second)
                {
                    return this;
                }

                if (first)
                {
                    return new One(First);
                }

                if (second)
                {
                    return new One(Second);
                }

                return Empty;
            }

            public override TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Boolean first = false;
                Boolean second = false;

                if (predicate(First, argument))
                {
                    first = true;
                }

                if (predicate(Second, argument))
                {
                    second = true;
                }

                if (first && second)
                {
                    return this;
                }

                if (first)
                {
                    return new One(First);
                }

                if (second)
                {
                    return new One(Second);
                }

                return Empty;
            }

            public override Boolean IsSubsetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> set)
                {
                    return set.Contains(First) && set.Contains(Second);
                }

                Boolean first = false;
                Boolean second = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (!first && First.Equals(key))
                    {
                        first = true;
                    }

                    if (!second && Second.Equals(key))
                    {
                        second = true;
                    }

                    if (first && second)
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

                Boolean first = false;
                Boolean second = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (!first && First.Equals(key))
                    {
                        first = true;
                    }

                    if (!second && Second.Equals(key))
                    {
                        second = true;
                    }

                    if (first && second)
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
                    return set.Contains(First) && set.Contains(Second);
                }

                Boolean first = false;
                Boolean second = false;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (!first && First == item)
                    {
                        first = true;
                    }

                    if (!second && Second == item)
                    {
                        second = true;
                    }

                    if (first && second)
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

                if (!set.Contains(First) || !set.Contains(Second))
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

                if (!set.Contains(First) || !set.Contains(Second))
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

                if (!set.Contains(First) || !set.Contains(Second))
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
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (!First.Equals(key) && !Second.Equals(key))
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
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (!First.Equals(key) && !Second.Equals(key))
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
                    if (First != item && Second != item)
                    {
                        return false;
                    }
                }

                return true;
            }

            public override Boolean IsProperSupersetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (First == item)
                    {
                        found1 = true;
                    }
                    else if (Second == item)
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2);
            }

            public override Boolean Overlaps(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key) || Second.Equals(key))
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean Overlaps(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key) || Second.Equals(key))
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean Overlaps(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (First == item || Second == item)
                    {
                        return true;
                    }
                }

                return false;
            }

            public override Boolean SetEquals(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> { Count: count } set)
                {
                    return set.Contains(First) && set.Contains(Second);
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2;
            }

            public override Boolean SetEquals(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2;
            }

            public override Boolean SetEquals(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> { Count: count } set)
                {
                    return set.Contains(First) && set.Contains(Second);
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (First == item)
                    {
                        found1 = true;
                    }
                    else if (Second == item)
                    {
                        found2 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2;
            }

            public override TypeSet Add(MonoCecilType? item)
            {
                return item is not null && !Contains(item) ? new Three(First, Second, item) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second)
            {
                if (first is null || Contains(first))
                {
                    return Add(second);
                }

                if (second is null || Contains(second) || first == second)
                {
                    return new Three(First, Second, first);
                }

                return new Multi(new HashSet<MonoCecilType>(count + 2) { First, Second, first, second });
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
                    return new Multi(new HashSet<MonoCecilType>(count + 2) { First, Second, first, second });
                }

                return new Multi(new HashSet<MonoCecilType>(count + 3) { First, Second, first, second, third });
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Add(TypeSet? set)
            {
                return Union(set);
            }

            private IEnumerable<MonoCecilType> YieldUnion(IEnumerable<MonoCecilType> other)
            {
                yield return First;
                yield return Second;

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
                    _ => new Multi(new HashSet<MonoCecilType>(set) { First, Second })
                };
            }

            public override TypeSet Intersect(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                }

                if (found1 && found2)
                {
                    return this;
                }

                if (found1)
                {
                    return Create(First);
                }

                if (found2)
                {
                    return Create(Second);
                }

                return Empty;
            }

            public override TypeSet Intersect(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        found1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        found2 = true;
                    }
                }

                if (found1 && found2)
                {
                    return this;
                }

                if (found1)
                {
                    return Create(First);
                }

                if (found2)
                {
                    return Create(Second);
                }

                return Empty;
            }

            public override TypeSet Intersect(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return Empty;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                switch (other)
                {
                    case TypeSet set:
                    {
                        return Intersect(set);
                    }
                    case IReadOnlySet<MonoCecilType> set:
                    {
                        if (set.Contains(First))
                        {
                            found1 = true;
                        }

                        if (set.Contains(Second))
                        {
                            found2 = true;
                        }

                        break;
                    }
                    default:
                    {
                        foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                        {
                            if (First == item)
                            {
                                found1 = true;
                            }
                            else if (Second == item)
                            {
                                found2 = true;
                            }
                        }

                        break;
                    }
                }

                if (found1 && found2)
                {
                    return this;
                }

                if (found1)
                {
                    return Create(First);
                }

                if (found2)
                {
                    return Create(Second);
                }

                return Empty;
            }

            public override TypeSet Intersect(TypeSet? set)
            {
                if (set is null || set.Count <= 0)
                {
                    return Empty;
                }

                Boolean found1 = false;
                Boolean found2 = false;

                if (set.Contains(First))
                {
                    found1 = true;
                }

                if (set.Contains(Second))
                {
                    found2 = true;
                }

                if (found1 && found2)
                {
                    return this;
                }

                if (found1)
                {
                    return Create(First);
                }

                if (found2)
                {
                    return Create(Second);
                }

                return Empty;
            }

            public override TypeSet Except(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                Boolean remove1 = false;
                Boolean remove2 = false;

                foreach (Type item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        remove1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        remove2 = true;
                    }
                }

                if (remove1 && remove2)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return Create(Second);
                }

                if (remove2)
                {
                    return Create(First);
                }

                return this;
            }

            public override TypeSet Except(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                Boolean remove1 = false;
                Boolean remove2 = false;

                foreach (TypeReference item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                    if (First.Equals(key))
                    {
                        remove1 = true;
                    }
                    else if (Second.Equals(key))
                    {
                        remove2 = true;
                    }
                }

                if (remove1 && remove2)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return Create(Second);
                }

                if (remove2)
                {
                    return Create(First);
                }

                return this;
            }

            public override TypeSet Except(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return this;
                }

                Boolean remove1 = false;
                Boolean remove2 = false;

                switch (other)
                {
                    case TypeSet set:
                    {
                        return Except(set);
                    }
                    case IReadOnlySet<MonoCecilType> set:
                    {
                        if (set.Contains(First))
                        {
                            remove1 = true;
                        }

                        if (set.Contains(Second))
                        {
                            remove2 = true;
                        }

                        break;
                    }
                    default:
                    {
                        foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                        {
                            if (First == item)
                            {
                                remove1 = true;
                            }
                            else if (Second == item)
                            {
                                remove2 = true;
                            }
                        }

                        break;
                    }
                }

                if (remove1 && remove2)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return Create(Second);
                }

                if (remove2)
                {
                    return Create(First);
                }

                return this;
            }

            public override TypeSet Except(TypeSet? set)
            {
                if (set is null || set.Count <= 0)
                {
                    return this;
                }

                Boolean remove1 = false;
                Boolean remove2 = false;

                if (set.Contains(First))
                {
                    remove1 = true;
                }

                if (set.Contains(Second))
                {
                    remove2 = true;
                }

                if (remove1 && remove2)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return Create(Second);
                }

                if (remove2)
                {
                    return Create(First);
                }

                return this;
            }

            public override TypeSet Remove(Type? item)
            {
                if (item is null)
                {
                    return this;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                if (Second.Equals(key))
                {
                    return new One(First);
                }

                if (First.Equals(key))
                {
                    return new One(Second);
                }

                return this;
            }

            public override TypeSet Remove(TypeReference? item)
            {
                if (item is null)
                {
                    return this;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);

                if (Second.Equals(key))
                {
                    return new One(First);
                }

                if (First.Equals(key))
                {
                    return new One(Second);
                }

                return this;
            }

            public override TypeSet Remove(MonoCecilType? item)
            {
                if (item is null)
                {
                    return this;
                }

                if (Second == item)
                {
                    return new One(First);
                }

                if (First == item)
                {
                    return new One(Second);
                }

                return this;
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
                return new[] { First, Second };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override MonoCecilType[] ToArray(MonoCecilType? @new)
            {
                return @new is not null ? new [] { First, Second, @new } : ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override List<MonoCecilType> ToList()
            {
                return new List<MonoCecilType>(count + 1) { First, Second };
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
                        0 => First,
                        1 => Second,
                        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
                    };
                }
            }
        }
    }
}