// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationProperty : ILocalizationPropertyInfo, IConfigProperty<ILocalizationString?>
    {
        public event EventHandler Reseted;
        public new event LocalizationValueChangedEventHandler Changed;
        public String? Current { get; }
    }

    public interface ILocalizationIdentifierProperty : ILocalizationIdentifierPropertyInfo, IConfigProperty
    {
        public new event LocalizationValueChangedEventHandler Changed;
    }
}