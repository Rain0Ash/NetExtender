// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Enumerators;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Lists;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Enums
{
    public partial class Enum<T, TEnum>
    {
        public new struct FlagsEnumerator : IEquatableStruct<FlagsEnumerator>, IReadOnlySortedList<Enum<T, TEnum>>, IReadOnlySortedList<Enum<T>>, IReadOnlySortedList<T>, IReadOnlySortedList<IEnum<T>>, IReadOnlySortedList<IEnum<T, TEnum>>, IEnumerator<Enum<T, TEnum>>, IEnumerator<Enum<T>>, IEnumerator<T>, IEnumerator<IEnum<T>>, IEnumerator<IEnum<T, TEnum>>, IEquatable<Enum<T, TEnum>>, IEquatable<Enum<T>>, IEquatable<T>, IEquatable<IEnum<T>>, IEquatable<IEnum<T, TEnum>>, IEquatable<EnumFlagsEnumerator<T>>, IEquatable<FlagsEnumerator.Any>, ICloneable<FlagsEnumerator>, ICloneable, IFormattable
        {
            public static implicit operator FlagsEnumerator(TEnum value)
            {
                return new FlagsEnumerator(value);
            }

            public static implicit operator Enum<T>.FlagsEnumerator(FlagsEnumerator value)
            {
                return value.Value is { } @enum ? new Enum<T>.FlagsEnumerator(@enum) : default;
            }

            public static implicit operator EnumFlagsEnumerator<T>(FlagsEnumerator value)
            {
                return new EnumFlagsEnumerator<T>(value.Id);
            }

            public static implicit operator TEnum(FlagsEnumerator value)
            {
                return value.Value;
            }

            public static implicit operator Enum<T, TEnum>(FlagsEnumerator value)
            {
                return value.Value;
            }

            public static implicit operator Enum<T>(FlagsEnumerator value)
            {
                return value.Value;
            }

            public static implicit operator T(FlagsEnumerator value)
            {
                return value.Id;
            }

            public static Boolean operator ==(FlagsEnumerator first, FlagsEnumerator second)
            {
                return first.Same(second);
            }

            public static Boolean operator !=(FlagsEnumerator first, FlagsEnumerator second)
            {
                return !(first == second);
            }

            private EnumFlagsEnumerator<T> Enumerator;

            public readonly EnumComparer<T> Comparer
            {
                get
                {
                    return Enumerator.Comparer;
                }
            }

            public readonly Any Interface
            {
                get
                {
                    return this;
                }
            }

            internal readonly Wrapper This
            {
                get
                {
                    return this;
                }
            }

            readonly IComparer<T> IReadOnlySortedList<T>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<Enum<T>> IReadOnlySortedList<Enum<T>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<Enum<T, TEnum>> IReadOnlySortedList<Enum<T, TEnum>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<IEnum<T>> IReadOnlySortedList<IEnum<T>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<IEnum<T, TEnum>> IReadOnlySortedList<IEnum<T, TEnum>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            public readonly T Id
            {
                get
                {
                    return Value?.Id ?? default;
                }
            }

            public TEnum Value { get; }

            public readonly Int32 Index
            {
                get
                {
                    return Enumerator.Index;
                }
            }

            public readonly Int32 Count
            {
                get
                {
                    return Enumerator.Count;
                }
            }

            public readonly TEnum Current
            {
                get
                {
                    Enum<T, TEnum> result = Enumerator.Current;
                    return result.This;
                }
            }

            readonly T IEnumerator<T>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly Enum<T> IEnumerator<Enum<T>>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly Enum<T, TEnum> IEnumerator<Enum<T, TEnum>>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly IEnum<T> IEnumerator<IEnum<T>>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly IEnum<T, TEnum> IEnumerator<IEnum<T, TEnum>>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Index >= 0 ? Current : null;
                }
            }
            
            public readonly Boolean IsFlags
            {
                get
                {
                    return Value?.IsFlags ?? Enumerator.IsFlags;
                }
            }
            
            public readonly Boolean IsIntern
            {
                get
                {
                    return Value?.IsIntern ?? true;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return Enumerator.IsEmpty;
                }
            }

            public FlagsEnumerator(TEnum value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                Enumerator = new EnumFlagsEnumerator<T>(value.Id);
            }

            public Boolean MoveNext()
            {
                return Enumerator.MoveNext();
            }

            public void Reset()
            {
                Enumerator.Reset();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly TEnum[] ToArray()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableArray<TEnum> ToImmutableArray()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToImmutableArray();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly List<TEnum> ToList()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToList();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableList<TEnum> ToImmutableList()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToImmutableList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedList<TEnum> ToSortedList()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToSortedList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly HashSet<TEnum> ToHashSet()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableHashSet<TEnum> ToImmutableHashSet()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToImmutableHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedSet<TEnum> ToSortedSet()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToSortedSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableSortedSet<TEnum> ToImmutableSortedSet()
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).ToImmutableSortedSet();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(T[] array)
            {
                new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum[] array)
            {
                new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T>[] array)
            {
                new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T, TEnum>[] array)
            {
                new EnumeratorFiller<Enum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(TEnum[] array)
            {
                new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum[] array)
            {
                new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T>[] array)
            {
                new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T, TEnum>[] array)
            {
                new EnumeratorFiller<IEnum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(T[] array, Int32 index)
            {
                new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum[] array, Int32 index)
            {
                new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T>[] array, Int32 index)
            {
                new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T, TEnum>[] array, Int32 index)
            {
                new EnumeratorFiller<Enum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(TEnum[] array, Int32 index)
            {
                new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum[] array, Int32 index)
            {
                new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T>[] array, Int32 index)
            {
                new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T, TEnum>[] array, Int32 index)
            {
                new EnumeratorFiller<IEnum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<T> destination)
            {
                return new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum> destination)
            {
                return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T>> destination)
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T, TEnum>> destination)
            {
                return new EnumeratorFiller<Enum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<TEnum> destination)
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum> destination)
            {
                return new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T>> destination)
            {
                return new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T, TEnum>> destination)
            {
                return new EnumeratorFiller<IEnum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
            {
                return new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
            {
                return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T>> destination, out Int32 written)
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T, TEnum>> destination, out Int32 written)
            {
                return new EnumeratorFiller<Enum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<TEnum> destination, out Int32 written)
            {
                return new EnumeratorFiller<TEnum, Wrapper, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum> destination, out Int32 written)
            {
                return new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T>> destination, out Int32 written)
            {
                return new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T, TEnum>> destination, out Int32 written)
            {
                return new EnumeratorFiller<IEnum<T, TEnum>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            public readonly FlagsEnumerator GetEnumerator()
            {
                FlagsEnumerator enumerator = this;
                enumerator.Reset();
                return enumerator;
            }

            readonly IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<Enum<T>> IEnumerable<Enum<T>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<Enum<T, TEnum>> IEnumerable<Enum<T, TEnum>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<IEnum<T>> IEnumerable<IEnum<T>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<IEnum<T, TEnum>> IEnumerable<IEnum<T, TEnum>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override readonly Int32 GetHashCode()
            {
                return Value?.GetHashCode() ?? 0;
            }

            public override readonly Boolean Equals(Object? other)
            {
                return other switch
                {
                    Any value => Equals(value),
                    FlagsEnumerator value => Equals(value),
                    EnumFlagsEnumerator<T> value => Equals(value),
                    Enum<T> value => Equals(value),
                    T value => Equals(value),
                    Enum value => Equals(value),
                    IEnum<T> value => Equals(value),
                    IEnum value => Equals(value),
                    _ => false
                };
            }

            public readonly Boolean Equals(Any other)
            {
                return Equals((FlagsEnumerator) other);
            }

            public readonly Boolean Equals(FlagsEnumerator other)
            {
                return Equals(other.Value) && Equals(other.Enumerator);
            }

            public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
            {
                return Enumerator.Equals(other);
            }

            public readonly Boolean Equals(T other)
            {
                return Comparer.Equals(Value?.Id, other) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(Enum? other)
            {
                return Comparer.Equals(Value?.Id, other) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(Enum<T>? other)
            {
                return Comparer.Equals(Value, other) && Enumerator.Equals(other?.Id ?? default);
            }

            public readonly Boolean Equals(Enum<T, TEnum>? other)
            {
                return Equals((Enum<T>?) other);
            }

            public readonly Boolean Equals(TEnum? other)
            {
                return Equals((Enum<T>?) other);
            }

            public readonly Boolean Equals(IEnum? other)
            {
                return Comparer.Equals(Value?.Id, other?.Id) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(IEnum<T>? other)
            {
                return Comparer.Equals(Value?.Id, other?.Id) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(IEnum<T, TEnum>? other)
            {
                return Comparer.Equals(Value?.Id, other?.Id) && Enumerator.Equals(other);
            }

            public readonly Boolean Same(Any other)
            {
                return Same((FlagsEnumerator) other);
            }

            public readonly Boolean Same(FlagsEnumerator other)
            {
                return Equals(other.Value) && Same(other.Enumerator);
            }

            public readonly Boolean Same(EnumFlagsEnumerator<T> other)
            {
                return Enumerator.Same(other);
            }

            public override readonly String ToString()
            {
                return ToString(null);
            }

            public readonly String ToString(Char separator)
            {
                return ToString($" {separator} ", (String?) null);
            }

            public readonly String ToString(String? format)
            {
                return ToString(null, format);
            }

            public readonly String ToString(Char separator, String? format)
            {
                return ToString($" {separator} ", format);
            }

            public readonly String ToString(String? separator, String? format)
            {
                return ToString(separator, format, null);
            }

            public readonly String ToString(String? format, IFormatProvider? provider)
            {
                return ToString(null, format, provider);
            }

            public readonly String ToString(Char separator, String? format, IFormatProvider? provider)
            {
                return ToString($" {separator} ", format, provider);
            }

            public readonly String ToString(String? separator, String? format, IFormatProvider? provider)
            {
                Wrapper enumerator = this;
                return String.Join(separator ?? EnumFlagsEnumerator<T>.StringSeparator, Stringify.Create(in enumerator, format, provider));
            }

            readonly FlagsEnumerator ICloneable<FlagsEnumerator>.Clone()
            {
                return GetEnumerator();
            }

            readonly Object ICloneable.Clone()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }

            public readonly TEnum this[Int32 index]
            {
                get
                {
                    Enum<T, TEnum> result = Enumerator[index];
                    return result.This;
                }
            }

            readonly T IReadOnlyList<T>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            readonly Enum<T> IReadOnlyList<Enum<T>>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            readonly Enum<T, TEnum> IReadOnlyList<Enum<T, TEnum>>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            readonly IEnum<T> IReadOnlyList<IEnum<T>>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            readonly IEnum<T, TEnum> IReadOnlyList<IEnum<T, TEnum>>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            public struct Any : IEquatableStruct<Any>, IReadOnlySortedList<Enum<T, TEnum>>, IReadOnlySortedList<Enum<T>>, IReadOnlySortedList<Enum>, IReadOnlySortedList<IEnum>, IReadOnlySortedList<IEnum<T>>, IReadOnlySortedList<IEnum<T, TEnum>>, IEnumerator<Enum<T, TEnum>>, IEnumerator<Enum<T>>, IEnumerator<Enum>, IEnumerator<IEnum>, IEnumerator<IEnum<T>>, IEnumerator<IEnum<T, TEnum>>, IEquatable<Enum<T, TEnum>>, IEquatable<Enum<T>>, IEquatable<Enum>, IEquatable<IEnum>, IEquatable<IEnum<T>>, IEquatable<IEnum<T, TEnum>>, IEquatable<EnumFlagsEnumerator<T>>, IEquatable<FlagsEnumerator>, ICloneable<Any>, ICloneable, IFormattable
            {
                public static implicit operator Any(FlagsEnumerator value)
                {
                    return new Any(value);
                }
                
                public static implicit operator Any(TEnum value)
                {
                    return new Any(value);
                }

                public static implicit operator FlagsEnumerator(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator Enum<T>.FlagsEnumerator(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator EnumFlagsEnumerator<T>(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator TEnum(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator Enum<T, TEnum>(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator Enum<T>(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator T(Any value)
                {
                    return value.Enumerator;
                }

                public static Boolean operator ==(Any first, Any second)
                {
                    return first.Same(second);
                }

                public static Boolean operator !=(Any first, Any second)
                {
                    return !(first == second);
                }

                private FlagsEnumerator Enumerator;

                public readonly EnumComparer<T> Comparer
                {
                    get
                    {
                        return Enumerator.Comparer;
                    }
                }

                readonly IComparer<Enum> IReadOnlySortedList<Enum>.Comparer
                {
                    get
                    {
                        return Comparer.Interface;
                    }
                }

                readonly IComparer<Enum<T>> IReadOnlySortedList<Enum<T>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                readonly IComparer<Enum<T, TEnum>> IReadOnlySortedList<Enum<T, TEnum>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                readonly IComparer<IEnum> IReadOnlySortedList<IEnum>.Comparer
                {
                    get
                    {
                        return Comparer.Interface;
                    }
                }

                readonly IComparer<IEnum<T>> IReadOnlySortedList<IEnum<T>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                readonly IComparer<IEnum<T, TEnum>> IReadOnlySortedList<IEnum<T, TEnum>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                public readonly T Id
                {
                    get
                    {
                        return Enumerator.Id;
                    }
                }

                public readonly TEnum Value
                {
                    get
                    {
                        return Enumerator.Value;
                    }
                }

                public readonly Int32 Index
                {
                    get
                    {
                        return Enumerator.Index;
                    }
                }

                public readonly Int32 Count
                {
                    get
                    {
                        return Enumerator.Count;
                    }
                }

                public readonly TEnum Current
                {
                    get
                    {
                        return Enumerator.Current;
                    }
                }

                readonly Enum IEnumerator<Enum>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly Enum<T> IEnumerator<Enum<T>>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly Enum<T, TEnum> IEnumerator<Enum<T, TEnum>>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly IEnum IEnumerator<IEnum>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly IEnum<T> IEnumerator<IEnum<T>>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly IEnum<T, TEnum> IEnumerator<IEnum<T, TEnum>>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly Object? IEnumerator.Current
                {
                    get
                    {
                        return Index >= 0 ? Current : null;
                    }
                }
                
                public readonly Boolean IsFlags
                {
                    get
                    {
                        return Enumerator.IsFlags;
                    }
                }
                
                public readonly Boolean IsIntern
                {
                    get
                    {
                        return Enumerator.IsIntern;
                    }
                }

                public readonly Boolean IsEmpty
                {
                    get
                    {
                        return Enumerator.IsEmpty;
                    }
                }

                public Any(FlagsEnumerator enumerator)
                {
                    Enumerator = enumerator;
                }

                public Boolean MoveNext()
                {
                    return Enumerator.MoveNext();
                }

                public void Reset()
                {
                    Enumerator.Reset();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly TEnum[] ToArray()
                {
                    return Enumerator.ToArray();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableArray<TEnum> ToImmutableArray()
                {
                    return Enumerator.ToImmutableArray();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly List<TEnum> ToList()
                {
                    return Enumerator.ToList();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableList<TEnum> ToImmutableList()
                {
                    return Enumerator.ToImmutableList();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly SortedList<TEnum> ToSortedList()
                {
                    return Enumerator.ToSortedList();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly HashSet<TEnum> ToHashSet()
                {
                    return Enumerator.ToHashSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableHashSet<TEnum> ToImmutableHashSet()
                {
                    return Enumerator.ToImmutableHashSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly SortedSet<TEnum> ToSortedSet()
                {
                    return Enumerator.ToSortedSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableSortedSet<TEnum> ToImmutableSortedSet()
                {
                    return Enumerator.ToImmutableSortedSet();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(T[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T, TEnum>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(TEnum[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T, TEnum>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(T[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T, TEnum>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(TEnum[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T, TEnum>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<T> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T, TEnum>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<TEnum> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T, TEnum>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T, TEnum>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<TEnum> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T, TEnum>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                public readonly Any GetEnumerator()
                {
                    return Enumerator.GetEnumerator();
                }

                readonly IEnumerator<Enum> IEnumerable<Enum>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<Enum<T>> IEnumerable<Enum<T>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<Enum<T, TEnum>> IEnumerable<Enum<T, TEnum>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<IEnum> IEnumerable<IEnum>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<IEnum<T>> IEnumerable<IEnum<T>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<IEnum<T, TEnum>> IEnumerable<IEnum<T, TEnum>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                public override readonly Int32 GetHashCode()
                {
                    return Enumerator.GetHashCode();
                }

                public override readonly Boolean Equals(Object? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Any other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(FlagsEnumerator other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(T other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum<T, TEnum>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(TEnum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum<T, TEnum>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Same(Any other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(FlagsEnumerator other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Same(other);
                }

                public override readonly String ToString()
                {
                    return Enumerator.ToString();
                }

                public readonly String ToString(Char separator)
                {
                    return Enumerator.ToString(separator);
                }

                public readonly String ToString(String? format)
                {
                    return Enumerator.ToString(format);
                }

                public readonly String ToString(Char separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(format, provider);
                }

                public readonly String ToString(Char separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                public readonly String ToString(String? separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                readonly Any ICloneable<Any>.Clone()
                {
                    return GetEnumerator();
                }

                readonly Object ICloneable.Clone()
                {
                    return GetEnumerator();
                }

                public void Dispose()
                {
                    Enumerator.Dispose();
                }

                public readonly TEnum this[Int32 index]
                {
                    get
                    {
                        return Enumerator[index];
                    }
                }

                readonly Enum IReadOnlyList<Enum>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly Enum<T> IReadOnlyList<Enum<T>>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly Enum<T, TEnum> IReadOnlyList<Enum<T, TEnum>>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly IEnum IReadOnlyList<IEnum>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly IEnum<T> IReadOnlyList<IEnum<T>>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly IEnum<T, TEnum> IReadOnlyList<IEnum<T, TEnum>>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }
            }

            public struct Wrapper : IReadOnlySortedList<TEnum>, IEnumerator<TEnum>, IEquatable<TEnum>, ICloneable<Wrapper>, ICloneable, IFormattable
            {
                public static implicit operator Wrapper(FlagsEnumerator value)
                {
                    return new Wrapper(value);
                }

                private FlagsEnumerator Enumerator;

                public readonly EnumComparer<T> Comparer
                {
                    get
                    {
                        return Enumerator.Comparer;
                    }
                }

                readonly IComparer<TEnum> IReadOnlySortedList<TEnum>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                public readonly Int32 Count
                {
                    get
                    {
                        return Enumerator.Count;
                    }
                }

                public readonly TEnum Current
                {
                    get
                    {
                        return Enumerator.Current;
                    }
                }

                readonly Object? IEnumerator.Current
                {
                    get
                    {
                        return Enumerator.Index >= 0 ? Current : null;
                    }
                }

                public Wrapper(FlagsEnumerator enumerator)
                {
                    Enumerator = enumerator;
                }

                public Boolean MoveNext()
                {
                    return Enumerator.MoveNext();
                }

                public void Reset()
                {
                    Enumerator.Reset();
                }

                public readonly Wrapper GetEnumerator()
                {
                    return Enumerator.GetEnumerator();
                }

                readonly IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                public override readonly Int32 GetHashCode()
                {
                    return Enumerator.GetHashCode();
                }

                public override readonly Boolean Equals(Object? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Any other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(FlagsEnumerator other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(T other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum<T, TEnum>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(TEnum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum<T, TEnum>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Same(Any other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(FlagsEnumerator other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Same(other);
                }

                public override readonly String ToString()
                {
                    return Enumerator.ToString();
                }

                public readonly String ToString(Char separator)
                {
                    return Enumerator.ToString(separator);
                }

                public readonly String ToString(String? format)
                {
                    return Enumerator.ToString(format);
                }

                public readonly String ToString(Char separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(format, provider);
                }

                public readonly String ToString(Char separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                public readonly String ToString(String? separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                readonly Wrapper ICloneable<Wrapper>.Clone()
                {
                    return GetEnumerator();
                }

                readonly Object ICloneable.Clone()
                {
                    return GetEnumerator();
                }

                public void Dispose()
                {
                    Enumerator.Dispose();
                }

                public readonly TEnum this[Int32 index]
                {
                    get
                    {
                        return Enumerator[index];
                    }
                }
            }
            
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public abstract record Stringify : EnumeratorStringFiller<TEnum, Wrapper, Stringify>, IReadOnlyList<String>
            {
                protected Stringify(in Wrapper enumerator, String? format, IFormatProvider? provider)
                    : base(in enumerator, format, provider)
                {
                }

                public new static Stringify Create(in Wrapper enumerator, String? format, IFormatProvider? provider)
                {
                    return Create(in enumerator, format, format, provider);
                }

                public new static Stringify Create(in Wrapper enumerator, String? filler, String? format, IFormatProvider? provider)
                {
                    if (filler == format)
                    {
                        format = null;
                    }
                    
                    return filler switch
                    {
                        null or "" => new Default(in enumerator, format, provider),
                        "u" or "T" or "U" or "upper" or "Upper" or "UPPER" => new UpperCase(in enumerator, format, provider),
                        "l" or "t" or "L" or "lower" or "Lower" or "LOWER" => new LowerCase(in enumerator, format, provider),
                        _ => EnumeratorStringFiller<TEnum, Wrapper, Stringify>.Create(in enumerator, filler, format, provider) ?? new Default(in enumerator, format, provider)
                    };
                }

                [return: NotNullIfNotNull("value")]
                protected override String? Convert(TEnum? value)
                {
                    return value?.ToString(Format, Provider);
                }
                
                public String this[Int32 index]
                {
                    get
                    {
                        return Convert(Enumerator[index]);
                    }
                }

                private sealed record Default : Stringify
                {
                    internal Default(in Wrapper enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }
                }

                private sealed record UpperCase : Stringify
                {
                    internal UpperCase(in Wrapper enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }

                    [return: NotNullIfNotNull("value")]
                    protected override String? Convert(TEnum? value)
                    {
                        return base.Convert(value)?.ToUpper();
                    }
                }

                private sealed record LowerCase : Stringify
                {
                    internal LowerCase(in Wrapper enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }

                    [return: NotNullIfNotNull("value")]
                    protected override String? Convert(TEnum? value)
                    {
                        return base.Convert(value)?.ToLower();
                    }
                }
            }
        }
    }
    
    public partial class Enum<T>
    {
        public struct FlagsEnumerator : IEquatableStruct<FlagsEnumerator>, IReadOnlySortedList<Enum<T>>, IReadOnlySortedList<T>, IReadOnlySortedList<IEnum<T>>, IEnumerator<Enum<T>>, IEnumerator<T>, IEnumerator<IEnum<T>>, IEquatable<Enum<T>>, IEquatable<T>, IEquatable<IEnum<T>>, IEquatable<EnumFlagsEnumerator<T>>, IEquatable<FlagsEnumerator.Any>, ICloneable<FlagsEnumerator>, ICloneable, IFormattable
        {
            public static implicit operator FlagsEnumerator(Enum<T> value)
            {
                return new FlagsEnumerator(value);
            }

            public static implicit operator EnumFlagsEnumerator<T>(FlagsEnumerator value)
            {
                return new EnumFlagsEnumerator<T>(value.Id);
            }

            public static implicit operator Enum<T>(FlagsEnumerator value)
            {
                return value.Value;
            }

            public static implicit operator T(FlagsEnumerator value)
            {
                return value.Id;
            }

            public static Boolean operator ==(FlagsEnumerator first, FlagsEnumerator second)
            {
                return first.Same(second);
            }

            public static Boolean operator !=(FlagsEnumerator first, FlagsEnumerator second)
            {
                return !(first == second);
            }

            private EnumFlagsEnumerator<T> Enumerator;

            public readonly EnumComparer<T> Comparer
            {
                get
                {
                    return Enumerator.Comparer;
                }
            }

            public readonly Any Interface
            {
                get
                {
                    return this;
                }
            }

            readonly IComparer<T> IReadOnlySortedList<T>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<Enum<T>> IReadOnlySortedList<Enum<T>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            readonly IComparer<IEnum<T>> IReadOnlySortedList<IEnum<T>>.Comparer
            {
                get
                {
                    return Comparer;
                }
            }

            public readonly T Id
            {
                get
                {
                    return Value?.Id ?? default;
                }
            }

            public Enum<T> Value { get; }

            public readonly Int32 Index
            {
                get
                {
                    return Enumerator.Index;
                }
            }

            public readonly Int32 Count
            {
                get
                {
                    return Enumerator.Count;
                }
            }

            public readonly Enum<T> Current
            {
                get
                {
                    return Enumerator.Current;
                }
            }

            readonly T IEnumerator<T>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly IEnum<T> IEnumerator<IEnum<T>>.Current
            {
                get
                {
                    return Current;
                }
            }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Index >= 0 ? Current : null;
                }
            }
            
            public readonly Boolean IsFlags
            {
                get
                {
                    return Value?.IsFlags ?? Enumerator.IsFlags;
                }
            }
            
            public readonly Boolean IsIntern
            {
                get
                {
                    return Value?.IsIntern ?? true;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return Enumerator.IsEmpty;
                }
            }

            public FlagsEnumerator(Enum<T> value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                Enumerator = new EnumFlagsEnumerator<T>(value.Id);
            }

            public Boolean MoveNext()
            {
                return Enumerator.MoveNext();
            }

            public void Reset()
            {
                Enumerator.Reset();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Enum<T>[] ToArray()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableArray<Enum<T>> ToImmutableArray()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToImmutableArray();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly List<Enum<T>> ToList()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToList();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableList<Enum<T>> ToImmutableList()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToImmutableList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedList<Enum<T>> ToSortedList()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToSortedList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly HashSet<Enum<T>> ToHashSet()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableHashSet<Enum<T>> ToImmutableHashSet()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToImmutableHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedSet<Enum<T>> ToSortedSet()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToSortedSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableSortedSet<Enum<T>> ToImmutableSortedSet()
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).ToImmutableSortedSet();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(T[] array)
            {
                new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum[] array)
            {
                new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T>[] array)
            {
                new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum[] array)
            {
                new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T>[] array)
            {
                new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(T[] array, Int32 index)
            {
                new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum[] array, Int32 index)
            {
                new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(Enum<T>[] array, Int32 index)
            {
                new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum[] array, Int32 index)
            {
                new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void CopyTo(IEnum<T>[] array, Int32 index)
            {
                new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<T> destination)
            {
                return new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum> destination)
            {
                return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T>> destination)
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum> destination)
            {
                return new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T>> destination)
            {
                return new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
            {
                return new EnumeratorFiller<T, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
            {
                return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum<T>> destination, out Int32 written)
            {
                return new EnumeratorFiller<Enum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum> destination, out Int32 written)
            {
                return new EnumeratorFiller<IEnum, FlagsEnumerator, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<IEnum<T>> destination, out Int32 written)
            {
                return new EnumeratorFiller<IEnum<T>, FlagsEnumerator, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
            }

            public readonly FlagsEnumerator GetEnumerator()
            {
                FlagsEnumerator enumerator = this;
                enumerator.Reset();
                return enumerator;
            }

            readonly IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<Enum<T>> IEnumerable<Enum<T>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator<IEnum<T>> IEnumerable<IEnum<T>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            readonly IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override readonly Int32 GetHashCode()
            {
                return Value?.GetHashCode() ?? 0;
            }

            public override readonly Boolean Equals(Object? other)
            {
                return other switch
                {
                    Any value => Equals(value),
                    FlagsEnumerator value => Equals(value),
                    EnumFlagsEnumerator<T> value => Equals(value),
                    Enum<T> value => Equals(value),
                    T value => Equals(value),
                    Enum value => Equals(value),
                    IEnum<T> value => Equals(value),
                    IEnum value => Equals(value),
                    _ => false
                };
            }

            public readonly Boolean Equals(Any other)
            {
                return Equals((FlagsEnumerator) other);
            }

            public readonly Boolean Equals(FlagsEnumerator other)
            {
                return Equals(other.Value) && Equals(other.Enumerator);
            }

            public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
            {
                return Enumerator.Equals(other);
            }

            public readonly Boolean Equals(T other)
            {
                return Comparer.Equals(Value?.Id, other) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(Enum? other)
            {
                return Comparer.Equals(Value?.Id, other) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(Enum<T>? other)
            {
                return Comparer.Equals(Value, other) && Enumerator.Equals(other?.Id ?? default);
            }

            public readonly Boolean Equals(IEnum? other)
            {
                return Comparer.Equals(Value?.Id, other?.Id) && Enumerator.Equals(other);
            }

            public readonly Boolean Equals(IEnum<T>? other)
            {
                return Comparer.Equals(Value?.Id, other?.Id) && Enumerator.Equals(other);
            }

            public readonly Boolean Same(Any other)
            {
                return Same((FlagsEnumerator) other);
            }

            public readonly Boolean Same(FlagsEnumerator other)
            {
                return Equals(other.Value) && Same(other.Enumerator);
            }

            public readonly Boolean Same(EnumFlagsEnumerator<T> other)
            {
                return Enumerator.Same(other);
            }

            public override readonly String ToString()
            {
                return ToString(null);
            }

            public readonly String ToString(Char separator)
            {
                return ToString($" {separator} ", (String?) null);
            }

            public readonly String ToString(String? format)
            {
                return ToString(null, format);
            }

            public readonly String ToString(Char separator, String? format)
            {
                return ToString($" {separator} ", format);
            }

            public readonly String ToString(String? separator, String? format)
            {
                return ToString(separator, format, null);
            }

            public readonly String ToString(String? format, IFormatProvider? provider)
            {
                return ToString(null, format, provider);
            }

            public readonly String ToString(Char separator, String? format, IFormatProvider? provider)
            {
                return ToString($" {separator} ", format, provider);
            }

            public readonly String ToString(String? separator, String? format, IFormatProvider? provider)
            {
                return String.Join(separator ?? EnumFlagsEnumerator<T>.StringSeparator, Stringify.Create(in this, format, provider));
            }

            readonly FlagsEnumerator ICloneable<FlagsEnumerator>.Clone()
            {
                return GetEnumerator();
            }

            readonly Object ICloneable.Clone()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
                Enumerator.Dispose();
            }

            public readonly Enum<T> this[Int32 index]
            {
                get
                {
                    return Enumerator[index];
                }
            }

            readonly T IReadOnlyList<T>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            readonly IEnum<T> IReadOnlyList<IEnum<T>>.this[Int32 index]
            {
                get
                {
                    return this[index];
                }
            }

            public struct Any : IEquatableStruct<Any>, IReadOnlySortedList<Enum>, IReadOnlySortedList<Enum<T>>, IReadOnlySortedList<IEnum>, IReadOnlySortedList<IEnum<T>>, IEnumerator<Enum>, IEnumerator<Enum<T>>, IEnumerator<IEnum>, IEnumerator<IEnum<T>>, IEquatable<Enum>, IEquatable<Enum<T>>, IEquatable<IEnum>, IEquatable<IEnum<T>>, IEquatable<EnumFlagsEnumerator<T>>, IEquatable<FlagsEnumerator>, ICloneable<Any>, ICloneable, IFormattable
            {
                public static implicit operator Any(FlagsEnumerator value)
                {
                    return new Any(value);
                }
                
                public static implicit operator Any(Enum<T> value)
                {
                    return new Any(value);
                }
                
                public static implicit operator FlagsEnumerator(Any value)
                {
                    return value.Enumerator;
                }
                
                public static implicit operator EnumFlagsEnumerator<T>(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator Enum<T>(Any value)
                {
                    return value.Enumerator;
                }

                public static implicit operator T(Any value)
                {
                    return value.Enumerator;
                }

                public static Boolean operator ==(Any first, Any second)
                {
                    return first.Same(second);
                }

                public static Boolean operator !=(Any first, Any second)
                {
                    return !(first == second);
                }

                private FlagsEnumerator Enumerator;

                public readonly EnumComparer<T> Comparer
                {
                    get
                    {
                        return Enumerator.Comparer;
                    }
                }

                readonly IComparer<Enum> IReadOnlySortedList<Enum>.Comparer
                {
                    get
                    {
                        return Comparer.Interface;
                    }
                }

                readonly IComparer<Enum<T>> IReadOnlySortedList<Enum<T>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                readonly IComparer<IEnum> IReadOnlySortedList<IEnum>.Comparer
                {
                    get
                    {
                        return Comparer.Interface;
                    }
                }

                readonly IComparer<IEnum<T>> IReadOnlySortedList<IEnum<T>>.Comparer
                {
                    get
                    {
                        return Comparer;
                    }
                }

                public readonly T Id
                {
                    get
                    {
                        return Enumerator.Id;
                    }
                }

                public readonly Enum<T> Value
                {
                    get
                    {
                        return Enumerator.Value;
                    }
                }

                public readonly Int32 Index
                {
                    get
                    {
                        return Enumerator.Index;
                    }
                }

                public readonly Int32 Count
                {
                    get
                    {
                        return Enumerator.Count;
                    }
                }

                public readonly Enum<T> Current
                {
                    get
                    {
                        return Enumerator.Current;
                    }
                }

                readonly Enum IEnumerator<Enum>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly IEnum IEnumerator<IEnum>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly IEnum<T> IEnumerator<IEnum<T>>.Current
                {
                    get
                    {
                        return Current;
                    }
                }

                readonly Object? IEnumerator.Current
                {
                    get
                    {
                        return Index >= 0 ? Current : null;
                    }
                }
                
                public readonly Boolean IsFlags
                {
                    get
                    {
                        return Enumerator.IsFlags;
                    }
                }
                
                public readonly Boolean IsIntern
                {
                    get
                    {
                        return Enumerator.IsIntern;
                    }
                }

                public readonly Boolean IsEmpty
                {
                    get
                    {
                        return Enumerator.IsEmpty;
                    }
                }

                public Any(FlagsEnumerator enumerator)
                {
                    Enumerator = enumerator;
                }

                public Boolean MoveNext()
                {
                    return Enumerator.MoveNext();
                }

                public void Reset()
                {
                    Enumerator.Reset();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Enum<T>[] ToArray()
                {
                    return Enumerator.ToArray();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableArray<Enum<T>> ToImmutableArray()
                {
                    return Enumerator.ToImmutableArray();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly List<Enum<T>> ToList()
                {
                    return Enumerator.ToList();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableList<Enum<T>> ToImmutableList()
                {
                    return Enumerator.ToImmutableList();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly SortedList<Enum<T>> ToSortedList()
                {
                    return Enumerator.ToSortedList();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly HashSet<Enum<T>> ToHashSet()
                {
                    return Enumerator.ToHashSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableHashSet<Enum<T>> ToImmutableHashSet()
                {
                    return Enumerator.ToImmutableHashSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly SortedSet<Enum<T>> ToSortedSet()
                {
                    return Enumerator.ToSortedSet();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly ImmutableSortedSet<Enum<T>> ToImmutableSortedSet()
                {
                    return Enumerator.ToImmutableSortedSet();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(T[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T>[] array)
                {
                    Enumerator.CopyTo(array);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(T[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(Enum<T>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly void CopyTo(IEnum<T>[] array, Int32 index)
                {
                    Enumerator.CopyTo(array, index);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<T> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T>> destination)
                {
                    return Enumerator.TryCopyTo(destination);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<Enum<T>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public readonly Boolean TryCopyTo(Span<IEnum<T>> destination, out Int32 written)
                {
                    return Enumerator.TryCopyTo(destination, out written);
                }

                public readonly Any GetEnumerator()
                {
                    return Enumerator.GetEnumerator();
                }

                readonly IEnumerator<Enum> IEnumerable<Enum>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<Enum<T>> IEnumerable<Enum<T>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<IEnum> IEnumerable<IEnum>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator<IEnum<T>> IEnumerable<IEnum<T>>.GetEnumerator()
                {
                    return GetEnumerator();
                }

                readonly IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }

                public override readonly Int32 GetHashCode()
                {
                    return Enumerator.GetHashCode();
                }

                public override readonly Boolean Equals(Object? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Any other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(FlagsEnumerator other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(T other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(Enum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Equals(IEnum<T>? other)
                {
                    return Enumerator.Equals(other);
                }

                public readonly Boolean Same(Any other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(FlagsEnumerator other)
                {
                    return Enumerator.Same(other);
                }

                public readonly Boolean Same(EnumFlagsEnumerator<T> other)
                {
                    return Enumerator.Same(other);
                }

                public override readonly String ToString()
                {
                    return Enumerator.ToString();
                }

                public readonly String ToString(Char separator)
                {
                    return Enumerator.ToString(separator);
                }

                public readonly String ToString(String? format)
                {
                    return Enumerator.ToString(format);
                }

                public readonly String ToString(Char separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? separator, String? format)
                {
                    return Enumerator.ToString(separator, format);
                }

                public readonly String ToString(String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(format, provider);
                }

                public readonly String ToString(Char separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                public readonly String ToString(String? separator, String? format, IFormatProvider? provider)
                {
                    return Enumerator.ToString(separator, format, provider);
                }

                readonly Any ICloneable<Any>.Clone()
                {
                    return GetEnumerator();
                }

                readonly Object ICloneable.Clone()
                {
                    return GetEnumerator();
                }

                public void Dispose()
                {
                    Enumerator.Dispose();
                }

                public readonly Enum<T> this[Int32 index]
                {
                    get
                    {
                        return Enumerator[index];
                    }
                }

                readonly Enum IReadOnlyList<Enum>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly IEnum IReadOnlyList<IEnum>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }

                readonly IEnum<T> IReadOnlyList<IEnum<T>>.this[Int32 index]
                {
                    get
                    {
                        return this[index];
                    }
                }
            }

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public abstract record Stringify : EnumeratorStringFiller<Enum<T>, FlagsEnumerator, Stringify>, IReadOnlyList<String>
            {
                protected Stringify(in FlagsEnumerator enumerator, String? format, IFormatProvider? provider)
                    : base(in enumerator, format, provider)
                {
                }

                public new static Stringify Create(in FlagsEnumerator enumerator, String? format, IFormatProvider? provider)
                {
                    return Create(in enumerator, format, format, provider);
                }

                public new static Stringify Create(in FlagsEnumerator enumerator, String? filler, String? format, IFormatProvider? provider)
                {
                    if (filler == format)
                    {
                        format = null;
                    }
                    
                    return filler switch
                    {
                        null or "" => new Default(in enumerator, format, provider),
                        "u" or "T" or "U" or "upper" or "Upper" or "UPPER" => new UpperCase(in enumerator, format, provider),
                        "l" or "t" or "L" or "lower" or "Lower" or "LOWER" => new LowerCase(in enumerator, format, provider),
                        _ => EnumeratorStringFiller<Enum<T>, FlagsEnumerator, Stringify>.Create(in enumerator, filler, format, provider) ?? new Default(in enumerator, format, provider)
                    };
                }

                [return: NotNullIfNotNull("value")]
                protected override String? Convert(Enum<T>? value)
                {
                    return value?.ToString(Format, Provider);
                }
                
                public String this[Int32 index]
                {
                    get
                    {
                        return Convert(Enumerator[index]);
                    }
                }

                private sealed record Default : Stringify
                {
                    internal Default(in FlagsEnumerator enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }
                }

                private sealed record UpperCase : Stringify
                {
                    internal UpperCase(in FlagsEnumerator enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }

                    [return: NotNullIfNotNull("value")]
                    protected override String? Convert(Enum<T>? value)
                    {
                        return base.Convert(value)?.ToUpper();
                    }
                }

                private sealed record LowerCase : Stringify
                {
                    internal LowerCase(in FlagsEnumerator enumerator, String? format, IFormatProvider? provider)
                        : base(in enumerator, format, provider)
                    {
                    }

                    [return: NotNullIfNotNull("value")]
                    protected override String? Convert(Enum<T>? value)
                    {
                        return base.Convert(value)?.ToLower();
                    }
                }
            }
        }
    }
}