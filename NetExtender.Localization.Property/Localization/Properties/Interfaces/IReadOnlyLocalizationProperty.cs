// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface IReadOnlyLocalizationProperty : ILocalizationPropertyMultiInfo, IReadOnlyConfigProperty<ILocalizationString?>
    {
    }

    public interface IReadOnlyLocalizationIdentifierProperty : ILocalizationIdentifierPropertyInfo, IReadOnlyConfigProperty
    {
        public new event LocalizationValueChangedEventHandler Changed;
    }
}