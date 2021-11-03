// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender
{
    public delegate void EmptyHandler();

    public delegate void FuncHandler<out T, in TOutput>(Func<T, TOutput> function);

    public delegate TOutput ParseHandler<in T, out TOutput>(T value);
    
    public delegate TOutput ParseHandler<in T, in THelper, out TOutput>(T value, THelper helper);
    
    public delegate Boolean TryParseHandler<in T, TOutput>(T value, [MaybeNullWhen(false)] out TOutput result);
    
    public delegate Boolean TryParseHandler<in T, in THelper, TOutput>(T value, THelper helper, [MaybeNullWhen(false)] out TOutput result);

    public delegate Boolean TryConverter<in TInput, TOutput>(TInput value, [MaybeNullWhen(false)] out TOutput result);
    
    public delegate void TypeHandler<in T>(T type);
}