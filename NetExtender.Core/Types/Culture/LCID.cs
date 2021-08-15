// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Culture
{
    public readonly struct LCID : IEquatable<LCID>, IEquatable<Int32>, IEquatable<UInt16>, IEquatable<CultureLCID>
    {
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
        
        public static Boolean operator ==(LCID first, CultureLCID second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(CultureLCID first, LCID second)
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
        
        public static Boolean operator !=(LCID first, CultureLCID second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(CultureLCID first, LCID second)
        {
            return !second.Equals(first);
        }

        public static implicit operator CultureLCID(LCID lcid)
        {
            return (CultureLCID) lcid.Code16;
        }

        public static implicit operator LCID(CultureLCID culture)
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
            return lcid == CultureUtilities.Default ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(lcid);
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

        public LCID(CultureLCID culture)
            : this((Int32) culture)
        {
        }

        public LCID(UInt16 lcid)
            : this((Int32) lcid)
        {
        }
        
        public LCID(Int32 lcid)
        {
            Code = lcid > 0 ? lcid : CultureUtilities.Default;
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

        public Boolean Equals(CultureLCID other)
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
                CultureLCID lcid => Equals(lcid),
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