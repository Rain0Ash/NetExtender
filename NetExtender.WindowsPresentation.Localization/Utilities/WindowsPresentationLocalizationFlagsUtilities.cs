// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using NetExtender.Localization.Utilities;
using NetExtender.Types.Culture;

namespace NetExtender.WindowsPresentation.Localization.Utilities
{
    public static class WindowsPresentationLocalizationFlagsUtilities
    {
        private static Assembly Assembly { get; }

        static WindowsPresentationLocalizationFlagsUtilities()
        {
            Assembly = typeof(LocalizationFlagsUtilities).Assembly;
            LocalizationFlagsUtilities.Initialize(Convert);
        }

        internal static void Initialize()
        {
        }

        private static BitmapSource? Convert(LocalizationIdentifier identifier)
        {
            try
            {
                String? region = identifier.TwoLetterISORegionName?.ToLower() ?? identifier.TwoLetterISOLanguageName?.ToLower();

                if (region is null)
                {
                    return null;
                }

                using Stream? stream = Assembly.GetManifestResourceStream($"NetExtender.Localization.Flags.Flags.{region}.png");

                if (stream is null)
                {
                    return null;
                }

                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return decoder.Frames[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static BitmapSource? GetFlagImage(this CultureInfo info)
        {
            return info.GetFlagImage<BitmapSource>();
        }

        public static BitmapSource? GetFlagImage(this CultureIdentifier identifier)
        {
            return identifier.GetFlagImage<BitmapSource>();
        }

        public static BitmapSource? GetFlagImage(this LocalizationIdentifier identifier)
        {
            return identifier.GetFlagImage<BitmapSource>();
        }

        public static LocalizationIdentifier SetFlagImage(this CultureInfo info, BitmapSource? image)
        {
            return info.SetFlagImage<BitmapSource>(image);
        }

        public static LocalizationIdentifier SetFlagImage(this CultureIdentifier identifier, BitmapSource? image)
        {
            return identifier.SetFlagImage<BitmapSource>(image);
        }

        public static LocalizationIdentifier SetFlagImage(this LocalizationIdentifier identifier, BitmapSource? image)
        {
            return identifier.SetFlagImage<BitmapSource>(image);
        }
    }
}