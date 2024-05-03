// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.History.Interfaces
{
    public interface IHistory<T, out TCollection> where T : IHistoryEntry where TCollection : IHistoryCollection<T>
    {
        public TCollection Past { get; }
        public TCollection Future { get; }

        public IHistoryTransaction Transaction();
        public Boolean Undo();
        public Boolean Redo();
        public void Clear();
    }
}