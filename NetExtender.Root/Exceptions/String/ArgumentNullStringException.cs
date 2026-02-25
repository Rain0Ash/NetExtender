// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class ArgumentNullStringException : ArgumentNullException
    {
        private new const String Message = "Value cannot be null.";

        public ArgumentNullStringException()
            : base(null, Message)
        {
        }

        public ArgumentNullStringException(String? parameter)
            : base(parameter, Message)
        {
        }

        public ArgumentNullStringException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public ArgumentNullStringException(String? parameter, String? message)
            : base(parameter, message ?? Message)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentNullStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}