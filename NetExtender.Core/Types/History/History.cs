// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.History.Interfaces;

namespace NetExtender.Types.History
{
    public abstract class History<T, TCollection> : IHistory<T, TCollection> where T : IHistoryEntry where TCollection : IHistoryCollection<T>
    {
        public abstract TCollection Past { get; }
        public abstract TCollection Future { get; }

        protected abstract Boolean Restore(T entry);

        public virtual IHistoryTransaction Transaction()
        {
            return new HistoryTransaction<T, TCollection>(this);
        }

        public Boolean Undo()
        {
            if (!Past.TryPop(out T? entry))
            {
                return false;
            }

            Future.Save();
            Restore(entry);
            return true;
        }

        public Boolean Redo()
        {
            if (!Future.TryPop(out T? entry))
            {
                return false;
            }
            
            Past.Save();
            Restore(entry);
            return true;
        }

        public void Clear()
        {
            Past.Clear();
            Future.Clear();
        }
    }
}