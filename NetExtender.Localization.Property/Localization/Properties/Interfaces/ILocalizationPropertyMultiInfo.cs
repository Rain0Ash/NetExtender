// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationPropertyMultiInfo : ILocalizationPropertyInfo, IConfigPropertyValueInfo<ILocalizationString?>
    {
        public event EventHandler StringChanged;
        public new event LocalizationValueChangedEventHandler Changed;
        public Int32 Count { get; }

        public String? this[LocalizationIdentifier identifier] { get; }
    }
}