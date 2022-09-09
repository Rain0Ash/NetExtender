// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;
using NetExtender.Initializer.Types.Currency;
using NetExtender.Initializer.Types.Region;
using NetExtender.Types.Culture;

namespace NetExtender.Utilities.Types
{
    public static class CurrencyUtilities
    {
        public static CurrencyInfo ToCurrencyInfo(this CurrencyIdentifier identifier)
        {
            return identifier;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this CountryIdentifier identifier)
        {
            return identifier;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this CountryInfo? info)
        {
            return info;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this CultureIdentifier identifier)
        {
            return identifier;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this LocalizationIdentifier identifier)
        {
            return identifier;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this CultureInfo? info)
        {
            return info;
        }
        
        public static CurrencyInfo? ToCurrencyInfo(this RegionInfo? info)
        {
            return info;
        }
    }
}