// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.WindowsPresentation.Types.Exceptions
{
    [Serializable]
    public sealed class CommandParameterTypeException : ArgumentException
    {
        public CommandParameterTypeException()
        {
        }
        
        public CommandParameterTypeException(String? message)
            : base(message)
        {
        }
        
        public CommandParameterTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        public CommandParameterTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }
        
        public CommandParameterTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }
        
        public CommandParameterTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}