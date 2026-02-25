using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class ArgumentMinMaxValueException<T, TSR> : ArgumentMinMaxValueException<T>, ISRException<TSR, ArgumentMinMaxValueException<T, TSR>.Argument_MinMaxValue>
    {
        public ArgumentMinMaxValueException(T minimum, T maximum)
            : base(minimum, maximum, ISRExceptionIdentifier<Argument_MinMaxValue>.Instance.Resource.Format((Object?) minimum ?? StringUtilities.NullString, (Object?) maximum ?? StringUtilities.NullString))
        {
        }

        public ArgumentMinMaxValueException(T minimum, T maximum, Exception? exception)
            : base(minimum, maximum, ISRExceptionIdentifier<Argument_MinMaxValue>.Instance.Resource.Format((Object?) minimum ?? StringUtilities.NullString, (Object?) maximum ?? StringUtilities.NullString), exception)
        {
        }

        public ArgumentMinMaxValueException(String? parameter, T minimum, T maximum)
            : base(parameter, minimum, maximum, ISRExceptionIdentifier<Argument_MinMaxValue>.Instance.Resource.Format((Object?) minimum ?? StringUtilities.NullString, (Object?) maximum ?? StringUtilities.NullString))
        {
        }

        public ArgumentMinMaxValueException(String? parameter, T minimum, T maximum, Exception? exception)
            : base(parameter, minimum, maximum, ISRExceptionIdentifier<Argument_MinMaxValue>.Instance.Resource.Format((Object?) minimum ?? StringUtilities.NullString, (Object?) maximum ?? StringUtilities.NullString), exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentMinMaxValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Argument_MinMaxValue : ISRExceptionIdentifier<TSR, Argument_MinMaxValue>
        {
        }
    }

    [Serializable]
    public class ArgumentMinMaxValueException<T> : ArgumentMinMaxValueException
    {
        public T Minimum { get; }
        public T Maximum { get; }

        public ArgumentMinMaxValueException(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ArgumentMinMaxValueException(T minimum, T maximum, String? message)
            : base(message)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ArgumentMinMaxValueException(T minimum, T maximum, String? message, Exception? exception)
            : base(message, exception)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ArgumentMinMaxValueException(String? parameter, T minimum, T maximum, String? message)
            : base(message, parameter)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ArgumentMinMaxValueException(String? parameter, T minimum, T maximum, String? message, Exception? exception)
            : base(message, parameter, exception)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentMinMaxValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Minimum = info.GetValue<T>(nameof(Minimum));
            Maximum = info.GetValue<T>(nameof(Maximum));
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Minimum), Minimum);
            info.AddValue(nameof(Maximum), Maximum);
        }
    }

    [Serializable]
    public class ArgumentMinMaxValueException : ArgumentException
    {
        public ArgumentMinMaxValueException()
        {
        }

        public ArgumentMinMaxValueException(String? message)
            : base(message)
        {
        }

        public ArgumentMinMaxValueException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentMinMaxValueException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentMinMaxValueException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentMinMaxValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}