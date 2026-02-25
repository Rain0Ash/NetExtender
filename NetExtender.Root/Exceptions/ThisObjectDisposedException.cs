using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class ThisObjectDisposedException : ObjectDisposedException
    {
        public ThisObjectDisposedException(String? @object)
            : base(@object)
        {
        }

        public ThisObjectDisposedException(Object? @object)
            : this(@object?.GetType().Name)
        {
        }

        public ThisObjectDisposedException(String? @object, String? message)
            : base(@object, message)
        {
        }

        public ThisObjectDisposedException(Object? @object, String? message)
            : this(@object?.GetType().Name, message)
        {
        }

        public ThisObjectDisposedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ThisObjectDisposedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}