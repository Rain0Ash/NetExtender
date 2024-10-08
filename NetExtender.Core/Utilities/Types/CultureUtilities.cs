// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.Culture;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    public enum CultureType
    {
        Invariant,
        Current,
        User,
        System
    }

    public static class CultureUtilities
    {
        public const Int32 Default = (Int32) CultureIdentifier.Invariant;

        public static CultureInfo Invariant
        {
            get
            {
                return CultureInfo.InvariantCulture;
            }
        }

        public static CultureInfo System
        {
            get
            {
                return CultureInfo.CurrentUICulture;
            }
        }

        public static CultureInfo Current
        {
            get
            {
                return CultureInfo.CurrentCulture;
            }
        }

        public static CultureInfo English { get; } = GetCultureInfo(CultureIdentifier.En);

        private static class CultureInfoCache
        {
            private static ConcurrentDictionary<LocalizationIdentifier, CultureInfo> Storage { get; } = new ConcurrentDictionary<LocalizationIdentifier, CultureInfo>();

            public static CultureInfo GetOrAdd(LocalizationIdentifier identifier)
            {
                return Storage.GetOrAdd(identifier, localization => CultureInfo.GetCultureInfo(localization));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalizationIdentifier GetLocalizationIdentifier(UInt16 lcid)
        {
            return GetCultureIdentifier(lcid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureIdentifier GetCultureIdentifier(UInt16 lcid)
        {
            return TryGetCultureIdentifier(lcid, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalizationIdentifier GetLocalizationIdentifier(Int32 lcid)
        {
            return GetCultureIdentifier(lcid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureIdentifier GetCultureIdentifier(Int32 lcid)
        {
            return TryGetCultureIdentifier(lcid, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(UInt16 lcid, out LocalizationIdentifier identifier)
        {
            return TryGetLocalizationIdentifier(lcid, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(UInt16 lcid, out CultureIdentifier identifier)
        {
            return TryGetCultureIdentifier(lcid, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLocalizationIdentifier(UInt16 lcid, out LocalizationIdentifier identifier)
        {
            Boolean successful = TryGetCultureIdentifier(lcid, out CultureIdentifier culture);
            identifier = culture;
            return successful;
        }

        public static Boolean TryGetCultureIdentifier(UInt16 lcid, out CultureIdentifier identifier)
        {
            if (lcid == default)
            {
                identifier = CultureIdentifier.Invariant;
                return true;
            }

            if (EnumUtilities.ContainsValue<CultureIdentifier>(lcid))
            {
                identifier = (CultureIdentifier) lcid;
                return true;
            }

            identifier = CultureIdentifier.Invariant;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(Int32 lcid, out LocalizationIdentifier identifier)
        {
            return TryGetLocalizationIdentifier(lcid, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(Int32 lcid, out CultureIdentifier identifier)
        {
            return TryGetCultureIdentifier(lcid, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLocalizationIdentifier(Int32 lcid, out LocalizationIdentifier identifier)
        {
            Boolean successful = TryGetCultureIdentifier(lcid, out CultureIdentifier culture);
            identifier = culture;
            return successful;
        }

        public static Boolean TryGetCultureIdentifier(Int32 lcid, out CultureIdentifier identifier)
        {
            if (lcid is >= 0 and <= UInt16.MaxValue)
            {
                return TryGetCultureIdentifier((UInt16) lcid, out identifier);
            }

            identifier = CultureIdentifier.Invariant;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalizationIdentifier GetLocalizationIdentifier(String code)
        {
            return GetCultureIdentifier(code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureIdentifier GetCultureIdentifier(String code)
        {
            return TryGetCultureIdentifier(code, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(String code, out LocalizationIdentifier identifier)
        {
            return TryGetLocalizationIdentifier(code, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetIdentifier(String code, out CultureIdentifier identifier)
        {
            return TryGetCultureIdentifier(code, out identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLocalizationIdentifier(String code, out LocalizationIdentifier identifier)
        {
            Boolean successful = TryGetCultureIdentifier(code, out CultureIdentifier culture);
            identifier = culture;
            return successful;
        }

        public static Boolean TryGetCultureIdentifier(String code, out CultureIdentifier identifier)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            try
            {
                if (UInt16.TryParse(code, out UInt16 lcid))
                {
                    return TryGetCultureIdentifier(lcid, out identifier);
                }

                CultureInfo info = CultureInfo.GetCultureInfo(code, true);

                if (EnumUtilities.ContainsValue<CultureIdentifier>(info.LCID))
                {
                    identifier = (CultureIdentifier) info.LCID;
                    return true;
                }

                if (EnumUtilities.TryParseName(info.TwoLetterISOLanguageName, true, out identifier) ||
                    EnumUtilities.TryParseName(info.ThreeLetterISOLanguageName, true, out identifier) ||
                    EnumUtilities.TryParseName(info.ThreeLetterWindowsLanguageName, true, out identifier))
                {
                    return true;
                }

                identifier = CultureIdentifier.Invariant;
                return false;
            }
            catch (Exception)
            {
                identifier = CultureIdentifier.Invariant;
                return false;
            }
        }

        public static String GetNativeLanguageName(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            String native = info.IsNeutralCulture ? info.NativeName : info.Parent.NativeName;
            return info.TextInfo.ToTitleCase(native);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalizationIdentifier LCID(this CultureInfo? info)
        {
            return info is not null ? new LocalizationIdentifier(info.LCID) : Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 LCID16(this CultureInfo? info)
        {
            return info is not null ? (UInt16) info.LCID : (UInt16) Default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(UInt16 identifier, out CultureInfo info)
        {
            return TryGetCultureInfo((Int32) identifier, out info);
        }

        public static Boolean TryGetCultureInfo(Int32 identifier, out CultureInfo info)
        {
            try
            {
                info = CultureInfoCache.GetOrAdd(identifier == default ? Default : identifier);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(this LocalizationIdentifier identifier, out CultureInfo info)
        {
            return TryGetCultureInfo(identifier.Code, out info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(this CultureIdentifier culture, out CultureInfo info)
        {
            return TryGetCultureInfo((Int32) culture, out info);
        }

        public static Boolean TryGetCultureInfo(String name, out CultureInfo info)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                info = CultureInfo.GetCultureInfo(name, true);
                return true;
            }
            catch (Exception)
            {
                info = CultureInfo.InvariantCulture;
                return false;
            }
        }

        public static CultureInfo GetCultureInfo(this CultureType type)
        {
            return type switch
            {
                CultureType.Invariant => CultureInfo.InvariantCulture,
                CultureType.Current => CultureInfo.CurrentCulture,
                CultureType.User => CultureInfo.CurrentUICulture,
                CultureType.System => CultureInfo.InstalledUICulture,
                _ => throw new EnumUndefinedOrNotSupportedException<CultureType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(UInt16 identifier)
        {
            return GetCultureInfo(identifier, CultureType.Invariant);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(UInt16 identifier, CultureType type)
        {
            return GetCultureInfo((Int32) identifier, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(Int32 identifier)
        {
            return GetCultureInfo(identifier, CultureType.Invariant);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(Int32 identifier, CultureType type)
        {
            return TryGetCultureInfo(identifier, out CultureInfo info) ? info : type.GetCultureInfo();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this LocalizationIdentifier identifier)
        {
            return GetCultureInfo(identifier, CultureType.Invariant);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this LocalizationIdentifier identifier, CultureType type)
        {
            return GetCultureInfo(identifier.Code, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo ToCultureInfo(this CultureIdentifier identifier)
        {
            return GetCultureInfo(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this CultureIdentifier identifier)
        {
            return GetCultureInfo(identifier, CultureType.Invariant);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo ToCultureInfo(this CultureIdentifier identifier, CultureType type)
        {
            return GetCultureInfo(identifier, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this CultureIdentifier identifier, CultureType type)
        {
            return GetCultureInfo((Int32) identifier, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(String culture)
        {
            return GetCultureInfo(culture, CultureType.Invariant);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(String culture, CultureType type)
        {
            return TryGetCultureInfo(culture, out CultureInfo info) ? info : type.GetCultureInfo();
        }
        
#if NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetUnicodeFlag(UInt16 identifier)
        {
            return GetUnicodeFlag((LocalizationIdentifier) identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetUnicodeFlag(Int32 identifier)
        {
            return GetUnicodeFlag((LocalizationIdentifier) identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetUnicodeFlag(this CultureIdentifier identifier)
        {
            return GetUnicodeFlag((LocalizationIdentifier) identifier);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetUnicodeFlag(this LocalizationIdentifier identifier)
        {
            return identifier.Region is RegionInfo region ? GetUnicodeFlag(region) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? GetUnicodeFlag(this CultureInfo culture)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return culture.ToRegionInfo() is RegionInfo region ? GetUnicodeFlag(region) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetUnicodeFlag(this RegionInfo region)
        {
            if (region is null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            const Int32 indicator = 0x1F1E6;
            const Int32 uppercase = indicator - 'A';
            const Int32 lowercase = indicator - 'a';

            String iso = region.TwoLetterISORegionName;
            Rune rune1 = iso[0] is >= 'a' and <= 'z' ? new Rune(iso[0] + lowercase) : new Rune(iso[0] + uppercase);
            Rune rune2 = iso[1] is >= 'a' and <= 'z' ? new Rune(iso[1] + lowercase) : new Rune(iso[1] + uppercase);

            const Int32 surrogate = 4;
            return String.Create(surrogate, (rune1, rune2), (span, _) =>
            {
                rune1.EncodeToUtf16(span);
                rune2.EncodeToUtf16(span[2..]);
            });
        }
#endif

        private static Dictionary<LocalizationIdentifier, LocalizationIdentifier> IdentifiersNormalization { get; } = new Dictionary<LocalizationIdentifier, LocalizationIdentifier>
        {
            [CultureIdentifier.Us] = CultureIdentifier.En
        };

        public static CultureIdentifier Normalize(this CultureIdentifier value)
        {
            return IdentifiersNormalization.TryGetValue(value, out LocalizationIdentifier identifier) ? identifier : value;
        }

        public static IEnumerable<CultureIdentifier> Normalize(this IEnumerable<CultureIdentifier> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(Normalize).Distinct();
        }

        public static LocalizationIdentifier Normalize(LocalizationIdentifier value)
        {
            return IdentifiersNormalization.TryGetValue(value, out LocalizationIdentifier identifier) ? identifier : value;
        }
        
        public static IEnumerable<LocalizationIdentifier> Normalize(this IEnumerable<LocalizationIdentifier> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(Normalize).Distinct();
        }

        private static class RegionInfoStorage
        {
            private static ConcurrentDictionary<LocalizationIdentifier, RegionInfo> Storage { get; } = new ConcurrentDictionary<LocalizationIdentifier, RegionInfo>();

            public static RegionInfo GetOrAdd(LocalizationIdentifier identifier)
            {
                return Storage.GetOrAdd(identifier, localization => new RegionInfo(localization));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegionInfo? ToRegionInfo(UInt16 identifier)
        {
            try
            {
                return RegionInfoStorage.GetOrAdd(identifier);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegionInfo? ToRegionInfo(Int32 identifier)
        {
            try
            {
                return RegionInfoStorage.GetOrAdd(identifier);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegionInfo? ToRegionInfo(this CultureIdentifier identifier)
        {
            try
            {
                return RegionInfoStorage.GetOrAdd(identifier);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegionInfo? ToRegionInfo(this LocalizationIdentifier identifier)
        {
            try
            {
                return RegionInfoStorage.GetOrAdd(identifier);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegionInfo? ToRegionInfo(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                return RegionInfoStorage.GetOrAdd(info);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCultureEquals(this CultureIdentifier identifier, CultureInfo? info)
        {
            return IsCultureEquals(info, identifier);
        }

        public static Boolean IsCultureEquals(this CultureInfo? info, CultureIdentifier identifier)
        {
            if (info is null)
            {
                return false;
            }

            CultureInfo second = identifier.GetCultureInfo();
            return info.LCID == second.LCID && info.Name == second.Name;
        }

        public static Boolean IsCultureEquals(this CultureInfo? first, CultureInfo? second)
        {
            if (first is null)
            {
                return second is null;
            }

            if (second is null)
            {
                return false;
            }

            return first.LCID == second.LCID && first.Name == second.Name;
        }
    }
}