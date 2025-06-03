// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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