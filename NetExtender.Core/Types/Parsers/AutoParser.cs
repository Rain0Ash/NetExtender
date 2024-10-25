using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Parsers.Interfaces;

namespace NetExtender.Types.Parsers
{
    public sealed class AutoParser<T, TResult> : RelayParser<T, TResult>
    {
        internal AutoParser()
        {
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            ParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(T) })?.CreateDelegate<ParseHandler<T, TResult>>();
            StyleParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(T), typeof(NumberStyles) })?.CreateDelegate<StyleParseHandler<T, TResult>>();
            FormatParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(T), typeof(IFormatProvider) })?.CreateDelegate<FormatParseHandler<T, TResult>>();
            NumericParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(T), typeof(NumberStyles), typeof(IFormatProvider) })?.CreateDelegate<NumericParseHandler<T, TResult>>();
            TryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(T), typeof(TResult).MakeByRefType() })?.CreateDelegate<TryParseHandler<T?, TResult>>();
            StyleTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(T), typeof(NumberStyles), typeof(TResult).MakeByRefType() })?.CreateDelegate<StyleTryParseHandler<T?, TResult>>();
            FormatTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(T), typeof(IFormatProvider), typeof(TResult).MakeByRefType() })?.CreateDelegate<FormatTryParseHandler<T?, TResult>>();
            NumericTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(T), typeof(NumberStyles), typeof(IFormatProvider), typeof(TResult).MakeByRefType() })?.CreateDelegate<NumericTryParseHandler<T?, TResult>>();
            
            if (GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).All(property => property.GetValue(this) is null))
            {
                throw new NotSupportedException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IParser<T, TResult>? Get()
        {
            return AutoParser.Get<T, TResult>();
        }
    }
    
    public sealed class AutoParser<T> : StringRelayParser<T>
    {
        internal AutoParser()
        {
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            ParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(String) })?.CreateDelegate<ParseHandler<String, T>>();
            ParseSpanHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(ReadOnlySpan<Char>) })?.CreateDelegate<ParseHandler<T>>();
            StyleParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(String), typeof(NumberStyles) })?.CreateDelegate<StyleParseHandler<String, T>>();
            StyleParseSpanHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(NumberStyles) })?.CreateDelegate<StyleParseHandler<T>>();
            FormatParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(String), typeof(IFormatProvider) })?.CreateDelegate<FormatParseHandler<String, T>>();
            FormatParseSpanHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(IFormatProvider) })?.CreateDelegate<FormatParseHandler<T>>();
            NumericParseHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(String), typeof(NumberStyles), typeof(IFormatProvider) })?.CreateDelegate<NumericParseHandler<String, T>>();
            NumericParseSpanHandler = typeof(T).GetMethod(nameof(Parse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(NumberStyles), typeof(IFormatProvider) })?.CreateDelegate<NumericParseHandler<T>>();
            TryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(String), typeof(T).MakeByRefType() })?.CreateDelegate<TryParseHandler<String?, T>>();
            TryParseSpanHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(T).MakeByRefType() })?.CreateDelegate<TryParseHandler<T>>();
            StyleTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(String), typeof(NumberStyles), typeof(T).MakeByRefType() })?.CreateDelegate<StyleTryParseHandler<String?, T>>();
            StyleTryParseSpanHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(NumberStyles), typeof(T).MakeByRefType() })?.CreateDelegate<StyleTryParseHandler<T>>();
            FormatTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(String), typeof(IFormatProvider), typeof(T).MakeByRefType() })?.CreateDelegate<FormatTryParseHandler<String?, T>>();
            FormatTryParseSpanHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(IFormatProvider), typeof(T).MakeByRefType() })?.CreateDelegate<FormatTryParseHandler<T>>();
            NumericTryParseHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(String), typeof(NumberStyles), typeof(IFormatProvider), typeof(T).MakeByRefType() })?.CreateDelegate<NumericTryParseHandler<String?, T>>();
            NumericTryParseSpanHandler = typeof(T).GetMethod(nameof(TryParse), binding, new [] { typeof(ReadOnlySpan<Char>), typeof(NumberStyles), typeof(IFormatProvider), typeof(T).MakeByRefType() })?.CreateDelegate<NumericTryParseHandler<T>>();
            
            if (GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).All(property => property.GetValue(this) is null))
            {
                throw new NotSupportedException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IStringParser<T>? Get()
        {
            return AutoParser.Get<T>();
        }
    }
    
    public static class AutoParser
    {
        private static ConcurrentDictionary<Type, IStringParser?> Storage { get; } = new ConcurrentDictionary<Type, IStringParser?>();
        private static ConcurrentDictionary<(Type, Type), IParser?> DynamicStorage { get; } = new ConcurrentDictionary<(Type, Type), IParser?>();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IStringParser? Get(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Storage.GetOrAdd(type, static type =>
            {
                try
                {
                    return (IStringParser?) Activator.CreateInstance(typeof(AutoParser<>).MakeGenericType(type));
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IStringParser<T>? Get<T>()
        {
            return (IStringParser<T>?) Storage.GetOrAdd(typeof(T), static _ =>
            {
                try
                {
                    return new AutoParser<T>();
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IParser? Get(Type type, Type result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            
            if (type == typeof(String))
            {
                return Get(result);
            }
            
            return DynamicStorage.GetOrAdd((type, result), static key =>
            {
                try
                {
                    (Type type, Type result) = key;
                    return (IStringParser?) Activator.CreateInstance(typeof(AutoParser<,>).MakeGenericType(type, result));
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IParser<T, TResult>? Get<T, TResult>()
        {
            if (typeof(T) == typeof(String))
            {
                return (IParser<T, TResult>?) Get<TResult>();
            }
            
            return (IParser<T, TResult>?) DynamicStorage.GetOrAdd((typeof(T), typeof(TResult)), static _ =>
            {
                try
                {
                    return new AutoParser<T, TResult>();
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }
    }
}