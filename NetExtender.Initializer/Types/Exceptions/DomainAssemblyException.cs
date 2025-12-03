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

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private DomainAssemblyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Assembly = GetType().Assembly;
        }
    }
}