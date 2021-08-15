// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces.Property;

namespace NetExtender.Utilities.Configuration
{
    public static class ConfigUtilities
    {
        public static IPropertyConfig Create(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return PropertyConfig.Create(behavior);
        }
    }
}