// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localization.Common;
using NetExtender.Types.Events;

namespace NetExtender.Localization.Events
{
    public delegate void LocalizationChangedEventHandler(Object? sender, LocalizationChangedEventArgs args);
    
    public class LocalizationChangedEventArgs : TypeHandledEventArgs<LocalizationEntry>
    {
        public LocalizationChangedEventArgs(LocalizationEntry value)
            : base(value)
        {
        }

        public LocalizationChangedEventArgs(LocalizationEntry value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}