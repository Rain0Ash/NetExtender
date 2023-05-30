// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Behavior.Transactions.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Transactions;
using NetExtender.Localization.Transactions.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization
{
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public class LocalizationConfig : Config, ILocalizationConfig, IReadOnlyLocalizationConfig
    {
        protected new ILocalizationBehavior Behavior
        {
            get
            {
                return (ILocalizationBehavior) base.Behavior;
            }
        }

        public new event LocalizationChangedEventHandler Changed
        {
            add
            {
                Behavior.Changed += value;
            }
            remove
            {
                Behavior.Changed -= value;
            }
        }

        public event LocalizationValueChangedEventHandler ValueChanged
        {
            add
            {
                Behavior.ValueChanged += value;
            }
            remove
            {
                Behavior.ValueChanged -= value;
            }
        }

        public LocalizationOptions LocalizationOptions
        {
            get
            {
                return Behavior.LocalizationOptions;
            }
        }

        public Boolean ThreeLetterName
        {
            get
            {
                return Behavior.ThreeLetterName;
            }
        }

        public Boolean WithoutSystem
        {
            get
            {
                return Behavior.WithoutSystem;
            }
        }

        public LocalizationIdentifier Default
        {
            get
            {
                return Behavior.Default;
            }
        }

        public LocalizationIdentifier System
        {
            get
            {
                return Behavior.System;
            }
        }

        public LocalizationIdentifier Localization
        {
            get
            {
                return Behavior.Localization;
            }
            set
            {
                Behavior.Localization = value;
            }
        }

        public LocalizationIdentifierBehaviorComparer Comparer
        {
            get
            {
                return Behavior.Comparer;
            }
        }

        public ILocalizationConverter Converter
        {
            get
            {
                return Behavior.Converter;
            }
        }

        public LocalizationConfig(ILocalizationBehavior behavior)
            : base(behavior)
        {
        }

        public new ILocalizationString? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public new virtual ILocalizationString? GetValue(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, sections);
        }

        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) ?? alternate;
        }

        public new ILocalizationString? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public new ILocalizationString? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) ?? LocalizationString.Create(Behavior.Default, alternate);
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections);
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, sections, CancellationToken.None);
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, sections, token);
        }

        public new virtual Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }

        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }

        public async Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) ?? alternate;
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }

        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }

        public new async Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) ?? LocalizationString.Create(Behavior.Default, alternate);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetValue(key, identifier, (IEnumerable<String>?) sections);
        }

        public virtual String? GetValue(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, identifier, sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return GetValue(key, identifier, alternate, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, identifier, sections) ?? alternate;
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetValueAsync(key, identifier, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, identifier, sections, CancellationToken.None);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, identifier, sections, token);
        }

        public virtual Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, identifier, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections)
        {
            return GetValueAsync(key, identifier, alternate, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, identifier, alternate, sections, CancellationToken.None);
        }

        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, identifier, alternate, sections, token);
        }

        public async Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, identifier, sections, token).ConfigureAwait(false) ?? alternate;
        }

        public Boolean SetValue(String? key, ILocalizationString? value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual Boolean SetValue(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, sections);
        }

        public Boolean SetValue(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections)
        {
            return SetValue(key, identifier, value, (IEnumerable<String>?) sections);
        }

        public virtual Boolean SetValue(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, identifier, value, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, params String[]? sections)
        {
            return SetValueAsync(key, value, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, value, sections, CancellationToken.None);
        }

        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, value, sections, token);
        }

        public virtual Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }

        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections)
        {
            return SetValueAsync(key, identifier, value, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, identifier, value, sections, CancellationToken.None);
        }

        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, identifier, value, sections, token);
        }

        public virtual Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, identifier, value, sections, token);
        }

        public ILocalizationString? GetOrSetValue(String? key, ILocalizationString? value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual ILocalizationString? GetOrSetValue(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, value, sections);
        }

        public String? GetOrSetValue(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections)
        {
            return GetOrSetValue(key, identifier, value, (IEnumerable<String>?) sections);
        }

        public virtual String? GetOrSetValue(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, identifier, value, sections);
        }

        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, (IEnumerable<String>?) sections);
        }

        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, sections, CancellationToken.None);
        }

        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, sections, token);
        }

        public virtual Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, sections, token);
        }

        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections)
        {
            return GetOrSetValueAsync(key, identifier, value, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, identifier, value, sections, CancellationToken.None);
        }

        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, identifier, value, sections, token);
        }

        public virtual Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, identifier, value, sections, token);
        }

        public Boolean RemoveValue(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return RemoveValue(key, identifier, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveValue(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return SetValue(key, identifier, null, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return RemoveValueAsync(key, identifier, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return RemoveValueAsync(key, identifier, sections, CancellationToken.None);
        }

        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return RemoveValueAsync(key, identifier, sections, token);
        }

        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetValueAsync(key, identifier, null, sections, token);
        }

        public Boolean KeyExist(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return KeyExist(key, identifier, (IEnumerable<String>?) sections);
        }

        public virtual Boolean KeyExist(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, identifier, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, params String[]? sections)
        {
            return KeyExistAsync(key, identifier, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return KeyExistAsync(key, identifier, sections, CancellationToken.None);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return KeyExistAsync(key, identifier, sections, token);
        }

        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, identifier, sections, token);
        }

        public new LocalizationMultiEntry[]? GetExists()
        {
            return GetExists((IEnumerable<String>?) null);
        }

        public new Task<LocalizationMultiEntry[]?> GetExistsAsync()
        {
            return GetExistsAsync(CancellationToken.None);
        }

        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return GetExistsAsync((IEnumerable<String>?) null, token);
        }

        public new LocalizationMultiEntry[]? GetExists(params String[]? sections)
        {
            return GetExists((IEnumerable<String>?) sections);
        }

        public new virtual LocalizationMultiEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections);
        }

        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(params String[]? sections)
        {
            return GetExistsAsync((IEnumerable<String>?) sections);
        }

        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections)
        {
            return GetExistsAsync(sections, CancellationToken.None);
        }

        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections)
        {
            return GetExistsAsync(sections, token);
        }

        public new virtual Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
        }

        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier)
        {
            return GetExists(identifier, (IEnumerable<String>?) null);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier)
        {
            return GetExistsAsync(identifier, CancellationToken.None);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token)
        {
            return GetExistsAsync(identifier, null, token);
        }

        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetExists(identifier, (IEnumerable<String>?) sections);
        }

        public virtual LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.GetExists(identifier, sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetExistsAsync(identifier, (IEnumerable<String>?) sections);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetExistsAsync(identifier, sections, CancellationToken.None);
        }

        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return GetExistsAsync(identifier, sections, token);
        }

        public virtual Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(identifier, sections, token);
        }

        public new LocalizationValueEntry[]? GetExistsValues()
        {
            return GetExistsValues((IEnumerable<String>?) null);
        }

        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync()
        {
            return GetExistsValuesAsync(CancellationToken.None);
        }

        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return GetExistsValuesAsync((IEnumerable<String>?) null, token);
        }

        public new LocalizationValueEntry[]? GetExistsValues(params String[]? sections)
        {
            return GetExistsValues((IEnumerable<String>?) sections);
        }

        public new virtual LocalizationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections);
        }

        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(params String[]? sections)
        {
            return GetExistsValuesAsync((IEnumerable<String>?) sections);
        }

        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections)
        {
            return GetExistsValuesAsync(sections, CancellationToken.None);
        }

        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections)
        {
            return GetExistsValuesAsync(sections, token);
        }

        public new virtual Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }

        public LocalizationMultiValueEntry[]? GetExistsMultiValues()
        {
            return GetExistsMultiValues((IEnumerable<String>?) null);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync()
        {
            return GetExistsMultiValuesAsync(CancellationToken.None);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token)
        {
            return GetExistsMultiValuesAsync(null, token);
        }

        public LocalizationMultiValueEntry[]? GetExistsMultiValues(params String[]? sections)
        {
            return GetExistsMultiValues((IEnumerable<String>?) sections);
        }

        public virtual LocalizationMultiValueEntry[]? GetExistsMultiValues(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsMultiValues(sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(params String[]? sections)
        {
            return GetExistsMultiValuesAsync((IEnumerable<String>?) sections);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections)
        {
            return GetExistsMultiValuesAsync(sections, CancellationToken.None);
        }

        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token, params String[]? sections)
        {
            return GetExistsMultiValuesAsync(sections, token);
        }

        public virtual Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsMultiValuesAsync(sections, token);
        }

        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier)
        {
            return GetExistsValues(identifier, (IEnumerable<String>?) null);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier)
        {
            return GetExistsValuesAsync(identifier, CancellationToken.None);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token)
        {
            return GetExistsValuesAsync(identifier, null, token);
        }

        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetExistsValues(identifier, (IEnumerable<String>?) sections);
        }

        public virtual LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(identifier, sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, params String[]? sections)
        {
            return GetExistsValuesAsync(identifier, (IEnumerable<String>?) sections);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections)
        {
            return GetExistsValuesAsync(identifier, sections, CancellationToken.None);
        }

        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections)
        {
            return GetExistsValuesAsync(identifier, sections, token);
        }

        public virtual Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(identifier, sections, token);
        }

        public Boolean Clear(String? key)
        {
            return Clear(key, null);
        }

        public Boolean Clear(String? key, params String[]? sections)
        {
            return Clear(key, (IEnumerable<String>?) sections);
        }

        public virtual Boolean Clear(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Clear(key, sections);
        }

        public Task<Boolean> ClearAsync(String? key)
        {
            return ClearAsync(key, CancellationToken.None);
        }

        public Task<Boolean> ClearAsync(String? key, CancellationToken token)
        {
            return ClearAsync(key, null, token);
        }

        public Task<Boolean> ClearAsync(String? key, params String[]? sections)
        {
            return ClearAsync(key, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections)
        {
            return ClearAsync(key, sections, CancellationToken.None);
        }

        public Task<Boolean> ClearAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return ClearAsync(key, sections, token);
        }

        public virtual Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ClearAsync(key, sections, token);
        }

        public virtual Boolean Merge(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Behavior.Merge(entries);
        }

        public Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries)
        {
            return MergeAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(entries, token);
        }

        public virtual Boolean Merge(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Behavior.Merge(entries);
        }

        public Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return MergeAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(entries, token);
        }

        public virtual Boolean Replace(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Behavior.Replace(entries);
        }

        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries)
        {
            return ReplaceAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(entries, token);
        }

        public virtual Boolean Replace(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Behavior.Replace(entries);
        }

        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return ReplaceAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(entries, token);
        }

        public virtual LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries)
        {
            return Behavior.Difference(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries)
        {
            return DifferenceAsync(entries, CancellationToken.None);
        }

        public virtual Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.DifferenceAsync(entries, token);
        }

        public virtual LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return Behavior.Difference(entries);
        }

        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries)
        {
            return DifferenceAsync(entries, CancellationToken.None);
        }

        public virtual Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token)
        {
            return Behavior.DifferenceAsync(entries, token);
        }

        public new virtual ILocalizationTransaction? Transaction()
        {
            ILocalizationBehaviorTransaction? transaction = Behavior.Transaction();
            return transaction is not null ? new LocalizationTransaction(this, transaction) : null;
        }

        public new Task<ILocalizationTransaction?> TransactionAsync()
        {
            return TransactionAsync(CancellationToken.None);
        }

        public new virtual async Task<ILocalizationTransaction?> TransactionAsync(CancellationToken token)
        {
            ILocalizationBehaviorTransaction? transaction = await Behavior.TransactionAsync(token).ConfigureAwait(false);
            return transaction is not null ? new LocalizationTransaction(this, transaction) : null;
        }

        public virtual void CopyTo(ILocalizationConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            foreach ((String? key, ImmutableArray<String> sections) in this)
            {
                config.SetValue(key, this[key, sections], sections);
            }
        }

        public Task CopyToAsync(ILocalizationConfig config)
        {
            return CopyToAsync(config, CancellationToken.None);
        }

        public virtual async Task CopyToAsync(ILocalizationConfig config, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            foreach ((String? key, ImmutableArray<String> sections) in this)
            {
                await config.SetValueAsync(key, this[key, sections], sections, token).ConfigureAwait(false);
            }
        }

        public new ILocalizationString? this[String? key, params String[]? sections]
        {
            get
            {
                return this[key, (IEnumerable<String>?) sections];
            }
            set
            {
                this[key, (IEnumerable<String>?) sections] = value;
            }
        }

        public new virtual ILocalizationString? this[String? key, IEnumerable<String>? sections]
        {
            get
            {
                return GetValue(key, sections);
            }
            set
            {
                SetValue(key, value, sections);
            }
        }

        public String? this[String? key, LocalizationIdentifier identifier, params String[]? sections]
        {
            get
            {
                return this[key, identifier, (IEnumerable<String>?) sections];
            }
            set
            {
                this[key, identifier, (IEnumerable<String>?) sections] = value;
            }
        }

        public virtual String? this[String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections]
        {
            get
            {
                return GetValue(key, identifier, sections);
            }
            set
            {
                SetValue(key, identifier, value, sections);
            }
        }
    }
}