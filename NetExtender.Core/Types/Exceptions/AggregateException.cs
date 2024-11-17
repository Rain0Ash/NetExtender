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

        public AggregateException(T result, params Exception[] innerExceptions)
            : base(result, innerExceptions)
        {
        }

        public AggregateException(T result, IEnumerable<Exception> innerExceptions)
            : base(result, innerExceptions)
        {
        }

        public AggregateException(T result, String? message)
            : base(result, message)
        {
        }

        public AggregateException(T result, String? message, Exception innerException)
            : base(result, message, innerException)
        {
        }

        public AggregateException(T result, String? message, params Exception[] innerExceptions)
            : base(result, message, innerExceptions)
        {
        }

        public AggregateException(T result, String? message, IEnumerable<Exception> innerExceptions)
            : base(result, message, innerExceptions)
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

        protected AggregateResultException(T result, params Exception[] innerExceptions)
            : base(innerExceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, IEnumerable<Exception> innerExceptions)
            : base(innerExceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message)
            : base(message)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, Exception innerException)
            : base(message, innerException)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, params Exception[] innerExceptions)
            : base(message, innerExceptions)
        {
            Result = result;
        }

        protected AggregateResultException(T result, String? message, IEnumerable<Exception> innerExceptions)
            : base(message, innerExceptions)
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

        protected AggregateResultException(params Exception[] innerExceptions)
            : base(innerExceptions)
        {
        }

        protected AggregateResultException(IEnumerable<Exception> innerExceptions)
            : base(innerExceptions)
        {
        }

        protected AggregateResultException(String? message)
            : base(message)
        {
        }

        protected AggregateResultException(String? message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AggregateResultException(String? message, params Exception[] innerExceptions)
            : base(message, innerExceptions)
        {
        }

        protected AggregateResultException(String? message, IEnumerable<Exception> innerExceptions)
            : base(message, innerExceptions)
        {
        }

        protected AggregateResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}