// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations
{
    public readonly struct LCID
    {
        public static LCID En { get; } = new LCID(0x409);
        public static LCID Ru { get; } = new LCID(0x419);
        public static LCID De { get; } = new LCID(0x407);
        public static LCID Fr { get; } = new LCID(0x40c);
        
        public static implicit operator Int32(LCID lcid)
        {
            return lcid.Code;
        }

        public static implicit operator LCID(Int32 lcid)
        {
            return new LCID(lcid);
        }
        
        public static implicit operator UInt16(LCID lcid)
        {
            return lcid.Code16;
        }

        public static implicit operator LCID(UInt16 lcid)
        {
            return new LCID(lcid);
        }
        
        public Int32 Code { get; }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Code;
            }
        }
        
        public LCID(Int32 lcid)
        {
            if (lcid < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lcid));
            }

            Code = lcid;
        }

        public override String ToString()
        {
            try
            {
                return CultureInfo.GetCultureInfo(Code).GetNativeLanguageName();
            }
            catch (Exception)
            {
                return Code.ToString();
            }
        }
    }
}