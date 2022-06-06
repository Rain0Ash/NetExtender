// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Transactions.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Interfaces
{
    public interface ILocalizationConfig : IConfig
    {
        public new event LocalizationChangedEventHandler Changed;
        public event LocalizationValueChangedEventHandler ValueChanged;

        public LocalizationOptions LocalizationOptions { get; }
        public Boolean ThreeLetterName { get; }
        public LocalizationIdentifier Default { get; }
        public LocalizationIdentifier System { get; }
        public LocalizationIdentifier Localization { get; set; }
        public LocalizationIdentifierBehaviorComparer Comparer { get; }
        public ILocalizationConverter Converter { get; }
        public new ILocalizationString? GetValue(String? key, params String[]? sections);
        public new ILocalizationString? GetValue(String? key, IEnumerable<String>? sections);
        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, params String[]? sections);
        public ILocalizationString? GetValue(String? key, ILocalizationString? alternate, IEnumerable<String>? sections);
        public new ILocalizationString? GetValue(String? key, String? alternate, params String[]? sections);
        public new ILocalizationString? GetValue(String? key, String? alternate, IEnumerable<String>? sections);
        public String? GetValue(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public String? GetValue(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections);
        public String? GetValue(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, params String[]? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, CancellationToken token, params String[]? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, params String[]? sections);
        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections);
        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, CancellationToken token, params String[]? sections);
        public Task<ILocalizationString?> GetValueAsync(String? key, ILocalizationString? alternate, IEnumerable<String>? sections, CancellationToken token);
        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, params String[]? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections);
        public new Task<ILocalizationString?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, LocalizationIdentifier identifier, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetValue(String? key, ILocalizationString? value, params String[]? sections);
        public Boolean SetValue(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Boolean SetValue(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections);
        public Boolean SetValue(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token);
        public ILocalizationString? GetOrSetValue(String? key, ILocalizationString? value, params String[]? sections);
        public ILocalizationString? GetOrSetValue(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public String? GetOrSetValue(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections);
        public String? GetOrSetValue(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, params String[]? sections);
        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, CancellationToken token, params String[]? sections);
        public Task<ILocalizationString?> GetOrSetValueAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token);
        public Boolean RemoveValue(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public Boolean RemoveValue(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean KeyExist(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public Boolean KeyExist(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public new LocalizationMultiEntry[]? GetExists();
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync();
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token);
        public new LocalizationMultiEntry[]? GetExists(params String[]? sections);
        public new LocalizationMultiEntry[]? GetExists(IEnumerable<String>? sections);
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(params String[]? sections);
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections);
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections);
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token);
        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, params String[]? sections);
        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, params String[]? sections);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public new LocalizationValueEntry[]? GetExistsValues();
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync();
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token);
        public new LocalizationValueEntry[]? GetExistsValues(params String[]? sections);
        public new LocalizationValueEntry[]? GetExistsValues(IEnumerable<String>? sections);
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(params String[]? sections);
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections);
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections);
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationMultiValueEntry[]? GetExistsMultiValues();
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync();
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token);
        public LocalizationMultiValueEntry[]? GetExistsMultiValues(params String[]? sections);
        public LocalizationMultiValueEntry[]? GetExistsMultiValues(IEnumerable<String>? sections);
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(params String[]? sections);
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections);
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(CancellationToken token, params String[]? sections);
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token);
        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, params String[]? sections);
        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, params String[]? sections);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, CancellationToken token, params String[]? sections);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Clear(String? key);
        public Boolean Clear(String? key, params String[]? sections);
        public Boolean Clear(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(String? key);
        public Task<Boolean> ClearAsync(String? key, CancellationToken token);
        public Task<Boolean> ClearAsync(String? key, params String[]? sections);
        public Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Merge(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public Boolean Merge(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public new ILocalizationTransaction? Transaction();
        public new Task<ILocalizationTransaction?> TransactionAsync();
        public new Task<ILocalizationTransaction?> TransactionAsync(CancellationToken token);
        public void CopyTo(ILocalizationConfig config);
        public Task CopyToAsync(ILocalizationConfig config);
        public Task CopyToAsync(ILocalizationConfig config, CancellationToken token);
        public new ILocalizationString? this[String? key, params String[]? sections] { get; set; }
        public new ILocalizationString? this[String? key, IEnumerable<String>? sections] { get; set; }
        public String? this[String? key, LocalizationIdentifier identifier, params String[]? sections] { get; set; }
        public String? this[String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections] { get; set; }
    }
}