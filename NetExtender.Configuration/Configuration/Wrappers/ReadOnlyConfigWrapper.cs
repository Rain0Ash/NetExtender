// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Configuration.Wrappers
{
    internal sealed class ReadOnlyConfigWrapper : IReadOnlyConfig
    {
        private IConfig Config { get; }
        
        public event ConfigurationChangedEventHandler? Changed
        {
            add
            {
                Config.Changed += value;
            }
            remove
            {
                Config.Changed -= value;
            }
        }

        public String Path
        {
            get
            {
                return Config.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Config.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Config.IsLazyWrite;
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return Config.IsThreadSafe;
            }
        }

        internal ReadOnlyConfigWrapper(IConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public String? GetValue(String? key, params String[]? sections)
        {
            return Config.GetValue(key, sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, sections);
        }

        public String? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, params String[]? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, sections, token);
        }

        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections)
        {
            return Config.KeyExistAsync(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExistAsync(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.KeyExistAsync(key, token, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.KeyExistAsync(key, sections, token);
        }

        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Config.Difference(entries);
        }
        
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Config.DifferenceAsync(entries);
        }

        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Config.DifferenceAsync(entries, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return Config.GetExists();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync()
        {
            return Config.GetExistsAsync();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return Config.GetExistsAsync(token);
        }

        public ConfigurationEntry[]? GetExists(params String[]? sections)
        {
            return Config.GetExists(sections);
        }

        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Config.GetExists(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(params String[]? sections)
        {
            return Config.GetExistsAsync(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections)
        {
            return Config.GetExistsAsync(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsAsync(token, sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsAsync(sections, token);
        }

        public ConfigurationValueEntry[]? GetExistsValues()
        {
            return Config.GetExistsValues();
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync()
        {
            return Config.GetExistsValuesAsync();
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return Config.GetExistsValuesAsync(token);
        }

        public ConfigurationValueEntry[]? GetExistsValues(params String[]? sections)
        {
            return Config.GetExistsValues(sections);
        }

        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Config.GetExistsValues(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(params String[]? sections)
        {
            return Config.GetExistsValuesAsync(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections)
        {
            return Config.GetExistsValuesAsync(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsValuesAsync(token, sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsValuesAsync(sections, token);
        }

        public void CopyTo(IConfig config)
        {
            Config.CopyTo(config);
        }

        public Task CopyToAsync(IConfig config)
        {
            return Config.CopyToAsync(config);
        }

        public Task CopyToAsync(IConfig config, CancellationToken token)
        {
            return Config.CopyToAsync(config, token);
        }

        public String? this[String? key, params String[]? sections]
        {
            get
            {
                return Config[key, sections];
            }
        }

        public String? this[String? key, IEnumerable<String>? sections]
        {
            get
            {
                return Config[key, sections];
            }
        }
        
        public IEnumerator<ConfigurationEntry> GetEnumerator()
        {
            return Config.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Config).GetEnumerator();
        }
    }
}