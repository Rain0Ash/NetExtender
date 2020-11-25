// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Threading
{
    /// <summary>
    /// Provides a way to lock a resource based on a value (such as an ID or path).
    /// </summary>
    public class Synchronizer<T>
    {
        private readonly Dictionary<T, SyncLock> _locks = new Dictionary<T, SyncLock>();
        private readonly Object _mLock = new Object();

        /// <summary>
        /// Returns an object that can be used in a lock statement. Ex: lock(MySync.Lock(MyValue)) { ... }
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SyncLock Lock(T value)
        {
            lock (_mLock)
            {
                if (_locks.TryGetValue(value, out SyncLock theLock))
                {
                    return theLock;
                }

                theLock = new SyncLock(value, this);
                _locks.Add(value, theLock);
                return theLock;
            }
        }

        /// <summary>
        /// Unlocks the object. Called from Lock.Dispose.
        /// </summary>
        /// <param name="theLock"></param>
        public void Unlock(SyncLock theLock)
        {
            lock (_locks)
            {
                _locks.Remove(theLock.Value);
            }
        }

        /// <summary>
        /// Represents a lock for the Synchronizer class.
        /// </summary>
        public class SyncLock
            : IDisposable
        {
            /// <summary>
            /// This class should only be instantiated from the Synchronizer class.
            /// </summary>
            /// <param name="value"></param>
            /// <param name="sync"></param>
            internal SyncLock(T value, Synchronizer<T> sync)
            {
                Value = value;
                Sync = sync;
            }

            /// <summary>
            /// Makes sure the lock is removed.
            /// </summary>
            public void Dispose()
            {
                Sync.Unlock(this);
            }

            /// <summary>
            /// Gets the value that this lock is based on.
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Gets the synchronizer this lock was created from.
            /// </summary>
            private Synchronizer<T> Sync { get; set; }
        }
    }
}