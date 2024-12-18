// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ShutdownException : Exception
    {
        public Int32 Code { get; }

        public ShutdownException()
            : this(0)
        {
        }

        public ShutdownException(Int32 code)
        {
            Code = code;
        }

        public ShutdownException(String? message)
            : this(0, message)
        {
        }

        public ShutdownException(Int32 code, String? message)
            : base(message)
        {
            Code = code;
        }

        public ShutdownException(String? message, Exception? exception)
            : this(0, message, exception)
        {
        }

        public ShutdownException(Int32 code, String? message, Exception? exception)
            : base(message, exception)
        {
            Code = code;
        }

        protected ShutdownException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetInt32(nameof(Code));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Code), Code);
        }
    }
}