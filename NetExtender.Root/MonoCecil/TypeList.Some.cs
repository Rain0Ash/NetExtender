using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mono.Cecil;

namespace NetExtender.Cecil
{
    public abstract partial class TypeList
    {
        private sealed class Some : TypeList
        {
            protected override TypeSet Set { get; }

            public override MonoCecilType Single
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Set.Single;
                }
            }

            public override MonoCecilType? SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Set.SingleOrDefault;
                }
            }

            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Set.Count;
                }
            }

            internal Some(TypeSet set)
            {
                Set = set;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(Type? item)
            {
                return Set.Contains(item);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(TypeReference? item)
            {
                return Set.Contains(item);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Contains(MonoCecilType? item)
            {
                return Set.Contains(item);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue(Type? equalValue, out Type? actualValue)
            {
                return Set.TryGetValue(equalValue, out actualValue);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue(MonoCecilType equalValue, out MonoCecilType actualValue)
            {
                return Set.TryGetValue(equalValue, out actualValue);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType? Search(Type type)
            {
                return Set.Search(type);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<Type?> other)
            {
                return Set.IsSubsetOf(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<TypeReference> other)
            {
                return Set.IsSubsetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSubsetOf(IEnumerable<MonoCecilType> other)
            {
                return Set.IsSubsetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<Type?> other)
            {
                return Set.IsProperSubsetOf(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<TypeReference> other)
            {
                return Set.IsProperSubsetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSubsetOf(IEnumerable<MonoCecilType> other)
            {
                return Set.IsProperSubsetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<Type?> other)
            {
                return Set.IsSupersetOf(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<TypeReference> other)
            {
                return Set.IsSupersetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsSupersetOf(IEnumerable<MonoCecilType> other)
            {
                return Set.IsSupersetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<Type?> other)
            {
                return Set.IsProperSupersetOf(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<TypeReference> other)
            {
                return Set.IsProperSupersetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsProperSupersetOf(IEnumerable<MonoCecilType> other)
            {
                return Set.IsProperSupersetOf(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<Type?> other)
            {
                return Set.Overlaps(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<TypeReference> other)
            {
                return Set.Overlaps(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Overlaps(IEnumerable<MonoCecilType> other)
            {
                return Set.Overlaps(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<Type?> other)
            {
                return Set.SetEquals(other!);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<TypeReference> other)
            {
                return Set.SetEquals(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean SetEquals(IEnumerable<MonoCecilType> other)
            {
                return Set.SetEquals(other);
            }

            public override TypeList Add(MonoCecilType? item)
            {
                TypeSet set = Set.Add(item);

                if (ReferenceEquals(Set, set))
                {
                    return new Multi(Set.ToArray(item), set);
                }

                return set.Count > TypeSet.Boundary ? new Multi(Set.ToArray(item), set) : new Some(set);
            }

            public override TypeList Union(IEnumerable<Type?>? other)
            {
                TypeSet set = Set.Union(other!);

                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                if (set.Count <= TypeSet.Boundary)
                {
                    return new Some(set);
                }

                List<MonoCecilType> array = new List<MonoCecilType>(set.Count);
                array.AddRange(Set);

                foreach (MonoCecilType item in set)
                {
                    if (!Set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return new Multi(array.ToArray(), set);
            }

            public override TypeList Union(IEnumerable<TypeReference>? other)
            {
                TypeSet set = Set.Union(other);

                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                if (set.Count <= TypeSet.Boundary)
                {
                    return new Some(set);
                }

                List<MonoCecilType> array = new List<MonoCecilType>(set.Count);
                array.AddRange(Set);

                foreach (MonoCecilType item in set)
                {
                    if (!Set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return new Multi(array.ToArray(), set);
            }

            public override TypeList Union(IEnumerable<MonoCecilType>? other)
            {
                TypeSet set = Set.Union(other);

                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                if (set.Count <= TypeSet.Boundary)
                {
                    return new Some(set);
                }

                List<MonoCecilType> array = new List<MonoCecilType>(set.Count);
                array.AddRange(Set);

                foreach (MonoCecilType item in set)
                {
                    if (!Set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return new Multi(array.ToArray(), set);
            }

            public override TypeList Intersect(IEnumerable<Type?>? other)
            {
                TypeSet set = Set.Intersect(other!);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Intersect(IEnumerable<TypeReference>? other)
            {
                TypeSet set = Set.Intersect(other);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Intersect(IEnumerable<MonoCecilType>? other)
            {
                TypeSet set = Set.Intersect(other);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Except(IEnumerable<Type?>? other)
            {
                TypeSet set = Set.Except(other!);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Except(IEnumerable<TypeReference>? other)
            {
                TypeSet set = Set.Except(other);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Except(IEnumerable<MonoCecilType>? other)
            {
                TypeSet set = Set.Except(other);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList Remove(Type? item)
            {
                return RemoveAll(item);
            }

            public override TypeList Remove(TypeReference? item)
            {
                return RemoveAll(item);
            }

            public override TypeList Remove(MonoCecilType? item)
            {
                return RemoveAll(item);
            }

            public override TypeList RemoveAll(Type? item)
            {
                TypeSet set = Set.Remove(item);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList RemoveAll(TypeReference? item)
            {
                TypeSet set = Set.Remove(item);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            public override TypeList RemoveAll(MonoCecilType? item)
            {
                TypeSet set = Set.Remove(item);
                return !ReferenceEquals(Set, set) ? new Some(set) : this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeList Clear()
            {
                return Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(Set);
            }

            public override MonoCecilType this[Int32 index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Set[index];
                }
            }
        }
    }
}