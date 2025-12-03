// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.JWT
{
    [Serializable]
    public sealed class JWTFormatException : JWTException
    {
        public JWTFormatException()
            : this("Token must consist of 3 dot-delimited parts.")
        {
        }

        public JWTFormatException(String? message)
            : base(message)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private JWTFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
