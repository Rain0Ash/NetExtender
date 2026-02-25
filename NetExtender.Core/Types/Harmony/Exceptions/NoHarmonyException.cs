using System;
using System.Runtime.Serialization;

namespace NetExtender.Harmony.Types
{
    [Serializable]
    public sealed class NoHarmonyException : NotSupportedException
    {
        private new const String Message = "Friendship might be magic, but code patching requires Harmony.";

        public NoHarmonyException()
            : base(Message)
        {
        }

        public NoHarmonyException(String? message)
            : base(message ?? Message)
        {
        }

        public NoHarmonyException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private NoHarmonyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}