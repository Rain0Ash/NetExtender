// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Wrappers
{
    internal sealed class ReadOnlyLocalizationConfigWrapper : IReadOnlyLocalizationConfig
    {
        private ILocalizationConfig Config { get; }

        event ConfigurationChangedEventHandler? IReadOnlyConfig.Changed
        {
            add
            {
                ((IConfig) Config).Changed += value;
            }
            remove
            {
                ((IConfig) Config).Changed -= value;
            }
        }

        public event LocalizationChangedEventHandler Changed
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

        public event LocalizationValueChangedEventHandler ValueChanged
        {
            add
            {
                Config.ValueChanged += value;
            }
            remove
            {
                Config.ValueChanged -= value;
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

        public LocalizationOptions LocalizationOptions
        {
            get
            {
                return Config.LocalizationOptions;
            }
        }

        public Boolean ThreeLetterName
        {
            get
            {
                return Config.ThreeLetterName;
            }
        }

        public Boolean WithoutSystem
        {
            get
            {
                return Config.WithoutSystem;
            }
        }

        public LocalizationIdentifier Default
        {
            get
            {
                return Config.Default;
            }
        }

        public LocalizationIdentifier System
        {
            get
            {
                return Config.System;
            }
        }

        public LocalizationIdentifier Localization
        {
            get
            {
                return Config.Localization;
            }
            set
            {
                Config.Localization = value;
            }
        }

        public LocalizationIdentifierBehaviorComparer Comparer
        {
            get
            {
                return Config.Comparer;
            }
        }

        public ILocalizationConverter Converter
        {
            get
            {
                return Config.Converter;
            }
        }

        public ReadOnlyLocalizationConfigWrapper(ILocalizationConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        String? IReadOnlyConfig.GetValue(String? key, params String[]? sections)
        {
            return ((IConfig) Config).GetValue(key, sections);
        }

        String? IReadOnlyConfig.GetValue(String? key, IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetValue(key, sections);
        }

        String? IReadOnlyConfig.GetValue(String? key, String? alternate, params String[]? sections)
        {
            return ((IConfig) Config).GetValue(key, alternate, sections);
        }

        String? IReadOnlyConfig.GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetValue(key, alternate, sections);
        }

        public ILocalizationString? GetValue(String? key, params String[]? sections)
        {
            return Config.GetValue(key, sections);
        }

        public ILocalizationString? GetValue(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, sections);
        }

        public ILocalizationString? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public ILocalizationString? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetValue(key, identifier, sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, identifier, sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return Config.GetValue(key, identifier, alternate, sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, identifier, alternate, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, params String[]? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, token, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return ((IConfig) Config).GetValueAsync(key, sections, token);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, alternate, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, alternate, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return ((IConfig) Config).GetValueAsync(key, alternate, token, sections);
        }

        Task<String?> IReadOnlyConfig.GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return ((IConfig) Config).GetValueAsync(key, alternate, sections, token);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, params String[]? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, token, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, sections, token);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, token, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, sections, token);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, token, sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetValueAsync(key, identifier, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, identifier, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, identifier, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, identifier, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return Config.GetValueAsync(key, identifier, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, identifier, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, identifier, alternate, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, identifier, alternate, sections, token);
        }

        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Boolean KeyExist(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.KeyExist(key, identifier, sections);
        }

        public Boolean KeyExist(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.KeyExist(key, identifier, sections);
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

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.KeyExistAsync(key, identifier, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.KeyExistAsync(key, identifier, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return Config.KeyExistAsync(key, identifier, token, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.KeyExistAsync(key, identifier, sections, token);
        }

        ConfigurationEntry[]? IConfigInfo.GetExists()
        {
            return ((IConfig) Config).GetExists();
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync()
        {
            return ((IConfig) Config).GetExistsAsync();
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync(CancellationToken token)
        {
            return ((IConfig) Config).GetExistsAsync(token);
        }

        ConfigurationEntry[]? IConfigInfo.GetExists(params String[]? sections)
        {
            return ((IConfig) Config).GetExists(sections);
        }

        ConfigurationEntry[]? IConfigInfo.GetExists(IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetExists(sections);
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync(params String[]? sections)
        {
            return ((IConfig) Config).GetExistsAsync(sections);
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync(IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetExistsAsync(sections);
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync(CancellationToken token, params String[]? sections)
        {
            return ((IConfig) Config).GetExistsAsync(token, sections);
        }

        Task<ConfigurationEntry[]?> IConfigInfo.GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return ((IConfig) Config).GetExistsAsync(sections, token);
        }

        public LocalizationMultiEntry[]? GetExists()
        {
            return Config.GetExists();
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync()
        {
            return Config.GetExistsAsync();
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return Config.GetExistsAsync(token);
        }

        public LocalizationMultiEntry[]? GetExists(params String[]? sections)
        {
            return Config.GetExists(sections);
        }

        public LocalizationMultiEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Config.GetExists(sections);
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync(params String[]? sections)
        {
            return Config.GetExistsAsync(sections);
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections)
        {
            return Config.GetExistsAsync(sections);
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsAsync(token, sections);
        }

        public Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsAsync(sections, token);
        }

        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier)
        {
            return Config.GetExists(identifier);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier)
        {
            return Config.GetExistsAsync(identifier);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token)
        {
            return Config.GetExistsAsync(identifier, token);
        }

        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetExists(identifier, sections);
        }

        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetExists(identifier, sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetExistsAsync(identifier, sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetExistsAsync(identifier, sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsAsync(identifier, token, sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsAsync(identifier, sections, token);
        }

        ConfigurationValueEntry[]? IConfigInfo.GetExistsValues()
        {
            return ((IConfig) Config).GetExistsValues();
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync()
        {
            return ((IConfig) Config).GetExistsValuesAsync();
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync(CancellationToken token)
        {
            return ((IConfig) Config).GetExistsValuesAsync(token);
        }

        ConfigurationValueEntry[]? IConfigInfo.GetExistsValues(params String[]? sections)
        {
            return ((IConfig) Config).GetExistsValues(sections);
        }

        ConfigurationValueEntry[]? IConfigInfo.GetExistsValues(IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetExistsValues(sections);
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync(params String[]? sections)
        {
            return ((IConfig) Config).GetExistsValuesAsync(sections);
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync(IEnumerable<String>? sections)
        {
            return ((IConfig) Config).GetExistsValuesAsync(sections);
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync(CancellationToken token, params String[]? sections)
        {
            return ((IConfig) Config).GetExistsValuesAsync(token, sections);
        }

        Task<ConfigurationValueEntry[]?> IConfigInfo.GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return ((IConfig) Config).GetExistsValuesAsync(sections, token);
        }

        public LocalizationValueEntry[]? GetExistsValues()
        {
            return Config.GetExistsValues();
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync()
        {
            return Config.GetExistsValuesAsync();
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return Config.GetExistsValuesAsync(token);
        }

        public LocalizationValueEntry[]? GetExistsValues(params String[]? sections)
        {
            return Config.GetExistsValues(sections);
        }

        public LocalizationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Config.GetExistsValues(sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(params String[]? sections)
        {
            return Config.GetExistsValuesAsync(sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections)
        {
            return Config.GetExistsValuesAsync(sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsValuesAsync(token, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsValuesAsync(sections, token);
        }

        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier)
        {
            return Config.GetExistsValues(identifier);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier)
        {
            return Config.GetExistsValuesAsync(identifier);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token)
        {
            return Config.GetExistsValuesAsync(identifier, token);
        }

        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetExistsValues(identifier, sections);
        }

        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetExistsValues(identifier, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, params String[]? sections)
        {
            return Config.GetExistsValuesAsync(identifier, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Config.GetExistsValuesAsync(identifier, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsValuesAsync(identifier, token, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsValuesAsync(identifier, sections, token);
        }

        public LocalizationMultiValueEntry[]? GetExistsMultiValues()
        {
            return Config.GetExistsMultiValues();
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync()
        {
            return Config.GetExistsMultiValuesAsync();
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token)
        {
            return Config.GetExistsMultiValuesAsync(token);
        }

        public LocalizationMultiValueEntry[]? GetExistsMultiValues(params String[]? sections)
        {
            return Config.GetExistsMultiValues(sections);
        }

        public LocalizationMultiValueEntry[]? GetExistsMultiValues(IEnumerable<String>? sections)
        {
            return Config.GetExistsMultiValues(sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(params String[]? sections)
        {
            return Config.GetExistsMultiValuesAsync(sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections)
        {
            return Config.GetExistsMultiValuesAsync(sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token, params String[]? sections)
        {
            return Config.GetExistsMultiValuesAsync(token, sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetExistsMultiValuesAsync(sections, token);
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

        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Config.Difference(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Config.DifferenceAsync(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Config.DifferenceAsync(entries, token);
        }

        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Config.Difference(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Config.DifferenceAsync(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Config.DifferenceAsync(entries, token);
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

        public void CopyTo(ILocalizationConfig config)
        {
            Config.CopyTo(config);
        }

        public Task CopyToAsync(ILocalizationConfig config)
        {
            return Config.CopyToAsync(config);
        }

        public Task CopyToAsync(ILocalizationConfig config, CancellationToken token)
        {
            return Config.CopyToAsync(config, token);
        }

        String? IReadOnlyConfig.this[String? key, params String[]? sections]
        {
            get
            {
                return ((IConfig) Config)[key, sections];
            }
        }

        String? IReadOnlyConfig.this[String? key, IEnumerable<String>? sections]
        {
            get
            {
                return ((IConfig) Config)[key, sections];
            }
        }

        public ILocalizationString? this[String? key, params String[]? sections]
        {
            get
            {
                return Config[key, sections];
            }
        }

        public ILocalizationString? this[String? key, IEnumerable<String>? sections]
        {
            get
            {
                return Config[key, sections];
            }
        }

        public String? this[String? key, LocalizationIdentifier identifier, params String[]? sections]
        {
            get
            {
                return Config[key, identifier, sections];
            }
        }

        public String? this[String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections]
        {
            get
            {
                return Config[key, identifier, sections];
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