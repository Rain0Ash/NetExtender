// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Culture
{
    public readonly struct LocalizationIdentifier : IEquatable<LocalizationIdentifier>, IEquatable<Int32>, IEquatable<UInt16>, IEquatable<CultureIdentifier>,
        IComparable<LocalizationIdentifier>, IComparable<Int32>, IComparable<UInt16>, IComparable<CultureIdentifier>
    {
        public static Boolean operator ==(LocalizationIdentifier first, LocalizationIdentifier second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(LocalizationIdentifier first, Int32 second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(Int32 first, LocalizationIdentifier second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator ==(LocalizationIdentifier first, UInt16 second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(UInt16 first, LocalizationIdentifier second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator ==(LocalizationIdentifier first, CultureIdentifier second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator ==(CultureIdentifier first, LocalizationIdentifier second)
        {
            return second.Equals(first);
        }
        
        public static Boolean operator !=(LocalizationIdentifier first, LocalizationIdentifier second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(LocalizationIdentifier first, Int32 second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(Int32 first, LocalizationIdentifier second)
        {
            return !second.Equals(first);
        }
        
        public static Boolean operator !=(LocalizationIdentifier first, UInt16 second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(UInt16 first, LocalizationIdentifier second)
        {
            return !second.Equals(first);
        }
        
        public static Boolean operator !=(LocalizationIdentifier first, CultureIdentifier second)
        {
            return !first.Equals(second);
        }
        
        public static Boolean operator !=(CultureIdentifier first, LocalizationIdentifier second)
        {
            return !second.Equals(first);
        }

        public static implicit operator CultureIdentifier(LocalizationIdentifier identifier)
        {
            return (CultureIdentifier) identifier.Code16;
        }

        public static implicit operator LocalizationIdentifier(CultureIdentifier culture)
        {
            return new LocalizationIdentifier(culture);
        }

        public static implicit operator Int32(LocalizationIdentifier identifier)
        {
            return identifier.Code;
        }

        public static implicit operator LocalizationIdentifier(Int32 identifier)
        {
            return new LocalizationIdentifier(identifier);
        }
        
        public static implicit operator UInt16(LocalizationIdentifier identifier)
        {
            return identifier.Code16;
        }

        public static implicit operator LocalizationIdentifier(UInt16 identifier)
        {
            return new LocalizationIdentifier(identifier);
        }

        public static implicit operator CultureInfo(LocalizationIdentifier identifier)
        {
            return identifier.IsDefault ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(identifier);
        }

        public static implicit operator LocalizationIdentifier(CultureInfo? info)
        {
            return info?.LCID ?? default;
        }

        public Int32 Code { get; }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Code;
            }
        }

        public String? TwoLetterISOLanguageName
        {
            get
            {
                return CultureUtilities.TryGetCultureInfo(Code, out CultureInfo info) ? info.TwoLetterISOLanguageName : null;
            }
        }
        
        public String? ThreeLetterISOLanguageName
        {
            get
            {
                return CultureUtilities.TryGetCultureInfo(Code, out CultureInfo info) ? info.ThreeLetterISOLanguageName : null;
            }
        }

        public Boolean IsDefault
        {
            get
            {
                return Code <= 0 || Code == CultureUtilities.Default;
            }
        }

        public LocalizationIdentifier(CultureIdentifier culture)
            : this((Int32) culture)
        {
        }

        public LocalizationIdentifier(UInt16 identifier)
            : this((Int32) identifier)
        {
        }
        
        public LocalizationIdentifier(Int32 identifier)
        {
            Code = identifier > 0 ? identifier : CultureUtilities.Default;
        }
        
        public override Int32 GetHashCode()
        {
            return Code;
        }

        public Boolean Equals(LocalizationIdentifier other)
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

        public Boolean Equals(CultureIdentifier other)
        {
            return Code == (Int32) other;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                LocalizationIdentifier identifier => Equals(identifier),
                Int32 identifier => Equals(identifier),
                UInt16 identifier => Equals(identifier),
                CultureIdentifier identifier => Equals(identifier),
                _ => false
            };
        }
        
        public Int32 CompareTo(LocalizationIdentifier other)
        {
            return Code.CompareTo(other.Code);
        }

        public Int32 CompareTo(Int32 other)
        {
            return Code.CompareTo(other);
        }

        public Int32 CompareTo(UInt16 other)
        {
            return Code.CompareTo(other);
        }

        public Int32 CompareTo(CultureIdentifier other)
        {
            return Code.CompareTo((Int32) other);
        }

        public override String ToString()
        {
            return CultureUtilities.TryGetCultureInfo(Code, out CultureInfo info) ? info.GetNativeLanguageName() : Code.ToString();
        }
    }
}