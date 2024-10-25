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