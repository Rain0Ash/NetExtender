using System;

namespace NetExtender.UserInterface.WindowsPresentation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DisplayMemberPathAttribute : Attribute
    {
        public String? Path { get; }

        public DisplayMemberPathAttribute(String? path)
        {
            Path = path;
        }
    }
}