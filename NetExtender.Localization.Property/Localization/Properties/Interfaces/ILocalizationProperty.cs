// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationProperty : ILocalizationPropertyMultiInfo, IConfigProperty<ILocalizationString?>
    {
    }

    public interface ILocalizationIdentifierProperty : ILocalizationIdentifierPropertyInfo, IConfigProperty
    {
        public new event LocalizationValueChangedEventHandler Changed;
    }
}