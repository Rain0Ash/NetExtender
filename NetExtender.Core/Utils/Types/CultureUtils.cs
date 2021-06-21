// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Culture;

namespace NetExtender.Utils.Types
{
    public enum CultureType
    {
        Invariant,
        Current,
        User,
        System
    }

    public static class CultureUtils
    {
        public const Int32 Default = (Int32) CultureLCID.Invariant;

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

        public static CultureInfo English { get; } = GetCultureInfo(CultureLCID.En);

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

        public static String GetNativeLanguageName(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            String native = info.IsNeutralCulture ? info.NativeName : info.Parent.NativeName;
            return info.TextInfo.ToTitleCase(native);
        }

        public static LCID LCID(this CultureInfo? info)
        {
            if (info is null)
            {
                return Default;
            }

            return new LCID(info.LCID);
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
        public static Boolean TryGetCultureInfo(UInt16 lcid, out CultureInfo info)
        {
            return TryGetCultureInfo((Int32) lcid, out info);
        }

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
            return TryGetCultureInfo(lcid.Code, out info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetCultureInfo(this CultureLCID culture, out CultureInfo info)
        {
            return TryGetCultureInfo((Int32) culture, out info);
        }

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
            return GetCultureInfo((Int32) lcid, type);
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
            return GetCultureInfo(lcid.Code, type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo ToCultureInfo(this CultureLCID lcid)
        {
            return GetCultureInfo(lcid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this CultureLCID lcid)
        {
            return GetCultureInfo(lcid, CultureType.Invariant);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo ToCultureInfo(this CultureLCID lcid, CultureType type)
        {
            return GetCultureInfo(lcid, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CultureInfo GetCultureInfo(this CultureLCID lcid, CultureType type)
        {
            return GetCultureInfo((Int32) lcid, type);
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

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern UInt16 SetThreadUILanguage(UInt16 lcid);

        public static Boolean SetUILanguage(UInt16 lcid)
        {
            if (lcid <= 0)
            {
                return false;
            }

            return SetThreadUILanguage(lcid) == lcid;
        }

        public static Boolean SetUILanguage(Int32 lcid)
        {
            if (lcid <= 0 || lcid > UInt16.MaxValue)
            {
                return false;
            }

            return SetUILanguage((UInt16) lcid);
        }

        public static Boolean SetUILanguage(this LCID lcid)
        {
            return SetUILanguage(lcid.Code);
        }

        public static Boolean SetUILanguage(this CultureLCID lcid)
        {
            return SetUILanguage((UInt16) lcid);
        }

        public static Boolean SetUILanguage(this CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return SetUILanguage(info.LCID);
        }

        public static Boolean IsCultureEquals(this CultureLCID lcid, CultureInfo? info)
        {
            return IsCultureEquals(info, lcid);
        }

        public static Boolean IsCultureEquals(this CultureInfo? info, CultureLCID lcid)
        {
            if (info is null)
            {
                return false;
            }

            CultureInfo second = lcid.GetCultureInfo();
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