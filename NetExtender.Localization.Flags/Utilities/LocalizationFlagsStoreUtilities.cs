// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Utilities
{
    public static partial class LocalizationFlagsUtilities
    {
        private static class ImageStorage<T> where T : class
        {
            private static Dictionary<LocalizationIdentifier, T> Storage { get; } = new Dictionary<LocalizationIdentifier, T>(EnumUtilities.Count<CultureIdentifier>());
            private static Func<LocalizationIdentifier, T?>? Converter { get; set; }

            public static Boolean Initialize(Func<LocalizationIdentifier, T?>? converter)
            {
                Converter = converter;
                return true;
            }

            private static T? ReadFlagImage(LocalizationIdentifier info)
            {
                try
                {
                    return Converter?.Invoke(info);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static T? GetFlagImage(LocalizationIdentifier info)
            {
                lock (Storage)
                {
                    if (Storage.TryGetValue(info, out T? image))
                    {
                        return image;
                    }

                    image = ReadFlagImage(info);
                    if (image is null)
                    {
                        return null;
                    }

                    Storage.Add(info, image);
                    return image;
                }
            }

            public static LocalizationIdentifier SetFlagImage(LocalizationIdentifier info, T? image)
            {
                lock (Storage)
                {
                    if (image is null)
                    {
                        Storage.Remove(info);
                        return info;
                    }

                    Storage[info] = image;
                    return info;
                }
            }
        }
    }
}