using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
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

        protected ThisObjectDisposedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}