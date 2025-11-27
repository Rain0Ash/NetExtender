// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Immutable;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableHashSet<T> : IConcurrentObservableSet<T>, IHashSet<T>
    {
        public new ImmutableHashSet<T> Immutable { get; }
    }
}