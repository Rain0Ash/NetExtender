using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Mono.Cecil;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Cecil
{
    public abstract partial class TypeList : IReadOnlySet<Type?>, IImmutableSet<Type?>, IReadOnlyList<Type?>, IReadOnlySet<MonoCecilType>, IImmutableSet<MonoCecilType>, IReadOnlyList<MonoCecilType>
    {
        public static TypeList Empty { get; } = new Some(TypeSet.Empty);

        protected abstract TypeSet Set { get; }
        public abstract MonoCecilType Single { get; }
        public abstract MonoCecilType? SingleOrDefault { get; }
        public abstract Int32 Count { get; }

        internal static TypeList Create(IEnumerable<Type?>? source)
        {
            if (source is null)
            {
                return Empty;
            }

            MonoCecilType[] array = EnumerableBaseUtilities.WhereNotNull(TypeSet.From(source)).ToArray();
            TypeSet set = TypeSet.Create(array);
            return array.Length != set.Count || set.Count > TypeSet.Boundary ? new Multi(array, set) : new Some(set);
        }

        internal static TypeList Create(IEnumerable<MonoCecilType?>? source)
        {
            if (source is null)
            {
                return Empty;
            }

            MonoCecilType[] array = EnumerableBaseUtilities.WhereNotNull(source).ToArray();
            TypeSet set = TypeSet.Create(array);
            return array.Length != set.Count || set.Count > TypeSet.Boundary ? new Multi(array, set) : new Some(set);
        }

        public abstract Boolean Contains(Type? item);
        public abstract Boolean Contains(TypeReference? item);
        public abstract Boolean Contains(MonoCecilType? item);
        public abstract Boolean TryGetValue(Type? equalValue, out Type? actualValue);
        public abstract Boolean TryGetValue(MonoCecilType equalValue, out MonoCecilType actualValue);
        public abstract MonoCecilType? Search(Type type);
        public abstract Boolean IsSubsetOf(IEnumerable<Type?> other);
        public abstract Boolean IsSubsetOf(IEnumerable<TypeReference> other);
        public abstract Boolean IsSubsetOf(IEnumerable<MonoCecilType> other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<Type?> other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<TypeReference> other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<MonoCecilType> other);
        public abstract Boolean IsSupersetOf(IEnumerable<Type?> other);
        public abstract Boolean IsSupersetOf(IEnumerable<TypeReference> other);
        public abstract Boolean IsSupersetOf(IEnumerable<MonoCecilType> other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<Type?> other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<TypeReference> other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<MonoCecilType> other);
        public abstract Boolean Overlaps(IEnumerable<Type?> other);
        public abstract Boolean Overlaps(IEnumerable<TypeReference> other);
        public abstract Boolean Overlaps(IEnumerable<MonoCecilType> other);
        public abstract Boolean SetEquals(IEnumerable<Type?> other);
        public abstract Boolean SetEquals(IEnumerable<TypeReference> other);
        public abstract Boolean SetEquals(IEnumerable<MonoCecilType> other);

        public TypeList Add(Type? item)
        {
            return Add(MonoCecilType.From(item));
        }

        IImmutableSet<Type?> IImmutableSet<Type?>.Add(Type? item)
        {
            return Add(item);
        }

        public TypeList Add(TypeReference? item)
        {
            return Add(MonoCecilType.From(item));
        }

        public abstract TypeList Add(MonoCecilType? item);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Add(MonoCecilType item)
        {
            return Add(item);
        }

        public abstract TypeList Union(IEnumerable<Type?>? other);

        IImmutableSet<Type?> IImmutableSet<Type?>.Union(IEnumerable<Type?>? other)
        {
            return Union(other);
        }

        public abstract TypeList Union(IEnumerable<TypeReference>? other);
        public abstract TypeList Union(IEnumerable<MonoCecilType>? other);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Union(IEnumerable<MonoCecilType>? other)
        {
            return Union(other);
        }

        public abstract TypeList Intersect(IEnumerable<Type?>? other);

        IImmutableSet<Type?> IImmutableSet<Type?>.Intersect(IEnumerable<Type?>? other)
        {
            return Intersect(other);
        }

        public abstract TypeList Intersect(IEnumerable<TypeReference>? other);
        public abstract TypeList Intersect(IEnumerable<MonoCecilType>? other);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Intersect(IEnumerable<MonoCecilType>? other)
        {
            return Intersect(other);
        }

        public abstract TypeList Except(IEnumerable<Type?>? other);

        IImmutableSet<Type?> IImmutableSet<Type?>.Except(IEnumerable<Type?>? other)
        {
            return Except(other);
        }

        public abstract TypeList Except(IEnumerable<TypeReference>? other);
        public abstract TypeList Except(IEnumerable<MonoCecilType>? other);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Except(IEnumerable<MonoCecilType>? other)
        {
            return Except(other);
        }

        IImmutableSet<Type?> IImmutableSet<Type?>.SymmetricExcept(IEnumerable<Type?>? other)
        {
            return ((IImmutableSet<Type?>) Set).SymmetricExcept(other!);
        }

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.SymmetricExcept(IEnumerable<MonoCecilType>? other)
        {
            return ((IImmutableSet<MonoCecilType>) Set).SymmetricExcept(other!);
        }

        public abstract TypeList Remove(Type? item);

        IImmutableSet<Type?> IImmutableSet<Type?>.Remove(Type? item)
        {
            return RemoveAll(item);
        }

        public abstract TypeList Remove(TypeReference? item);
        public abstract TypeList Remove(MonoCecilType? item);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Remove(MonoCecilType item)
        {
            return RemoveAll(item);
        }

        public abstract TypeList RemoveAll(Type? item);
        public abstract TypeList RemoveAll(TypeReference? item);
        public abstract TypeList RemoveAll(MonoCecilType? item);

        public abstract TypeList Clear();

        IImmutableSet<Type?> IImmutableSet<Type?>.Clear()
        {
            return Clear();
        }

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Clear()
        {
            return Clear();
        }

        public abstract Enumerator GetEnumerator();

        IEnumerator<Type?> IEnumerable<Type?>.GetEnumerator()
        {
            foreach (MonoCecilType item in this)
            {
                yield return item;
            }
        }

        IEnumerator<MonoCecilType> IEnumerable<MonoCecilType>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract MonoCecilType this[Int32 index] { get; }

        Type? IReadOnlyList<Type?>.this[Int32 index]
        {
            get
            {
                return this[index];
            }
        }

        public struct Enumerator : IEnumerator<MonoCecilType>
        {
            private readonly IEnumerator<MonoCecilType>? Internal = null;
            private TypeSet.Enumerator Set;

            public readonly MonoCecilType Current
            {
                get
                {
                    return Internal is { } enumerator ? enumerator.Current : Set.Current;
                }
            }

            readonly Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(IEnumerable<MonoCecilType> array)
            {
                Internal = array.GetEnumerator();
                Set = default;
            }

            public Enumerator(TypeSet set)
            {
                Set = set.GetEnumerator();
            }

            public Boolean MoveNext()
            {
                return Internal is { } enumerator ? enumerator.MoveNext() : Set.MoveNext();
            }

            public void Reset()
            {
                if (Internal is { } enumerator)
                {
                    enumerator.Reset();
                }
                else
                {
                    Set.Reset();
                }
            }

            public void Dispose()
            {
                if (Internal is { } enumerator)
                {
                    enumerator.Dispose();
                }
                else
                {
                    Set.Dispose();
                }
            }
        }
    }
}