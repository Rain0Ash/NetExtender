// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IBinaryReflectionOperator<in TFirst, in TSecond, out TResult> : IBinaryReflectionOperator
    {
        public TResult Invoke(TFirst first, TSecond second);
    }

    public interface IBinaryReflectionOperator : IReflectionOperator
    {
        public BinaryOperator Operator { get; }

        public Object? Invoke(Object? first, Object? second);
    }

    public interface IUnaryReflectionOperator<in T, out TResult> : IUnaryReflectionOperator
    {
        public TResult Invoke(T value);
    }

    public interface IUnaryReflectionOperator : IReflectionOperator
    {
        public UnaryOperator Operator { get; }
        
        public Object? Invoke(Object? value);
    }

    public interface IReflectionOperator
    {
        public String Name { get; }
        public MethodInfo? Method { get; }
    }
}