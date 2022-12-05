// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Region;

namespace NetExtender.Utilities.Types
{
    public static class RegionUtilities
    {
        public static RegionIdentifier ToRegionIdentifier(this SubregionIdentifier identifier)
        {
            return identifier switch
            {
                SubregionIdentifier.None => RegionIdentifier.None,
                SubregionIdentifier.NorthernEurope => RegionIdentifier.Europe,
                SubregionIdentifier.WesternEurope => RegionIdentifier.Europe,
                SubregionIdentifier.SouthernEurope => RegionIdentifier.Europe,
                SubregionIdentifier.EasternEurope => RegionIdentifier.Europe,
                SubregionIdentifier.CentralAmerica => RegionIdentifier.America,
                SubregionIdentifier.NorthAmerica => RegionIdentifier.America,
                SubregionIdentifier.SouthAmerica => RegionIdentifier.America,
                SubregionIdentifier.CentralAsia => RegionIdentifier.Asia,
                SubregionIdentifier.EasternAsia => RegionIdentifier.Asia,
                SubregionIdentifier.SouthernAsia => RegionIdentifier.Asia,
                SubregionIdentifier.SouthEasternAsia => RegionIdentifier.Asia,
                SubregionIdentifier.WesternAsia => RegionIdentifier.Asia,
                SubregionIdentifier.EasternAfrica => RegionIdentifier.Africa,
                SubregionIdentifier.MiddleAfrica => RegionIdentifier.Africa,
                SubregionIdentifier.NorthernAfrica => RegionIdentifier.Africa,
                SubregionIdentifier.SouthernAfrica => RegionIdentifier.Africa,
                SubregionIdentifier.WesternAfrica => RegionIdentifier.Africa,
                SubregionIdentifier.AustraliaAndNewZealand => RegionIdentifier.Oceania,
                SubregionIdentifier.Melanesia => RegionIdentifier.Oceania,
                SubregionIdentifier.Polynesia => RegionIdentifier.Oceania,
                SubregionIdentifier.Micronesia => RegionIdentifier.Oceania,
                SubregionIdentifier.Caribbean => RegionIdentifier.Oceania,
                SubregionIdentifier.Arctic => RegionIdentifier.Arctic,
                SubregionIdentifier.Antarctic => RegionIdentifier.Antarctic,
                _ => throw new EnumUndefinedOrNotSupportedException<SubregionIdentifier>(identifier, nameof(identifier), null)
            };
        }
    }
}