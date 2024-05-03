// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.History.Interfaces
{
    public interface IHistoryCollection<T> : IReadOnlyCollection<T> where T : IHistoryEntry
    {
        public T Snapshot { get; }

        public Boolean TryPeek([MaybeNullWhen(false)] out T entry);
        public Boolean TryPop([MaybeNullWhen(false)] out T entry);
        public void Push(T entry);
        public T Save();
        public void Clear();
    }
}