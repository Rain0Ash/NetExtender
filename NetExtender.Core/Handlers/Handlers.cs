// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender
{
    public delegate void EmptyHandler();

    public delegate void FuncHandler<out T, in TOutput>(Func<T, TOutput> function);

    public delegate TOutput ParseHandler<in T, out TOutput>(T value);
    
    public delegate Boolean TryParseHandler<in T, TOutput>(T value, out TOutput result);

    public delegate Boolean TryConverter<in TInput, TOutput>(TInput value, out TOutput converted);
    
    public delegate void TypeHandler<in T>(T type);
}