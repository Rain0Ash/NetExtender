using System;

namespace NetExtender.Network.IPC.Synchronization
{
	public interface ITinyReadWriteLock
	{
		/// <summary>
		/// Is true if at least one read lock is being held
		/// </summary>
		public Boolean IsReaderLockHeld { get; }

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
