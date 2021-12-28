// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Attributes
{
    [Flags]
    public enum StaticInitializerAttributePlatform : UInt64
    {
        None = 0,
        Windows = 1,
        Linux = 2,
        FreeBSD = 4,
        OSX = 8,
        All = UInt64.MaxValue
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public class StaticInitializerAttribute : Attribute
    {
        public Type? Type { get; init; }
        public Int32 Priority { get; init; }
        public Boolean Active { get; init; } = true;
        public StaticInitializerAttributePlatform Platform { get; init; } = StaticInitializerAttributePlatform.All;
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class StaticInitializerRequiredAttribute : StaticInitializerAttribute
    {
    }
}