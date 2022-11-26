// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Globalization;
using NetExtender.NewtonSoft.Types.Culture;
using NetExtender.Types.Currency;
using NetExtender.Types.Region;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Culture
{
    [Serializable]
    [JsonConverter(typeof(LocalizationIdentifierJsonConverter))]
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
            return identifier.IsDefault ? CultureInfo.InvariantCulture : identifier.GetCultureInfo();
        }

        public static implicit operator LocalizationIdentifier(CultureInfo? info)
        {
            return info?.LCID ?? default;
        }

        public static implicit operator CountryIdentifier(LocalizationIdentifier identifier)
        {
            return identifier.CountryIdentifier;
        }

        public static implicit operator CurrencyIdentifier(LocalizationIdentifier identifier)
        {
            return identifier.CurrencyIdentifier;
        }

        public static LocalizationIdentifier Default
        {
            get
            {
                return CultureUtilities.Default;
            }
        }

        public static LocalizationIdentifier Invariant
        {
            get
            {
                return CultureUtilities.Invariant;
            }
        }

        public static LocalizationIdentifier System
        {
            get
            {
                return CultureUtilities.System;
            }
        }

        public static LocalizationIdentifier Current
        {
            get
            {
                return CultureUtilities.Current;
            }
        }

        public static LocalizationIdentifier English
        {
            get
            {
                return CultureUtilities.English;
            }
        }

        public Int32 Code { get; }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Code;
            }
        }

        public CultureIdentifier Identifier
        {
            get
            {
                return this;
            }
        }

        public CultureInfo? Info
        {
            get
            {
                return CultureUtilities.TryGetCultureInfo(Code, out CultureInfo info) ? info : null;
            }
        }

        public RegionInfo? Region
        {
            get
            {
                return CultureUtilities.ToRegionInfo(Code);
            }
        }

        public CountryInfo? Country
        {
            get
            {
                return this;
            }
        }

        public CurrencyInfo? Currency
        {
            get
            {
                return this;
            }
        }

        public String? LanguageName
        {
            get
            {
                return Info?.GetNativeLanguageName();
            }
        }

        public String? CultureLanguageName
        {
            get
            {
                return Info?.Name;
            }
        }

        public String? EnglishLanguageName
        {
            get
            {
                return Info?.EnglishName;
            }
        }

        public String? DisplayLanguageName
        {
            get
            {
                return Info?.DisplayName;
            }
        }

        public String? NativeLanguageName
        {
            get
            {
                return Info?.NativeName;
            }
        }

        public String? TwoLetterISOLanguageName
        {
            get
            {
                return Info?.TwoLetterISOLanguageName;
            }
        }

        public String? ThreeLetterISOLanguageName
        {
            get
            {
                return Info?.ThreeLetterISOLanguageName;
            }
        }

        public String? RegionName
        {
            get
            {
                return Region?.Name;
            }
        }

        public String? EnglishRegionName
        {
            get
            {
                return Region?.EnglishName;
            }
        }

        public String? DisplayRegionName
        {
            get
            {
                return Region?.DisplayName;
            }
        }

        public String? NativeRegionName
        {
            get
            {
                return Region?.NativeName;
            }
        }

        public String? TwoLetterISORegionName
        {
            get
            {
                return Region?.TwoLetterISORegionName;
            }
        }

        public String? ThreeLetterISORegionName
        {
            get
            {
                return Region?.ThreeLetterISORegionName;
            }
        }

        public String? ThreeLetterWindowsRegionName
        {
            get
            {
                return Region?.ThreeLetterWindowsRegionName;
            }
        }

        public String? RegionCurrencySymbol
        {
            get
            {
                return Region?.CurrencySymbol;
            }
        }

        public String? RegionISOCurrencySymbol
        {
            get
            {
                return Region?.ISOCurrencySymbol;
            }
        }

        public String? RegionCurrencyEnglishName
        {
            get
            {
                return Region?.CurrencyEnglishName;
            }
        }

        public String? RegionCurrencyNativeName
        {
            get
            {
                return Region?.CurrencyNativeName;
            }
        }

        public Boolean IsMetric
        {
            get
            {
                return Region?.IsMetric ?? false;
            }
        }

        public Int32 GeoId
        {
            get
            {
                return Region?.GeoId ?? 0;
            }
        }

        public String? CountryName
        {
            get
            {
                return Country?.Name;
            }
        }

        public String? CountryOfficialName
        {
            get
            {
                return Country?.OfficialName;
            }
        }

        public String? CountryNativeName
        {
            get
            {
                return Country?.NativeName;
            }
        }

        public CountryIdentifier CountryIdentifier
        {
            get
            {
                return Country?.Identifier ?? CountryIdentifier.Default;
            }
        }

        public Int32 CountryCode
        {
            get
            {
                return Country?.Code ?? 0;
            }
        }

        public UInt16 CountryCode16
        {
            get
            {
                return Country?.Code16 ?? 0;
            }
        }

        public String? TwoLetterISOCountryName
        {
            get
            {
                return Country?.TwoLetterISOCountryName;
            }
        }

        public RegionIdentifier CountryRegion
        {
            get
            {
                return Country?.Region ?? RegionIdentifier.None;
            }
        }

        public SubregionIdentifier CountrySubregion
        {
            get
            {
                return Country?.Subregion ?? SubregionIdentifier.None;
            }
        }

        public CountryInfo.CountryDomain? CountryDomain
        {
            get
            {
                return Country?.Domain;
            }
        }

        public ImmutableHashSet<CountryIdentifier>? CountryBorder
        {
            get
            {
                return Country?.Border;
            }
        }

        public CountryInfo.CountryCurrency? CountryCurrency
        {
            get
            {
                return Country?.Currency;
            }
        }

        public CountryInfo.CountryCalling? CountryCalling
        {
            get
            {
                return Country?.Calling;
            }
        }

        public String? CurrencySymbol
        {
            get
            {
                return Currency?.Symbol;
            }
        }

        public String? CurrencyName
        {
            get
            {
                return Currency?.Name;
            }
        }

        public String? CurrencyFullName
        {
            get
            {
                return Currency?.FullName;
            }
        }

        public String? ThreeLetterISOCurrencyName
        {
            get
            {
                return Currency?.ThreeLetterISOCurrencyName;
            }
        }

        public CurrencyIdentifier CurrencyIdentifier
        {
            get
            {
                return Currency?.Identifier ?? CurrencyIdentifier.Default;
            }
        }

        public Int32 CurrencyCode
        {
            get
            {
                return Currency?.Code ?? 0;
            }
        }

        public UInt16 CurrencyCode16
        {
            get
            {
                return Currency?.Code16 ?? 0;
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
            return Info?.GetNativeLanguageName() ?? Code.ToString();
        }
    }
}