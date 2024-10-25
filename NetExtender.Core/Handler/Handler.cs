// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

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
    public delegate Boolean SenderPredicate<in T>(Object? sender, T value);
}