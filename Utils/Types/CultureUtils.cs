// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Images.Flags;
using NetExtender.Localizations;

namespace NetExtender.Utils.Types
{
    public enum CultureType
    {
        Invariant,
        Current,
        User,
        System
    }
    
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public static class CultureUtils
    {
        public static Int32 GetLCIDByCode(String code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return CultureInfo.GetCultureInfo(code).LCID;
        }
        
        public static Int32 GetLCID16ByCode(String code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return (UInt16) GetLCIDByCode(code);
        }
        
        public static String GetNativeLanguageName(this CultureInfo culture)
        {
            String native = culture.IsNeutralCulture ? culture.NativeName : culture.Parent.NativeName;
            return culture.TextInfo.ToTitleCase(native);
        }
        
        private static IDictionary<CultureInfo, Image> ImagesCache { get; } = new Dictionary<CultureInfo, Image>();

        private static Image ReadImage(CultureInfo culture)
        {
            try
            {
                return FlagsImages.ResourceManager.GetObject(culture.TwoLetterISOLanguageName) as Image ??
                       FlagsImages.ResourceManager.GetObject($"_{culture.TwoLetterISOLanguageName}") as Image ?? Images.Images.Basic.Null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static Image GetImage(this CultureInfo culture)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            if (ImagesCache.TryGetValue(culture, out Image image) && image is not null)
            {
                return image;
            }

            image = ReadImage(culture);
            if (image is null)
            {
                return Images.Images.Basic.Null;
            }
            
            ImagesCache.Add(culture, image);
            return image;
        }
        
        public static void SetImage(this CultureInfo culture, Image image)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            if (image is null)
            {
                ImagesCache.Remove(culture);
            }

            ImagesCache[culture] = image;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 LCID16(this CultureInfo culture)
        {
            return (UInt16) culture.LCID;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(UInt16 lcid, out CultureInfo info)
        {
            try
            {
                info = CultureInfo.GetCultureInfo(lcid);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(Int32 lcid, out CultureInfo info)
        {
            try
            {
                info = CultureInfo.GetCultureInfo(lcid);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(this LCID lcid, out CultureInfo info)
        {
            try
            {
                info = CultureInfo.GetCultureInfo(lcid);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(String culture, out CultureInfo info)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            try
            {
                info = CultureInfo.GetCultureInfo(culture);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this CultureType type)
        {
            return type switch
            {
                CultureType.Invariant => CultureInfo.InvariantCulture,
                CultureType.Current => CultureInfo.CurrentCulture,
                CultureType.User => CultureInfo.CurrentUICulture,
                CultureType.System => CultureInfo.InstalledUICulture,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(UInt16 lcid)
        {
            return GetCultureInfo(lcid, CultureType.Invariant);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(UInt16 lcid, CultureType type)
        {
            return TryGetCultureInfo(lcid, out CultureInfo info) ? info : type.GetCultureInfo();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(Int32 lcid)
        {
            return GetCultureInfo(lcid, CultureType.Invariant);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(Int32 lcid, CultureType type)
        {
            return TryGetCultureInfo(lcid, out CultureInfo info) ? info : type.GetCultureInfo();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this LCID lcid)
        {
            return GetCultureInfo(lcid, CultureType.Invariant);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this LCID lcid, CultureType type)
        {
            return TryGetCultureInfo(lcid, out CultureInfo info) ? info : type.GetCultureInfo();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(String culture)
        {
            return GetCultureInfo(culture, CultureType.Invariant);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(String culture, CultureType type)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return TryGetCultureInfo(culture, out CultureInfo info) ? info : type.GetCultureInfo();
        }
    }
}