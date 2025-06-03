// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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