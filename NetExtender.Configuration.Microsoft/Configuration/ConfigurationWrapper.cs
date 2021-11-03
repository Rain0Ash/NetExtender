// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Configuration.Microsoft
{
    public sealed class ConfigurationWrapper : IConfiguration
    {
        private IConfig Config { get; }

        public ConfigurationWrapper(IConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IConfigurationSection GetSection(String key)
        {
            return new ConfigurationSection(Config, key);
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            ConfigurationEntry[]? exists = Config.GetExists();

            if (exists is null)
            {
                return Array.Empty<IConfigurationSection>();
            }

            return exists
                .Where(entry => entry.Length <= 0)
                .Select(entry => new ConfigurationSection(Config, entry.Key, entry.Sections));
        }

        public IChangeToken GetReloadToken()
        {
            return new ChangeToken(Config);
        }

        public String? this[String key]
        {
            get
            {
                return Config.GetValue(key);
            }
            set
            {
                Config.SetValue(key, value);
            }
        }
        
        private sealed class ConfigurationSection : IConfigurationSection
        {
            private IConfig Config { get; }
            
            public String? Path { get; init; }
            
            public String? Key { get; }

            public String? Value
            {
                get
                {
                    return Config.GetValue(Key, Sections);
                }
                set
                {
                    if (Config.IsReadOnly)
                    {
                        return;
                    }

                    Config.SetValue(Key, value, Sections);
                }
            }
            
            public ImmutableArray<String> Sections { get; }

            public ConfigurationSection(IConfig config, String? key)
                : this(config, key, ImmutableArray<String>.Empty)
            {
            }
            
            public ConfigurationSection(IConfig config, String? key, ImmutableArray<String> sections)
            {
                Config = config ?? throw new ArgumentNullException(nameof(config));
                
                Key = key;
                Sections = sections;
            }
            
            public IConfigurationSection GetSection(String key)
            {
                return new ConfigurationSection(Config, key, Key is not null ? Sections.Add(Key) : Sections);
            }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                ConfigurationEntry[]? exists = Config.GetExists();

                if (exists is null)
                {
                    return Array.Empty<IConfigurationSection>();
                }

                return exists
                    .Where(entry => entry.Length == Sections.Length && entry.Sections.SequenceEqual(Sections))
                    .Select(entry => new ConfigurationSection(Config, entry.Key, entry.Sections));
            }

            public IChangeToken GetReloadToken()
            {
                return new ChangeToken(Config);
            }

            public String? this[String key]
            {
                get
                {
                    return Config.GetValue(key, Sections);
                }
                set
                {
                    Config.SetValue(key, value, Sections);
                }
            }
        }

        private sealed class ChangeToken : IChangeToken, IDisposable
        {
            private IConfig Config { get; }
            
            private CancellationTokenSource Source { get; } = new CancellationTokenSource();

            public Boolean HasChanged
            {
                get
                {
                    return Source.IsCancellationRequested;
                }
            }

            public Boolean ActiveChangeCallbacks
            {
                get
                {
                    return true;
                }
            }

            public ChangeToken(IConfig config)
            {
                Config = config ?? throw new ArgumentNullException(nameof(config));
                Config.Changed += Dispose;
            }

            public IDisposable RegisterChangeCallback(Action<Object?> callback, Object? state)
            {
                return Source.Token.Register(callback, state);
            }

            public void Dispose()
            {
                Config.Changed -= Dispose;
                Source.Cancel();
                Source.Dispose();
            }
            
            private void Dispose(Object? sender, ConfigurationEntry value)
            {
                Dispose();
            }
        }
    }
}