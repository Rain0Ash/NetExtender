// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Culture
{
    public readonly struct LCID : IEquatable<LCID>, IEquatable<Int32>, IEquatable<UInt16>, IEquatable<CultureData>
    {
        public static LCID En { get; } = new LCID(0x409);
        public static LCID Ru { get; } = new LCID(0x419);
        public static LCID De { get; } = new LCID(0x407);
        public static LCID Fr { get; } = new LCID(0x40c);
        
        public static Boolean operator ==(LCID first, LCID second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(LCID first, Int32 second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(Int32 first, LCID second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator ==(LCID first, UInt16 second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(UInt16 first, LCID second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator ==(LCID first, CultureData second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(CultureData first, LCID second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator !=(LCID first, LCID second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(LCID first, Int32 second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(Int32 first, LCID second)
        {
            return !second.Equals(first);
        }
        
        public static Boolean operator !=(LCID first, UInt16 second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(UInt16 first, LCID second)
        {
            return !second.Equals(first);
        }
        
        public static Boolean operator !=(LCID first, CultureData second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(CultureData first, LCID second)
        {
            return !second.Equals(first);
        }

        public static implicit operator CultureData(LCID lcid)
        {
            return (CultureData) lcid.Code16;
        }

        public static implicit operator LCID(CultureData culture)
        {
            return new LCID(culture);
        }
        
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

        public static implicit operator CultureInfo(LCID lcid)
        {
            return lcid == CultureUtils.Default ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(lcid);
        }

        public static implicit operator LCID(CultureInfo info)
        {
            return info.LCID;
        }

        public Int32 Code { get; }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Code;
            }
        }

        public LCID(CultureData culture)
            : this((Int32) culture)
        {
        }

        public LCID(UInt16 lcid)
            : this((Int32) lcid)
        {
        }
        
        public LCID(Int32 lcid)
        {
            Code = lcid > 0 ? lcid : CultureUtils.Default;
        }

        public Boolean Equals(LCID other)
        {
            return Code == other.Code;
        }

        public Boolean Equals(Int32 other)
        {
            return Code == other;
        }

        public Boolean Equals(UInt16 other)
        {
            return Code == other;
        }

        public Boolean Equals(CultureData other)
        {
            return Code == (Int32) other;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                LCID lcid => Equals(lcid),
                Int32 lcid => Equals(lcid),
                UInt16 lcid => Equals(lcid),
                CultureData lcid => Equals(lcid),
                _ => false
            };
        }

        public override Int32 GetHashCode()
        {
            return Code;
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