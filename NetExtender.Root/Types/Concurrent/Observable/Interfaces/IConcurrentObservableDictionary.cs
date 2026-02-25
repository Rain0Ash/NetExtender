// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableDictionary<TKey, TValue> : IConcurrentObservableBase<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        public IImmutableDictionary<TKey, TValue> Immutable { get; }
        public new IDictionary<TKey, TValue> View { get; }
    }

    public interface IReadOnlyConcurrentObservableDictionary<TKey, TValue> : IReadOnlyConcurrentObservableBase<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>
    {
        public IImmutableDictionary<TKey, TValue> Immutable { get; }
        public new IReadOnlyDictionary<TKey, TValue> View { get; }
    }
}