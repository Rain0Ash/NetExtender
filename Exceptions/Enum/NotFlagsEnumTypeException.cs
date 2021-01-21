// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NetExtender.Exceptions.Enum
{
    [Serializable]
    public class NotFlagsEnumTypeException : ArgumentException
    {
        public NotFlagsEnumTypeException()
        {
        }

        public NotFlagsEnumTypeException([CanBeNull] String? message)
            : base(message)
        {
        }

        public NotFlagsEnumTypeException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }

        public NotFlagsEnumTypeException([CanBeNull] String? message, [CanBeNull] String? paramName)
            : base(message, paramName)
        {
        }

        public NotFlagsEnumTypeException([CanBeNull] String? message, [CanBeNull] String? paramName, [CanBeNull] Exception? innerException)
            : base(message, paramName, innerException)
        {
        }
        
        protected NotFlagsEnumTypeException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}