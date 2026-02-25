// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using AgileObjects.ReadableExpressions;
using NetExtender.Types.Tuples;
using NetExtender.Types.Collections;
using NetExtender.Types.Expressions.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class StringConvertUtilities
    {
        #region ToString

        private readonly struct StringConverterInfo<T> : IEquatable<StringConverterInfo<T>>
        {
            public static implicit operator StringConverterInfo(StringConverterInfo<T> value)
            {
                return new StringConverterInfo(Convert(value.Handler), Convert(value.Format));
            }

            private StringConverter<T> Handler { get; }
            private StringFormatConverter<T> Format { get; }

            public StringConverterInfo(StringConverter<T> handler, StringFormatConverter<T> format)
            {
                Handler = handler ?? throw new ArgumentNullException(nameof(handler));
                Format = format ?? throw new ArgumentNullException(nameof(format));
            }

            [return: NotNullIfNotNull("converter")]
            private static StringConverter? Convert(StringConverter<T>? converter)
            {
                return converter is not null ? (@object, escape, provider) => converter((T?) @object, escape, provider) : null;
            }

            [return: NotNullIfNotNull("converter")]
            private static StringFormatConverter? Convert(StringFormatConverter<T>? converter)
            {
                return converter is not null ? (@object, escape, format, provider) => converter((T?) @object, escape, format, provider) : null;
            }

            public String? Convert(T? value, EscapeType escape, IFormatProvider? provider)
            {
                return Handler(value, escape, provider);
            }

            public String? Convert(T? value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return Format(value, escape, format, provider);
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Handler, Format);
            }

            public override Boolean Equals(Object? other)
            {
                return other is StringConverterInfo<T> value && Equals(value);
            }

            public Boolean Equals(StringConverterInfo<T> other)
            {
                return Equals(Handler, other.Handler) && Equals(Format, other.Format);
            }
        }

        private readonly struct StringConverterInfo : IEquatable<StringConverterInfo>
        {
            private StringConverter Handler { get; }
            private StringFormatConverter Format { get; }

            public StringConverterInfo(StringConverter handler, StringFormatConverter format)
            {
                Handler = handler ?? throw new ArgumentNullException(nameof(handler));
                Format = format ?? throw new ArgumentNullException(nameof(format));
            }

            public String? Convert(Object? value, EscapeType escape, IFormatProvider? provider)
            {
                return Handler(value, escape, provider);
            }

            public String? Convert(Object? value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return Format(value, escape, format, provider);
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Handler, Format);
            }

            public override Boolean Equals(Object? other)
            {
                return other is StringConverterInfo value && Equals(value);
            }

            public Boolean Equals(StringConverterInfo other)
            {
                return Equals(Handler, other.Handler) && Equals(Format, other.Format);
            }
        }

        private static ConcurrentDictionary<Type, StringConverterInfo> StringConverters { get; } = new ConcurrentDictionary<Type, StringConverterInfo>();

        public static Boolean RegisterStringHandler(Type type, StringConverter handler)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            String? Format(Object? value, EscapeType escape, String? _, IFormatProvider? provider)
            {
                return handler.Invoke(value, escape, provider);
            }

            return RegisterStringHandler(type, handler, Format);
        }

        public static Boolean RegisterStringHandler<T>(StringConverter<T> handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            String? Format(T? value, EscapeType escape, String? _, IFormatProvider? provider)
            {
                return handler.Invoke(value, escape, provider);
            }

            return RegisterStringHandler(handler, Format);
        }

        public static Boolean RegisterStringHandler(Type type, StringConverter handler, StringFormatConverter format)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            StringConverterInfo info = new StringConverterInfo(handler, format);
            StringConverters.AddOrUpdate(type, info, (_, _) => info);
            return true;
        }

        public static Boolean RegisterStringHandler<T>(StringConverter<T> handler, StringFormatConverter<T> format)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            StringConverterInfo<T> info = new StringConverterInfo<T>(handler, format);
            StringConverters.AddOrUpdate(typeof(T), info, (_, _) => info);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(GetString);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source, String? format)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => GetString(item, format));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source, IFormatProvider? provider)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => GetString(item, provider));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetStringEnumerable<T>(this IEnumerable<T> source, String? format, IFormatProvider? provider)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => GetString(item, format, provider));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value)
        {
            return GetString(value, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape<T>(T value)
        {
            return GetString(value, EscapeType.FullWithNull);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value, EscapeType escape)
        {
            return GetString(value, escape, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape<T>(T value, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.FullWithNull, provider);
        }

        public static String? GetString<T>(this T value, EscapeType escape, IFormatProvider? provider)
        {
            return value switch
            {
                null => escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null,
                Char character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(provider)}\'" : character.GetString(provider),
                Char32 character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(provider)}\'" : character.GetString(provider),
#if NETCOREAPP3_1_OR_GREATER
                Rune character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(provider)}\'" : character.GetString(provider),
#endif
                String @string => escape.HasFlag(EscapeType.Full) ? $"\"{@string.GetString(provider)}\"" : @string.GetString(provider),
                Boolean number => number.GetString(provider),
                SByte number => number.GetString(provider),
                Byte number => number.GetString(provider),
                Int16 number => number.GetString(provider),
                UInt16 number => number.GetString(provider),
                Int32 number => number.GetString(provider),
                UInt32 number => number.GetString(provider),
                Int64 number => number.GetString(provider),
                UInt64 number => number.GetString(provider),
                Single number => number.GetString(provider),
                Double number => number.GetString(provider),
                Decimal number => number.GetString(provider),
                Complex number => number.GetString(provider),
                BigInteger number => number.GetString(provider),
                DateTime number => number.GetString(provider),
                TimeSpan number => number.GetString(provider),
                Enum number => number.GetString(escape, provider),
                Exception exception => exception.GetString(escape, provider),
                Expression expression => expression.GetString(escape, provider),
                IReadableExpression expression => expression.ToString(),
                IString @string => escape.HasFlag(EscapeType.Full) ? $"\"{@string.ToString(provider)}\"" : @string.ToString(provider),
                ITuple tuple => tuple.GetString(escape, provider),
                IEnumerable enumerable => enumerable.GetString(escape, provider),
                IFormattable formattable => formattable.ToString(null, provider),
                IConvertible convertible => convertible.ToString(provider),
                _ => ToStringUnknown(value, escape, provider)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value, String? format)
        {
            return GetString(value, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value, String? format, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape<T>(T value, String? format)
        {
            return GetStringEscape(value, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape<T>(T value, String? format, IFormatProvider? provider)
        {
            return GetString(value, EscapeType.FullWithNull, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString<T>(this T value, EscapeType escape, String? format)
        {
            return GetString(value, escape, format, null);
        }

        public static String? GetString<T>(this T value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return value switch
            {
                null => escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null,
                Char character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(format, provider)}\'" : character.GetString(format, provider),
                Char32 character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(format, provider)}\'" : character.GetString(format, provider),
#if NETCOREAPP3_1_OR_GREATER
                Rune character => escape.HasFlag(EscapeType.Full) ? $"\'{character.GetString(format, provider)}\'" : character.GetString(format, provider),
#endif
                String @string => escape.HasFlag(EscapeType.Full) ? $"\"{@string.GetString(format, provider)}\"" : @string.GetString(format, provider),
                Boolean number => number.GetString(format, provider),
                SByte number => number.GetString(format, provider),
                Byte number => number.GetString(format, provider),
                Int16 number => number.GetString(format, provider),
                UInt16 number => number.GetString(format, provider),
                Int32 number => number.GetString(format, provider),
                UInt32 number => number.GetString(format, provider),
                Int64 number => number.GetString(format, provider),
                UInt64 number => number.GetString(format, provider),
                Single number => number.GetString(format, provider),
                Double number => number.GetString(format, provider),
                Decimal number => number.GetString(format, provider),
                Complex number => number.GetString(format, provider),
                BigInteger number => number.GetString(format, provider),
                DateTime number => number.GetString(format, provider),
                TimeSpan number => number.GetString(format, provider),
                Enum number => number.GetString(escape, format, provider),
                Exception exception => exception.GetString(escape, format, provider),
                Expression expression => expression.GetString(escape, format, provider),
                IReadableExpression expression => expression.ToString(),
                IString @string => escape.HasFlag(EscapeType.Full) ? $"\"{@string.ToString(format, provider)}\"" : @string.ToString(format, provider),
                ITuple tuple => tuple.GetString(escape, format, provider),
                IEnumerable enumerable => enumerable.GetString(escape, format, provider),
                IFormattable formattable => formattable.ToString(format, provider),
                IConvertible convertible => convertible.ToString(provider),
                _ => ToStringUnknown(value, escape, format, provider)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? GetStringUnknown(Object? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetStringUnknownCore(value, escape, provider, out String? result) ? result : value.GetString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? ToStringUnknown(Object? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetStringUnknownCore(value, escape, provider, out String? result) ? result : value?.ToString();
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean GetStringUnknownCore(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
        {
            if (value is null)
            {
                result = null;
                return false;
            }

            Type? type = value.GetType();

            if (UnknownTypeCache.TryGetAccessor(type, out UnknownTypeCache.ConvertAccessor? accessor))
            {
                return accessor.TryConvert(value, escape, provider, out result);
            }

            while (type is not null)
            {
                if (StringConverters.TryGetValue(type, out StringConverterInfo converter))
                {
                    result = converter.Convert(value, escape, provider);
                    return true;
                }

                type = type.BaseType;
            }

            result = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? GetStringUnknown(Object? value, EscapeType escape, String? format)
        {
            return GetStringUnknown(value, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? GetStringUnknown(Object? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetStringUnknownCore(value, escape, format, provider, out String? result) ? result : value.GetString(escape, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? ToStringUnknown(Object? value, EscapeType escape, String? format)
        {
            return ToStringUnknown(value, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String? ToStringUnknown(Object? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetStringUnknownCore(value, escape, format, provider, out String? result) ? result : value?.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean GetStringUnknownCore(Object? value, EscapeType escape, String? format, out String? result)
        {
            return GetStringUnknownCore(value, escape, format, null, out result);
        }

        private static Boolean GetStringUnknownCore(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
        {
            if (value is null)
            {
                result = null;
                return false;
            }

            Type? type = value.GetType();

            if (UnknownTypeCache.TryGetAccessor(type, out UnknownTypeCache.ConvertAccessor? accessor))
            {
                return accessor.TryFormatConvert(value, escape, format, provider, out result);
            }

            while (type is not null)
            {
                if (StringConverters.TryGetValue(type, out StringConverterInfo converter))
                {
                    result = converter.Convert(value, escape, format, provider);
                    return true;
                }

                type = type.BaseType;
            }

            result = null;
            return false;
        }

        private static class UnknownTypeCache
        {
            public delegate Boolean TryUnknownConvertDelegate(Object? value, EscapeType escape, IFormatProvider? provider, out String? result);

            public delegate Boolean TryUnknownFormatConvertDelegate(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result);

            private static ConcurrentDictionary<Type, ConvertAccessor> Cache { get; } = new ConcurrentDictionary<Type, ConvertAccessor>();

            public record ConvertAccessor
            {
                public Type Type { get; }
                public TryUnknownConvertDelegate TryConvert { get; }
                public TryUnknownFormatConvertDelegate TryFormatConvert { get; }

                private ConvertAccessor(Type type)
                {
                    Type = type ?? throw new ArgumentNullException(nameof(type));
                    (TryConvert, TryFormatConvert) = CreateAccessor(type);
                }

                public static ConvertAccessor Create(Type type)
                {
                    return new ConvertAccessor(type);
                }

                // ReSharper disable once CognitiveComplexity
                private static (TryUnknownConvertDelegate, TryUnknownFormatConvertDelegate) CreateAccessor(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    if (type == typeof(DictionaryEntry))
                    {
                        static Boolean DictionaryEntryConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            if (value is not DictionaryEntry entry)
                            {
                                result = null;
                                return false;
                            }

                            result = $"{{{entry.Key.GetString(escape, provider)} : {entry.Value.GetString(escape, provider)}}}";
                            return true;
                        }

                        static Boolean DictionaryEntryFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            if (value is not DictionaryEntry entry)
                            {
                                result = null;
                                return false;
                            }

                            result = $"{{{entry.Key.GetString(escape, format, provider)} : {entry.Value.GetString(escape, format, provider)}}}";
                            return true;
                        }

                        return (DictionaryEntryConvert, DictionaryEntryFormatConvert);
                    }

                    if (type.IsAnonymousType())
                    {
                        Boolean AnonymousTypeConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            if (value is null || type.GetAnonymousProperties() is not { } properties)
                            {
                                result = null;
                                return false;
                            }

                            IEnumerable<String> strings = properties.Select(property => $"{property.Property.Name}: {GetString(property.Getter(value), escape, provider)}");
                            result = $"{{{String.Join(", ", strings)}}}";
                            return true;
                        }

                        Boolean AnonymousTypeFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            if (value is null || type.GetAnonymousProperties() is not { } properties)
                            {
                                result = null;
                                return false;
                            }

                            IEnumerable<String> strings = properties.Select(property => $"{property.Property.Name}: {GetString(property.Getter(value), escape, format, provider)}");
                            result = $"{{{String.Join(", ", strings)}}}";
                            return true;
                        }

                        return (AnonymousTypeConvert, AnonymousTypeFormatConvert);
                    }

                    Type generic = type.TryGetGenericTypeDefinition();

                    if (generic == KeyValuePairUtilities.KeyValuePairType)
                    {
                        static Boolean KeyValuePairConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            if (value is null || !KeyValuePairUtilities.TryGetAccessor(value.GetType(), out KeyValuePairAccessor? accessors))
                            {
                                result = null;
                                return false;
                            }

                            result = $"{{{accessors.Key.Invoke(value).GetString(escape, provider)} : {accessors.Value.Invoke(value).GetString(escape, provider)}}}";
                            return true;
                        }

                        static Boolean KeyValuePairFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            if (value is null || !KeyValuePairUtilities.TryGetAccessor(value.GetType(), out KeyValuePairAccessor? accessors))
                            {
                                result = null;
                                return false;
                            }

                            result = $"{{{accessors.Key.Invoke(value).GetString(escape, format, provider)} : {accessors.Value.Invoke(value).GetString(escape, format, provider)}}}";
                            return true;
                        }

                        return (KeyValuePairConvert, KeyValuePairFormatConvert);
                    }

                    if (generic == MaybeUtilities.MaybeType)
                    {
                        static Boolean MaybeConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case IMaybe maybe:
                                    result = maybe.HasValue ? GetString(maybe.Value, escape, provider) : GetString(default(String), escape, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString(item.HasValue ? (Object) item.Value : null, escape, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        static Boolean MaybeFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case IMaybe maybe:
                                    result = maybe.HasValue ? GetString(maybe.Value, escape, format, provider) : GetString(default(String), escape, format, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString(item.HasValue ? (Object) item.Value : null, escape, format, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        return (MaybeConvert, MaybeFormatConvert);
                    }

                    if (generic == MaybeUtilities.NullMaybeType)
                    {
                        static Boolean NullMaybeConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case INullMaybe maybe:
                                    result = GetString(maybe.Value, escape, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString((Object) item.Value, escape, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        static Boolean NullMaybeFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case INullMaybe maybe:
                                    result = GetString(maybe.Value, escape, format, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString((Object) item.Value, escape, format, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        return (NullMaybeConvert, NullMaybeFormatConvert);
                    }

                    if (generic == MaybeUtilities.WeakMaybeType)
                    {
                        static Boolean WeakMaybeConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case IWeakMaybe { Maybe: var maybe }:
                                    result = maybe.HasValue ? GetString(maybe.Internal, escape, provider) : GetString(default(String), escape, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString(item.HasValue ? (Object) item.Value : null, escape, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        static Boolean WeakMaybeFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            switch (value)
                            {
                                case null:
                                    result = null;
                                    return false;
                                case IWeakMaybe { Maybe: var maybe }:
                                    result = maybe.HasValue ? GetString(maybe.Internal, escape, format, provider) : GetString(default(String), escape, format, provider);
                                    return true;
                                default:
                                {
                                    try
                                    {
                                        dynamic item = value;
                                        result = GetString(item.HasValue ? (Object) item.Value : null, escape, format, provider);
                                        return true;
                                    }
                                    catch (Exception)
                                    {
                                        result = null;
                                        return false;
                                    }
                                }
                            }
                        }

                        return (WeakMaybeConvert, WeakMaybeFormatConvert);
                    }

                    if (generic.IsMemorySpan())
                    {
                        static Boolean MemorySpanConvert(Object? value, EscapeType escape, IFormatProvider? provider, out String? result)
                        {
                            if (value is null)
                            {
                                result = null;
                                return false;
                            }

                            result = GetString((dynamic) value, escape, provider);
                            return true;
                        }

                        static Boolean MemorySpanFormatConvert(Object? value, EscapeType escape, String? format, IFormatProvider? provider, out String? result)
                        {
                            if (value is null)
                            {
                                result = null;
                                return false;
                            }

                            result = GetString((dynamic) value, escape, format, provider);
                            return true;
                        }

                        return (MemorySpanConvert, MemorySpanFormatConvert);
                    }

                    throw new NotSupportedException();
                }
            }

            public static Boolean TryGetAccessor(Type type, [MaybeNullWhen(false)] out ConvertAccessor result)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                try
                {
                    result = Cache.GetOrAdd(type, ConvertAccessor.Create);
                    return true;
                }
                catch (Exception)
                {
                    result = null;
                    return false;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Boolean value, String? format, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value, IFormatProvider? provider)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char value, String? format, IFormatProvider? provider)
        {
            return Char.ToString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char32 value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char32 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char32 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Char32 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this SByte value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Byte value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int16 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt16 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int32 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt32 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Int64 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this UInt64 value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Single value, String? format, IFormatProvider? provider)
        {
            provider ??= CultureInfo.InvariantCulture;

            static String Separator(IFormatProvider provider)
            {
                return NumberFormatInfo.GetInstance(provider).NumberDecimalSeparator;
            }

            return format switch
            {
                "." => value.CountDigitsAfterPoint() > 0 ? value.ToString(provider) : $"{value.ToString(provider)}{Separator(provider)}",
                { Length: >= 1 } when format[0] == '.' => value.CountDigitsAfterPoint() > 0 ? value.ToString(format[1..], provider) : $"{value.ToString("N0", provider)}{Separator(provider)}",
                _ => value.ToString(format, provider)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Double value, String? format, IFormatProvider? provider)
        {
            provider ??= CultureInfo.InvariantCulture;

            static String Separator(IFormatProvider provider)
            {
                return NumberFormatInfo.GetInstance(provider).NumberDecimalSeparator;
            }

            return format switch
            {
                "." => value.CountDigitsAfterPoint() > 0 ? value.ToString(provider) : $"{value.ToString(provider)}{Separator(provider)}",
                { Length: >= 1 } when format[0] == '.' => value.CountDigitsAfterPoint() > 0 ? value.ToString(format[1..], provider) : $"{value.ToString("N0", provider)}{Separator(provider)}",
                _ => value.ToString(format, provider)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value, IFormatProvider? provider)
        {
            return value.Normalize().ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Decimal value, String? format, IFormatProvider? provider)
        {
            value = value.Normalize();
            provider ??= CultureInfo.InvariantCulture;

            static String Separator(IFormatProvider provider)
            {
                return NumberFormatInfo.GetInstance(provider).NumberDecimalSeparator;
            }

            return format switch
            {
                "." => value.CountDigitsAfterPoint() > 0 ? value.ToString(provider) : $"{value.ToString(provider)}{Separator(provider)}",
                { Length: >= 1 } when format[0] == '.' => value.CountDigitsAfterPoint() > 0 ? value.ToString(format[1..], provider) : $"{value.ToString("N0", provider)}{Separator(provider)}",
                _ => value.ToString(format, provider)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Complex value)
        {
            return GetString(value, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Complex value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Complex value, IFormatProvider? provider)
        {
            return GetString(value, null, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Complex value, String? format, IFormatProvider? provider)
        {
            return value.Imaginary switch
            {
                < 0 => $"{value.Real.ToString(format, provider ?? CultureInfo.InvariantCulture)} - {Math.Abs(value.Imaginary).ToString(format, provider ?? CultureInfo.InvariantCulture)}i",
                0 => value.Real.ToString(format, provider ?? CultureInfo.InvariantCulture),
                _ => $"{value.Real.ToString(format, provider ?? CultureInfo.InvariantCulture)} + {value.Imaginary.ToString(format, provider ?? CultureInfo.InvariantCulture)}i"
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this BigInteger value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value, IFormatProvider? provider)
        {
            return value.ToString(provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this DateTime value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value)
        {
            return value.GetString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value, IFormatProvider? provider)
        {
            return value.ToString(null, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this TimeSpan value, String? format, IFormatProvider? provider)
        {
            return value.ToString(format, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value)
        {
            return GetStringEscape(value?.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, EscapeType escape)
        {
            return value is not null ? GetStringEscape(escape.HasFlag(EscapeType.Full) ? $"{value.GetType().Name}.{value.ToString()}" : value.ToString()) : StringUtilities.NullString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, String? format)
        {
            return GetString(value, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, IFormatProvider? provider)
        {
            return GetString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, String? format, IFormatProvider? provider)
        {
            return GetString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, EscapeType escape, String? format)
        {
            return GetString(value, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Enum? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value)
        {
            return GetString(value, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, IFormatProvider? provider)
        {
            return GetString(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, String? format, IFormatProvider? provider)
        {
            return GetString(value, ConvertUtilities.DefaultEscapeType, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, EscapeType escape)
        {
            return GetString(value, escape, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, EscapeType escape, String? format)
        {
            return GetString(value, escape, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(value, escape, null, provider);
        }

        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Exception? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            if (value is null)
            {
                return GetString((String?) null, escape, provider);
            }

            StringBuilder builder = new StringBuilder();
            FormatException(builder, value, escape, format, provider, 0);
            return builder.ToString();
        }

        // ReSharper disable once UnusedParameter.Local
        private static void FormatException(StringBuilder builder, Exception exception, EscapeType escape, String? format, IFormatProvider? provider, Int32 depth)
        {
            provider ??= CultureInfo.InvariantCulture;

            start:
            if (depth > 0)
            {
                builder.AppendLine();
                builder.AppendLine("--- Inner Exception ---");
            }

            builder.Append(exception.GetType().FullName);
            builder.Append(": ");
            builder.AppendLine(exception.Message);
            builder.Append($"{nameof(Exception.HResult)}: ");
            builder.AppendLine(exception.HResult.ToString(provider));

            if (exception.Data.Count > 0)
            {
                builder.AppendLine($"{nameof(Exception.Data)}:");
                foreach (Object? key in exception.Data.Keys)
                {
                    builder.Append('\t');
                    builder.Append(key);
                    builder.Append(": ");
                    builder.AppendLine(exception.Data[key]?.GetString(escape, provider));
                }
            }

            builder.AppendLine(exception.ToString());

            if (exception is AggregateException aggregate)
            {
                foreach (Exception inner in aggregate.InnerExceptions)
                {
                    FormatException(builder, inner, escape, format, provider, depth + 1);
                }

                return;
            }

            if (exception.InnerException is not null)
            {
                depth++;
                exception = exception.InnerException;
                goto start;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value)
        {
            return GetString(value, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, IFormatProvider? provider)
        {
            return GetString(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, String? format, IFormatProvider? provider)
        {
            return GetString(value, ConvertUtilities.DefaultEscapeType, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, EscapeType escape)
        {
            if (value is null)
            {
                return GetString((String?) null, escape);
            }

            try
            {
                return value.ToReadableString(ReadableExpressionUtilities.Settings);
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, EscapeType escape, String? format)
        {
            return GetString(value, escape, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this Expression? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this ITuple source)
        {
            return GetString(source, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this ITuple source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this ITuple source, EscapeType escape)
        {
            return GetString(source, escape, (IFormatProvider?) null);
        }

        public static String GetString(this ITuple source, EscapeType escape, IFormatProvider? provider)
        {
            switch (source.Length)
            {
                case 0:
                    return "[]";
                case 1:
                    return $"[{GetString(source[0], escape, provider)}]";
                default:
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append('(');

                    using TupleEnumerator<ITuple> enumerator = source.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        builder.Append(GetString(enumerator.Current, escape, provider));

                        while (enumerator.MoveNext())
                        {
                            builder.Append(", ");
                            builder.Append(GetString(enumerator.Current, escape, provider));
                        }
                    }

                    builder.Append(')');
                    return builder.ToString();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this ITuple source, EscapeType escape, String? format)
        {
            return GetString(source, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this ITuple source, String? format, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, format, provider);
        }

        public static String GetString(this ITuple source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            switch (source.Length)
            {
                case 0:
                    return "[]";
                case 1:
                    return $"[{GetString(source[0], escape, format, provider)}]";
                default:
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append('(');

                    using TupleEnumerator<ITuple> enumerator = source.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        builder.Append(GetString(enumerator.Current, escape, format, provider));

                        while (enumerator.MoveNext())
                        {
                            builder.Append(", ");
                            builder.Append(GetString(enumerator.Current, escape, format, provider));
                        }
                    }

                    builder.Append(')');
                    return builder.ToString();
                }
            }
        }

#if NETCOREAPP3_1_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Rune value)
        {
            return value.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Rune value, String? format)
        {
            return GetString(value, format, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Rune value, IFormatProvider? provider)
        {
            return GetString(value, null, provider ?? CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Rune value, String? format, IFormatProvider? provider)
        {
            return StringUtilities.ToString(value, format, provider ?? CultureInfo.InvariantCulture);
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source)
        {
            return GetString(source.Span, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, String? format)
        {
            return GetString(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, IFormatProvider? provider)
        {
            return GetString(source.Span, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, EscapeType escape)
        {
            return GetString(source.Span, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(source.Span, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, EscapeType escape, String? format)
        {
            return GetString(source, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, String? format, IFormatProvider? provider)
        {
            return GetString(source.Span, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Memory<T> source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString(source.Span, escape, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source)
        {
            return GetString(source, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, String? format)
        {
            return GetString(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, EscapeType escape)
        {
            return GetString((ReadOnlySpan<T>) source, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, EscapeType escape, IFormatProvider? provider)
        {
            return GetString((ReadOnlySpan<T>) source, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, EscapeType escape, String? format)
        {
            return GetString(source, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, String? format, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this Span<T> source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString((ReadOnlySpan<T>) source, escape, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source)
        {
            return GetString(source.Span, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, String? format)
        {
            return GetString(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, IFormatProvider? provider)
        {
            return GetString(source.Span, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, EscapeType escape)
        {
            return GetString(source.Span, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, EscapeType escape, IFormatProvider? provider)
        {
            return GetString(source.Span, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, EscapeType escape, String? format)
        {
            return GetString(source, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, String? format, IFormatProvider? provider)
        {
            return GetString(source.Span, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlyMemory<T> source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return GetString(source.Span, escape, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source)
        {
            return GetString(source, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source, EscapeType escape)
        {
            return GetString(source, escape, (IFormatProvider?) null);
        }

        public static String GetString<T>(this ReadOnlySpan<T> source, EscapeType escape, IFormatProvider? provider)
        {
            switch (source.Length)
            {
                case 0:
                    return "[]";
                case 1:
                    return $"[{GetString(source[0], escape, provider)}]";
                default:
                    StringBuilder builder = new StringBuilder();
                    builder.Append('[');

                    ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        builder.Append(GetString(enumerator.Current, escape, provider));

                        while (enumerator.MoveNext())
                        {
                            builder.Append(", ");
                            builder.Append(GetString(enumerator.Current, escape, provider));
                        }
                    }

                    builder.Append(']');
                    return builder.ToString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source, String? format)
        {
            return GetString(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source, String? format, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString<T>(this ReadOnlySpan<T> source, EscapeType escape, String? format)
        {
            return GetString(source, escape, format, null);
        }

        public static String GetString<T>(this ReadOnlySpan<T> source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            switch (source.Length)
            {
                case 0:
                    return "[]";
                case 1:
                    return $"[{GetString(source[0], escape, format, provider)}]";
                default:
                    StringBuilder builder = new StringBuilder();
                    builder.Append('[');

                    ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        builder.Append(GetString(enumerator.Current, escape, format, provider));

                        while (enumerator.MoveNext())
                        {
                            builder.Append(", ");
                            builder.Append(GetString(enumerator.Current, escape, format, provider));
                        }
                    }

                    builder.Append(']');
                    return builder.ToString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this String? value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(this String? value)
        {
            return value ?? StringUtilities.NullString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("value")]
        public static String? GetString(this String? value, EscapeType escape)
        {
            return escape.HasFlag(EscapeType.Null) ? GetStringEscape(value) : GetString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, String? format)
        {
            return GetString(value, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, IFormatProvider? provider)
        {
            return value?.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, String? format, IFormatProvider? provider)
        {
            return value?.ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(String? value, String? format)
        {
            return GetStringEscape(value, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(String? value, IFormatProvider? provider)
        {
            return value?.ToString(provider) ?? StringUtilities.NullString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetStringEscape(String? value, String? format, IFormatProvider? provider)
        {
            return value?.ToString(provider) ?? StringUtilities.NullString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, EscapeType escape, String? format)
        {
            return GetString(value, escape, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, EscapeType escape, IFormatProvider? provider)
        {
            return escape.HasFlag(EscapeType.Null) ? GetStringEscape(value, provider) : GetString(value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this String? value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return escape.HasFlag(EscapeType.Null) ? GetStringEscape(value, format, provider) : GetString(value, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source)
        {
            return GetString(source, EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source)
        {
            return GetString(source, EscapeType.FullWithNull);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, EscapeType escape)
        {
            return GetString(source, escape, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.FullWithNull, provider);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static String? GetString(this IEnumerable? source, EscapeType escape, IFormatProvider? provider)
        {
            switch (source)
            {
                case null:
                    return escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null;
                case String result:
                    return escape.HasFlag(EscapeType.Full) ? $"\"{result.GetString(provider)}\"" : result.GetString(provider);
                default:
                    switch (source.GetCollectionType())
                    {
                        case CollectionType.Set:
                        case CollectionType.GenericSet:
                            return source.SetGetString(escape, provider);
                        case CollectionType.Dictionary:
                        case CollectionType.GenericDictionary:
                        case CollectionType.Map:
                        case CollectionType.GenericMap:
                            return source.DictionaryGetString(escape, provider);
                        default:
                            if (source is IEnumerable<IEnumerable> jagged)
                            {
                                return jagged.JaggedGetString(escape, provider);
                            }

                            return source.EnumerableGetString(escape, provider);
                    }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, String? format)
        {
            return GetString(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetString(this IEnumerable source, String? format, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source, String? format)
        {
            return GetStringEscape(source, format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetStringEscape(IEnumerable source, String? format, IFormatProvider? provider)
        {
            return GetString(source, EscapeType.FullWithNull, format, provider);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static String? GetString(this IEnumerable? source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            switch (source)
            {
                case null:
                    return escape.HasFlag(EscapeType.Null) ? StringUtilities.NullString : null;
                case String result:
                    return escape.HasFlag(EscapeType.Full) ? $"\"{result.GetString(format, provider)}\"" : result.GetString(format, provider);
                default:
                    switch (source.GetCollectionType())
                    {
                        case CollectionType.Set:
                        case CollectionType.GenericSet:
                            return source.SetGetString(escape, format, provider);
                        case CollectionType.Dictionary:
                        case CollectionType.GenericDictionary:
                        case CollectionType.Map:
                        case CollectionType.GenericMap:
                            return source.DictionaryGetString(escape, format, provider);
                        default:
                            if (source is IEnumerable<IEnumerable> jagged)
                            {
                                return jagged.JaggedGetString(escape, format, provider);
                            }

                            return source.EnumerableGetString(escape, format, provider);
                    }
            }
        }

        private static IEnumerable<String?> PairGetString(this IEnumerable dictionary, EscapeType escape, IFormatProvider? provider)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (Object? value in dictionary)
            {
                yield return GetStringUnknown(value, escape, provider);
            }
        }

        private static IEnumerable<String?> PairGetString(this IEnumerable dictionary, EscapeType escape, String? format, IFormatProvider? provider)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (Object? value in dictionary)
            {
                yield return GetStringUnknown(value, escape, format, provider);
            }
        }

        private static String DictionaryGetString(this IEnumerable dictionary, EscapeType escape, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", dictionary.PairGetString(escape, provider))}}}";
        }

        private static String DictionaryGetString(this IEnumerable dictionary, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", dictionary.PairGetString(escape, format, provider))}}}";
        }

        private static String SetGetString(this IEnumerable set, EscapeType escape, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", set.Cast<Object>().Select(item => item.GetString(escape, provider)))}}}";
        }

        private static String SetGetString(this IEnumerable set, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return $"{{{String.Join(", ", set.Cast<Object>().Select(item => item.GetString(escape, format, provider)))}}}";
        }

        private static String JaggedGetString(this IEnumerable<IEnumerable> jagged, EscapeType escape, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", jagged.Select(enumerable => enumerable.GetString(escape, provider)))}]";
        }

        private static String JaggedGetString(this IEnumerable<IEnumerable> jagged, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", jagged.Select(enumerable => enumerable.GetString(escape, format, provider)))}]";
        }

        private static String EnumerableGetString(this IEnumerable source, EscapeType escape, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", source.Cast<Object>().Select(item => item.GetString(escape, provider)))}]";
        }

        private static String EnumerableGetString(this IEnumerable source, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return $"[{String.Join(", ", source.Cast<Object>().Select(item => item.GetString(escape, format, provider)))}]";
        }

        #endregion
    }
}