// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableSet<T> : IConcurrentObservableBase<T>, ISet<T>
    {
        public IImmutableSet<T> Immutable { get; }
        public new ISet<T> View { get; }
    }

    public interface IReadOnlyConcurrentObservableSet<T> : IReadOnlyConcurrentObservableBase<T>, IReadOnlySet<T>
    {
        public IImmutableSet<T> Immutable { get; }
        public new IReadOnlySet<T> View { get; }
    }
}