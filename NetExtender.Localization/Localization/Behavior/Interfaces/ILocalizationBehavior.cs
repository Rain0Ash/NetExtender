// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Localization.Behavior.Transactions.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Behavior.Interfaces
{
    public interface ILocalizationBehavior : IConfigBehavior, IEnumerable<LocalizationMultiValueEntry>
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
        public Boolean Contains(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<Boolean> ContainsAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public new ILocalizationString? Get(String? key, IEnumerable<String>? sections);
        public new Task<ILocalizationString?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String? Get(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token);
        public ILocalizationString? GetOrSet(String? key, ILocalizationString? value, IEnumerable<String>? sections);
        public Task<ILocalizationString?> GetOrSetAsync(String? key, ILocalizationString? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSet(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetAsync(String? key, LocalizationIdentifier identifier, String? value, IEnumerable<String>? sections, CancellationToken token);
        public new LocalizationMultiEntry[]? GetExists(IEnumerable<String>? sections);
        public new Task<LocalizationMultiEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationEntry[]? GetExists(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationEntry[]?> GetExistsAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public new LocalizationValueEntry[]? GetExistsValues(IEnumerable<String>? sections);
        public new Task<LocalizationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationMultiValueEntry[]? GetExistsMultiValues(IEnumerable<String>? sections);
        public Task<LocalizationMultiValueEntry[]?> GetExistsMultiValuesAsync(IEnumerable<String>? sections, CancellationToken token);
        public LocalizationValueEntry[]? GetExistsValues(LocalizationIdentifier identifier, IEnumerable<String>? sections);
        public Task<LocalizationValueEntry[]?> GetExistsValuesAsync(LocalizationIdentifier identifier, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Clear(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Merge(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public Boolean Merge(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<LocalizationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationValueEntry>? entries, CancellationToken token);
        public LocalizationValueEntry[]? Difference(IEnumerable<LocalizationMultiValueEntry>? entries);
        public Task<LocalizationValueEntry[]?> DifferenceAsync(IEnumerable<LocalizationMultiValueEntry>? entries, CancellationToken token);
        public new ILocalizationBehaviorTransaction? Transaction();
        public new Task<ILocalizationBehaviorTransaction?> TransactionAsync(CancellationToken token);
        public new IEnumerator<LocalizationMultiValueEntry> GetEnumerator();
    }
}