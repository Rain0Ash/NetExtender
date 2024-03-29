// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationPropertyInfo : IConfigPropertyInfo, IFormattable
    {
        public event LocalizationChangedEventHandler LocalizationChanged;
        public LocalizationIdentifier Identifier { get; }
        public String Current { get; }
    }

    public interface ILocalizationIdentifierPropertyInfo : ILocalizationPropertyInfo
    {
        public event LocalizationValueChangedEventHandler Changed;
    }
}