// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using NetExtender.Types.Culture;

namespace NetExtender.Types.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
    public sealed class EnumDescriptionAttribute : DescriptionAttribute
    {
        public static explicit operator KeyValuePair<LocalizationIdentifier, String>(EnumDescriptionAttribute value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new KeyValuePair<LocalizationIdentifier, String>(value.Identifier, value.Description);
        }
        
        public LocalizationIdentifier Identifier { get; }

        public EnumDescriptionAttribute(CultureIdentifier identifier, String description)
            : base(description)
        {
            Identifier = identifier;
        }

        public EnumDescriptionAttribute(LocalizationIdentifier identifier, String description)
            : base(description)
        {
            Identifier = identifier;
        }
    }
}