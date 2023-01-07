// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Configuration.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;

namespace NetExtender.Localization.Interfaces
{
    public interface ILocalizationConfigInfo : IConfigInfo, ILocalizationInfo
    {
        public new event LocalizationChangedEventHandler Changed;
        public event LocalizationValueChangedEventHandler ValueChanged;

        public ILocalizationConverter Converter { get; }
    }
}