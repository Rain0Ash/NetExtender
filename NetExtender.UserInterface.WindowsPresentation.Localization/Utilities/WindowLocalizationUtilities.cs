// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.UserInterface.WindowsPresentation.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.Localization.Utilities
{
    public static class WindowLocalizationUtilities
    {
        public static T Subscribe<T>(this T value, WindowLocalizationInitializer window) where T : ILocalizationPropertyInfo
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            
            window.Subscribe(value);
            return value;
        }
        
        public static T Subscribe<T>(this T value, String name, WindowLocalizationInitializer window) where T : ILocalizationPropertyInfo
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            
            window.Subscribe(value, name);
            return value;
        }
    }
}