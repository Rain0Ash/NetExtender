// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;
using NetExtender.Types.Culture;
using NetExtender.Types.Region;

namespace NetExtender.Utilities.Types
{
    public static class CountryUtilities
    {
        public static CountryInfo ToCountryInfo(this CountryIdentifier identifier)
        {
            return identifier;
        }

        public static CountryInfo? ToCountryInfo(this CountryInfo? info)
        {
            return info;
        }

        public static CountryInfo? ToCountryInfo(this CultureIdentifier identifier)
        {
            return identifier;
        }

        public static CountryInfo? ToCountryInfo(this LocalizationIdentifier identifier)
        {
            return identifier;
        }

        public static CountryInfo? ToCountryInfo(this CultureInfo? info)
        {
            return info;
        }

        public static CountryInfo? ToCountryInfo(this RegionInfo? info)
        {
            return info;
        }
    }
}