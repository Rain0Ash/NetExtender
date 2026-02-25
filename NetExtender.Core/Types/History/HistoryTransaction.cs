// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using System.Transactions;
using NetExtender.Types.Transactions;
using NetExtender.Types.History.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.History
{
    public class HistoryTransaction<T, TCollection> : IHistoryTransaction where T : IHistoryEntry where TCollection : IHistoryCollection<T>
    {
        private History<T, TCollection> History { get; }
        private T Entry { get; }
        public Boolean? IsCommit { get; protected set; }
        public TransactionCommitPolicy Policy { get; init; }
        public IsolationLevel Isolation { get; init; }
        public TimeSpan Timeout { get; init; }

        public HistoryTransaction(History<T, TCollection> history)
        {
            History = history ?? throw new ArgumentNullException(nameof(history));
            Entry = history.Past.Snapshot;
        }

        public virtual Boolean Commit()
        {
            IsCommit = true;
            return true;
        }

        public virtual Boolean Rollback()
        {
            IsCommit = false;
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (Policy.IsCommit(IsCommit))
            {
                return;
            }

            History.Past.Push(Entry);
            History.Future.Clear();
        }

        public ValueTask DisposeAsync()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return ValueTask.CompletedTask;
        }

        ~HistoryTransaction()
        {
            Dispose(false);
        }
    }
}