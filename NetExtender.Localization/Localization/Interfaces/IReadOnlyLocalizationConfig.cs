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
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Interfaces
{
    public interface IReadOnlyLocalizationConfig : IReadOnlyConfig
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
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public void CopyTo(ILocalizationConfig config);
        public Task CopyToAsync(ILocalizationConfig config);
        public Task CopyToAsync(ILocalizationConfig config, CancellationToken token);
        public new ILocalizationString? this[String? key, params String[]? sections] { get; }
        public new ILocalizationString? this[String? key, IEnumerable<String>? sections] { get; }
        public String? this[String? key, LocalizationIdentifier identifier, params String[]? sections] { get; }
        public String? this[String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections] { get; }
    }
}