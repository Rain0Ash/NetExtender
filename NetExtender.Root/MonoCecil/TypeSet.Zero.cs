using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Cecil
{
    public abstract partial class TypeSet
    {
        private sealed class Zero : TypeSet
        {
            public static TypeSet Instance { get; } = new Zero();

            private const Int32 count = 0;
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
                    throw new InvalidOperationNoElementsException();
                }
            }

            public override MonoCecilType Single
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new InvalidOperationNoElementsException();
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

            private Zero()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(Type? item)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TypeReference? item)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(MonoCecilType? item)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue(Type? equalValue, out Type actualValue)
            {
                actualValue = equalValue!;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue(MonoCecilType? equalValue, out MonoCecilType actualValue)
            {
                actualValue = equalValue!;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType? Search(Type? type)
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Where(Func<MonoCecilType, Boolean> predicate)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<Type>? other)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<TypeReference>? other)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<Type>? other)
            {
                return other is not null && EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<TypeReference>? other)
            {
                return other is not null && EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<MonoCecilType>? other)
            {
                return other is not null && EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<Type>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<TypeReference>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<Type>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<Type>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<TypeReference>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<MonoCecilType>? other)
            {
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<Type>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<TypeReference>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<MonoCecilType>? other)
            {
                return other is null || !EnumerableBaseUtilities.AnyNotNull(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Add(MonoCecilType? item)
            {
                return item is not null ? new One(item) : this;
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second)
            {
                if (first is null)
                {
                    return Add(second);
                }

                if (second is null || first == second)
                {
                    return Add(first);
                }

                return new Two(first, second);
            }

            public override TypeSet Add(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third)
            {
                if (first is null)
                {
                    return Add(second, third);
                }

                if (second is null || first == second)
                {
                    return Add(first, third);
                }

                if (third is null || first == third || second == third)
                {
                    return new Two(first, second);
                }

                return new Three(first, second, third);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Add(TypeSet? set)
            {
                return Union(set);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Union(IEnumerable<Type>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(From(other)) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Union(IEnumerable<TypeReference>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(From(other)) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Union(IEnumerable<MonoCecilType>? other)
            {
                return other is not null && other.CanHaveCount() ? Create(other) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Union(TypeSet? set)
            {
                return set ?? this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Intersect(IEnumerable<Type>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Intersect(IEnumerable<TypeReference>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Intersect(IEnumerable<MonoCecilType>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Intersect(TypeSet? set)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Except(IEnumerable<Type>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Except(IEnumerable<TypeReference>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Except(IEnumerable<MonoCecilType>? other)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Except(TypeSet? set)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(Type? item)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeReference? item)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(MonoCecilType? item)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Remove(TypeSet? set)
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeSet Clear()
            {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType[] ToArray()
            {
                return Array.Empty<MonoCecilType>();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override MonoCecilType[] ToArray(MonoCecilType? @new)
            {
                return @new is not null ? new [] { @new } : ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override List<MonoCecilType> ToList()
            {
                return new List<MonoCecilType>(count + 1);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(this);
            }

            public override MonoCecilType this[Int32 index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
            }
        }
    }
}