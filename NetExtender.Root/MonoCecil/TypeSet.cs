using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Cecil
{
    public abstract partial class TypeSet : IReadOnlySet<Type>, IImmutableSet<Type>, IReadOnlySet<MonoCecilType>, IImmutableSet<MonoCecilType>, IReadOnlyList<MonoCecilType>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator TypeSet?(Assembly? value)
        {
            return value is not null ? MonoCecilType.From(value) : null;
        }

        public static TypeSet Empty
        {
            get
            {
                return Zero.Instance;
            }
        }

        public const Int32 Boundary = 3;

        public abstract Int32 Count { get; }
        public abstract MonoCecilType First { get; }
        public abstract MonoCecilType Single { get; }
        public abstract MonoCecilType? SingleOrDefault { get; }

        public IImmutableSet<Type> Types
        {
            get
            {
                return this;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TypeSet Create(MonoCecilType? single)
        {
            return single is not null ? new One(single) : Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeSet Create(MonoCecilType? first, MonoCecilType? second)
        {
            if (first is null)
            {
                return Create(second);
            }

            if (second is null)
            {
                return Create(first);
            }

            return new Two(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeSet Create(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third)
        {
            if (first is null)
            {
                return Create(second, third);
            }

            if (second is null)
            {
                return Create(first, third);
            }

            if (third is null)
            {
                return Create(first, second);
            }

            return new Three(first, second, third);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static TypeSet Create(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third, MonoCecilType? fourth)
        {
            if (first is null)
            {
                return Create(second, third, fourth);
            }

            if (second is null)
            {
                return Create(first, third, fourth);
            }

            if (third is null)
            {
                return Create(first, second, fourth);
            }

            if (fourth is null)
            {
                return Create(first, second, third);
            }

            return new Multi(new HashSet<MonoCecilType> { first, second, third, fourth });
        }

        internal static TypeSet Create(IEnumerable<Type?>? source)
        {
            return source.CanHaveCount() ? new Builder<IEnumerator<MonoCecilType?>>(source.Select(MonoCecilType.From).GetEnumerator()).Build() : Empty;
        }

        internal static TypeSet Create(IEnumerable<MonoCecilType?>? source)
        {
            return source.CanHaveCount() ? new Builder<IEnumerator<MonoCecilType?>>(source.GetEnumerator()).Build() : Empty;
        }

        internal static TypeSet Create(HashSet<MonoCecilType?>? set)
        {
            if (set is null || set.Count <= 0)
            {
                return Empty;
            }

            return set.Count switch
            {
                1 or 2 or 3 => new FastBuilder<HashSet<MonoCecilType?>.Enumerator>(set.GetEnumerator()).Build(),
                _ => new Multi(set!)
            };
        }

        internal static TypeSet Create(ImmutableHashSet<MonoCecilType?>? set)
        {
            return set is not null && set.Count > 0 ? new FastBuilder<ImmutableHashSet<MonoCecilType?>.Enumerator>(set.GetEnumerator()).Build() : Empty;
        }

        internal static TypeSet Create(ImmutableHashSet<MonoCecilType?>.Builder? set)
        {
            return set is not null && set.Count > 0 ? new FastBuilder<ImmutableHashSet<MonoCecilType?>.Enumerator>(set.GetEnumerator()).Build() : Empty;
        }

        internal static IEnumerable<MonoCecilType> From(IEnumerable<Type?> source)
        {
            return EnumerableBaseUtilities.WhereNotNull(source.Select(MonoCecilType.From));
        }

        internal static IEnumerable<MonoCecilType> From(IEnumerable<TypeReference?> source)
        {
            return EnumerableBaseUtilities.WhereNotNull(source.Select(MonoCecilType.From));
        }

        public abstract Boolean Contains(Type? item);
        public abstract Boolean Contains(TypeReference? item);
        public abstract Boolean Contains(MonoCecilType? item);
        public abstract Boolean TryGetValue(Type? equalValue, out Type actualValue);
        public abstract Boolean TryGetValue(MonoCecilType? equalValue, out MonoCecilType actualValue);
        public abstract MonoCecilType? Search(Type? type);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type? Search(Predicate<Type> predicate)
        {
            return Search(predicate, false);
        }

        public Type? Search(Predicate<Type> predicate, Boolean single)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Type? result = null;

            foreach (MonoCecilType item in this)
            {
                if (item.Type is not { } type || !predicate(type))
                {
                    continue;
                }

                if (result is not null)
                {
                    throw new InvalidOperationMoreThanOneMatchException();
                }

                result = type;

                if (!single)
                {
                    break;
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type Require(Predicate<Type> predicate)
        {
            return Require(predicate, false);
        }

        public Type Require(Predicate<Type> predicate, Boolean single)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Type? result = null;

            foreach (MonoCecilType item in this)
            {
                if (item.Type is not { } type || !predicate(type))
                {
                    continue;
                }

                if (result is not null)
                {
                    throw new InvalidOperationMoreThanOneMatchException();
                }

                result = type;

                if (!single)
                {
                    break;
                }
            }

            return result ?? throw new InvalidOperationNoMatchException();
        }

        public abstract TypeSet Where(Func<MonoCecilType, Boolean> predicate);
        public abstract TypeSet Where<TArgument>(TArgument argument, Func<MonoCecilType, TArgument, Boolean> predicate);

        public abstract Boolean IsSubsetOf(IEnumerable<Type>? other);
        public abstract Boolean IsSubsetOf(IEnumerable<TypeReference>? other);
        public abstract Boolean IsSubsetOf(IEnumerable<MonoCecilType>? other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<Type>? other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<TypeReference>? other);
        public abstract Boolean IsProperSubsetOf(IEnumerable<MonoCecilType>? other);
        public abstract Boolean IsSupersetOf(IEnumerable<Type>? other);
        public abstract Boolean IsSupersetOf(IEnumerable<TypeReference>? other);
        public abstract Boolean IsSupersetOf(IEnumerable<MonoCecilType>? other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<Type>? other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<TypeReference>? other);
        public abstract Boolean IsProperSupersetOf(IEnumerable<MonoCecilType>? other);
        public abstract Boolean Overlaps(IEnumerable<Type>? other);
        public abstract Boolean Overlaps(IEnumerable<TypeReference>? other);
        public abstract Boolean Overlaps(IEnumerable<MonoCecilType>? other);
        public abstract Boolean SetEquals(IEnumerable<Type>? other);
        public abstract Boolean SetEquals(IEnumerable<TypeReference>? other);
        public abstract Boolean SetEquals(IEnumerable<MonoCecilType>? other);

        public TypeSet Add(Type? item)
        {
            return Add(MonoCecilType.From(item));
        }

        IImmutableSet<Type> IImmutableSet<Type>.Add(Type item)
        {
            return Add(item);
        }

        public TypeSet Add(TypeReference? item)
        {
            return Add(MonoCecilType.From(item));
        }

        public abstract TypeSet Add(MonoCecilType? item);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Add(MonoCecilType item)
        {
            return Add(item);
        }

        public TypeSet Add(Type? first, Type? second)
        {
            return Add(MonoCecilType.From(first), MonoCecilType.From(second));
        }

        public TypeSet Add(TypeReference? first, TypeReference? second)
        {
            return Add(MonoCecilType.From(first), MonoCecilType.From(second));
        }

        public abstract TypeSet Add(MonoCecilType? first, MonoCecilType? second);

        public TypeSet Add(Type? first, Type? second, Type? third)
        {
            return Add(MonoCecilType.From(first), MonoCecilType.From(second), MonoCecilType.From(third));
        }

        public TypeSet Add(TypeReference? first, TypeReference? second, TypeReference? third)
        {
            return Add(MonoCecilType.From(first), MonoCecilType.From(second), MonoCecilType.From(third));
        }

        public abstract TypeSet Add(MonoCecilType? first, MonoCecilType? second, MonoCecilType? third);
        public abstract TypeSet Add(TypeSet? set);

        public abstract TypeSet Union(IEnumerable<Type>? other);

        IImmutableSet<Type> IImmutableSet<Type>.Union(IEnumerable<Type>? other)
        {
            return Union(other);
        }

        public abstract TypeSet Union(IEnumerable<TypeReference>? other);
        public abstract TypeSet Union(IEnumerable<MonoCecilType>? other);
        public abstract TypeSet Union(TypeSet? set);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Union(IEnumerable<MonoCecilType>? other)
        {
            return Union(other);
        }

        public abstract TypeSet Intersect(IEnumerable<Type>? other);

        IImmutableSet<Type> IImmutableSet<Type>.Intersect(IEnumerable<Type>? other)
        {
            return Intersect(other);
        }

        public abstract TypeSet Intersect(IEnumerable<TypeReference>? other);
        public abstract TypeSet Intersect(IEnumerable<MonoCecilType>? other);
        public abstract TypeSet Intersect(TypeSet? set);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Intersect(IEnumerable<MonoCecilType>? other)
        {
            return Intersect(other);
        }

        public abstract TypeSet Except(IEnumerable<Type>? other);

        IImmutableSet<Type> IImmutableSet<Type>.Except(IEnumerable<Type>? other)
        {
            return Except(other);
        }

        public abstract TypeSet Except(IEnumerable<TypeReference>? other);
        public abstract TypeSet Except(IEnumerable<MonoCecilType>? other);
        public abstract TypeSet Except(TypeSet? set);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Except(IEnumerable<MonoCecilType>? other)
        {
            return Except(other);
        }

        IImmutableSet<Type> IImmutableSet<Type>.SymmetricExcept(IEnumerable<Type>? other)
        {
            if (other.CantHaveCount())
            {
                return this;
            }

            HashSet<MonoCecilType?> set = new HashSet<MonoCecilType?>(this);
            set.SymmetricExceptWith(other.Select(MonoCecilType.From));
            return Create(set);
        }

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.SymmetricExcept(IEnumerable<MonoCecilType>? other)
        {
            if (other.CantHaveCount())
            {
                return this;
            }

            HashSet<MonoCecilType?> set = new HashSet<MonoCecilType?>(this);
            set.SymmetricExceptWith(other);
            return Create(set);
        }

        public abstract TypeSet Remove(Type? item);

        IImmutableSet<Type> IImmutableSet<Type>.Remove(Type item)
        {
            return Remove(item);
        }

        public abstract TypeSet Remove(TypeReference? item);
        public abstract TypeSet Remove(MonoCecilType? item);

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Remove(MonoCecilType item)
        {
            return Remove(item);
        }

        public abstract TypeSet Remove(TypeSet? set);
        public abstract TypeSet Clear();

        IImmutableSet<Type> IImmutableSet<Type>.Clear()
        {
            return Clear();
        }

        IImmutableSet<MonoCecilType> IImmutableSet<MonoCecilType>.Clear()
        {
            return Clear();
        }

        public abstract MonoCecilType[] ToArray();
        internal abstract MonoCecilType[] ToArray(MonoCecilType? @new);
        public abstract List<MonoCecilType> ToList();

        public abstract Enumerator GetEnumerator();

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            foreach (MonoCecilType item in this)
            {
                Type? result;
                if ((result = item) is not null)
                {
                    yield return result;
                }
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

        public struct Enumerator : IEnumerator<MonoCecilType>
        {
#if NET8_0_OR_GREATER
            private FrozenSet<MonoCecilType>.Enumerator Internal;
#else
            private HashSet<MonoCecilType>.Enumerator Internal;
#endif
            private Int32 _index = Int32.MinValue;
            private TypeSet Set { get; } = Zero.Instance;

            public readonly MonoCecilType Current
            {
                get
                {
                    return _index switch
                    {
                        -1 or -2 => throw new InvalidOperationException(),
                        Int32.MinValue => Internal.Current,
                        _ => Set[_index]
                    };
                }
            }

            readonly Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

#if NET8_0_OR_GREATER
            public Enumerator(FrozenSet<MonoCecilType>.Enumerator enumerator)
            {
                Internal = enumerator;
            }
#else
            public Enumerator(HashSet<MonoCecilType>.Enumerator enumerator)
            {
                Internal = enumerator;
            }
#endif
            internal Enumerator(TypeSet set)
            {
                Internal = default;
                Set = set;
                _index = -1;
            }

            public Boolean MoveNext()
            {
                if (_index == Int32.MinValue)
                {
                    return Internal.MoveNext();
                }

                Int32 index = _index;
                if (index < -1 || ++index >= Set.Count)
                {
                    _index = -2;
                    return false;
                }

                _index = index;
                return true;
            }

            public void Reset()
            {
                if (_index == Int32.MinValue)
                {
                    Internal.TryReset();
                    return;
                }

                _index = -1;
            }

            public void Dispose()
            {
                if (_index == Int32.MinValue)
                {
                    Internal.TryDispose();
                    return;
                }

                _index = -1;
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct Builder<TEnumerator> : IDisposable where TEnumerator : IEnumerator<MonoCecilType?>, IDisposable
        {
            private TEnumerator Enumerator;

            private MonoCecilType? First = null;
            private MonoCecilType? Second = null;
            private MonoCecilType? Third = null;

            private readonly Int32 Count
            {
                get
                {
                    Int32 count = 0;

                    if (First is not null)
                    {
                        count++;
                    }

                    if (Second is not null)
                    {
                        count++;
                    }

                    if (Third is not null)
                    {
                        count++;
                    }

                    return count;
                }
            }

            public Builder(TEnumerator enumerator)
            {
                Enumerator = enumerator;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public TypeSet Build()
            {
                HashSet<MonoCecilType>? set;

                while (Enumerator.MoveNext())
                {
                    if (Enumerator.Current is not { } item)
                    {
                        continue;
                    }

                    if (First is null)
                    {
                        First = item;
                        continue;
                    }

                    if (First == item)
                    {
                        continue;
                    }

                    if (Second is null)
                    {
                        Second = item;
                        continue;
                    }

                    if (Second == item)
                    {
                        continue;
                    }

                    if (Third is null)
                    {
                        Third = item;
                        continue;
                    }

                    if (Third == item)
                    {
                        continue;
                    }

                    set = new HashSet<MonoCecilType> { First, Second, Third, item };
                    goto set;
                }

                Dispose();

                return Count switch
                {
                    0 => Empty,
                    1 => new One(First!),
                    2 => new Two(First!, Second!),
                    3 => new Three(First!, Second!, Third!),
                    _ => throw new NeverOperationException()
                };

                set:
                while (Enumerator.MoveNext())
                {
                    if (Enumerator.Current is { } item)
                    {
                        set.Add(item);
                    }
                }

                Dispose();
                return new Multi(set);
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private struct FastBuilder<TEnumerator> where TEnumerator : IEnumerator<MonoCecilType?>
        {
            private TEnumerator Enumerator;

            private MonoCecilType? First = null;
            private MonoCecilType? Second = null;
            private MonoCecilType? Third = null;

            private readonly Int32 Count
            {
                get
                {
                    Int32 count = 0;

                    if (First is not null)
                    {
                        count++;
                    }

                    if (Second is not null)
                    {
                        count++;
                    }

                    if (Third is not null)
                    {
                        count++;
                    }

                    return count;
                }
            }

            public FastBuilder(TEnumerator enumerator)
            {
                Enumerator = enumerator;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public TypeSet Build()
            {
                HashSet<MonoCecilType>? set;

                while (Enumerator.MoveNext())
                {
                    if (Enumerator.Current is not { } item)
                    {
                        continue;
                    }

                    if (First is null)
                    {
                        First = item;
                        continue;
                    }

                    if (Second is null)
                    {
                        Second = item;
                        continue;
                    }

                    if (Third is null)
                    {
                        Third = item;
                        continue;
                    }

                    set = new HashSet<MonoCecilType> { First, Second, Third, item };
                    goto set;
                }

                Dispose();

                return Count switch
                {
                    0 => Empty,
                    1 => new One(First!),
                    2 => new Two(First!, Second!),
                    3 => new Three(First!, Second!, Third!),
                    _ => throw new NeverOperationException()
                };

                set:
                while (Enumerator.MoveNext())
                {
                    if (Enumerator.Current is { } item)
                    {
                        set.Add(item);
                    }
                }

                Dispose();
                return new Multi(set);
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }
        }
    }
}