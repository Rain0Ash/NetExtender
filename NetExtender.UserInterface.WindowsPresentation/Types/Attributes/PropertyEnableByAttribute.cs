// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.WindowsPresentation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropertyEnableByAttribute : Attribute
    {
        public String? Property { get; }
        public Object? Value { get; }
        
        public PropertyEnableByAttribute(String? property)
            : this(property, null)
        {
        }

        public PropertyEnableByAttribute(String? property, Object? value)
        {
            Property = property;
            Value = value;
        }
    }
}