// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using DynamicData.Annotations;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class FactoryException : ArgumentException
    {
        public FactoryException()
        {
        }

        public FactoryException([CanBeNull] String? message)
            : base(message)
        {
        }

        public FactoryException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }

        public FactoryException([CanBeNull] String? message, [CanBeNull] String? paramName)
            : base(message, paramName)
        {
        }

        public FactoryException([CanBeNull] String? message, [CanBeNull] String? paramName, [CanBeNull] Exception? innerException)
            : base(message, paramName, innerException)
        {
        }
        
        protected FactoryException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}