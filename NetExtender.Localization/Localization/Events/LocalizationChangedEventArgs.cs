// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localization.Common;
using NetExtender.Types.Culture;
using NetExtender.Types.Events;

namespace NetExtender.Localization.Events
{
    public delegate void LocalizationChangedEventHandler(Object? sender, LocalizationChangedEventArgs args);
    public delegate void LocalizationValueChangedEventHandler(Object? sender, LocalizationValueChangedEventArgs args);

    public class LocalizationChangedEventArgs : HandledEventArgs<LocalizationIdentifier>
    {
        public LocalizationChangedEventArgs(LocalizationIdentifier value)
            : base(value)
        {
        }

        public LocalizationChangedEventArgs(LocalizationIdentifier value, Boolean handled)
            : base(value, handled)
        {
        }
    }

    public class LocalizationValueChangedEventArgs : HandledEventArgs<LocalizationValueEntry>
    {
        public LocalizationValueChangedEventArgs(LocalizationValueEntry value)
            : base(value)
        {
        }

        public LocalizationValueChangedEventArgs(LocalizationValueEntry value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}