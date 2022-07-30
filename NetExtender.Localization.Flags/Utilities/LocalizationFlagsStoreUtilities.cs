// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Culture;

namespace NetExtender.Utilities.Types
{
    public static partial class LocalizationFlagsUtilities
    {
        private static class ImageStore<T> where T : class
        {
            private static Dictionary<LocalizationIdentifier, T> Store { get; } = new Dictionary<LocalizationIdentifier, T>(EnumUtilities.Count<CultureIdentifier>());
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
                lock (Store)
                {
                    if (Store.TryGetValue(info, out T? image))
                    {
                        return image;
                    }

                    image = ReadFlagImage(info);
                    if (image is null)
                    {
                        return null;
                    }

                    Store.Add(info, image);
                    return image;
                }
            }

            public static LocalizationIdentifier SetFlagImage(LocalizationIdentifier info, T? image)
            {
                lock (Store)
                {
                    if (image is null)
                    {
                        Store.Remove(info);
                        return info;
                    }

                    Store[info] = image;
                    return info;
                }
            }
        }
    }
}