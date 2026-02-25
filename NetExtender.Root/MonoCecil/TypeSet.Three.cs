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
        private class Three : TypeSet
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

            public override MonoCecilType First { get; }
            public MonoCecilType Second { get; }
            public MonoCecilType Third { get; }

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

            public Three(MonoCecilType first, MonoCecilType second, MonoCecilType third)
            {
                First = first;
                Second = second;
                Third = third;
            }

            public override Boolean Contains(Type? item)
            {
                if (item is null)
                {
                    return false;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);
                return First.Equals(key) || Second.Equals(key) || Third.Equals(key);
            }

            public override Boolean Contains(TypeReference? item)
            {
                if (item is null)
                {
                    return false;
                }

                MonoCecilType.TypeKey key = new MonoCecilType.TypeKey(item);
                return First.Equals(key) || Second.Equals(key) || Third.Equals(key);
            }

            public override Boolean Contains(MonoCecilType? item)
            {
                if (item is null)
                {
                    return false;
                }

                return First == item || Second == item || Third == item;
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

                if (Third.Equals(key))
                {
                    actualValue = Third.Type ?? equalValue;
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

                if (Third == equalValue)
                {
                    actualValue = Third;
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
                return First.Equals(key) ? First : Second.Equals(key) ? Second : Third.Equals(key) ? Third : null;
            }

            public override TypeSet Where(Func<MonoCecilType, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Boolean first = false;
                Boolean second = false;
                Boolean third = false;

                if (predicate(First))
                {
                    first = true;
                }

                if (predicate(Second))
                {
                    second = true;
                }

                if (predicate(Third))
                {
                    third = true;
                }

                if (first && second && third)
                {
                    return this;
                }

                if (first)
                {
                    return second ? new Two(First, Second) : third ? new Two(First, Third) : new One(First);
                }

                if (second)
                {
                    return third ? new Two(Second, Third) : new One(Second);
                }

                return third ? new One(Third) : Empty;
            }

            public override TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate)
            {
                if (predicate is null)
                {
                    throw new ArgumentNullException(nameof(predicate));
                }

                Boolean first = false;
                Boolean second = false;
                Boolean third = false;

                if (predicate(First, argument))
                {
                    first = true;
                }

                if (predicate(Second, argument))
                {
                    second = true;
                }

                if (predicate(Third, argument))
                {
                    third = true;
                }

                if (first && second && third)
                {
                    return this;
                }

                if (first)
                {
                    return second ? new Two(First, Second) : third ? new Two(First, Third) : new One(First);
                }

                if (second)
                {
                    return third ? new Two(Second, Third) : new One(Second);
                }

                return third ? new One(Third) : Empty;
            }

            public override Boolean IsSubsetOf(IEnumerable<Type>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> set)
                {
                    return set.Contains(First) && set.Contains(Second) && set.Contains(Third);
                }

                Boolean first = false;
                Boolean second = false;
                Boolean third = false;

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

                    if (!third && Third.Equals(key))
                    {
                        third = true;
                    }

                    if (first && second && third)
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
                Boolean third = false;

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

                    if (!third && Third.Equals(key))
                    {
                        third = true;
                    }

                    if (first && second && third)
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
                    return set.Contains(First) && set.Contains(Second) && set.Contains(Third);
                }

                Boolean first = false;
                Boolean second = false;
                Boolean third = false;

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

                    if (!third && Third == item)
                    {
                        third = true;
                    }

                    if (first && second && third)
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

                if (!set.Contains(First) || !set.Contains(Second) || !set.Contains(Third))
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

                if (!set.Contains(First) || !set.Contains(Second) || !set.Contains(Third))
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

                if (!set.Contains(First) || !set.Contains(Second) || !set.Contains(Third))
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

                    if (!First.Equals(key) && !Second.Equals(key) && !Third.Equals(key))
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

                    if (!First.Equals(key) && !Second.Equals(key) && !Third.Equals(key))
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
                    if (First != item && Second != item && Third != item)
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
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2 && found3);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                Boolean found1 = false;
                Boolean found2 = false;
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2 && found3);
            }

            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return true;
                }

                Boolean found1 = false;
                Boolean found2 = false;
                Boolean found3 = false;

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
                    else if (Third == item)
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return !(found1 && found2 && found3);
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

                    if (First.Equals(key) || Second.Equals(key) || Third.Equals(key))
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

                    if (First.Equals(key) || Second.Equals(key) || Third.Equals(key))
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
                    if (First == item || Second == item || Third == item)
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
                    return set.Contains(First) && set.Contains(Second) && set.Contains(Third);
                }

                Boolean found1 = false;
                Boolean found2 = false;
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2 && found3;
            }

            public override Boolean SetEquals(IEnumerable<TypeReference>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                Boolean found1 = false;
                Boolean found2 = false;
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2 && found3;
            }

            public override Boolean SetEquals(IEnumerable<MonoCecilType>? other)
            {
                if (other.CantHaveCount())
                {
                    return false;
                }

                if (other is IReadOnlySet<MonoCecilType> { Count: count } set)
                {
                    return set.Contains(First) && set.Contains(Second) && set.Contains(Third);
                }

                Boolean found1 = false;
                Boolean found2 = false;
                Boolean found3 = false;

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
                    else if (Third == item)
                    {
                        found3 = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return found1 && found2 && found3;
            }

            public override TypeSet Add(MonoCecilType? item)
            {
                return item is not null && !Contains(item) ? new Multi(new HashSet<MonoCecilType> { First, Second, Third, item }) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second)
            {
                if (first is null || Contains(first))
                {
                    return Add(second);
                }

                if (second is null || Contains(second) || first == second)
                {
                    return new Multi(new HashSet<MonoCecilType>(count + 1) { First, Second, Third, first });
                }

                return new Multi(new HashSet<MonoCecilType>(count + 2) { First, Second, Third, first, second });
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
                    return new Multi(new HashSet<MonoCecilType>(count + 2) { First, Second, Third, first, second });
                }

                return new Multi(new HashSet<MonoCecilType>(count + 3) { First, Second, Third, first, second, third });
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
                yield return Third;

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
                    _ => Create(new HashSet<MonoCecilType?>(set) { First, Second, Third })
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
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                }

                if (found1 && found2 && found3)
                {
                    return this;
                }

                if (found1)
                {
                    return found2 ? Create(First, Second) : found3 ? Create(First, Third) : Create(First);
                }

                if (found2)
                {
                    return found3 ? Create(Second, Third) : Create(Second);
                }

                if (found3)
                {
                    return Create(Third);
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
                Boolean found3 = false;

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
                    else if (Third.Equals(key))
                    {
                        found3 = true;
                    }
                }

                if (found1 && found2 && found3)
                {
                    return this;
                }

                if (found1)
                {
                    return found2 ? Create(First, Second) : found3 ? Create(First, Third) : Create(First);
                }

                if (found2)
                {
                    return found3 ? Create(Second, Third) : Create(Second);
                }

                if (found3)
                {
                    return Create(Third);
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
                Boolean found3 = false;

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

                        if (set.Contains(Third))
                        {
                            found3 = true;
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
                            else if (Third == item)
                            {
                                found3 = true;
                            }
                        }

                        break;
                    }
                }

                if (found1 && found2 && found3)
                {
                    return this;
                }

                if (found1)
                {
                    return found2 ? Create(First, Second) : found3 ? Create(First, Third) : Create(First);
                }

                if (found2)
                {
                    return found3 ? Create(Second, Third) : Create(Second);
                }

                if (found3)
                {
                    return Create(Third);
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
                Boolean found3 = false;

                if (set.Contains(First))
                {
                    found1 = true;
                }

                if (set.Contains(Second))
                {
                    found2 = true;
                }

                if (set.Contains(Third))
                {
                    found3 = true;
                }

                if (found1 && found2 && found3)
                {
                    return this;
                }

                if (found1)
                {
                    return found2 ? Create(First, Second) : found3 ? Create(First, Third) : Create(First);
                }

                if (found2)
                {
                    return found3 ? Create(Second, Third) : Create(Second);
                }

                if (found3)
                {
                    return Create(Third);
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
                Boolean remove3 = false;

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
                    else if (Third.Equals(key))
                    {
                        remove3 = true;
                    }
                }

                if (remove1 && remove2 && remove3)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return remove2 ? Create(Third) : remove3 ? Create(Second) : Create(Second, Third);
                }

                if (remove2)
                {
                    return remove3 ? Create(First) : Create(First, Third);
                }

                if (remove3)
                {
                    return Create(First, Second);
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
                Boolean remove3 = false;

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
                    else if (Third.Equals(key))
                    {
                        remove3 = true;
                    }
                }

                if (remove1 && remove2 && remove3)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return remove2 ? Create(Third) : remove3 ? Create(Second) : Create(Second, Third);
                }

                if (remove2)
                {
                    return remove3 ? Create(First) : Create(First, Third);
                }

                if (remove3)
                {
                    return Create(First, Second);
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
                Boolean remove3 = false;

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

                        if (set.Contains(Third))
                        {
                            remove3 = true;
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
                            else if (Third == item)
                            {
                                remove3 = true;
                            }
                        }

                        break;
                    }
                }

                if (remove1 && remove2 && remove3)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return remove2 ? Create(Third) : remove3 ? Create(Second) : Create(Second, Third);
                }

                if (remove2)
                {
                    return remove3 ? Create(First) : Create(First, Third);
                }

                if (remove3)
                {
                    return Create(First, Second);
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
                Boolean remove3 = false;

                if (set.Contains(First))
                {
                    remove1 = true;
                }

                if (set.Contains(Second))
                {
                    remove2 = true;
                }

                if (set.Contains(Third))
                {
                    remove3 = true;
                }

                if (remove1 && remove2 && remove3)
                {
                    return Empty;
                }

                if (remove1)
                {
                    return remove2 ? Create(Third) : remove3 ? Create(Second) : Create(Second, Third);
                }

                if (remove2)
                {
                    return remove3 ? Create(First) : Create(First, Third);
                }

                if (remove3)
                {
                    return Create(First, Second);
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

                if (Third.Equals(key))
                {
                    return new Two(First, Second);
                }

                if (Second.Equals(key))
                {
                    return new Two(First, Third);
                }

                if (First.Equals(key))
                {
                    return new Two(Second, Third);
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

                if (Third.Equals(key))
                {
                    return new Two(First, Second);
                }

                if (Second.Equals(key))
                {
                    return new Two(First, Third);
                }

                if (First.Equals(key))
                {
                    return new Two(Second, Third);
                }

                return this;
            }

            public override TypeSet Remove(MonoCecilType? item)
            {
                if (item is null)
                {
                    return this;
                }

                if (Third == item)
                {
                    return new Two(First, Second);
                }

                if (Second == item)
                {
                    return new Two(First, Third);
                }

                if (First == item)
                {
                    return new Two(Second, Third);
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
                return new [] { First, Second, Third };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override MonoCecilType[] ToArray(MonoCecilType? @new)
            {
                return @new is not null ? new [] { First, Second, Third, @new } : ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override List<MonoCecilType> ToList()
            {
                return new List<MonoCecilType>(count + 1) { First, Second, Third };
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
                        2 => Third,
                        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
                    };
                }
            }
        }
    }
}