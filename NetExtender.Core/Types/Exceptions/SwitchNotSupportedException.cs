using System;
using System.Runtime.Serialization;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class SwitchNotSupportedException<T> : SwitchNotSupportedBoxException<T>
    {
        public new T Argument
        {
            get
            {
                return base.Argument.Value;
            }
        }
        
        public SwitchNotSupportedException(T argument)
            : base(argument)
        {
        }

        public SwitchNotSupportedException(T argument, String? message)
            : base(argument, message)
        {
        }

        public SwitchNotSupportedException(T argument, String? message, Exception? exception)
            : base(argument, message, exception)
        {
        }

        public SwitchNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class SwitchNotSupportedBoxException<T> : SwitchNotSupportedException
    {
        public sealed override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public sealed override Box<T> Argument { get; }
        
        protected SwitchNotSupportedBoxException(T argument)
        {
            Argument = argument;
        }

        protected SwitchNotSupportedBoxException(T argument, String? message)
            : base(message)
        {
            Argument = argument;
        }

        protected SwitchNotSupportedBoxException(T argument, String? message, Exception? exception)
            : base(message, exception)
        {
            Argument = argument;
        }

        protected SwitchNotSupportedBoxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Argument = info.GetValue<T>(nameof(Argument));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Argument), Argument.Value);
        }
    }
    
    [Serializable]
    public abstract class SwitchNotSupportedException : NotSupportedException
    {
        public virtual Type? Type
        {
            get
            {
                return Argument?.GetType();
            }
        }

        public abstract Object? Argument { get; }
        
        protected SwitchNotSupportedException()
        {
        }

        protected SwitchNotSupportedException(String? message)
            : base(message)
        {
        }

        protected SwitchNotSupportedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected SwitchNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}