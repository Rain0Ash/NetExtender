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
    public abstract partial class TypeList
    {
        private sealed class Multi : TypeList
        {
            private MonoCecilType[] Array { get; }
            protected override TypeSet Set { get; }

            public override MonoCecilType Single
            {
                get
                {
                    return Array.Length switch
                    {
                        0 => throw new InvalidOperationNoElementsException(),
                        1 => Array[0],
                        _ => throw new InvalidOperationMoreThanOneElementException()
                    };
                }
            }

            public override MonoCecilType? SingleOrDefault
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Array is { Length: 1 } ? Array[0] : null;
                }
            }

            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Array.Length;
                }
            }

            internal Multi(MonoCecilType[] array, TypeSet set)
            {
                Array = array;
                Set = set;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static TypeList Create(MonoCecilType[] array, TypeSet set)
            {
                if (array.Length <= 0)
                {
                    return Empty;
                }

                return array.Length != set.Count || set.Count > TypeSet.Boundary ? new Multi(array, set) : new Some(set);
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

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override MonoCecilType? Search(Type type)
            {
                foreach (MonoCecilType item in Array)
                {
                    if (item == type)
                    {
                        return item;
                    }
                }

                return null;
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

                MonoCecilType[] array = new MonoCecilType[Array.Length + 1];
                Array.CopyTo(array, 0);
                array[^1] = item!;

                return new Multi(array, set);
            }

            public override TypeList Union(IEnumerable<Type?>? other)
            {
                if (other is null)
                {
                    return this;
                }

                HashSet<MonoCecilType?> set = new HashSet<MonoCecilType?>(Set);
                List<MonoCecilType>? @new = null;

                foreach (Type? item in other)
                {
                    MonoCecilType? value = MonoCecilType.From(item);
                    if (value is null || !set.Add(value))
                    {
                        continue;
                    }

                    @new ??= new List<MonoCecilType>();
                    @new.Add(value);
                }

                if (@new is null)
                {
                    return this;
                }

                TypeSet result = TypeSet.Create(set);

                MonoCecilType[] array = new MonoCecilType[Array.Length + @new.Count];
                Array.CopyTo(array, 0);
                @new.CopyTo(array, Array.Length);

                return Create(array, result);
            }

            public override TypeList Union(IEnumerable<TypeReference>? other)
            {
                if (other is null)
                {
                    return this;
                }

                HashSet<MonoCecilType?> set = new HashSet<MonoCecilType?>(Set);
                List<MonoCecilType>? @new = null;

                foreach (TypeReference item in other)
                {
                    MonoCecilType? value = MonoCecilType.From(item);
                    if (value is null || !set.Add(value))
                    {
                        continue;
                    }

                    @new ??= new List<MonoCecilType>();
                    @new.Add(value);
                }

                if (@new is null)
                {
                    return this;
                }

                TypeSet result = TypeSet.Create(set);

                MonoCecilType[] array = new MonoCecilType[Array.Length + @new.Count];
                Array.CopyTo(array, 0);
                @new.CopyTo(array, Array.Length);

                return Create(array, result);
            }

            public override TypeList Union(IEnumerable<MonoCecilType>? other)
            {
                if (other is null)
                {
                    return this;
                }

                HashSet<MonoCecilType?> set = new HashSet<MonoCecilType?>(Set);
                List<MonoCecilType>? @new = null;

                foreach (MonoCecilType item in EnumerableBaseUtilities.WhereNotNull(other))
                {
                    if (!set.Add(item))
                    {
                        continue;
                    }

                    @new ??= new List<MonoCecilType>();
                    @new.Add(item);
                }

                if (@new is null)
                {
                    return this;
                }

                TypeSet result = TypeSet.Create(set);

                MonoCecilType[] array = new MonoCecilType[Array.Length + @new.Count];
                Array.CopyTo(array, 0);
                @new.CopyTo(array, Array.Length);

                return Create(array, result);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Intersect(IEnumerable<Type?>? other)
            {
                TypeSet set = Set.Intersect(other!);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Intersect(IEnumerable<TypeReference>? other)
            {
                TypeSet set = Set.Intersect(other);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Intersect(IEnumerable<MonoCecilType>? other)
            {
                TypeSet set = Set.Intersect(other);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Except(IEnumerable<Type?>? other)
            {
                TypeSet set = Set.Except(other!);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Except(IEnumerable<TypeReference>? other)
            {
                TypeSet set = Set.Except(other);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList Except(IEnumerable<MonoCecilType>? other)
            {
                TypeSet set = Set.Except(other);
                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType item in Array)
                {
                    if (set.Contains(item))
                    {
                        array.Add(item);
                    }
                }

                return Create(array.ToArray(), set);
            }

            public override TypeList Remove(Type? item)
            {
                return Remove(MonoCecilType.From(item));
            }

            public override TypeList Remove(TypeReference? item)
            {
                return Remove(MonoCecilType.From(item));
            }

            public override TypeList Remove(MonoCecilType? item)
            {
                if (item is null)
                {
                    return this;
                }

                Int32 index = -1;
                Boolean multi = false;
                for (Int32 i = Array.Length - 1; i >= 0; i--)
                {
                    if (Array[i] != item)
                    {
                        continue;
                    }

                    if (index >= 0)
                    {
                        multi = true;
                        break;
                    }

                    index = i;
                }

                if (index < 0)
                {
                    return this;
                }

                List<MonoCecilType> array = Array.ToList();
                array.RemoveAt(index);

                if (multi)
                {
                    return Create(array.ToArray(), Set);
                }

                TypeSet set = Set.Remove(item);
                return Create(array.ToArray(), set);
            }

            public override TypeList RemoveAll(Type? item)
            {
                return RemoveAll(MonoCecilType.From(item));
            }

            public override TypeList RemoveAll(TypeReference? item)
            {
                return RemoveAll(MonoCecilType.From(item));
            }

            [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
            public override TypeList RemoveAll(MonoCecilType? item)
            {
                TypeSet set = Set.Remove(item);

                if (ReferenceEquals(Set, set))
                {
                    return this;
                }

                List<MonoCecilType> array = new List<MonoCecilType>(Array.Length);
                foreach (MonoCecilType value in Array)
                {
                    if (value != item)
                    {
                        array.Add(value);
                    }
                }

                return Create(array.ToArray(), set);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override TypeList Clear()
            {
                return Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Enumerator GetEnumerator()
            {
                return new Enumerator(Array);
            }

            public override MonoCecilType this[Int32 index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Array[index];
                }
            }
        }
    }
}