// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class NotEnumTypeException<T> : IncorrentEnumTypeException<T>
    {
        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public NotEnumTypeException()
        {
        }

        public NotEnumTypeException(String? message)
            : base(message)
        {
        }

        public NotEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public NotEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public NotEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected NotEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}