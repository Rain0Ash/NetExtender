// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace NetExtender.Types.Splitters.Interfaces
{
    public interface ISplitter<in T, out TResult>
    {
        public IEnumerable<TResult> Split(T value);
    }
}