// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class NotEnumTypeException<T> : IncorrectEnumTypeException<T>
    {
        public NotEnumTypeException()
        {
        }

        public NotEnumTypeException(String? message)
            : base(message)
        {
        }

        public NotEnumTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NotEnumTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public NotEnumTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected NotEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}