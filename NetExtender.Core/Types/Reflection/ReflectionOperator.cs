// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public abstract class ReflectionOperator : IReflectionOperator
    {
        public abstract String Name { get; }
        public MethodInfo? Method { get; }
        
        protected ReflectionOperator(MethodInfo? method)
        {
            Method = method;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator? Get(Type type, UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Get(type, @operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator<T, T>? Get<T>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Get<T>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator<T, TResult>? Get<T, TResult>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Get<T, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator? Get(Type first, Type second, BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get(first, second, @operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<T, T, T>? Get<T>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<T>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<T, T, TResult>? Get<T, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<T, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TFirst, TSecond, TResult>? Get<TFirst, TSecond, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<TFirst, TSecond, TResult>(@operator);
        }
    }
}