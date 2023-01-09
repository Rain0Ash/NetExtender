// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Utilities
{
    public static partial class LocalizationFlagsUtilities
    {
        public static Boolean Initialize<T>(Func<LocalizationIdentifier, T?>? converter) where T : class
        {
            return ImageStore<T>.Initialize(converter);
        }

        public static T? GetFlagImage<T>(this CultureInfo info) where T : class
        {
            return ImageStore<T>.GetFlagImage(info);
        }

        public static T? GetFlagImage<T>(this CultureIdentifier identifier) where T : class
        {
            return ImageStore<T>.GetFlagImage(identifier);
        }

        public static T? GetFlagImage<T>(this LocalizationIdentifier identifier) where T : class
        {
            return ImageStore<T>.GetFlagImage(identifier);
        }

        public static LocalizationIdentifier SetFlagImage<T>(this CultureInfo info, T? image) where T : class
        {
            return ImageStore<T>.SetFlagImage(info, image);
        }

        public static LocalizationIdentifier SetFlagImage<T>(this CultureIdentifier identifier, T? image) where T : class
        {
            return ImageStore<T>.SetFlagImage(identifier, image);
        }

        public static LocalizationIdentifier SetFlagImage<T>(this LocalizationIdentifier identifier, T? image) where T : class
        {
            return ImageStore<T>.SetFlagImage(identifier, image);
        }
    }
}