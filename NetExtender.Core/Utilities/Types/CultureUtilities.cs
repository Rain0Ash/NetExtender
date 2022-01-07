// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Types.Culture;

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

        public static LocalizationIdentifier GetLocalizationIdentifier(String code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return CultureInfo.GetCultureInfo(code, true).LCID;
        }
        
        public static CultureIdentifier GetCultureIdentifier(UInt16 lcid)
        {
            return TryGetCultureIdentifier(lcid, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
        }
        
        public static CultureIdentifier GetCultureIdentifier(Int32 lcid)
        {
            return TryGetCultureIdentifier(lcid, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
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
        
        public static Boolean TryGetCultureIdentifier(Int32 lcid, out CultureIdentifier identifier)
        {
            if (lcid is >= 0 and <= UInt16.MaxValue)
            {
                return TryGetCultureIdentifier((UInt16) lcid, out identifier);
            }

            identifier = CultureIdentifier.Invariant;
            return false;
        }

        public static CultureIdentifier GetCultureIdentifier(String code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return TryGetCultureIdentifier(code, out CultureIdentifier identifier) ? identifier : CultureIdentifier.Invariant;
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

        public static LocalizationIdentifier LCID(this CultureInfo? info)
        {
            if (info is null)
            {
                return Default;
            }

            return new LocalizationIdentifier(info.LCID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 LCID16(this CultureInfo? info)
        {
            if (info is null)
            {
                return Default;
            }

            return (UInt16) info.LCID;
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
                info = CultureInfo.GetCultureInfo(identifier == default ? Default : identifier);
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
                _ => throw new NotSupportedException()
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