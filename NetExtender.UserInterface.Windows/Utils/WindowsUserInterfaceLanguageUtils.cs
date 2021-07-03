// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using NetExtender.Types.Culture;

namespace NetExtender.Utils.UserInterface
{
    public static class WindowsUserInterfaceLanguageUtils
    {
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
    }
}