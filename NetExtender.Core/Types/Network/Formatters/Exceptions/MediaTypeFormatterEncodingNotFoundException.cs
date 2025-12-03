// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeFormatterEncodingNotFoundException : InvalidOperationException
    {
        private new const String Message = "Media type formatter encoding not found.";

        public MediaTypeFormatterEncodingNotFoundException()
            : base(Message)
        {
        }

        public MediaTypeFormatterEncodingNotFoundException(String? message)
            : base(message ?? Message)
        {
        }

        public MediaTypeFormatterEncodingNotFoundException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected MediaTypeFormatterEncodingNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}