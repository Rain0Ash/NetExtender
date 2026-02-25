using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Concurrent;

namespace NetExtender.Utilities.Types
{
    public sealed class SyncRoot
    {
        private SyncRoot()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SyncRoot Create()
        {
            return new SyncRoot();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDisposable? Lock()
        {
            ConcurrentLocker locker = new ConcurrentLocker(this);
            return locker.Lock() ? locker : null;
        }
    }
}