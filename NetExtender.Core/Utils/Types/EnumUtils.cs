// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NetExtender.Exceptions;
using NetExtender.Exceptions.Enum;
using NetExtender.Types.Arrays;
using NetExtender.Types.Enums;
using NetExtender.Types.Enums.Attributes;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Utils.Numerics;

// ReSharper disable StaticMemberInGenericType

namespace NetExtender.Utils.Types
{
    public static class EnumUtils
    {
        private const String IsDefinedTypeMismatchMessage = "The underlying type of the enum and the value must be the same type.";

        public static T RandomEnumValue<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.Values.GetRandom();
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static T GetRandomEnumValue<T>(this IEnumerable<T> source) where T : unmanaged, Enum
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32? count = source.CountIfMaterialized();

            if (count is not null)
            {
                return count > 0 ? source.GetRandom() : RandomEnumValue<T>();
            }

            List<T> items = source.ToList();
            return items.Count > 0 ? items.GetRandom() : RandomEnumValue<T>();
        }

        public static IEnumerable<Decimal> AsDecimal<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.Values.OfType<IConvertible>().ToDecimal();
        }

        public static IEnumerable<UInt64> AsUInt64<T>(Boolean negative = true) where T : unmanaged, Enum
        {
            IEnumerable<Decimal> decimals = AsDecimal<T>();

            if (!negative)
            {
                decimals = decimals.Where(MathUtils.IsPositive);
            }

            return decimals.Select(ConvertUtils.ToUInt64);
        }

        public static Int32 GetCountOfFlags<T>() where T : unmanaged, Enum
        {
            if (!IsFlags<T>())
            {
                throw new NotFlagsEnumTypeException();
            }

            UInt64[] values = AsUInt64<T>().ToArray();

            return values.Length < 2 ? values.Length : values.Count(MathUtils.IsPowerOf2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static unsafe Boolean HasFlags<T>(T* first, T* second) where T : unmanaged, Enum
        {
            Byte* pf = (Byte*) first;
            Byte* ps = (Byte*) second;

            for (Int32 i = 0; i < sizeof(T); i++)
            {
                if ((pf[i] & ps[i]) != ps[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <remarks>Faster analog of Enum.HasFlag</remarks>
        /// <inheritdoc cref="Enum.HasFlag" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean HasFlags<T>(this T first, T second) where T : unmanaged, Enum
        {
            return HasFlags(&first, &second);
        }

        /// <remarks>Faster analog of Enum.HasFlag</remarks>
        /// <inheritdoc cref="Enum.HasFlag" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean HasFlags<T>(in T first, in T second) where T : unmanaged, Enum
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return HasFlags(pf, ps);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static unsafe T SetFlags<T>(T* first, T* second) where T : unmanaged, Enum
        {
            Byte* pf = (Byte*) first;
            Byte* ps = (Byte*) second;

            Span<Byte> value = stackalloc Byte[sizeof(T)];
            
            for (Int32 i = 0; i < value.Length; i++)
            {
                value[i] = (Byte) (pf[i] | ps[i]);
            }

            return MemoryMarshal.Read<T>(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T SetFlags<T>(this T first, T second) where T : unmanaged, Enum
        {
            return SetFlags(&first, &second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SetFlags<T>(ref T first, T second) where T : unmanaged, Enum
        {
            fixed (T* pf = &first)
            {
                first = SetFlags(pf, &second);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SetFlags<T>(ref T first, in T second) where T : unmanaged, Enum
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                first = SetFlags(pf, ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static unsafe T RemoveFlags<T>(T* first, T* second) where T : unmanaged, Enum
        {
            Byte* pf = (Byte*) first;
            Byte* ps = (Byte*) second;

            Span<Byte> value = stackalloc Byte[sizeof(T)];
            
            for (Int32 i = 0; i < value.Length; i++)
            {
                value[i] = (Byte) (pf[i] & ~ps[i]);
            }

            return MemoryMarshal.Read<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T RemoveFlags<T>(this T first, T second) where T : unmanaged, Enum
        {
            return RemoveFlags(&first, &second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void RemoveFlags<T>(ref T first, T second) where T : unmanaged, Enum
        {
            fixed (T* pf = &first)
            {
                first = RemoveFlags(pf, &second);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void RemoveFlags<T>(ref T first, in T second) where T : unmanaged, Enum
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                first = RemoveFlags(pf, ps);
            }
        }

        /// <summary>
        ///     Returns the underlying type of the specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Type GetUnderlyingType<T>() where T : unmanaged, Enum
        {
            return CacheType<T>.UnderlyingType;
        }

        /// <summary>
        ///     Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<T> GetValues<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.Values;
        }
        
        /// <summary>
        ///     Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<T> GetValues<T>(Boolean @default) where T : unmanaged, Enum
        {
            return @default ? GetValues<T>() : GetValuesWithoutDefault<T>();
        }

        /// <summary>
        ///     Retrieves an array of the values (exclude default values) of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<T> GetValuesWithoutDefault<T>() where T : unmanaged, Enum
        {
            return CacheValuesWithoutDefault<T>.Values;
        }

        /// <summary>
        ///     Retrieves an array of the names of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<String> GetNames<T>() where T : unmanaged, Enum
        {
            return CacheNames<T>.Names;
        }

        /// <summary>
        ///     Retrieves the name of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetName<T>(T value) where T : unmanaged, Enum
        {
            return GetMember(value).Name;
        }

        /// <summary>
        ///     Retrieves an array of the member information of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<EnumMember<T>> GetMembers<T>() where T : unmanaged, Enum
        {
            return CacheMembers<T>.Members;
        }

        /// <summary>
        ///     Retrieves the member information of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumMember<T> GetMember<T>(T value) where T : unmanaged, Enum
        {
            return CacheUnderlyingOperation<T>.UnderlyingOperation.GetMember(ref value);
        }

        /// <summary>
        ///     Returns the minimum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetMinValue<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.IsEmpty ? null : CacheMinMaxValues<T>.MinValue;
        }

        /// <summary>
        ///     Returns the maximum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetMaxValue<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.IsEmpty ? null : CacheMinMaxValues<T>.MaxValue;
        }

        /// <summary>
        ///     Returns whether no fields in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsEmpty<T>() where T : unmanaged, Enum
        {
            return CacheValues<T>.IsEmpty;
        }

        /// <summary>
        ///     Returns whether the values of the constants in a specified enumeration are continuous.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsContinuous<T>() where T : unmanaged, Enum
        {
            return CacheUnderlyingOperation<T>.UnderlyingOperation.IsContinuous;
        }

        /// <summary>
        ///     Returns whether the <see cref="FlagsAttribute" /> is defined.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsFlags<T>() where T : unmanaged, Enum
        {
            return CacheIsFlags<T>.IsFlags;
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined<T>(T value) where T : unmanaged, Enum
        {
            return CacheUnderlyingOperation<T>.UnderlyingOperation.IsDefined(ref value);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified name exists in a specified enumeration.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(String name) where T : unmanaged, Enum
        {
            return TryParseName<T>(name, false, out _);
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or more enumerated constants to an
        ///     equivalent enumerated object.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T Parse<T>(String value) where T : unmanaged, Enum
        {
            return TryParse(value, false, out T result) ? result : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or more enumerated constants to an
        ///     equivalent enumerated object.
        ///     A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T Parse<T>(String value, Boolean ignoreCase) where T : unmanaged, Enum
        {
            return TryParse(value, ignoreCase, out T result) ? result : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or more enumerated constants to an
        ///     equivalent enumerated object.
        ///     The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        public static Boolean TryParse<T>(String value, out T result) where T : unmanaged, Enum
        {
            return TryParse(value, false, out result);
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or more enumerated constants to an
        ///     equivalent enumerated object.
        ///     A parameter specifies whether the operation is case-sensitive.
        ///     The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        public static Boolean TryParse<T>(String value, Boolean ignoreCase, out T result) where T : unmanaged, Enum
        {
            if (!String.IsNullOrEmpty(value))
            {
                return IsNumeric(value[0]) ? CacheUnderlyingOperation<T>.UnderlyingOperation.TryParse(value, out result) : TryParseName(value, ignoreCase, out result);
            }

            result = default;
            return false;
        }

        /// <summary>
        ///     Checks whether specified charactor is number.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsNumeric(Char c)
        {
            return Char.IsDigit(c) || c == '-' || c == '+';
        }

        /// <summary>
        ///     Converts the string representation of the name of one or more enumerated constants to an equivalent enumerated
        ///     object.
        ///     A parameter specifies whether the operation is case-sensitive.
        ///     The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean TryParseName<T>(String name, Boolean ignoreCase, out T result) where T : unmanaged, Enum
        {
            if (ignoreCase)
            {
                foreach (EnumMember<T> member in CacheMembers<T>.Members.Where(member => name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    result = member.Value;
                    return true;
                }
            }
            else
            {
                if (CacheMembersByName<T>.MemberByName.TryGetValue(name, out EnumMember<T>? member))
                {
                    result = member.Value;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        ///     Converts to the 8-bit signed integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SByte ToSByte<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(SByte) ? Unsafe.As<T, SByte>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 8-bit unsigned integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Byte ToByte<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(Byte) ? Unsafe.As<T, Byte>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 16-bit signed integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int16 ToInt16<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(Int16) ? Unsafe.As<T, Int16>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 16-bit unsigned integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt16 ToUInt16<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(UInt16) ? Unsafe.As<T, UInt16>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 32-bit signed integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 ToInt32<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(Int32) ? Unsafe.As<T, Int32>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 32-bit unsigned integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 ToUInt32<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(UInt32) ? Unsafe.As<T, UInt32>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 64-bit signed integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 ToInt64<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(Int64) ? Unsafe.As<T, Int64>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the 64-bit unsigned integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 ToUInt64<T>(this T value) where T : unmanaged, Enum
        {
            return GetUnderlyingType<T>() == typeof(UInt64) ? Unsafe.As<T, UInt64>(ref value) : throw new ArgumentException(nameof(value));
        }

        /// <summary>
        ///     Converts to the member information of the constant in the specified enumeration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumMember<T> ToMember<T>(this T value) where T : unmanaged, Enum
        {
            return GetMember(value);
        }
        
        /// <summary>
        /// Gets the Attribute of specified enumration member.
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <typeparam name="TAttribute">Attribute Type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<TAttribute> GetAttributes<T, TAttribute>(this T value) where T : unmanaged, Enum where TAttribute : Attribute
        {
            return CacheAttributes<T, TAttribute>.Cache[value];
        }

        /// <summary>
        ///     Converts to the name of the constant in the specified enumeration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToName<T>(this T value) where T : unmanaged, Enum
        {
            return GetName(value);
        }

        /// <summary>
        ///     Gets the WellKnown types of specified enumeration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns></returns>
        public static String? GetEnumMemberValue<T>(this T value) where T : unmanaged, Enum
        {
            if (value.TryGetEnumMemberValue(out String? member))
            {
                return member;
            }
            
            throw new NotFoundException($"{nameof(EnumMemberAttribute)} is not found.");
        }

        /// <summary>
        ///     Gets the WellKnown types of specified enumeration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static Boolean TryGetEnumMemberValue<T>(this T value, out String? member) where T : unmanaged, Enum
        {
            EnumMemberAttribute? attribute = value.ToMember().EnumMemberAttribute;
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (attribute is not null)
            {
                member = attribute.Value;
                return true;
            }

            member = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the <see cref="EnumLabelAttribute.Value" /> of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns></returns>
        public static String? GetLabel<T>(this T value, Int32 index = 0) where T : unmanaged, Enum
        {
            return value.ToMember().GetLabel(index);
        }

        /// <summary>
        ///     Gets the WellKnown types of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="index"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns></returns>
        public static String? GetLabel<T>(this EnumMember<T> member, Int32 index = 0) where T : unmanaged, Enum
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (member.TryGetLabel(index, out String? label))
            {
                return label;
            }

            throw new NotFoundException($"{nameof(EnumLabelAttribute)} that is specified index {index} is not found.");
        }
        
        /// <summary>
        ///     Gets the <see cref="EnumLabelAttribute.Value" /> of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Boolean TryGetLabel<T>(this T value, out String? label) where T : unmanaged, Enum
        {
            return value.TryGetLabel(0, out label);
        }

        /// <summary>
        ///     Gets the WellKnown types of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Boolean TryGetLabel<T>(this EnumMember<T> member, out String? label) where T : unmanaged, Enum
        {
            return member.TryGetLabel(0, out label);
        }

        /// <summary>
        ///     Gets the <see cref="EnumLabelAttribute.Value" /> of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Boolean TryGetLabel<T>(this T value, Int32 index, out String? label) where T : unmanaged, Enum
        {
            return value.ToMember().TryGetLabel(index, out label);
        }

        /// <summary>
        ///     Gets the WellKnown types of specified enumration member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member"></param>
        /// <param name="index"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Boolean TryGetLabel<T>(this EnumMember<T> member, Int32 index, out String? label) where T : unmanaged, Enum
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            IImmutableDictionary<Int32, String>? labels = member.Labels;

            if (labels is not null)
            {
                return labels.TryGetValue(index, out label);
            }

            label = default;
            return false;
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(SByte value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(SByte))
            {
                return SByteOperation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(Byte value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(Byte))
            {
                return ByteOperation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(Int16 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(Int16))
            {
                return Int16Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(UInt16 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(UInt16))
            {
                return UInt16Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(Int32 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(Int32))
            {
                return Int32Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(UInt32 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(UInt32))
            {
                return UInt32Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(Int64 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(Int64))
            {
                return Int64Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        /// <summary>
        ///     Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Boolean IsDefined<T>(UInt64 value) where T : unmanaged, Enum
        {
            if (CacheUnderlyingOperation<T>.UnderlyingType == typeof(UInt64))
            {
                return UInt64Operation<T>.IsDefined(ref value);
            }

            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }

        private static class CacheType<T> where T : unmanaged, Enum
        {
            public static Type Type { get; }
            public static Type UnderlyingType { get; }

            static CacheType()
            {
                Type = typeof(T);
                UnderlyingType = Enum.GetUnderlyingType(Type);
            }
        }

        private static class CacheValues<T> where T : unmanaged, Enum
        {
            public static ReadOnlyArray<T> Values { get; }
            public static Boolean IsEmpty { get; }

            static CacheValues()
            {
                Type type = CacheType<T>.Type;
                T[] values = Enum.GetValues(type) as T[] ?? throw new ArgumentException(nameof(T));
                Values = values.ToReadOnlyArray();
                IsEmpty = values.Length <= 0;
            }
        }
        
        private static class CacheValuesWithoutDefault<T> where T : unmanaged, Enum
        {
            public static ReadOnlyArray<T> Values { get; }

            public static Boolean IsEmpty
            {
                get
                {
                    return Values.Count <= 0;
                }
            }

            static CacheValuesWithoutDefault()
            {
                Values = CacheValues<T>.Values.Where(GenericUtils.IsNotDefault).ToReadOnlyArray();
            }
        }

        private static class CacheNames<T> where T : unmanaged, Enum
        {
            public static ReadOnlyArray<String> Names { get; }

            static CacheNames()
            {
                Type type = CacheType<T>.Type;
                Names = Enum.GetNames(type).ToReadOnlyArray();
            }
        }

        private static class CacheMembers<T> where T : unmanaged, Enum
        {
            public static ReadOnlyArray<EnumMember<T>> Members { get; }

            static CacheMembers()
            {
                Members = CacheNames<T>.Names.Select(value => new EnumMember<T>(value)).ToReadOnlyArray();
            }
        }
        
        /// <summary>
        /// Provides cache for enum attributes.
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <typeparam name="TAttribute">Attribute Type</typeparam>
        internal static class CacheAttributes<T, TAttribute> where T : unmanaged, Enum where TAttribute : Attribute
        {
            public static IImmutableDictionary<T, IReadOnlyList<TAttribute>> Cache { get; }

            static CacheAttributes()
            {
                Cache = GetValues<T>().ToImmutableDictionary(key => key, value => value.ToMember().FieldInfo!
                    .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().ToArray() as IReadOnlyList<TAttribute>);
            }
        }

        private static class CacheMinMaxValues<T> where T : unmanaged, Enum
        {
            public static T MinValue { get; }
            public static T MaxValue { get; }

            static CacheMinMaxValues()
            {
                ReadOnlyArray<T> values = CacheValues<T>.Values;
                MinValue = values.DefaultIfEmpty().Min();
                MaxValue = values.DefaultIfEmpty().Max();
            }
        }

        private static class CacheIsFlags<T> where T : unmanaged, Enum
        {
            public static Boolean IsFlags { get; }

            static CacheIsFlags()
            {
                Type type = CacheType<T>.Type;
                IsFlags = Attribute.IsDefined(type, typeof(FlagsAttribute));
            }
        }

        private static class CacheMembersByName<T> where T : unmanaged, Enum
        {
            public static IImmutableDictionary<String, EnumMember<T>> MemberByName { get; }

            static CacheMembersByName()
            {
                MemberByName = CacheMembers<T>.Members.ToImmutableDictionary(x => x.Name);
            }
        }

        private static class CacheUnderlyingOperation<T> where T : unmanaged, Enum
        {
            public static Type UnderlyingType { get; }
            public static IUnderlyingEnumOperation<T> UnderlyingOperation { get; }

            static CacheUnderlyingOperation()
            {
                Type type = CacheType<T>.Type;
                T min = CacheMinMaxValues<T>.MinValue;
                T max = CacheMinMaxValues<T>.MaxValue;
                EnumMember<T>[] distincted = CacheMembers<T>.Members.OrderBy(x => x.Value).Distinct(new EnumMember<T>.ValueComparer()).ToArray();
                UnderlyingType = CacheType<T>.UnderlyingType;
                
                // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
                UnderlyingOperation = Type.GetTypeCode(type) switch
                {
                    TypeCode.SByte => SByteOperation<T>.Create(min, max, distincted),
                    TypeCode.Byte => ByteOperation<T>.Create(min, max, distincted),
                    TypeCode.Int16 => Int16Operation<T>.Create(min, max, distincted),
                    TypeCode.UInt16 => UInt16Operation<T>.Create(min, max, distincted),
                    TypeCode.Int32 => Int32Operation<T>.Create(min, max, distincted),
                    TypeCode.UInt32 => UInt32Operation<T>.Create(min, max, distincted),
                    TypeCode.Int64 => Int64Operation<T>.Create(min, max, distincted),
                    TypeCode.UInt64 => UInt64Operation<T>.Create(min, max, distincted),
                    _ => throw new InvalidOperationException()
                };
            }
        }
    }
}