// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Enums.Interfaces;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace NetExtender.Types.Enums
{
    /// <summary>
    /// Provides sbyte specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class SByteOperation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, SByte>
        {
            public Continuous(SByte min, SByte max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                SByte convert = Unsafe.As<T, SByte>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(SByte value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                SByte convert = Unsafe.As<T, SByte>(ref value);
                Int32 index = convert - Min;

                return Members[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return SByteOperation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, SByte>
        {
            public Discontinuous(ImmutableDictionary<SByte, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                SByte convert = Unsafe.As<T, SByte>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(SByte value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                SByte convert = Unsafe.As<T, SByte>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                SByte convert = Unsafe.As<T, SByte>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return SByteOperation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, SByte>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, SByte> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            SByte minimum = Unsafe.As<T, SByte>(ref min);
            SByte maximum = Unsafe.As<T, SByte>(ref max);

            ImmutableDictionary<SByte, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, SByte>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(SByte value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref SByte convert = ref Unsafe.As<T, SByte>(ref result);

            return SByte.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides byte specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class ByteOperation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, Byte>
        {
            public Continuous(Byte min, Byte max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Byte convert = Unsafe.As<T, Byte>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Byte value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Byte convert = Unsafe.As<T, Byte>(ref value);
                Int32 index = convert - Min;

                return Members[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return ByteOperation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, Byte>
        {
            public Discontinuous(ImmutableDictionary<Byte, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Byte convert = Unsafe.As<T, Byte>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Byte value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Byte convert = Unsafe.As<T, Byte>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                Byte convert = Unsafe.As<T, Byte>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return ByteOperation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, Byte>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, Byte> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            Byte minimum = Unsafe.As<T, Byte>(ref min);
            Byte maximum = Unsafe.As<T, Byte>(ref max);

            ImmutableDictionary<Byte, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, Byte>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(Byte value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref Byte convert = ref Unsafe.As<T, Byte>(ref result);

            return Byte.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides short specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int16Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, Int16>
        {
            public Continuous(Int16 min, Int16 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int16 convert = Unsafe.As<T, Int16>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int16 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int16 convert = Unsafe.As<T, Int16>(ref value);
                Int32 index = convert - Min;

                return Members[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int16Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, Int16>
        {
            public Discontinuous(ImmutableDictionary<Int16, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int16 convert = Unsafe.As<T, Int16>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int16 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int16 convert = Unsafe.As<T, Int16>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                Int16 convert = Unsafe.As<T, Int16>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int16Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, Int16>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, Int16> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            Int16 minimum = Unsafe.As<T, Int16>(ref min);
            Int16 maximum = Unsafe.As<T, Int16>(ref max);

            ImmutableDictionary<Int16, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, Int16>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(Int16 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref Int16 convert = ref Unsafe.As<T, Int16>(ref result);

            return Int16.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides ushort specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt16Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, UInt16>
        {
            public Continuous(UInt16 min, UInt16 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt16 convert = Unsafe.As<T, UInt16>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt16 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt16 convert = Unsafe.As<T, UInt16>(ref value);
                Int32 index = convert - Min;

                return Members[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt16Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, UInt16>
        {
            public Discontinuous(ImmutableDictionary<UInt16, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt16 convert = Unsafe.As<T, UInt16>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt16 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt16 convert = Unsafe.As<T, UInt16>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                UInt16 convert = Unsafe.As<T, UInt16>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt16Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, UInt16>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, UInt16> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            UInt16 minimum = Unsafe.As<T, UInt16>(ref min);
            UInt16 maximum = Unsafe.As<T, UInt16>(ref max);

            ImmutableDictionary<UInt16, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, UInt16>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(UInt16 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref UInt16 convert = ref Unsafe.As<T, UInt16>(ref result);

            return UInt16.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides int specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int32Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, Int32>
        {
            public Continuous(Int32 min, Int32 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int32 convert = Unsafe.As<T, Int32>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int32 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int32 convert = Unsafe.As<T, Int32>(ref value);
                Int32 index = convert - Min;

                return Members[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int32Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, Int32>
        {
            public Discontinuous(ImmutableDictionary<Int32, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int32 convert = Unsafe.As<T, Int32>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int32 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int32 convert = Unsafe.As<T, Int32>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                Int32 convert = Unsafe.As<T, Int32>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int32Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, Int32>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, Int32> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            Int32 minimum = Unsafe.As<T, Int32>(ref min);
            Int32 maximum = Unsafe.As<T, Int32>(ref max);

            ImmutableDictionary<Int32, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, Int32>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(Int32 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref Int32 convert = ref Unsafe.As<T, Int32>(ref result);

            return Int32.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides uint specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt32Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, UInt32>
        {
            public Continuous(UInt32 min, UInt32 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt32 convert = Unsafe.As<T, UInt32>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt32 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt32 convert = Unsafe.As<T, UInt32>(ref value);
                UInt32 index = convert - Min;

                return Members[unchecked((Int32) index)];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt32Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, UInt32>
        {
            public Discontinuous(ImmutableDictionary<UInt32, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt32 convert = Unsafe.As<T, UInt32>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt32 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt32 convert = Unsafe.As<T, UInt32>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                UInt32 convert = Unsafe.As<T, UInt32>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt32Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, UInt32>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, UInt32> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            UInt32 minimum = Unsafe.As<T, UInt32>(ref min);
            UInt32 maximum = Unsafe.As<T, UInt32>(ref max);

            ImmutableDictionary<UInt32, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, UInt32>(ref value);
            });

            if (dictionary.Count > 0)
            {
                UInt32 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(UInt32 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref UInt32 convert = ref Unsafe.As<T, UInt32>(ref result);

            return UInt32.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides long specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class Int64Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, Int64>
        {
            public Continuous(Int64 min, Int64 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int64 convert = Unsafe.As<T, Int64>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int64 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int64 convert = Unsafe.As<T, Int64>(ref value);
                Int64 index = convert - Min;

                return Members[unchecked((Int32) index)];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int64Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, Int64>
        {
            public Discontinuous(ImmutableDictionary<Int64, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                Int64 convert = Unsafe.As<T, Int64>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(Int64 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                Int64 convert = Unsafe.As<T, Int64>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                Int64 convert = Unsafe.As<T, Int64>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return Int64Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, Int64>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, Int64> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            Int64 minimum = Unsafe.As<T, Int64>(ref min);
            Int64 maximum = Unsafe.As<T, Int64>(ref max);

            ImmutableDictionary<Int64, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, Int64>(ref value);
            });

            if (dictionary.Count > 0)
            {
                Int64 length = maximum - minimum;
                Int32 count = dictionary.Count - 1;

                if (length == count)
                {
                    Operation = new Continuous(minimum, maximum, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(Int64 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref Int64 convert = ref Unsafe.As<T, Int64>(ref result);

            return Int64.TryParse(value, out convert);
        }
    }

    /// <summary>
    /// Provides ulong specified operation.
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    internal static class UInt64Operation<T> where T : unmanaged, Enum
    {
        private sealed class Continuous : ContinuousUnderlyingEnumOperation<T, UInt64>
        {
            public Continuous(UInt64 min, UInt64 max, ImmutableArray<EnumMember<T>> members)
                : base(min, max, members)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt64 convert = Unsafe.As<T, UInt64>(ref value);
                return Min <= convert && convert <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt64 value)
            {
                return Min <= value && value <= Max;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt64 convert = Unsafe.As<T, UInt64>(ref value);
                UInt64 index = convert - Min;

                return Members[unchecked((Int32) index)];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                try
                {
                    result = GetMember(value);
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    result = null;
                    return false;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt64Operation<T>.TryParse(value, out result);
            }
        }

        private sealed class Discontinuous : DiscontinuousUnderlyingEnumOperation<T, UInt64>
        {
            public Discontinuous(ImmutableDictionary<UInt64, EnumMember<T>> value)
                : base(value)
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(T value)
            {
                UInt64 convert = Unsafe.As<T, UInt64>(ref value);
                return Value.ContainsKey(convert);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean IsDefined(UInt64 value)
            {
                return Value.ContainsKey(value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override EnumMember<T> GetMember(T value)
            {
                UInt64 convert = Unsafe.As<T, UInt64>(ref value);
                return Value[convert];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result)
            {
                UInt64 convert = Unsafe.As<T, UInt64>(ref value);
                return Value.TryGetValue(convert, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryParse(String value, out T result)
            {
                return UInt64Operation<T>.TryParse(value, out result);
            }
        }

        private static UnderlyingEnumOperation<T, UInt64>? Operation { get; set; }

        public static IUnderlyingEnumOperation<T, UInt64> Create(T min, T max, ImmutableArray<EnumMember<T>> members)
        {
            UInt64 minval = Unsafe.As<T, UInt64>(ref min);
            UInt64 maxval = Unsafe.As<T, UInt64>(ref max);

            ImmutableDictionary<UInt64, EnumMember<T>> dictionary = members.ToImmutableDictionary(static member =>
            {
                T value = member.Value;
                return Unsafe.As<T, UInt64>(ref value);
            });

            if (dictionary.Count > 0)
            {
                UInt64 length = maxval - minval;
                Int32 count = dictionary.Count - 1;
                if (length == (UInt64) count)
                {
                    Operation = new Continuous(minval, maxval, members);
                    return Operation;
                }
            }

            Operation = new Discontinuous(dictionary);
            return Operation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined(UInt64 value)
        {
            return Operation?.IsDefined(value) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, out T result)
        {
            result = default;
            ref UInt64 convert = ref Unsafe.As<T, UInt64>(ref result);

            return UInt64.TryParse(value, out convert);
        }
    }
}