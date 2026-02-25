using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class ArgumentAddingDuplicateWithKeyException<T, TSR> : ArgumentAddingDuplicateWithKeyException<T>, ISRException<TSR, ArgumentAddingDuplicateWithKeyException<T, TSR>.Argument_AddingDuplicateWithKey>
    {
        public ArgumentAddingDuplicateWithKeyException(T key)
            : base(key, ISRExceptionIdentifier<Argument_AddingDuplicateWithKey>.Instance.Resource.Format((Object?) key ?? StringUtilities.NullString))
        {
        }

        public ArgumentAddingDuplicateWithKeyException(T key, Exception? exception)
            : base(key, ISRExceptionIdentifier<Argument_AddingDuplicateWithKey>.Instance.Resource.Format((Object?) key ?? StringUtilities.NullString), exception)
        {
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? parameter)
            : base(key, ISRExceptionIdentifier<Argument_AddingDuplicateWithKey>.Instance.Resource.Format((Object?) key ?? StringUtilities.NullString), parameter)
        {
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? parameter, Exception? exception)
            : base(key, ISRExceptionIdentifier<Argument_AddingDuplicateWithKey>.Instance.Resource.Format((Object?) key ?? StringUtilities.NullString), parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentAddingDuplicateWithKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Argument_AddingDuplicateWithKey : ISRExceptionIdentifier<TSR, Argument_AddingDuplicateWithKey>
        {
        }
    }

    [Serializable]
    public class ArgumentAddingDuplicateWithKeyException<T> : ArgumentAddingDuplicateWithKeyException
    {
        public T Key { get; }

        public ArgumentAddingDuplicateWithKeyException(T key)
        {
            Key = key;
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? message)
            : base(message)
        {
            Key = key;
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? message, Exception? exception)
            : base(message, exception)
        {
            Key = key;
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? message, String? parameter)
            : base(message, parameter)
        {
            Key = key;
        }

        public ArgumentAddingDuplicateWithKeyException(T key, String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
            Key = key;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentAddingDuplicateWithKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Key = info.GetValue<T>(nameof(Key));
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Key), Key);
        }
    }

    [Serializable]
    public class ArgumentAddingDuplicateWithKeyException : ArgumentException
    {
        public ArgumentAddingDuplicateWithKeyException()
        {
        }

        public ArgumentAddingDuplicateWithKeyException(String? message)
            : base(message)
        {
        }

        public ArgumentAddingDuplicateWithKeyException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentAddingDuplicateWithKeyException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentAddingDuplicateWithKeyException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentAddingDuplicateWithKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}