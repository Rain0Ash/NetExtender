// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Globalization;
using NetExtender.Images.Flags;
using NetExtender.Types.Culture;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class CultureFlagsUtilities
    {
        private static IStore<CultureInfo, Image> Images { get; } = new WeakStore<CultureInfo, Image>();
        
        private static Image? ReadFlagImage(CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                return FlagsImages.ResourceManager.GetObject(info.TwoLetterISOLanguageName) as Image ??
                       FlagsImages.ResourceManager.GetObject($"_{info.TwoLetterISOLanguageName}") as Image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Image? GetFlagImage(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            lock (Images)
            {
                if (Images.TryGetValue(info, out Image? image))
                {
                    return image;
                }

                image = ReadFlagImage(info);
                if (image is null)
                {
                    return null;
                }

                Images.Add(info, image);
                return image;
            }
        }

        public static Image? GetFlagImage(this CultureIdentifier identifier)
        {
            return GetFlagImage((LocalizationIdentifier) identifier);
        }

        public static Image? GetFlagImage(this LocalizationIdentifier identifier)
        {
            CultureInfo? info = identifier.Info;
            return info is not null ? GetFlagImage(info) : null;
        }

        public static CultureInfo SetFlagImage(this CultureInfo info, Image? image)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            lock (Images)
            {
                if (image is null)
                {
                    Images.Remove(info);
                    return info;
                }
                
                Images.AddOrUpdate(info, image);
            }

            return info;
        }
        
        public static CultureInfo? SetFlagImage(this CultureIdentifier identifier, Image? image)
        {
            return SetFlagImage((LocalizationIdentifier) identifier, image);
        }
        
        public static CultureInfo? SetFlagImage(this LocalizationIdentifier identifier, Image? image)
        {
            CultureInfo? info = identifier.Info;
            return info is not null ? SetFlagImage(info, image) : null;
        }
    }
}