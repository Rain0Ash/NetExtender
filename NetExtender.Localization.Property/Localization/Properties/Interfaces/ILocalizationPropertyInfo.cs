// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Properties.Interfaces;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationPropertyInfo : IConfigPropertyInfo, ILocalizationStringInfo
    {
    }

    public interface ILocalizationIdentifierPropertyInfo : ILocalizationPropertyInfo, ILocalizationIdentifierInfo
    {
    }
}