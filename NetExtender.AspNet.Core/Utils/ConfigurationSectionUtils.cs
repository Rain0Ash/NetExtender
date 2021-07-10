// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class ConfigurationSectionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetConfigurations<T>(this IConfigurationSection section) where T : class
        {
            if (section is null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return section.Get<T>() ?? Activator.CreateInstance<T>();
        }
    }
}