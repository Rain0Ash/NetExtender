using System;

namespace NetExtender.UserInterface.WindowsPresentation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ItemsSourcePropertyAttribute : Attribute
    {
        public String? Property { get; }
        
        public ItemsSourcePropertyAttribute(String? property)
        {
            Property = property;
        }
    }
}