// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    public sealed class DomainAssemblyException : Exception
    {
        public Assembly Assembly { get; }

        public DomainAssemblyException(Assembly assembly)
            : this(assembly, null)
        {
        }

        public DomainAssemblyException(Assembly assembly, String? message)
            : base(assembly is not null ? message ?? $"Assembly '{assembly.GetName().Name}' domain exception." : throw new ArgumentNullException(nameof(assembly)))
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        private DomainAssemblyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Assembly = GetType().Assembly;
        }
    }
}