// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent
{
    public sealed class ConcurrentLocker : IDisposable
    {
        private Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;
        private Boolean IsLocked { get; set; }
        private Object Item { get; }
        private Boolean Disposed { get; set; }

        public ConcurrentLocker(Object item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public Boolean Lock()
        {
            lock (SyncRoot)
            {
                if (IsLocked)
                {
                    return true;
                }

                Boolean locked = false;
                Monitor.TryEnter(Item, ref locked);

                if (locked)
                {
                    IsLocked = true;
                }

                return locked;
            }
        }

        public Boolean Unlock()
        {
            lock (SyncRoot)
            {
                if (!IsLocked)
                {
                    return false;
                }

                Monitor.Exit(Item);
                return true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(Boolean disposing)
        {
            if (Disposed)
            {
                return;
            }

            Disposed = true;
            Unlock();
        }

        ~ConcurrentLocker()
        {
            Dispose(false);
        }
    }
}