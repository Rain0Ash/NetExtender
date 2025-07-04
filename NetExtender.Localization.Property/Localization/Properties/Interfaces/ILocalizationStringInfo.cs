// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using NetExtender.Localization.Events;
using NetExtender.Types.Culture;
using NetExtender.Types.Formattable.Interfaces;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationIdentifierInfo : ILocalizationStringInfo
    {
        public event LocalizationValueChangedEventHandler Changed;
    }

    public interface ILocalizationStringInfo : IString, ILocalizationFormattable, INotifyPropertyChanged
    {
        public event LocalizationChangedEventHandler LocalizationChanged;
        public LocalizationIdentifier Identifier { get; }
        public String Current { get; }
        public new String? ToString();
    }
}