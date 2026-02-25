// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableList<T> : IConcurrentObservableBase<T>, IObservableCollection<T>
    {
        public ImmutableList<T> Immutable { get; }
        public new IList<T> View { get; }
    }

    public interface IReadOnlyConcurrentObservableList<T> : IReadOnlyConcurrentObservableBase<T>, IReadOnlyObservableCollection<T>
    {
        public ImmutableList<T> Immutable { get; }
        public new IReadOnlyList<T> View { get; }
    }
}