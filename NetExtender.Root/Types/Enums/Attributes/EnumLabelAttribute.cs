// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Enums.Attributes
{
    /// <summary>
    /// Provides the label annotaion to be tagged to enum type fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumLabelAttribute : Attribute
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        public String Value { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        public Int32 Index { get; }

        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public EnumLabelAttribute(String value, Int32 index = 0)
        {
            Value = value;
            Index = index;
        }
    }
}