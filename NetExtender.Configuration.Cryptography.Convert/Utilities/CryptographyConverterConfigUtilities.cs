// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Convert.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;

namespace NetExtender.Configuration.Cryptography.Convert.Utilities
{
    public static class CryptographyConverterConfigUtilities
    {
        public static IConverterConfig Converter(this ICryptographyConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new CryphographyConverterConfig(behavior);
        }
    }
}