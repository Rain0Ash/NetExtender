// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Common;
using NetExtender.Types.Events;

namespace NetExtender.Configuration
{
    public delegate void ConfigurationChangedEventHandler(Object? sender, ConfigurationChangedEventArgs args);
    
    public class ConfigurationChangedEventArgs : TypeHandledEventArgs<ConfigurationEntry>
    {
        public ConfigurationChangedEventArgs(ConfigurationEntry value)
            : base(value)
        {
        }

        public ConfigurationChangedEventArgs(ConfigurationEntry value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}