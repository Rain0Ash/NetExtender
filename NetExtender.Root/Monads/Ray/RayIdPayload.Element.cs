using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Exceptions;
using NetExtender.Types.Entities;
using NetExtender.Types.Enums;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Strings;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Monads
{
    public abstract partial class RayIdPayload
    {
        [Serializable]
        private sealed record StringElement : Element<String>
        {
            public StringElement(String value)
                : base(value)
            {
            }
        }

        [Serializable]
        public record Element<T> : Element where T : notnull
        {
            public delegate String? FormatHandler(T value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written);
            public delegate Int32? ParseHandler(ReadOnlySpan<Char> buffer, IFormatProvider? provider, [MaybeNullWhen(false)] out T result);

            internal static (FormatHandler, ParseHandler) Default
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (DefaultFormatter, DefaultParser);
                }
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set
                {
                    (DefaultFormatter, DefaultParser) = value;
                }
            }

            private static FormatHandler formatter;
            public static FormatHandler DefaultFormatter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return formatter;
                }
                set
                {
                    formatter = value ?? throw new ArgumentNullException(nameof(value));
                }
            }

            private static ParseHandler parser;
            public static ParseHandler DefaultParser
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return parser;
                }
                set
                {
                    parser = value ?? throw new ArgumentNullException(nameof(value));
                }
            }

            static Element()
            {
                static (FormatHandler, ParseHandler) Factory()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                    MethodInfo method = typeof(T).GetMethod(nameof(ToString), binding, new [] { typeof(String) }) ?? throw new MissingMethodException(typeof(T).Name, nameof(ToString));
                    Func<T, String?, String> @delegate = method.CreateTargetDelegate<T, Func<T, String?, String>>();

                    String? Format(T value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
                    {
                        const String prefix = "enum(";
                        String suffix = $":{typeof(T).Name})";

                        String @string = @delegate.Invoke(value, "D");
                        if (buffer.Length >= prefix.Length + suffix.Length)
                        {
                            written = @string.Length;
                            if (@string.TryCopyTo(buffer[prefix.Length..^suffix.Length]))
                            {
                                prefix.CopyTo(buffer);
                                suffix.CopyTo(buffer[(prefix.Length + written)..]);
                                written += prefix.Length + suffix.Length;
                                return null;
                            }
                        }

                        written = 0;
                        return prefix + @string + suffix;
                    }

                    System.Type underlying = System.Enum.GetUnderlyingType(typeof(T));

                    if (underlying == typeof(SByte))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!SByte.TryParse(buffer, NumberStyles.Integer, provider, out SByte value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<SByte, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Byte))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!Byte.TryParse(buffer, NumberStyles.Integer, provider, out Byte value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<Byte, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int16))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!Int16.TryParse(buffer, NumberStyles.Integer, provider, out Int16 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<Int16, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt16))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!UInt16.TryParse(buffer, NumberStyles.Integer, provider, out UInt16 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<UInt16, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int32))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!Int32.TryParse(buffer, NumberStyles.Integer, provider, out Int32 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<Int32, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt32))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!UInt32.TryParse(buffer, NumberStyles.Integer, provider, out UInt32 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<UInt32, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int64))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!Int64.TryParse(buffer, NumberStyles.Integer, provider, out Int64 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<Int64, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt64))
                    {
                        static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);
                            if (!UInt64.TryParse(buffer, NumberStyles.Integer, provider, out UInt64 value))
                            {
                                return null;
                            }

                            result = System.Runtime.CompilerServices.Unsafe.As<UInt64, T>(ref value);
                            return length;
                        }

                        AllowStorage.TryAdd(typeof(T).Name, typeof(T));
                        return (Format, Parser);
                    }

                    static Int32? NoParser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                    {
                        result = default!;
                        return null;
                    }

                    return (Format, NoParser);
                }

                static (FormatHandler, ParseHandler) Interface()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                    MethodInfo method = typeof(T).GetProperty(nameof(IEnum.Underlying), binding, null, typeof(System.Type), System.Type.EmptyTypes, null)?.GetMethod ?? throw new MissingMethodException(typeof(T).Name, nameof(IEnum.Underlying));
                    Func<T, System.Type> target = method.CreateTargetDelegate<T, Func<T, System.Type>>();

                    method = typeof(T).GetMethod(nameof(ToString), binding, new [] { typeof(String), typeof(IFormatProvider) }) ?? throw new MissingMethodException(typeof(T).Name, nameof(ToString));
                    Func<T, String?, IFormatProvider?, String> @delegate = method.CreateTargetDelegate<T, Func<T, String?, IFormatProvider?, String>>();

                    String? Format(T value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
                    {
                        const String prefix = "enum(";
                        String suffix = $":{target.Invoke(value).Name})";

                        String @string = @delegate.Invoke(value, "D", provider);
                        if (buffer.Length >= prefix.Length + suffix.Length)
                        {
                            written = @string.Length;
                            if (@string.TryCopyTo(buffer[prefix.Length..^suffix.Length]))
                            {
                                prefix.CopyTo(buffer);
                                suffix.CopyTo(buffer[(prefix.Length + written)..]);
                                written += prefix.Length + suffix.Length;
                                return null;
                            }
                        }

                        written = 0;
                        return prefix + @string + suffix;
                    }

                    System.Type? generic;
                    System.Type? underlying = null;
                    System.Type? @enum = null;
                    System.Type? self = null;

                    if (typeof(T) is { IsGenericType: true, IsInterface: true })
                    {
                        generic = typeof(T).GetGenericTypeDefinition();
                        if (generic == typeof(IEnum<,>))
                        {
                            System.Type[] arguments = typeof(T).GetGenericArguments();
                            (underlying, @enum, self) = (System.Enum.GetUnderlyingType(arguments[0]), arguments[0], arguments[1]);
                            goto verify;
                        }

                        if (generic == typeof(IEnum<>))
                        {
                            System.Type[] arguments = typeof(T).GetGenericArguments();
                            (underlying, @enum) = (System.Enum.GetUnderlyingType(arguments[0]), arguments[0]);
                            goto verify;
                        }
                    }

                    foreach (System.Type candidate in typeof(T).GetSafeInterfacesUnsafe())
                    {
                        if (!candidate.IsGenericType)
                        {
                            continue;
                        }

                        generic = candidate.GetGenericTypeDefinition();
                        if (generic == typeof(IEnum<,>))
                        {
                            System.Type[] arguments = candidate.GetGenericArguments();

                            if (@enum is not null && arguments[0] != @enum || self is not null && arguments[1] != self)
                            {
                                underlying = @enum = self = null;
                                break;
                            }

                            (underlying, @enum, self) = (System.Enum.GetUnderlyingType(arguments[0]), arguments[0], arguments[1]);
                        }
                        else if (generic == typeof(IEnum<>))
                        {
                            System.Type[] arguments = candidate.GetGenericArguments();

                            if (@enum is not null && arguments[0] != @enum)
                            {
                                underlying = @enum = self = null;
                                break;
                            }

                            (underlying, @enum) = (System.Enum.GetUnderlyingType(arguments[0]), arguments[0]);
                        }
                    }

                    verify:
                    if (underlying is null || @enum is null)
                    {
                        return (Format, NoParser);
                    }

                    try
                    {
                        generic = self is not null ? typeof(Enum<,>).MakeGenericType(@enum, self) : typeof(Enum<>).MakeGenericType(@enum);
                    }
                    catch (ArgumentException)
                    {
                        return (Format, NoParser);
                    }

                    MethodInfo @operator = generic.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public, new[] { @enum }) ?? throw new NeverOperationException();

                    if (underlying == typeof(SByte))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(SByte), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<SByte, T> invoke = Expression.Lambda<Func<SByte, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!SByte.TryParse(buffer, NumberStyles.Integer, provider, out SByte value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Byte))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(Byte), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<Byte, T> invoke = Expression.Lambda<Func<Byte, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!Byte.TryParse(buffer, NumberStyles.Integer, provider, out Byte value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int16))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(Int16), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<Int16, T> invoke = Expression.Lambda<Func<Int16, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!Int16.TryParse(buffer, NumberStyles.Integer, provider, out Int16 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt16))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(UInt16), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<UInt16, T> invoke = Expression.Lambda<Func<UInt16, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!UInt16.TryParse(buffer, NumberStyles.Integer, provider, out UInt16 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int32))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(Int32), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<Int32, T> invoke = Expression.Lambda<Func<Int32, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!Int32.TryParse(buffer, NumberStyles.Integer, provider, out Int32 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt32))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(UInt32), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<UInt32, T> invoke = Expression.Lambda<Func<UInt32, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!UInt32.TryParse(buffer, NumberStyles.Integer, provider, out UInt32 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(Int64))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(Int64), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<Int64, T> invoke = Expression.Lambda<Func<Int64, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!Int64.TryParse(buffer, NumberStyles.Integer, provider, out Int64 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    if (underlying == typeof(UInt64))
                    {
                        ParameterExpression value = Expression.Parameter(typeof(UInt64), nameof(value));
                        UnaryExpression convert = Expression.Convert(value, @enum);
                        MethodCallExpression call = Expression.Call(null, @operator, convert);
                        Func<UInt64, T> invoke = Expression.Lambda<Func<UInt64, T>>(Expression.Convert(call, typeof(T)), value).Compile();

                        Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                        {
                            result = default!;
                            buffer = Trim(Trim(buffer, ')', out Int32 length), ':', out _);

                            if (!UInt64.TryParse(buffer, NumberStyles.Integer, provider, out UInt64 value))
                            {
                                return null;
                            }

                            result = invoke(value);
                            return length;
                        }

                        AllowStorage.TryAdd(@enum.Name, @enum);
                        return (Format, Parser);
                    }

                    static Int32? NoParser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                    {
                        result = default!;
                        return null;
                    }

                    return (Format, NoParser);
                }

                static (FormatHandler, ParseHandler) Default()
                {
                    static String? Format(T value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
                    {
                        return StringUtilities.TryFormat(value, buffer, out written, format, provider ?? CultureInfo.InvariantCulture);
                    }

                    static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out T result)
                    {
                        result = default!;
                        return null;
                    }

                    return (Format, Parser);
                }

                (formatter, parser) = typeof(T).IsEnum ? Factory() : typeof(T).HasInterface(typeof(IEnum)) ? Interface() : Default();
            }

            private FormatHandler? _formatter;
            public FormatHandler Formatter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _formatter ?? formatter;
                }
                set
                {
                    _formatter = value;
                }
            }

            public sealed override System.Type Underlying
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return typeof(T);
                }
            }

            public T Item { get; }

            public sealed override Object Value
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Item;
                }
            }

            protected internal Element(T value)
            {
                Item = value ?? throw new ArgumentNullException(nameof(value));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean Allow()
            {
                return true;
            }

            public override Boolean TryFormat(Span<Char> destination, out Int32 written, String? format = null, IFormatProvider? provider = null)
            {
                return Formatter(Item, destination, format ?? Format, provider, out written) is null;
            }

            public override String ToString(String? format, IFormatProvider? provider)
            {
                return Formatter(Item, default, format ?? Format, provider, out _) ?? String.Empty;
            }
        }

        [Serializable]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public abstract record Element : ISpanFormattable
        {
            public enum Type : Byte
            {
                Unknown,
                String,
                Enum,
                Boolean,
                Char,
#if NETCOREAPP3_1_OR_GREATER
                Rune,
#endif
                Char32,
                IntPtr,
                UIntPtr,
                SByte,
                Byte,
                Int16,
                UInt16,
                Int32,
                UInt32,
                Int64,
                UInt64,
#if NET7_0_OR_GREATER
                Int128,
                UInt128,
#endif
                Half,
                Single,
                Double,
                Decimal,
                BigInteger,
                Guid,
                TimeSpan,
#if NET6_0_OR_GREATER
                TimeOnly,
                DateOnly,
#endif
                DateTime,
                DateTimeOffset
            }

            internal delegate Boolean EnumHandler(RayIdPayload payload, String key, ReadOnlySpan<Char> element, IFormatProvider? provider);
            public delegate System.Type? EnumSelectorHandler(ReadOnlySpan<Char> buffer);
            public delegate Type SelectorHandler(ReadOnlySpan<Char> buffer, out ReadOnlySpan<Char> element, out System.Type? complex);

            private static StringPool<Element> Pool { get; } = new StringPool<Element>(32);
            private static ConcurrentDictionary<System.Type, EnumHandler> Storage { get; } = new ConcurrentDictionary<System.Type, EnumHandler>();
            protected static ConcurrentDictionary<String, System.Type> AllowStorage { get; } = new ConcurrentDictionary<String, System.Type>();

            private static SelectorHandler selector = Strategy;
            public static SelectorHandler Selector
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return selector;
                }
                set
                {
                    selector = value ?? throw new ArgumentNullException(nameof(value));
                }
            }

            private static EnumSelectorHandler @enum = EnumStrategy;
            public static EnumSelectorHandler EnumSelector
            {
                get
                {
                    return @enum;
                }
                set
                {
                    @enum = value ?? throw new ArgumentNullException(nameof(value));
                }
            }

            static Element()
            {
                Element<Boolean>.Default = (Formatter, Parser);

                Element<Char>.Default = (Formatter, Parser);
                Element<Rune>.Default = (Formatter, Parser);
                Element<Char32>.Default = (Formatter, Parser);

                Element<IntPtr>.Default = (Formatter, Parser);
                Element<UIntPtr>.Default = (Formatter, Parser);
                Element<SByte>.Default = (Formatter, Parser);
                Element<Byte>.Default = (Formatter, Parser);
                Element<Int16>.Default = (Formatter, Parser);
                Element<UInt16>.Default = (Formatter, Parser);
                Element<Int32>.Default = (Formatter, Parser);
                Element<UInt32>.Default = (Formatter, Parser);
                Element<Int64>.Default = (Formatter, Parser);
                Element<UInt64>.Default = (Formatter, Parser);

#if NET7_0_OR_GREATER
                Element<Int128>.Default = (Formatter, Parser);
                Element<UInt128>.Default = (Formatter, Parser);
#endif
                Element<Half>.Default = (Formatter, Parser);
                Element<Single>.Default = (Formatter, Parser);
                Element<Double>.Default = (Formatter, Parser);
                Element<Decimal>.Default = (Formatter, Parser);
                Element<BigInteger>.Default = (Formatter, Parser);

                Element<Guid>.Default = (Formatter, Parser);
                Element<TimeSpan>.Default = (Formatter, Parser);
#if NET6_0_OR_GREATER
                Element<TimeOnly>.Default = (Formatter, Parser);
                Element<DateOnly>.Default = (Formatter, Parser);
#endif
                Element<DateTime>.Default = (Formatter, Parser);
                Element<DateTimeOffset>.Default = (Formatter, Parser);
            }

            public abstract System.Type Underlying { get; }
            public abstract Object Value { get; }
            internal Int32 Version { get; set; }
            public String? Format { get; init; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Boolean Allow<T>() where T : notnull
            {
                return Element<T>.Allow();
            }

            public virtual Boolean TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider = null)
            {
                return TryFormat(destination, out written, format.IsEmpty ? Format : format.ToString(), provider);
            }

            public abstract Boolean TryFormat(Span<Char> destination, out Int32 written, String? format = null, IFormatProvider? provider = null);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public sealed override String ToString()
            {
                return ToString(null, null);
            }

            public abstract String ToString(String? format, IFormatProvider? provider);

            internal static EnumHandler? Enum(System.Type? underlying)
            {
                if (underlying is not { IsEnum: true })
                {
                    return null;
                }

                return Storage.GetOrAdd(underlying, static underlying =>
                {
                    ParameterExpression payload = Expression.Parameter(typeof(RayIdPayload), nameof(payload));
                    ParameterExpression key = Expression.Parameter(typeof(String), nameof(key));
                    ParameterExpression element = Expression.Parameter(typeof(ReadOnlySpan<Char>), nameof(element));
                    ParameterExpression provider = Expression.Parameter(typeof(IFormatProvider), nameof(provider));

                    MethodInfo get = typeof(Element<>).MakeGenericType(underlying).GetProperty(nameof(Element<Any.Value>.DefaultParser), BindingFlags.Static | BindingFlags.Public)?.GetMethod ?? throw new NeverOperationException();
                    MethodInfo method = typeof(RayIdPayload).GetMethod(nameof(AddCore), BindingFlags.Instance | BindingFlags.NonPublic, new[] { typeof(String), System.Type.MakeGenericMethodParameter(0) })?.MakeGenericMethod(underlying) ?? throw new NeverOperationException();
                    Expression parser = Expression.Property(null, get);

                    ParameterExpression @return = Expression.Variable(underlying, nameof(@return));
                    ParameterExpression result = Expression.Variable(typeof(Int32?), nameof(result));

                    Expression body = Expression.Block(new[] { @return, result },
                        Expression.Assign(result, Expression.Invoke(parser, element, provider, @return)),
                        Expression.Condition(
                            Expression.NotEqual(result, Expression.Constant(null, typeof(Int32?))),
                            Expression.Block(Expression.Call(payload, method, key, @return), Expression.Constant(true)),
                            Expression.Constant(false)
                        )
                    );

                    return Expression.Lambda<EnumHandler>(body, payload, key, element, provider).Compile();
                });
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            protected static ReadOnlySpan<Char> Trim(ReadOnlySpan<Char> buffer, Char separator, out Int32 length)
            {
                length = 0;
                if (buffer.IsEmpty)
                {
                    return ReadOnlySpan<Char>.Empty;
                }

                Int32 index = MemoryExtensions.IndexOf(buffer, separator);
                length = index >= 0 ? index : buffer.Length;

                buffer = buffer.Slice(0, length);

                if (buffer.IsEmpty)
                {
                    return ReadOnlySpan<Char>.Empty;
                }

                Int32 start = 0;
                while (start < buffer.Length && Char.IsWhiteSpace(buffer[start]))
                {
                    start++;
                }

                Int32 end = buffer.Length;
                while (end > start && Char.IsWhiteSpace(buffer[end - 1]))
                {
                    end--;
                }

                Int32 count = end - start;
                return count > 0 ? buffer.Slice(start, count) : ReadOnlySpan<Char>.Empty;
            }

            private static String? Formatter(Boolean value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                (String @true, String @false) = format switch
                {
                    "b" or "B" => ("1", "0"),
                    "d" => ("t", "f"),
                    "D" => ("T", "F"),
                    "y" => ("y", "n"),
                    "Y" => ("Y", "N"),
                    "u" or "U" => ("TRUE", "FALSE"),
                    "!" => ("True", "False"),
                    "w" => ("yes", "no"),
                    "W" => ("YES", "NO"),
                    "w!" or "W!" => ("Yes", "No"),
                    "s" => ("on", "off"),
                    "S" => ("ON", "OFF"),
                    "s!" or "S!" => ("On", "Off"),
                    _ => ("true", "false")
                };

                ReadOnlySpan<Char> text = value ? @true : @false;

                if (!buffer.IsEmpty)
                {
                    if (buffer.Length < text.Length)
                    {
                        written = 0;
                        return value ? @true : @false;
                    }

                    text.CopyTo(buffer);
                    written = text.Length;
                    return null;
                }

                written = 0;
                return value ? @true : @false;
            }

            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Boolean result)
            {
                result = default;
                if (buffer.IsEmpty)
                {
                    return null;
                }

                buffer = Trim(buffer, ',', out Int32 length);
                Boolean? @return = buffer.Length switch
                {
                    1 => buffer[0] switch
                    {
                        '1' or 't' or 'T' or 'y' or 'Y' => true,
                        '0' or 'f' or 'F' or 'n' or 'N' => false,
                        _ => null
                    },
                    2 => buffer[0] switch
                    {
                        'o' or 'O' when buffer[1] is 'n' or 'N' => true,
                        'n' or 'N' when buffer[1] is 'o' or 'O' => false,
                        _ => null
                    },
                    3 => buffer[0] switch
                    {
                        'y' or 'Y' when buffer[1] is 'e' or 'E' && buffer[2] is 's' or 'S' => true,
                        'o' or 'O' when buffer[1] is 'f' or 'F' && buffer[2] is 'f' or 'F' => false,
                        _ => null
                    },
                    4 => buffer[0] switch
                    {
                        't' or 'T' when buffer[1] is 'r' or 'R' && buffer[2] is 'u' or 'U' && buffer[3] is 'e' or 'E' => true,
                        _ => null
                    },
                    5 => buffer[0] switch
                    {
                        'f' or 'F' when buffer[1] is 'a' or 'A' && buffer[2] is 'l' or 'L' && buffer[3] is 's' or 'S' && buffer[4] is 'e' or 'E' => false,
                        _ => null
                    },
                    _ => null
                };

                if (!@return.HasValue)
                {
                    return null;
                }

                result = @return.Value;
                return length;
            }

            private static String? Formatter(Char value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                value = format switch
                {
                    { Length: 1 } when Char.IsUpper(format[0]) => value.ToUpperInvariant(),
                    { Length: 1 } when Char.IsLower(format[0]) => value.ToLowerInvariant(),
                    _ => value
                };

                return Formatter((UInt16) value, buffer, "c(", ")", "X", provider ?? CultureInfo.InvariantCulture, out written);
            }

            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Char result)
            {
                buffer = Trim(buffer, ')', out Int32 length);

                if (buffer.Length >= 2 && buffer[0] == '0' && buffer[1] == 'x')
                {
                    if (UInt16.TryParse(buffer, NumberStyles.Integer | NumberStyles.AllowHexSpecifier, provider ?? CultureInfo.InvariantCulture, out UInt16 value))
                    {
                        result = (Char) value;
                        return length;
                    }
                }
                else
                {
                    Span<Char> hex = stackalloc Char[buffer.Length + 2];
                    (hex[0], hex[1]) = ('0', 'x');
                    buffer.CopyTo(hex.Slice(2));

                    if (UInt16.TryParse(hex, NumberStyles.Integer | NumberStyles.AllowHexSpecifier, provider ?? CultureInfo.InvariantCulture, out UInt16 value))
                    {
                        result = (Char) value;
                        return length;
                    }
                }

                result = default;
                return null;
            }

