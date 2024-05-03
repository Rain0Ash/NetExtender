using System;
using System.ComponentModel;
using NetExtender.Localization.Events;
using NetExtender.Types.Culture;
using NetExtender.Types.Formattable.Interfaces;

namespace NetExtender.Localization.Properties.Interfaces
{
    public interface ILocalizationStringInfo : ILocalizationFormattable, INotifyPropertyChanged
    {
        public event LocalizationChangedEventHandler LocalizationChanged;
        public LocalizationIdentifier Identifier { get; }
        public String Current { get; }
    }

    public interface ILocalizationIdentifierInfo : ILocalizationStringInfo
    {
        public event LocalizationValueChangedEventHandler Changed;
    }
}