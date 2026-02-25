// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Immutable.Dictionaries;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableHashDictionary<TKey, TValue> : IConcurrentObservableDictionary<TKey, TValue>, IHashDictionary<TKey, TValue>
    {
        public new ImmutableNullableDictionary<TKey, TValue> Immutable { get; }
    }

    public interface IReadOnlyConcurrentObservableHashDictionary<TKey, TValue> : IReadOnlyConcurrentObservableDictionary<TKey, TValue>, IReadOnlyHashDictionary<TKey, TValue>
    {
        public new ImmutableNullableDictionary<TKey, TValue> Immutable { get; }
    }
}