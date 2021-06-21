// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using NetExtender.Images.Flags;

namespace NetExtender.Utils.Types
{
    public static class CultureFlagsUtils
    {
        private static IDictionary<CultureInfo, Image> ImagesCache { get; } = new Dictionary<CultureInfo, Image>();

        private static Image? ReadImage(CultureInfo info)
        {
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

        public static Image? GetImage(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            lock (ImagesCache)
            {
                if (ImagesCache.TryGetValue(info, out Image? image))
                {
                    return image;
                }

                image = ReadImage(info);
                if (image is null)
                {
                    return null;
                }

                ImagesCache.Add(info, image);
                return image;
            }
        }

        public static void SetImage(this CultureInfo info, Image? image)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            lock (ImagesCache)
            {
                if (image is null)
                {
                    ImagesCache.Remove(info);
                    return;
                }

                ImagesCache[info] = image;
            }
        }
    }
}