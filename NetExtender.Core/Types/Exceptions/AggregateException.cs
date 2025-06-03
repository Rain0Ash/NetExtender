// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class AggregateException<T> : AggregateResultException<T>
    {
        public new T Result
        {
            get
            {
                return base.Result.Value;
            }
        }

        public AggregateException(T result)
            : base(result)
        {
        }

        public AggregateException(T result, params Exception[] exceptions)
            : base(result, exceptions)
        {
        }

        public AggregateException(T result, IEnumerable<Exception> exceptions)
            : base(result, exceptions)
        {
        }

        public AggregateException(T result, String? message)
            : base(result, message)
        {
        }

        public AggregateException(T result, String? message, Exception exception)
            : base(result, message, exception)
        {
        }

        public AggregateException(T result, String? message, params Exception[] exceptions)
            : base(result, message, exceptions)
        {
        }

        public AggregateException(T result, String? message, IEnumerable<Exception> exceptions)
            : base(result, message, exceptions)
        {
        }

        protected AggregateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class AggregateResultException<T> : AggregateResultException
    {
        public sealed override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public sealed override Box<T> Result { get; }

        protected AggregateResultException(T result)
        {
            Result = result;
        }

        protected AggregateResultException(T result, params Exception[] exceptions)
            : base(exceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, IEnumerable<Exception> exceptions)
            : base(exceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message)
            : base(message)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, Exception exception)
            : base(message, exception)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, params Exception[] exceptions)
            : base(message, exceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, IEnumerable<Exception> exceptions)
            : base(message, exceptions)
        {
            Result = result;
        }

        protected AggregateResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Result = info.GetValue<T>(nameof(Result));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Result), Result.Value);
        }
    }

    [Serializable]
    public abstract class AggregateResultException : AggregateException
    {
        public virtual Type? Type
        {
            get
            {
                return Result?.GetType();
            }
        }

        public abstract Object? Result { get; }

        protected AggregateResultException()
        {
        }

        protected AggregateResultException(params Exception[] exceptions)
            : base(exceptions)
        {
        }

        protected AggregateResultException(IEnumerable<Exception> exceptions)
            : base(exceptions)
        {
        }

        protected AggregateResultException(String? message)
            : base(message)
        {
        }

        protected AggregateResultException(String? message, Exception exception)
            : base(message, exception)
        {
        }

        protected AggregateResultException(String? message, params Exception[] exceptions)
            : base(message, exceptions)
        {
        }

        protected AggregateResultException(String? message, IEnumerable<Exception> exceptions)
            : base(message, exceptions)
        {
        }

        protected AggregateResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}