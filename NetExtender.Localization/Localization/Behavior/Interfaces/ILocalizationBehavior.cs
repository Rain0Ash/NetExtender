// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Behavior.Interfaces
{
    public interface ILocalizationBehavior : IConfigBehavior
    {
        public new event LocalizationChangedEventHandler Changed;
        public event ConfigurationChangedEventHandler ValueChanged;
        public LocalizationIdentifier Default { get; }
        public LocalizationIdentifier System { get; }
        public LocalizationIdentifier Localization { get; }
        public IComparer<LocalizationIdentifier> Comparer { get; }
        public Boolean Contains(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> ContainsAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public new ILocalizationString? Get(String? key, IEnumerable<String>? sections);
        public new Task<ILocalizationString?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String? Get(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public ILocalizationString? GetOrSet(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<ILocalizationString?> GetOrSetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSet(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<String?> GetOrSetAsync(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetLocalization(LocalizationIdentifier identifier);
    }
}