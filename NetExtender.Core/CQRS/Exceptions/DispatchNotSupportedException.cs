using System;
using System.Runtime.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.CQRS.Exceptions
{
    [Serializable]
    public class DispatchNotSupportedException<TEntity> : DispatchNotSupportedException
    {
        private new const String Message = "Dispatch of entity '{0}' not supported for dispatch.";
        private const String DispatchMessage = "Dispatch of entity '{0}' not supported for dispatcher '{1}'.";
        
        public Type? Dispatcher { get; }
        
        public Type Type
        {
            get
            {
                return typeof(TEntity);
            }
        }

        public DispatchNotSupportedException(Type? dispatcher)
            : base(Format(dispatcher, null))
        {
            Dispatcher = dispatcher;
        }

        public DispatchNotSupportedException(Type? dispatcher, String? message)
            : base(Format(dispatcher, message))
        {
            Dispatcher = dispatcher;
        }

        public DispatchNotSupportedException(Type? dispatcher, String? message, Exception? exception)
            : base(Format(dispatcher, message), exception)
        {
            Dispatcher = dispatcher;
        }
        
        protected DispatchNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        private static String Format(Type? dispatcher, String? message)
        {
            return message ?? (dispatcher is not null ? DispatchMessage.Format(typeof(TEntity), dispatcher?.GetType()) : Message.Format(typeof(TEntity)));
        }
    }
    
    [Serializable]
    public class DispatchNotSupportedException : NotSupportedException
    {
        private new const String Message = "Dispatch not supported.";
        
        public DispatchNotSupportedException()
            : base(Message)
        {
        }
        
        public DispatchNotSupportedException(String? message)
            : base(message ?? Message)
        {
        }
        
        public DispatchNotSupportedException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected DispatchNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}