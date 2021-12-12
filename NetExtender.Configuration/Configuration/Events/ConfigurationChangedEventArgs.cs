// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Common;
using NetExtender.Types.Events;

namespace NetExtender.Configuration
{
    public delegate void ConfigurationChangedEventHandler(Object? sender, ConfigurationChangedEventArgs args);
    public delegate void ConfigurationChangedEventHandler<T>(Object? sender, ConfigurationChangedEventArgs<T> args);
    
    public class ConfigurationChangedEventArgs : HandledEventArgs<ConfigurationValueEntry>
    {
        public ConfigurationChangedEventArgs(ConfigurationValueEntry value)
            : base(value)
        {
        }

        public ConfigurationChangedEventArgs(ConfigurationValueEntry value, Boolean handled)
            : base(value, handled)
        {
        }
    }
    
    public class ConfigurationChangedEventArgs<T> : HandledEventArgs<ConfigurationValueEntry<T>>
    {
        public ConfigurationChangedEventArgs(ConfigurationValueEntry<T> value)
            : base(value)
        {
        }

        public ConfigurationChangedEventArgs(ConfigurationValueEntry<T> value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}