// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public abstract class ReflectionOperator : IReflectionOperator
    {
        public abstract String Name { get; }
        public MethodInfo? Method { get; }
        
        protected ReflectionOperator()
        {
        }
        
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
        public static IUnaryReflectionOperator<TSelf, TSelf>? Get<TSelf>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Get<TSelf>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator<TSelf, TResult>? Get<TSelf, TResult>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Get<TSelf, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator? Get(Type first, Type second, BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get(first, second, @operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TSelf, TSelf, TSelf>? Get<TSelf>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<TSelf>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TSelf, TSelf, TResult>? Get<TSelf, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<TSelf, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TFirst, TSecond, TResult>? Get<TFirst, TSecond, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Get<TFirst, TSecond, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator<TSelf, TSelf> Exception<TSelf>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Exception<TSelf>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUnaryReflectionOperator<TSelf, TResult> Exception<TSelf, TResult>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator.Exception<TSelf, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TSelf, TSelf, TSelf> Exception<TSelf>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Exception<TSelf>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TSelf, TSelf, TResult> Exception<TSelf, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Exception<TSelf, TResult>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IBinaryReflectionOperator<TFirst, TSecond, TResult> Exception<TFirst, TSecond, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator.Exception<TFirst, TSecond, TResult>(@operator);
        }
    }

    public abstract class ReflectionOperatorNotImplementedException : NotImplementedReflectionException
    {
        private new const String Message = "Reflection operator not implemented.";
        private const String FormatMessage = "Reflection operator '{0}' not implemented.";
        
        public abstract String Identifier { get; }

        protected ReflectionOperatorNotImplementedException(String? identifier)
            : base(Format(identifier))
        {
        }

        protected ReflectionOperatorNotImplementedException(String? identifier, Exception? exception)
            : base(Format(identifier), exception)
        {
        }

        protected ReflectionOperatorNotImplementedException(String? identifier, String? message)
            : base(Format(identifier, message))
        {
        }

        protected ReflectionOperatorNotImplementedException(String? identifier, String? message, Exception? exception)
            : base(Format(identifier, message), exception)
        {
        }
        
        protected ReflectionOperatorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String Format(String? identifier)
        {
            return identifier is not null ? String.Format(FormatMessage, identifier) : Message;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static String Format(String? identifier, String? message)
        {
            return message ?? Format(identifier);
        }
    }
}