#if NETCOREAPP3_1_OR_GREATER
            private static String? Formatter(Rune value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                value = format switch
                {
                    { Length: 1 } when Char.IsUpper(format[0]) => value.ToUpperInvariant(),
                    { Length: 1 } when Char.IsLower(format[0]) => value.ToLowerInvariant(),
                    _ => value
                };

                return Formatter(unchecked((UInt32) value.Value), buffer, "rune(", ")", "X", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Rune result)
            {
                Int32? length = Parser(buffer, provider, out Char32 value);
                result = value;
                return length;
            }
#endif
            private static String? Formatter(Char32 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                value = format switch
                {
                    { Length: 1 } when Char.IsUpper(format[0]) => value.ToUpperInvariant(),
                    { Length: 1 } when Char.IsLower(format[0]) => value.ToLowerInvariant(),
                    _ => value
                };

                return Formatter((UInt32) value, buffer, "c+(", ")", "X", provider ?? CultureInfo.InvariantCulture, out written);
            }

            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Char32 result)
            {
                buffer = Trim(buffer, ')', out Int32 length);

                if (buffer.Length >= 2 && buffer[0] == '0' && buffer[1] == 'x')
                {
                    if (UInt32.TryParse(buffer, NumberStyles.Integer | NumberStyles.AllowHexSpecifier, provider ?? CultureInfo.InvariantCulture, out UInt32 value))
                    {
                        result = value;
                        return length;
                    }
                }
                else
                {
                    Span<Char> hex = stackalloc Char[buffer.Length + 2];
                    (hex[0], hex[1]) = ('0', 'x');
                    buffer.CopyTo(hex.Slice(2));

                    if (UInt32.TryParse(hex, NumberStyles.Integer | NumberStyles.AllowHexSpecifier, provider ?? CultureInfo.InvariantCulture, out UInt32 value))
                    {
                        result = value;
                        return length;
                    }
                }

                result = default;
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static String? Formatter<T>(T value, Span<Char> buffer, String prefix, String suffix, String format, IFormatProvider provider, out Int32 written) where T : ISpanFormattable
            {
                if (buffer.Length >= prefix.Length + suffix.Length && value.TryFormat(buffer[prefix.Length..^suffix.Length], out written, format, provider))
                {
                    prefix.CopyTo(buffer);
                    suffix.CopyTo(buffer[(prefix.Length + written)..]);
                    written += prefix.Length + suffix.Length;
                    return null;
                }

                written = 0;
                return prefix + value.ToString(format, provider) + suffix;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(IntPtr value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "sz(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out IntPtr result)
            {
                return IntPtr.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(UIntPtr value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "usz(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out UIntPtr result)
            {
                return UIntPtr.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(SByte value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i1(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out SByte result)
            {
                return SByte.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Byte value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "u1(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Byte result)
            {
                return Byte.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Int16 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i2(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Int16 result)
            {
                return Int16.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(UInt16 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "u2(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out UInt16 result)
            {
                return UInt16.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Int32 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i4(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Int32 result)
            {
                return Int32.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(UInt32 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "u4(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out UInt32 result)
            {
                return UInt32.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Int64 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i8(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Int64 result)
            {
                return Int64.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(UInt64 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "u8(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out UInt64 result)
            {
                return UInt64.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

    #if NET7_0_OR_GREATER
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Int128 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i16(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Int128 result)
            {
                return Int128.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(UInt128 value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "u16(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out UInt128 result)
            {
                return UInt128.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }
    #endif
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Half value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "f2(", ")", format ?? "R", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Half result)
            {
                return Half.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Float | NumberStyles.AllowThousands, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Single value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "f4(", ")", format ?? "R", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Single result)
            {
                return Single.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Float | NumberStyles.AllowThousands, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Double value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "f8(", ")", format ?? "R", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Double result)
            {
                return Double.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Float | NumberStyles.AllowThousands, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Decimal value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "f(", ")", format ?? "G29", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Decimal result)
            {
                return Decimal.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Number, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(BigInteger value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "i(", ")", format ?? "D", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out BigInteger result)
            {
                return BigInteger.TryParse(Trim(buffer, ')', out Int32 length), NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(Guid value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                format ??= "D";
                provider ??= CultureInfo.InvariantCulture;

                if (!buffer.IsEmpty)
                {
                    return value.TryFormat(buffer, out written, format) ? null : value.ToString(format, provider);
                }

                written = 0;
                return value.ToString(format, provider);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out Guid result)
            {
                return Guid.TryParse(Trim(buffer, ')', out Int32 length), out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(TimeSpan value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "ts(", ")", format ?? "c", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out TimeSpan result)
            {
                return TimeSpan.TryParse(Trim(buffer, ')', out Int32 length), provider ?? CultureInfo.InvariantCulture, out result) ? length : null;
            }

#if NET6_0_OR_GREATER
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(TimeOnly value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "to(", ")", format ?? "O", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out TimeOnly result)
            {
                return TimeOnly.TryParse(Trim(buffer, ')', out Int32 length), provider ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(DateOnly value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "do(", ")", format ?? "O", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out DateOnly result)
            {
                return DateOnly.TryParse(Trim(buffer, ')', out Int32 length), provider ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? length : null;
            }
#endif
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(DateTime value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "dt(", ")", format ?? "O", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out DateTime result)
            {
                return DateTime.TryParse(Trim(buffer, ')', out Int32 length), provider ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static String? Formatter(DateTimeOffset value, Span<Char> buffer, String? format, IFormatProvider? provider, out Int32 written)
            {
                return Formatter(value, buffer, "dto(", ")", format ?? "O", provider ?? CultureInfo.InvariantCulture, out written);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Int32? Parser(ReadOnlySpan<Char> buffer, IFormatProvider? provider, out DateTimeOffset result)
            {
                return DateTimeOffset.TryParse(Trim(buffer, ')', out Int32 length), provider ?? CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? length : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Type Prefix(ReadOnlySpan<Char> prefix)
            {
                return prefix.Length switch
                {
                    0 => Type.String,
                    1 => prefix[0] switch
                    {
                        'c' or 'C' => Type.Char,
                        'f' or 'F' => Type.Decimal,
                        'i' or 'I' => Type.BigInteger,
                        _ => Type.String
                    },
                    2 => prefix[0] switch
                    {
                        'c' or 'C' when prefix[1] == '+' => Type.Char32,
                        's' or 'S' when prefix[1] is 'z' or 'Z' => Type.IntPtr,
                        'i' or 'I' => prefix[1] switch
                        {
                            '1' => Type.SByte,
                            '2' => Type.Int16,
                            '4' => Type.Int32,
                            '8' => Type.Int64,
                            'd' or 'D' => Type.Guid,
                            _ => Type.String
                        },
                        'u' or 'U' => prefix[1] switch
                        {
                            '1' => Type.Byte,
                            '2' => Type.UInt16,
                            '4' => Type.UInt32,
                            '8' => Type.UInt64,
                            _ => Type.String
                        },
                        'f' or 'F' => prefix[1] switch
                        {
                            '2' => Type.Half,
                            '4' => Type.Single,
                            '8' => Type.Double,
                            _ => Type.String
                        },
                        't' or 'T' => prefix[1] switch
                        {
                            's' or 'S' => Type.TimeSpan,
#if NET6_0_OR_GREATER
                            'o' or 'O' => Type.TimeOnly,
#endif
                            _ => Type.String
                        },
                        'd' or 'D' => prefix[1] switch
                        {
                            't' or 'T' => Type.DateTime,
#if NET6_0_OR_GREATER
                            'o' or 'O' => Type.DateOnly,
#endif
                            _ => Type.String
                        },
                        _ => Type.String
                    },
                    3 => prefix[0] switch
                    {
                        'u' or 'U' when prefix[1] is 's' or 'S' && prefix[2] is 'z' or 'Z' => Type.UIntPtr,
#if NET7_0_OR_GREATER
                        'i' or 'I' when prefix[1] == '1' && prefix[2] == '6' => Type.Int128,
                        'u' or 'U' when prefix[1] == '1' && prefix[2] == '6' => Type.UInt128,
#endif
                        'd' or 'D' when prefix[1] is 't' or 'T' && prefix[2] is 'o' or 'O' => Type.DateTimeOffset,
                        _ => Type.String
                    },
                    4 => prefix[0] switch
                    {
                        'e' or 'E' when prefix[1] is 'n' or 'N' && prefix[2] is 'u' or 'U' && prefix[3] is 'm' or 'M' => Type.Enum,
#if NETCOREAPP3_1_OR_GREATER
                        'r' or 'R' when prefix[1] is 'u' or 'U' && prefix[2] is 'n' or 'N' && prefix[3] is 'e' or 'E' => Type.Rune,
#endif
                        _ => Type.String
                    },
                    _ => Type.String
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static System.Type? EnumStrategy(ReadOnlySpan<Char> buffer)
            {
                System.Type? result;
                if (Pool.Get(buffer, out String? key))
                {
                    return AllowStorage.TryGetValue(key, out result) ? result : null;
                }

                key = buffer.ToString();
                if (!AllowStorage.TryGetValue(key, out result))
                {
                    return null;
                }

                Pool.Add(key);
                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
            private static Type Strategy(ReadOnlySpan<Char> buffer, out ReadOnlySpan<Char> element, out System.Type? complex)
            {
                complex = null;
                buffer = Trim(buffer, ',', out _);

                if (buffer.IsEmpty)
                {
                    element = buffer;
                    return Type.Unknown;
                }

                Int32 start = MemoryExtensions.IndexOf(buffer, '(');
                if (start < 0)
                {
                    element = buffer;
                    return Element<Boolean>.DefaultParser(buffer, null, out _) is not null ? Type.Boolean : Element<Guid>.DefaultParser(buffer, null, out _) is not null ? Type.Guid : Type.String;
                }

                Type prefix = Prefix(buffer.Slice(0, start));
                element = Trim(buffer.Slice(start + 1), ')', out Int32 length);
                Int32 end = start + length + 1;

                if (end < buffer.Length - 1 || buffer[end] != ')')
                {
                    element = buffer;
                    return Type.String;
                }

                if (prefix is not Type.Enum)
                {
                    return prefix;
                }

                Int32 separator = MemoryExtensions.IndexOf(element, ':');

                if (separator < 0)
                {
                    return prefix;
                }

                if (EnumSelector(element.Slice(separator + 1)) is { IsEnum: true } @enum)
                {
                    complex = @enum;
                    element = element.Slice(0, separator);
                    return Type.Enum;
                }

                element = buffer;
                return Type.String;
            }
        }
    }
}