// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Localization.Property.Localization.Initializers;

namespace NetExtender.Localization.Utilities
{
    public static class LocalizationInitializerUtilities
    {
        public static T Subscribe<T>(this T value, LocalizationInitializer initializer) where T : ILocalizationPropertyInfo
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (initializer is null)
            {
                throw new ArgumentNullException(nameof(initializer));
            }
            
            initializer.Subscribe(value);
            return value;
        }
        
        public static T Subscribe<T>(this T value, String name, LocalizationInitializer initializer) where T : ILocalizationPropertyInfo
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (initializer is null)
            {
                throw new ArgumentNullException(nameof(initializer));
            }
            
            initializer.Subscribe(value, name);
            return value;
        }
    }
}