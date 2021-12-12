// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public class StaticInitializerAttribute : Attribute
    {
        public Type? Type { get; init; }
        public Int32 Priority { get; init; }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class StaticInitializerRequiredAttribute : StaticInitializerAttribute
    {
    }
}