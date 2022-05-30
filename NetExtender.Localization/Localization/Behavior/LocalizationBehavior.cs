// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Memory;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Behavior.Transactions;
using NetExtender.Localization.Behavior.Transactions.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Utilities;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Behavior
{
    public class LocalizationBehavior : ILocalizationBehavior
    {
        protected IConfigBehavior Behavior { get; }

        public event LocalizationChangedEventHandler? Changed;
        public event ConfigurationChangedEventHandler? ValueChanged;
        
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

        private LocalizationIdentifier _identifier;
        public LocalizationIdentifier Localization
        {
            get
            {
                return _identifier;
            }
            set
            {
                _identifier = value;
                Changed?.Invoke(this, new LocalizationChangedEventArgs(Localization));
            }
        }

        public LocalizationIdentifierBehaviorComparer Comparer { get; }

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

        public Boolean IsThreadSafe
        {
            get
            {
                return Behavior.IsThreadSafe;
            }
        }

        public LocalizationOptions LocalizationOptions { get; }

        public Boolean ThreeLetterName
        {
            get
            {
                return LocalizationOptions.HasFlag(LocalizationOptions.ThreeLetterName);
            }
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationOptions options)
            : this(behavior, default(LocalizationIdentifier), options)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationOptions options)
            : this(behavior, localization, default, options)
        {
        }
        
        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, LocalizationOptions options)
            : this(behavior, localization, system, options, null)
        {
        }
        
        public LocalizationBehavior(IConfigBehavior behavior, IComparer<LocalizationIdentifier>? comparer, LocalizationOptions options)
            : this(behavior, default, options, comparer)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
            : this(behavior, localization, default, options, comparer)
        {
        }

        public LocalizationBehavior(IConfigBehavior behavior, LocalizationIdentifier localization, LocalizationIdentifier system, LocalizationOptions options, IComparer<LocalizationIdentifier>? comparer)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Behavior.Changed += OnChanged;
            LocalizationOptions = options;
            System = system == default(LocalizationIdentifier) ? Default : system;
            Localization = localization == default(LocalizationIdentifier) ? System : localization;
            Comparer = new LocalizationIdentifierBehaviorComparer(this, comparer);
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

            if (!identifier.TryGetCultureInfo(out CultureInfo info))
            {
                return identifier.ToString();
            }
            
            return ThreeLetterName ? info.ThreeLetterISOLanguageName : info.TwoLetterISOLanguageName;
        }

        protected virtual Boolean Extract(ConfigurationEntry entry, out LocalizationEntry result)
        {
            return LocalizationEntry.TryConvert(entry, out result);
        }

        protected virtual Boolean Extract(ConfigurationValueEntry entry, out LocalizationValueEntry result)
        {
            return LocalizationValueEntry.TryConvert(entry, out result);
        }
        
        [return: NotNullIfNotNull("entries")]
        protected IEnumerable<LocalizationEntry>? Extract(IEnumerable<ConfigurationEntry>? entries)
        {
            return entries?.TryParse<ConfigurationEntry, LocalizationEntry>(Extract);
        }
        
        [return: NotNullIfNotNull("entries")]
        protected IEnumerable<LocalizationValueEntry>? Extract(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return entries?.TryParse<ConfigurationValueEntry, LocalizationValueEntry>(Extract);
        }
        
        protected virtual Boolean Pack(LocalizationEntry entry, out ConfigurationEntry result)
        {
            result = entry;
            return true;
        }

        protected virtual Boolean Pack(LocalizationValueEntry entry, out ConfigurationValueEntry result)
        {
            result = entry;
            return true;
        }
        
        protected virtual Boolean Pack(LocalizationMultiValueEntry entry, out ConfigurationValueEntry[] result)
        {
            result = entry;
            return true;
        }
        
        [return: NotNullIfNotNull("entries")]
        protected IEnumerable<ConfigurationEntry>? Pack(IEnumerable<LocalizationEntry>? entries)
        {
            return entries?.TryParse<LocalizationEntry, ConfigurationEntry>(Pack);
        }
        
        [return: NotNullIfNotNull("entries")]
        protected IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationValueEntry>? entries)
        {
            return entries?.TryParse<LocalizationValueEntry, ConfigurationValueEntry>(Pack);
        }
        
        [return: NotNullIfNotNull("entries")]
        protected IEnumerable<ConfigurationValueEntry>? Pack(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return entries?.TryParse<LocalizationMultiValueEntry, ConfigurationValueEntry[]>(Pack).SelectMany();
        }
        
        public virtual Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(Convert(key, sections));
            return entries is not null && entries.Length > 0;
        }

        public virtual async Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(Convert(key, sections), token);
            return entries is not null && entries.Length > 0;
        }
        
        public virtual Boolean Contains(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Contains(Convert(key, identifier, ref sections), sections);
        }

        public virtual Task<Boolean> ContainsAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(Convert(key, identifier, ref sections), sections, token);
        }
        
        public virtual ILocalizationString? Get(String? key, IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(Convert(key, sections));
            return entries is not null && entries.Length > 0 ? new LocalizationString(this, Extract(entries)) : null;
        }

        public virtual async Task<ILocalizationString?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(Convert(key, sections), token);
            return entries is not null && entries.Length > 0 ? new LocalizationString(this, Extract(entries)) : null;
        }
        
        String? IConfigBehavior.Get(String? key, IEnumerable<String>? sections)
        {
            return Get(key, sections)?.ToString();
        }

        async Task<String?> IConfigBehavior.GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return (await GetAsync(key, sections, token))?.ToString();
        }

        public virtual String? Get(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Get(Convert(key, identifier, ref sections), sections);
        }

        public virtual Task<String?> GetAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(Convert(key, identifier, ref sections), sections, token);
        }
        
        public virtual Boolean Set(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            if (value is null)
            {
                return Behavior.Set(key, null, sections);
            }

            sections = sections?.ToImmutableArray();
            
            Boolean successful = false;
            foreach ((LocalizationIdentifier identifier, String? entry) in value)
            {
                successful |= Set(key, identifier, entry, sections);
            }
            
            return successful;
        }

        public virtual async Task<Boolean> SetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            if (value is null)
            {
                return await Behavior.SetAsync(key, null, sections, token);
            }

            sections = sections?.ToImmutableArray();
            
            Boolean successful = false;
            foreach ((LocalizationIdentifier identifier, String? entry) in value)
            {
                successful |= await SetAsync(key, identifier, entry, sections, token);
            }
            
            return successful;
        }

        Boolean IConfigBehavior.Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Set(key, Localization, value, sections);
        }

        Task<Boolean> IConfigBehavior.SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetAsync(key, Localization, value, sections, token);
        }

        public virtual Boolean Set(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(Convert(key, identifier, ref sections), value, sections);
        }

        public virtual Task<Boolean> SetAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(Convert(key, identifier, ref sections), value, sections, token);
        }

        public virtual ILocalizationString? GetOrSet(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            sections = sections?.ToImmutableArray();
            ILocalizationString? entry = Get(key, sections);
            
            if (entry is not null)
            {
                return entry;
            }

            if (IsReadOnly)
            {
                return null;
            }

            return Set(key, value, sections) ? Get(key, sections) : null;
        }

        public virtual async Task<ILocalizationString?> GetOrSetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            sections = sections?.ToImmutableArray();
            ILocalizationString? entry = await GetAsync(key, sections, token);
            
            if (entry is not null)
            {
                return entry;
            }

            if (IsReadOnly)
            {
                return null;
            }

            return await SetAsync(key, value, sections, token) ? await GetAsync(key, sections, token) : null;
        }

        String? IConfigBehavior.GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSet(key, Localization, value, sections);
        }

        Task<String?> IConfigBehavior.GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetAsync(key, Localization, value, sections, token);
        }

        public virtual String? GetOrSet(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(Convert(key, identifier, ref sections), value, sections);
        }

        public virtual Task<String?> GetOrSetAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(Convert(key, identifier, ref sections), value, sections, token);
        }

        protected virtual IEnumerable<LocalizationMultiEntry> GetExistsInternal(IEnumerable<ConfigurationEntry> values)
        {
            foreach (IGrouping<ImmutableArray<String>, ConfigurationEntry> group in values.GroupBy(entry => entry.Sections))
            {
                using IEnumerator<LocalizationEntry> enumerator = Extract(group).GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    continue;
                }
                    
                yield return new LocalizationMultiEntry(enumerator.Current.Key, enumerator.Current.Sections);
            }
        }
        
        public virtual LocalizationMultiEntry[]? GetExists(IEnumerable<String>? sections)
        {
            ConfigurationEntry[]? entries = Behavior.GetExists(sections);
            return entries is not null ? GetExistsInternal(entries).ToArray() : null;
        }

        public virtual async Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationEntry[]? entries = await Behavior.GetExistsAsync(sections, token);
            return entries is not null ? GetExistsInternal(entries).ToArray() : null;
        }
        
        ConfigurationEntry[]? IConfigBehavior.GetExists(IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections);
        }

        Task<ConfigurationEntry[]?> IConfigBehavior.GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
        }
        
        protected virtual IEnumerable<LocalizationEntry> GetExistsInternal(LocalizationIdentifier identifier, IEnumerable<ConfigurationEntry> values)
        {
            return values.GroupBy(entry => entry.Sections).SelectMany(group => Extract(group).Where(entry => entry.Identifier == identifier));
        }
        
        public virtual LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            ConfigurationEntry[]? entries = Behavior.GetExists(sections);
            return entries is not null ? GetExistsInternal(identifier, entries).ToArray() : null;
        }

        public virtual async Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationEntry[]? entries = await Behavior.GetExistsAsync(sections, token);
            return entries is not null ? GetExistsInternal(identifier, entries).ToArray() : null;
        }
        
        protected virtual IEnumerable<LocalizationValueEntry> GetExistsValuesInternal(IEnumerable<ConfigurationValueEntry> values)
        {
            return Extract(values);
        }

        public virtual LocalizationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(sections);
            return entries is not null ? GetExistsValuesInternal(entries).ToArray() : null;
        }

        public virtual async Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(sections, token);
            return entries is not null ? GetExistsValuesInternal(entries).ToArray() : null;
        }
        
        protected virtual IEnumerable<LocalizationMultiValueEntry> GetExistsMultiValuesInternal(IEnumerable<ConfigurationValueEntry> values)
        {
            foreach (IGrouping<ImmutableArray<String>, ConfigurationValueEntry> group in values.GroupBy(entry => entry.Sections))
            {
                using IEnumerator<LocalizationValueEntry> enumerator = Extract(group).GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    continue;
                }
                
                LocalizationValueEntry value = enumerator.Current;
                ILocalizationString entry = new LocalizationString(this, enumerator.AsEnumerable().Prepend(value));
                yield return new LocalizationMultiValueEntry(value.Key, entry, value.Sections);
            }
        }

        public virtual LocalizationMultiValueEntry[]? GetExistsMultiValues(IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(sections);
            return entries is not null ? GetExistsMultiValuesInternal(entries).ToArray() : null;
        }

        public virtual async Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(sections, token);
            return entries is not null ? GetExistsMultiValuesInternal(entries).ToArray() : null;
        }

        ConfigurationValueEntry[]? IConfigBehavior.GetExistsValues(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections);
        }

        Task<ConfigurationValueEntry[]?> IConfigBehavior.GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }

        protected virtual IEnumerable<LocalizationValueEntry> GetExistsValuesInternal(LocalizationIdentifier identifier, IEnumerable<ConfigurationValueEntry> values)
        {
            return values.GroupBy(entry => entry.Sections).SelectMany(group => Extract(group).Where(entry => entry.Identifier == identifier));
        }

        public virtual LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            ConfigurationValueEntry[]? entries = Behavior.GetExistsValues(sections);
            return entries is not null ? GetExistsValuesInternal(identifier, entries).ToArray() : null;
        }

        public virtual async Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await Behavior.GetExistsValuesAsync(sections, token);
            return entries is not null ? GetExistsValuesInternal(identifier, entries).ToArray() : null;
        }

        public virtual Boolean Clear(IEnumerable<String>? sections)
        {
            return Behavior.Clear(sections);
        }

        public virtual Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ClearAsync(sections, token);
        }
        
        public virtual Boolean Clear(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Clear(Convert(key, sections));
        }

        public virtual Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ClearAsync(Convert(key, sections), token);
        }

        public virtual Boolean Reload()
        {
            return Behavior.Reload();
        }

        public virtual Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        public virtual Boolean Reset()
        {
            return Behavior.Reset();
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Behavior.ResetAsync(token);
        }

        Boolean IConfigBehavior.Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Merge(entries);
        }

        Task<Boolean> IConfigBehavior.MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(entries, token);
        }
        
        public virtual Boolean Merge(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Behavior.Merge(Pack(entries));
        }

        public virtual Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(Pack(entries), token);
        }

        public virtual Boolean Merge(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Behavior.Merge(Pack(entries));
        }

        public virtual Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(Pack(entries), token);
        }

        Boolean IConfigBehavior.Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Replace(entries);
        }

        Task<Boolean> IConfigBehavior.ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(entries, token);
        }
        
        public virtual Boolean Replace(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Behavior.Replace(Pack(entries));
        }

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(Pack(entries), token);
        }

        public virtual Boolean Replace(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Behavior.Replace(Pack(entries));
        }

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(Pack(entries), token);
        }

        ConfigurationValueEntry[]? IConfigBehavior.Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Difference(entries);
        }

        Task<ConfigurationValueEntry[]?> IConfigBehavior.DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.DifferenceAsync(entries, token);
        }
        
        public virtual LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Extract(Behavior.Difference(Pack(entries)))?.ToArray();
        }

        public virtual async Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Extract(await Behavior.DifferenceAsync(Pack(entries), token))?.ToArray();
        }

        public virtual LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Extract(Behavior.Difference(Pack(entries)))?.ToArray();
        }

        public virtual async Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Extract(await Behavior.DifferenceAsync(Pack(entries), token))?.ToArray();
        }
        
        IConfigBehaviorTransaction? IConfigBehavior.Transaction()
        {
            return Behavior.Transaction();
        }

        Task<IConfigBehaviorTransaction?> IConfigBehavior.TransactionAsync(CancellationToken token)
        {
            return Behavior.TransactionAsync(token);
        }

        public virtual ILocalizationBehaviorTransaction? Transaction()
        {
            if (IsReadOnly)
            {
                return null;
            }
            
            LocalizationMultiValueEntry[]? entries = GetExistsMultiValues(null);
            
            ILocalizationBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent).Localization();

            transaction.Merge(entries);
            return new LocalizationBehaviorTransaction(this, transaction);
        }
        
        public virtual async Task<ILocalizationBehaviorTransaction?> TransactionAsync(CancellationToken token)
        {
            if (IsReadOnly)
            {
                return null;
            }
            
            LocalizationMultiValueEntry[]? entries = await GetExistsMultiValuesAsync(null, token);
            
            ILocalizationBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent).Localization();

            await transaction.MergeAsync(entries, token);
            return new LocalizationBehaviorTransaction(this, transaction);
        }
        
        public virtual IEnumerator<LocalizationMultiValueEntry> GetEnumerator()
        {
            LocalizationMultiValueEntry[]? entries = GetExistsMultiValues(null);

            if (entries is null)
            {
                yield break;
            }
            
            foreach (LocalizationMultiValueEntry entry in entries)
            {
                yield return entry;
            }
        }

        IEnumerator<ConfigurationValueEntry> IEnumerable<ConfigurationValueEntry>.GetEnumerator()
        {
            return Behavior.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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