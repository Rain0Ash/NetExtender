// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;

namespace NetExtender
{
    public delegate void EmptyHandler();
    public delegate void TypeHandler<in T>(T type);

    public delegate Int32 Comparison<in T1, in T2>(T1 x, T2 y);

    public delegate Boolean EqualityComparison<in T>(T x, T y);
    public delegate Boolean EqualityComparison<in T1, in T2>(T1 x, T2 y);

    public delegate Int32 HashHandler<in T>(T value);

    public delegate void FuncHandler<out T, in TResult>(Func<T, TResult> function);
    
    public delegate TResult UnaryOperatorHandler<in T, out TResult>(T value);
    public delegate TResult BinaryOperatorHandler<in TFirst, in TSecond, out TResult>(TFirst first, TSecond second);

    public delegate T ParseHandler<out T>(ReadOnlySpan<Char> value);
    public delegate TResult ParseHandler<in T, out TResult>(T value);
    public delegate TResult ParseHandler<in T, in THelper, out TResult>(T value, THelper helper);
    public delegate T StyleParseHandler<out T>(ReadOnlySpan<Char> value, NumberStyles style);
    public delegate TResult StyleParseHandler<in T, out TResult>(T value, NumberStyles style);
    public delegate T FormatParseHandler<out T>(ReadOnlySpan<Char> value, IFormatProvider? provider);
    public delegate TResult FormatParseHandler<in T, out TResult>(T value, IFormatProvider? provider);
    public delegate T NumericParseHandler<out T>(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider);
    public delegate TResult NumericParseHandler<in T, out TResult>(T value, NumberStyles style, IFormatProvider? provider);
    public delegate Boolean TryParseHandler<T>(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out T result);
    public delegate Boolean TryParseHandler<in T, TResult>(T value, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean TryParseHandler<in T, in THelper, TResult>(T value, THelper helper, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean StyleTryParseHandler<T>(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out T result);
    public delegate Boolean StyleTryParseHandler<in T, TResult>(T value, NumberStyles style, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean FormatTryParseHandler<T>(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out T result);
    public delegate Boolean FormatTryParseHandler<in T, TResult>(T value, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean NumericTryParseHandler<T>(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out T result);
    public delegate Boolean NumericTryParseHandler<in T, TResult>(T value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean TryConverter<in TSource, TResult>(TSource value, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean TryGetter<in TSource, TResult>(TSource value, [MaybeNullWhen(false)] out TResult result);
    public delegate Boolean TrySetter<in TSource, in TValue>(TSource value, TValue result);
    
    public delegate void SenderAction<in T>(Object? sender, T value);
    public delegate Boolean Predicate<in T1, in T2>(T1 first, T2 second);
    public delegate Boolean Predicate<in T1, in T2, in T3>(T1 first, T2 second, T3 third);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4>(T1 first, T2 second, T3 third, T4 fourth);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4, in T5>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4, in T5, in T6>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
    public delegate Boolean Predicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);
    public delegate Boolean SenderPredicate<in T>(Object? sender, T value);
    public delegate Boolean SenderPredicate<in T1, in T2>(Object? sender, T1 first, T2 second);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3>(Object? sender, T1 first, T2 second, T3 third);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4>(Object? sender, T1 first, T2 second, T3 third, T4 fourth);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4, in T5>(Object? sender, T1 first, T2 second, T3 third, T4 fourth, T5 fifth);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4, in T5, in T6>(Object? sender, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7>(Object? sender, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>(Object? sender, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth);
    public delegate Boolean SenderPredicate<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>(Object? sender, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth);

    
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public delegate void EventHandler<in TSender, TArgument>(TSender sender, TArgument argument);
    
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public delegate ValueTask EventAsyncHandler<TArgument>(Object? sender, TArgument argument);
    
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public delegate ValueTask EventAsyncHandler<in TSender, TArgument>(TSender sender, TArgument argument);
    
    public delegate Object? PlatformNotSupportedHandler(String method, Type? @return, PlatformNotSupportedException exception, params Object?[]? arguments);
    public delegate T PlatformNotSupportedHandler<out T>(String method, PlatformNotSupportedException exception, params Object?[]? arguments);
    public delegate T PlatformNotSupportedHandler<out T, in TArgument>(String method, PlatformNotSupportedException exception, TArgument argument);
}