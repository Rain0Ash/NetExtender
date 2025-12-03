// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentWhiteSpaceStringException : ArgumentException
    {
        private new const String Message = "Value cannot be whitespace.";

        public ArgumentWhiteSpaceStringException()
            : base(Message)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter)
            : base(Message, parameter)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, Exception? exception)
            : base(Message, parameter, exception)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, String? message)
            : base(message, parameter)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, String? message, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentWhiteSpaceStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}