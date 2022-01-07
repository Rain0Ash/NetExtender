// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using NetExtender.Types.Culture;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsUserInterfaceLanguageUtilities
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern UInt16 SetThreadUILanguage(UInt16 identifier);

        public static Boolean SetUILanguage(UInt16 identifier)
        {
            if (identifier <= 0)
            {
                return false;
            }

            return SetThreadUILanguage(identifier) == identifier;
        }

        public static Boolean SetUILanguage(Int32 identifier)
        {
            if (identifier <= 0 || identifier > UInt16.MaxValue)
            {
                return false;
            }

            return SetUILanguage((UInt16) identifier);
        }

        public static Boolean SetUILanguage(this LocalizationIdentifier identifier)
        {
            return SetUILanguage(identifier.Code);
        }

        public static Boolean SetUILanguage(this CultureIdentifier identifier)
        {
            return SetUILanguage((UInt16) identifier);
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