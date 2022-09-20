// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class BussinessException : BusinessException<Int32>
    {
        public BussinessException(Int32 code)
            : base(code)
        {
        }

        public BussinessException(Int32 code, String? message)
            : base(code, message)
        {
        }

        public BussinessException(Int32 code, String? message, Exception? innerException)
            : base(code, message, innerException)
        {
        }

        protected BussinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class BusinessException<T> : Exception where T : struct
    {
        public T Code { get; }
        public String? Description { get; init; }

        public BusinessException(T code)
        {
            Code = code;
        }

        public BusinessException(T code, String? message)
            : base(message)
        {
            Code = code;
        }

        public BusinessException(T code, String? message, Exception? innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetValue(nameof(Code), typeof(T)) is T code ? code : default;
        }
    }
}