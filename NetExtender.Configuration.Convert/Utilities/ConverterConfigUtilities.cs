// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Convert.Interfaces;

namespace NetExtender.Configuration.Convert.Utilities
{
    public static class ConverterConfigUtilities
    {
        public static IConverterConfig Converter(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new ConverterConfig(behavior);
        }
    }
}