// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IReadOnlySuppressObservableCollection<out T> : IReadOnlyObservableCollection<T>
    {
        public Boolean IsAllowSuppress { get; }
        public Boolean IsSuppressed { get; }
        public Int32 SuppressDepth { get; }

        public IDisposable? Suppress();
    }
}