using System;

namespace NetExtender.UserInterface.WindowsPresentation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SelectedValuePathAttribute : Attribute
    {
        public String? Path { get; }
        
        public SelectedValuePathAttribute(String? path)
        {
            Path = path;
        }
    }
}