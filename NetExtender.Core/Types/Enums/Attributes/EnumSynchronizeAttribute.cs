// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class EnumSynchronizeAttribute : Attribute
    {
        public String? Name { get; }
        public Int32 Order { get; }

        public EnumSynchronizeAttribute()
            : this(0)
        {
        }

        public EnumSynchronizeAttribute(Int32 order)
            : this(null, order)
        {
        }

        public EnumSynchronizeAttribute(String? name)
            : this(name, 0)
        {
        }

        public EnumSynchronizeAttribute(String? name, Int32 order)
        {
            Name = !String.IsNullOrWhiteSpace(name) ? name.Trim() : null;
            Order = order;
        }
    }
}