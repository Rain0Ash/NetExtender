// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class IncorrentEnumTypeException<T> : IncorrentEnumTypeException
    {
        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public IncorrentEnumTypeException()
        {
        }

        public IncorrentEnumTypeException(String? message)
            : base(message)
        {
        }

        public IncorrentEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public IncorrentEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public IncorrentEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected IncorrentEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class IncorrentEnumTypeException : ArgumentException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Type?(IncorrentEnumTypeException? exception)
        {
            return exception?.Type;
        }
        
        public abstract Type Type { get; }

        protected IncorrentEnumTypeException()
        {
        }

        protected IncorrentEnumTypeException(String? message)
            : base(message)
        {
        }

        protected IncorrentEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected IncorrentEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        protected IncorrentEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected IncorrentEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}