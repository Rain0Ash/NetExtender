// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class ApplicationEntryAssemblyAttribute : Attribute
    {
        public Type? Type { get; }

        public ApplicationEntryAssemblyAttribute()
            : this(null)
        {
        }

        public ApplicationEntryAssemblyAttribute(Type? type)
        {
            Type = type;
        }
    }
}