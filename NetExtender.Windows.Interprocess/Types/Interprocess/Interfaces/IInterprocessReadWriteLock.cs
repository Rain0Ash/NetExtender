// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Interprocess.Interfaces
{
    public interface IInterprocessReadWriteLock : IDisposable
    {
        /// <summary>
        /// Is true if at least one read lock is being held
        /// </summary>
        public Boolean IsLockHeld { get; }

        /// <summary>
        /// Is true if a write lock (which means all read locks) is being held
        /// </summary>
        public Boolean IsWriterLockHeld { get; }

        /// <summary>
        /// Acquire one read lock
        /// </summary>
        public void AcquireReadLock();

        /// <summary>
        /// Acquires exclusive write locking by consuming all read locks
        /// </summary>
        public void AcquireWriteLock();

        /// <summary>
        /// Release one read lock
        /// </summary>
        public void ReleaseReadLock();

        /// <summary>
        /// Release write lock
        /// </summary>
        public void ReleaseWriteLock();
    }
}
