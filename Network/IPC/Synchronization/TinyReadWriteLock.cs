using System;
using System.Threading;

namespace NetExtender.Network.IPC.Synchronization
{
	/// <summary>
	/// Implements a simple inter process read/write locking mechanism
	/// Inspired by http://www.joecheng.com/blog/entries/Writinganinter-processRea.html
	/// </summary>
	public class TinyReadWriteLock : IDisposable, ITinyReadWriteLock
	{
		private readonly Mutex _mutex;
		private readonly Semaphore _semaphore;
		private readonly Int32 _maxReaderCount;
		private readonly TimeSpan _waitTimeout;

		private Boolean _disposed;
		private Int32 _readLocks;

		public Boolean IsReaderLockHeld
		{
			get
			{
				return _readLocks > 0;
			}
		}

		public Boolean IsWriterLockHeld { get; private set; }

		public const Int32 DefaultMaxReaderCount = 6;
		public static readonly TimeSpan DefaultWaitTimeout = TimeSpan.FromSeconds(5);

		/// <summary>
		/// Initializes a new instance of the TinyReadWriteLock class.
		/// </summary>
		/// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
		public TinyReadWriteLock(String name)
			: this(name, DefaultMaxReaderCount, DefaultWaitTimeout)
		{
		}

		/// <summary>
		/// Initializes a new instance of the TinyReadWriteLock class.
		/// </summary>
		/// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
		/// <param name="maxReaderCount">Maxium simultaneous readers, default is 6</param>
		public TinyReadWriteLock(String name, Int32 maxReaderCount)
			: this(name, maxReaderCount, DefaultWaitTimeout)
		{
		}

		/// <summary>
		/// Initializes a new instance of the TinyReadWriteLock class.
		/// </summary>
		/// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
		/// <param name="maxReaderCount">Maxium simultaneous readers, default is 6</param>
		/// <param name="waitTimeout">How long to wait before giving up aquiring read and write locks</param>
		public TinyReadWriteLock(String name, Int32 maxReaderCount, TimeSpan waitTimeout)
		{
			if (String.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(@"Lock must be named", nameof(name));
			}

			if (maxReaderCount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maxReaderCount), @"Need at least one reader");
			}

			_maxReaderCount = maxReaderCount;
			_waitTimeout = waitTimeout;

			_mutex = CreateMutex(name);
			_semaphore = CreateSemaphore(name, maxReaderCount);
		}

		/// <summary>
		/// Initializes a new instance of the TinyReadWriteLock class.
		/// </summary>
		/// <param name="mutex">Should be a system wide Mutex that is used to control access to the semaphore</param>
		/// <param name="semaphore">Should be a system wide Semaphore with at least one max count, default is 6</param>
		/// <param name="maxReaderCount">Maxium simultaneous readers, must be the same as the Semaphore count, default is 6</param>
		/// <param name="waitTimeout">How long to wait before giving up aquiring read and write locks</param>
		public TinyReadWriteLock(Mutex mutex, Semaphore semaphore, Int32 maxReaderCount, TimeSpan waitTimeout)
		{
			if (maxReaderCount <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maxReaderCount), @"Need at least one reader");
			}

			_maxReaderCount = maxReaderCount;
			_waitTimeout = waitTimeout;
			_mutex = mutex ?? throw new ArgumentNullException(nameof(mutex));
			_semaphore = semaphore ?? throw new ArgumentNullException(nameof(semaphore));
		}

		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;

			if (_readLocks > 0)
			{
				_semaphore.Release(_readLocks);
			}
			else if (IsWriterLockHeld)
			{
				_semaphore.Release(_maxReaderCount);
			}

			_readLocks = 0;
			IsWriterLockHeld = false;
			_mutex.Dispose();
			_semaphore.Dispose();
		}

		/// <summary>
		/// Acquire one read lock
		/// </summary>
		public void AcquireReadLock()
		{
			if (!_mutex.WaitOne(_waitTimeout))
			{
				throw new TimeoutException("Gave up waiting for read lock");
			}

			try
			{
				if (!_semaphore.WaitOne(_waitTimeout))
				{
					throw new TimeoutException("Gave up waiting for read lock");
				}

				Interlocked.Increment(ref _readLocks);
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Acquires exclusive write locking by consuming all read locks
		/// </summary>
		public void AcquireWriteLock()
		{
			if (!_mutex.WaitOne(_waitTimeout))
			{
				throw new TimeoutException("Gave up waiting for write lock");
			}

			Int32 readersAcquired = 0;
			try
			{
				for (Int32 i = 0; i < _maxReaderCount; i++)
				{
					if (!_semaphore.WaitOne(_waitTimeout))
					{
						throw new TimeoutException("Gave up waiting for write lock");
					}

					readersAcquired++;
				}
				IsWriterLockHeld = true;
			}
			catch (TimeoutException) when (readersAcquired > 0)
			{
				_semaphore.Release(readersAcquired);
				throw;
			}
			finally
			{
				_mutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Release one read lock
		/// </summary>
		public void ReleaseReadLock()
		{
			_semaphore.Release();
			Interlocked.Decrement(ref _readLocks);
		}

		/// <summary>
		/// Release write lock
		/// </summary>
		public void ReleaseWriteLock()
		{
			IsWriterLockHeld = false;
			_semaphore.Release(_maxReaderCount);
		}

		/// <summary>
		/// Create a system wide Mutex that can be used to construct a TinyReadWriteLock
		/// </summary>
		/// <param name="name">A system wide unique name, the name will have a prefix appended</param>
		/// <returns>A system wide Mutex</returns>
		public static Mutex CreateMutex(String name)
		{
			return new Mutex(false, "TinyReadWriteLock_Mutex_" + name);
		}

		/// <summary>
		/// Create a system wide Semaphore that can be used to construct a TinyReadWriteLock
		/// </summary>
		/// <param name="name">A system wide unique name, the name will have a prefix appended</param>
		/// <param name="maxReaderCount">Maximum number of simultaneous readers</param>
		/// <returns>A system wide Semaphore</returns>
		public static Semaphore CreateSemaphore(String name, Int32 maxReaderCount)
		{
			return new Semaphore(maxReaderCount, maxReaderCount, "TinyReadWriteLock_Semaphore_" + name);
		}
	}
}
