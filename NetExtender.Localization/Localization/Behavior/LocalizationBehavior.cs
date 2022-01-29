// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Behavior
{
    public class LocalizationBehavior : ILocalizationBehavior
    {
        protected IConfigBehavior Behavior { get; }

        public event LocalizationChangedEventHandler Changed = null!;
        public event ConfigurationChangedEventHandler ValueChanged = null!;
        
        event ConfigurationChangedEventHandler IConfigBehavior.Changed
        {
            add
            {
                ValueChanged += value;
            }
            remove
            {
                ValueChanged -= value;
            }
        }

        public LocalizationIdentifier Default
        {
            get
            {
                return CultureIdentifier.En;
            }
        }

        public LocalizationIdentifier System { get; }
        public LocalizationIdentifier Localization { get; private set; }

        public IComparer<LocalizationIdentifier> Comparer { get; }

        public String Path
        {
            get
            {
                return Behavior.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Behavior.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Behavior.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Behavior.IsIgnoreEvent;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Behavior.IsLazyWrite;
            }
        }

        public String Joiner
        {
            get
            {
                return Behavior.Joiner;
            }
        }

        public LocalizationOptions LocalizationOptions { get; }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationOptions options)
            : this(behavior, default(LocalizationIdentifier), options)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationOptions options)
            : this(behavior, localization, default(LocalizationIdentifier), options)
        {
        }
        
        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, LocalizationOptions options)
            : this(behavior, localization, system, null, options)
        {
        }
        
        public LocalizationBehavior(IConfigBehavior behavior, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
            : this(behavior, default, comparer, options)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
            : this(behavior, localization, default, comparer, options)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Behavior.Changed += OnChanged;
            LocalizationOptions = options;
            System = system == default(LocalizationIdentifier) ? Default : system;
            Localization = localization == default(LocalizationIdentifier) ? System : localization;
            Comparer = comparer ?? LocalizationComparer.Default;
        }

        private void OnChanged(Object? sender, ConfigurationChangedEventArgs args)
        {
            ValueChanged?.Invoke(this, args);
        }

        [return: NotNullIfNotNull("sections")]
        protected virtual IEnumerable<String>? Convert(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            Convert(null, identifier, ref sections);
            return sections;
        }
        
        protected virtual IEnumerable<String>? Convert(String? key, IEnumerable<String>? sections)
        {
            if (key is not null)
            {
                sections = sections.AppendOr(key);
            }

            return sections;
        }
        
        protected virtual String Convert(String? key, LocalizationIdentifier identifier, ref IEnumerable<String>? sections)
        {
            sections = Convert(key, sections);
            return identifier.TryGetCultureInfo(out CultureInfo info) ? info.TwoLetterISOLanguageName : identifier.ToString();
        }

        protected Int32 IdentifierComparison(String? first, String? second)
        {
            if (first is null || !CultureUtilities.TryGetCultureIdentifier(first, out CultureIdentifier x))
            {
                return second is null || !CultureUtilities.TryGetCultureIdentifier(second, out CultureIdentifier _) ? 0 : -1;
            }

            if (second is null || !CultureUtilities.TryGetCultureIdentifier(second, out CultureIdentifier y))
            {
                return 1;
            }

            if (x == Localization)
            {
                return 4;
            }

            if (x == System)
            {
                return 3;
            }

            if (x == Default)
            {
                return 2;
            }

            return Comparer.Compare(x, y);
        }

        protected virtual IEnumerable<KeyValuePair<LocalizationIdentifier, String>> Extract(IEnumerable<ConfigurationValueEntry> entries)
        {
            static Boolean Parse(ConfigurationValueEntry entry, out KeyValuePair<LocalizationIdentifier, String> result)
            {
                (String? key, String? value, _) = entry;
                if (key is not null && value is not null && CultureUtilities.TryGetCultureIdentifier(key, out CultureIdentifier identifier))
                {
                    result = new KeyValuePair<LocalizationIdentifier, String>(identifier, value);
                    return true;
                }

                result = default;
                return false;
            }
            
            return entries.TryParse<ConfigurationValueEntry, KeyValuePair<LocalizationIdentifier, String>>(Parse);
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, sections);
        }

        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, sections, token);
        }
        
        public Boolean Contains(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Contains(Convert(key, identifier, ref sections), sections);
        }

        public Task<Boolean> ContainsAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(Convert(key, identifier, ref sections), sections, token);
        }
        
        public ILocalizationString? Get(String? key, IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(Convert(key, sections));
            return entries is not null ? new LocalizationString(this, Extract(entries), Comparer) : null;
        }

        public async Task<ILocalizationString?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(Convert(key, sections), token);
            return entries is not null ? new LocalizationString(this, Extract(entries), Comparer) : null;
        }

        String? IConfigBehavior.Get(String? key, IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = GetExistsValues(Convert(key, sections));

            if (entries is null)
            {
                return null;
            }
            
            return entries.OrderBy(entry => entry.Key, IdentifierComparison).TryGetFirst(entry => entry.Value is not null, out ConfigurationValueEntry result) ? result.Value : null;
        }

        async Task<String?> IConfigBehavior.GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await GetExistsValuesAsync(Convert(key, sections), token);

            if (entries is null)
            {
                return null;
            }
            
            return entries.OrderBy(entry => entry.Key, IdentifierComparison).TryGetFirst(entry => entry.Value is not null, out ConfigurationValueEntry result) ? result.Value : null;
        }

        public String? Get(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Get(Convert(key, identifier, ref sections), sections);
        }

        public Task<String?> GetAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(Convert(key, identifier, ref sections), sections, token);
        }
        
        public Boolean Set(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            sections = Convert(key, sections)?.ToArray();
            ConfigurationEntry[]? entries = GetExists(sections);

            if (entries is not null)
            {
                foreach ((String? entrykey, ImmutableArray<String> entrysections) in entries)
                {
                    Behavior.Set(entrykey, null, entrysections);
                }
            }

            return true;

            /*if (value is null)
            {
                return true;
            }

            if (value.Count <= 0)
            {
                return true;
            }
            
            Boolean successful = false;
            foreach (KeyValuePair<LocalizationIdentifier, String> item in value.Values)
            {
                IEnumerable<String>? temp = sections;
                successful |= Behavior.Set(Convert(null, item.Key, ref temp), item.Value, temp);
            }

            return successful;*/
        }

        public async Task<Boolean> SetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            sections = Convert(key, sections)?.ToArray();
            ConfigurationEntry[]? entries = await GetExistsAsync(sections, token);

            if (entries is not null)
            {
                foreach ((String? entrykey, ImmutableArray<String> entrysections) in entries)
                {
                    await Behavior.SetAsync(entrykey, null, entrysections, token);
                }
            }

            if (value is null)
            {
                return true;
            }

            if (value.Count <= 0)
            {
                return true;
            }
            
            Boolean successful = false;
            foreach (KeyValuePair<LocalizationIdentifier, String> item in value.Values)
            {
                IEnumerable<String>? temp = sections;
                successful |= await Behavior.SetAsync(Convert(null, item.Key, ref temp), item.Value, temp, token);
            }

            return successful;
        }

        Boolean IConfigBehavior.Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(Convert(key, Localization, ref sections), value, sections);
        }

        Task<Boolean> IConfigBehavior.SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(Convert(key, Localization, ref sections), value, sections, token);
        }
        
        public Boolean Set(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Set(Convert(key, identifier, ref sections), value, sections);
        }

        public Task<Boolean> SetAsync(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(Convert(key, identifier, ref sections), value, sections, token);
        }

        public ILocalizationString? GetOrSet(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            throw new NotImplementedException();
        }

        public Task<ILocalizationString?> GetOrSetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        String? IConfigBehavior.GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            throw new NotImplementedException();
        }

        Task<String?> IConfigBehavior.GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        public String? GetOrSet(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(Convert(key, identifier, ref sections), value, sections);
        }

        public Task<String?> GetOrSetAsync(String? key, String? value, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(Convert(key, identifier, ref sections), value, sections, token);
        }

        protected virtual Boolean ExistsIdentifierEquals(String? key, LocalizationIdentifier identifier)
        {
            return key is not null && CultureUtilities.TryGetCultureIdentifier(key, out CultureIdentifier result) && result == identifier;
        }

        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
        }
        
        public ConfigurationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections)?.Where(entry => ExistsIdentifierEquals(entry.Key, identifier)).ToArray();
        }

        public async Task<ConfigurationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return (await Behavior.GetExistsAsync(sections, token))?.Where(entry => ExistsIdentifierEquals(entry.Key, identifier)).ToArray();
        }

        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }
        
        public ConfigurationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections)?.Where(entry => ExistsIdentifierEquals(entry.Key, identifier)).ToArray();
        }

        public async Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return (await Behavior.GetExistsValuesAsync(sections, token))?.Where(entry => ExistsIdentifierEquals(entry.Key, identifier)).ToArray();
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        public Boolean Reset()
        {
            return Behavior.Reset();
        }

        public Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Behavior.ResetAsync(token);
        }
        
        public Boolean SetLocalization(LocalizationIdentifier identifier)
        {
            Localization = identifier;
            Changed?.Invoke(this, new LocalizationChangedEventArgs(Localization));
            return true;
        }

        public void Dispose()
        {
            Behavior.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await Behavior.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}