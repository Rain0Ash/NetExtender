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
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Enums
{
    public unsafe partial struct EnumFlagsEnumerator<T> : IEquatableStruct<EnumFlagsEnumerator<T>>, IReadOnlySortedList<T>, IEnumerator<T>, IEquatable<T>, IEquatable<Enum<T>>, IEquatable<IEnum<T>>, IEquatable<EnumFlagsEnumerator<T>.Any>, ICloneable<EnumFlagsEnumerator<T>>, ICloneable, IFormattable where T : unmanaged, Enum
    {
        public static implicit operator EnumFlagsEnumerator<T>(T value)
        {
            return new EnumFlagsEnumerator<T>(value);
        }

        public static implicit operator T(EnumFlagsEnumerator<T> value)
        {
            return value._value;
        }

        public static Boolean operator ==(EnumFlagsEnumerator<T> first, EnumFlagsEnumerator<T> second)
        {
            return first.Same(second);
        }

        public static Boolean operator !=(EnumFlagsEnumerator<T> first, EnumFlagsEnumerator<T> second)
        {
            return !(first == second);
        }

        public const Char Separator = '+';
        internal const String StringSeparator = " + ";

        public readonly EnumComparer<T> Comparer
        {
            get
            {
                return EnumComparer<T>.Default;
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

        private readonly T _value;
        public readonly T Value
        {
            get
            {
                return _value;
            }
        }

        private Byte _index = 0;
        public readonly Int32 Index
        {
            get
            {
                return _counter * BitUtilities.BitInByte + _index - 1;
            }
        }

        private Byte _written = 0;
        private Byte _counter = 0;
        private fixed Byte _destination[BitUtilities.BitInByte];

        public readonly Int32 Count
        {
            get
            {
                return EnumUtilities.CountOfFlags(_value);
            }
        }

        private T? _current = null;
        public readonly T Current
        {
            get
            {
                return _current ?? throw new InvalidOperationException();
            }
        }

        readonly Object? IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        public Boolean IsFlags
        {
            get
            {
                return EnumUtilities.IsFlags<T>();
            }
        }

        public readonly Boolean IsEmpty
        {
            get
            {
                return Index < 0 && _current is null && Comparer.Equals(_value, default);
            }
        }

        public EnumFlagsEnumerator(T value)
        {
            _value = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean MoveNext()
        {
            ReadOnlySpan<Byte> source = EnumUtilities.AsSpan(in _value);

            start:
            if (_counter >= source.Length)
            {
                return false;
            }

            if (_written <= 0)
            {
                fixed (Byte* destination = _destination)
                {
                    if (!BitUtilities.TryGetSetBits(source[_counter], new Span<Byte>(destination, BitUtilities.BitInByte), out Int32 written))
                    {
                        throw new InvalidOperationException();
                    }
                    
                    _written = unchecked((Byte) written);
                }
            }
            
            if (_index >= _written)
            {
                _index = 0;
                _written = 0;
                _counter++;
                goto start;
            }

            UInt64 result = 1UL << (_counter * BitUtilities.BitInByte + _destination[_index++]);
            _current = Unsafe.As<UInt64, T>(ref result);
            return true;
        }

        public void Reset()
        {
            _index = 0;
            _written = 0;
            _counter = 0;
            _current = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToArray()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ImmutableArray<T> ToImmutableArray()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToImmutableArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly List<T> ToList()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ImmutableList<T> ToImmutableList()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToImmutableList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SortedList<T> ToSortedList()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToSortedList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly HashSet<T> ToHashSet()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ImmutableHashSet<T> ToImmutableHashSet()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToImmutableHashSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SortedSet<T> ToSortedSet()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ImmutableSortedSet<T> ToImmutableSortedSet()
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).ToImmutableSortedSet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(T[] array)
        {
            new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).CopyTo(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Enum[] array)
        {
            new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(T[] array, Int32 index)
        {
            new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).CopyTo(array, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Enum[] array, Int32 index)
        {
            new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).CopyTo(array, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<T> destination)
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<Enum> destination)
        {
            return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
        {
            return new EnumeratorFiller<T, EnumFlagsEnumerator<T>, EnumComparer<T>>(this, Comparer).TryCopyTo(destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
        {
            return new EnumeratorFiller<Enum, Any, EnumComparer<T>.Any>(this, Comparer).TryCopyTo(destination, out written);
        }

        public readonly EnumFlagsEnumerator<T> GetEnumerator()
        {
            EnumFlagsEnumerator<T> enumerator = this;
            enumerator.Reset();
            return enumerator;
        }

        readonly IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override readonly Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override readonly Boolean Equals(Object? other)
        {
            return other switch
            {
                Any value => Equals(value),
                EnumFlagsEnumerator<T> value => Equals(value),
                T value => Equals(value),
                Enum value => Equals(value),
                Enum<T> value => Equals(value),
                IEnum<T> value => Equals(value),
                IEnum value => Equals(value),
                _ => false
            };
        }

        public readonly Boolean Equals(Any other)
        {
            return Equals((EnumFlagsEnumerator<T>) other);
        }

        public readonly Boolean Equals(EnumFlagsEnumerator<T> other)
        {
            return Equals(other._value) && Index == other.Index && Comparer.Equals(_current, other._current);
        }

        public readonly Boolean Equals(T other)
        {
            return Comparer.Equals(_value, other);
        }

        public readonly Boolean Equals(Enum? other)
        {
            return other is T value && Equals(value);
        }

        public readonly Boolean Equals(Enum<T>? other)
        {
            return other is not null && Equals(other.Id);
        }

        public readonly Boolean Equals(IEnum? other)
        {
            return Equals(other?.Id);
        }

        public readonly Boolean Equals(IEnum<T>? other)
        {
            return other is not null && Equals(other.Id);
        }

        public readonly Boolean Same(Any other)
        {
            return Same((EnumFlagsEnumerator<T>) other);
        }

        public readonly Boolean Same(EnumFlagsEnumerator<T> other)
        {
            return Equals(other._value);
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
            return String.Join(separator ?? StringSeparator, Stringify.Create(in this, format, provider));
        }

        readonly EnumFlagsEnumerator<T> ICloneable<EnumFlagsEnumerator<T>>.Clone()
        {
            return GetEnumerator();
        }

        readonly Object ICloneable.Clone()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Reset();
        }

        public readonly T this[Int32 index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }

                using EnumFlagsEnumerator<T> enumerator = GetEnumerator();

                Int32 i;
                for (i = -1; i < index && enumerator.MoveNext(); i++)
                {
                }

                if (i == index)
                {
                    return enumerator.Current;
                }
                
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
        }
    }

    public partial struct EnumFlagsEnumerator<T>
    {
        public struct Any : IEquatableStruct<Any>, IReadOnlySortedList<Enum>, IEnumerator<Enum>, IEquatable<Enum>, IEquatable<Enum<T>>, IEquatable<IEnum<T>>, IEquatable<EnumFlagsEnumerator<T>>, ICloneable<Any>, ICloneable, IFormattable
        {
            public static implicit operator Any(EnumFlagsEnumerator<T> value)
            {
                return new Any(value);
            }
            
            public static implicit operator Any(T value)
            {
                return new Any(value);
            }

            public static implicit operator EnumFlagsEnumerator<T>(Any value)
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
            
            private EnumFlagsEnumerator<T> Enumerator;

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
                    return Enumerator.Comparer.Interface;
                }
            }

            public readonly T Value
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

            public readonly T Current
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

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Index >= 0 ? Current : null;
                }
            }

            public Boolean IsFlags
            {
                get
                {
                    return Enumerator.IsFlags;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return Enumerator.IsEmpty;
                }
            }

            public Any(EnumFlagsEnumerator<T> enumerator)
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
            public readonly T[] ToArray()
            {
                return Enumerator.ToArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableArray<T> ToImmutableArray()
            {
                return Enumerator.ToImmutableArray();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly List<T> ToList()
            {
                return Enumerator.ToList();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableList<T> ToImmutableList()
            {
                return Enumerator.ToImmutableList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedList<T> ToSortedList()
            {
                return Enumerator.ToSortedList();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly HashSet<T> ToHashSet()
            {
                return Enumerator.ToHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableHashSet<T> ToImmutableHashSet()
            {
                return Enumerator.ToImmutableHashSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly SortedSet<T> ToSortedSet()
            {
                return Enumerator.ToSortedSet();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ImmutableSortedSet<T> ToImmutableSortedSet()
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
            public readonly Boolean TryCopyTo(Span<T> destination, out Int32 written)
            {
                return Enumerator.TryCopyTo(destination, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean TryCopyTo(Span<Enum> destination, out Int32 written)
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
                return Equals(other.Enumerator);
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

            public readonly T this[Int32 index]
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
        }
    }

    public partial struct EnumFlagsEnumerator<T>
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public abstract record Stringify : EnumeratorStringFiller<T, EnumFlagsEnumerator<T>, Stringify>, IReadOnlyList<String>
        {
            protected Stringify(in EnumFlagsEnumerator<T> enumerator, String? format, IFormatProvider? provider)
                : base(in enumerator, format, provider)
            {
            }

            public new static Stringify Create(in EnumFlagsEnumerator<T> enumerator, String? format, IFormatProvider? provider)
            {
                return Create(in enumerator, format, format, provider);
            }

            public new static Stringify Create(in EnumFlagsEnumerator<T> enumerator, String? filler, String? format, IFormatProvider? provider)
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
                    _ => EnumeratorStringFiller<T, EnumFlagsEnumerator<T>, Stringify>.Create(in enumerator, filler, format, provider) ?? new Default(in enumerator, format, provider)
                };
            }

            protected override String Convert(T value)
            {
                return EnumUtilities.GetName(value);
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
                internal Default(in EnumFlagsEnumerator<T> enumerator, String? format, IFormatProvider? provider)
                    : base(in enumerator, format, provider)
                {
                }
            }

            private sealed record UpperCase : Stringify
            {
                private static ImmutableDictionary<T, String> Storage { get; }

                static UpperCase()
                {
                    Storage = EnumUtilities.GetMembers<T>().ToImmutableDictionary(static key => key.Value, static key => key.Name.ToUpper());
                }

                internal UpperCase(in EnumFlagsEnumerator<T> enumerator, String? format, IFormatProvider? provider)
                    : base(in enumerator, format, provider)
                {
                }

                protected override String Convert(T value)
                {
                    return Storage.TryGetValue(value, out String? result) ? result : base.Convert(value).ToUpper();
                }
            }

            private sealed record LowerCase : Stringify
            {
                private static ImmutableDictionary<T, String> Storage { get; }

                static LowerCase()
                {
                    Storage = EnumUtilities.GetMembers<T>().ToImmutableDictionary(static key => key.Value, static key => key.Name.ToLower());
                }

                internal LowerCase(in EnumFlagsEnumerator<T> enumerator, String? format, IFormatProvider? provider)
                    : base(in enumerator, format, provider)
                {
                }

                protected override String Convert(T value)
                {
                    return Storage.TryGetValue(value, out String? result) ? result : base.Convert(value).ToLower();
                }
            }
        }
    }
}