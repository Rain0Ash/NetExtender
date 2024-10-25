using System;
using System.Runtime.Serialization;

namespace NetExtender.WindowsPresentation.Types.Exceptions
{
    [Serializable]
    public abstract class CommandException : InvalidOperationException, ICommandException
    {
        protected CommandException()
        {
        }

        protected CommandException(String? message)
            : base(message)
        {
        }

        protected CommandException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected CommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}