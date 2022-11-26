// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Interprocess.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Interprocess
{
    internal sealed class InterprocessReadWriteLock : IInterprocessReadWriteLock
    {
        private Mutex? Mutex { get; set; }
        private Semaphore? Semaphore { get; set; }
        private Int32 MaximumCount { get; }
        private TimeSpan Timeout { get; }

        private Int32 _locks;
        private Int32 Locks
        {
            get
            {
                return _locks;
            }
            set
            {
                _locks = value;
            }
        }

        public Boolean IsLockHeld
        {
            get
            {
                return Locks > 0;
            }
        }

        public Boolean IsWriterLockHeld { get; private set; }

        public static TimeSpan DefaultWaitTimeout
        {
            get
            {
                return Time.Second.Five;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessReadWriteLock"/> class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        public InterprocessReadWriteLock(String name)
            : this(name, 6, DefaultWaitTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessReadWriteLock"/> class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="count">Maxium simultaneous readers, default is 6</param>
        public InterprocessReadWriteLock(String name, Int32 count)
            : this(name, count, DefaultWaitTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessReadWriteLock"/> class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="count">Maxium simultaneous readers, default is 6</param>
        /// <param name="timeout">How long to wait before giving up aquiring read and write locks</param>
        public InterprocessReadWriteLock(String name, Int32 count, TimeSpan timeout)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"Lock must be named", nameof(name));
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Can't be less than 1");
            }

            Mutex = CreateMutex(name);
            Semaphore = CreateSemaphore(name, count);
            MaximumCount = count;
            Timeout = timeout;
        }

        /// <summary>
        /// Create a system wide Mutex that can be used to construct a <see cref="InterprocessReadWriteLock"/>
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <returns>A system wide Mutex</returns>
        private static Mutex CreateMutex(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new Mutex(false, $"{nameof(InterprocessReadWriteLock)}_Mutex_{name}");
        }

        /// <summary>
        /// Create a system wide Semaphore that can be used to construct a <see cref="InterprocessReadWriteLock"/>
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <param name="count">Maximum number of simultaneous readers</param>
        /// <returns>A system wide Semaphore</returns>
        private static Semaphore CreateSemaphore(String name, Int32 count)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new Semaphore(count, count, $"{nameof(InterprocessReadWriteLock)}_Semaphore_{name}");
        }

        /// <summary>
        /// Acquire one read lock
        /// </summary>
        public void AcquireReadLock()
        {
            if (Mutex is null)
            {
                throw new ObjectDisposedException(nameof(Mutex));
            }

            if (Semaphore is null)
            {
                throw new ObjectDisposedException(nameof(Semaphore));
            }

            if (!Mutex.WaitOne(Timeout))
            {
                throw new TimeoutException("Gave up waiting for read lock");
            }

            try
            {
                if (!Semaphore.WaitOne(Timeout))
                {
                    throw new TimeoutException("Gave up waiting for read lock");
                }

                Interlocked.Increment(ref _locks);
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Acquires exclusive write locking by consuming all read locks
        /// </summary>
        public void AcquireWriteLock()
        {
            if (Mutex is null)
            {
                throw new ObjectDisposedException(nameof(Mutex));
            }

            if (Semaphore is null)
            {
                throw new ObjectDisposedException(nameof(Semaphore));
            }

            if (!Mutex.WaitOne(Timeout))
            {
                throw new TimeoutException("Gave up waiting for write lock");
            }

            Int32 acquired = 0;
            try
            {
                for (Int32 i = 0; i < MaximumCount; i++)
                {
                    if (!Semaphore.WaitOne(Timeout))
                    {
                        throw new TimeoutException("Gave up waiting for write lock");
                    }

                    acquired++;
                }

                IsWriterLockHeld = true;
            }
            catch (TimeoutException) when (acquired > 0)
            {
                Semaphore.Release(acquired);
                throw;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Release one read lock
        /// </summary>
        public void ReleaseReadLock()
        {
            if (Semaphore is null)
            {
                throw new ObjectDisposedException(nameof(Semaphore));
            }

            Semaphore.Release();
            Interlocked.Decrement(ref _locks);
        }

        /// <summary>
        /// Release write lock
        /// </summary>
        public void ReleaseWriteLock()
        {
            if (Semaphore is null)
            {
                throw new ObjectDisposedException(nameof(Semaphore));
            }

            IsWriterLockHeld = false;
            Semaphore.Release(MaximumCount);
        }

        public void Dispose()
        {
            if (Mutex is null || Semaphore is null)
            {
                return;
            }

            if (Locks > 0)
            {
                Semaphore.Release(Locks);
            }
            else if (IsWriterLockHeld)
            {
                Semaphore.Release(MaximumCount);
            }

            Locks = 0;
            IsWriterLockHeld = false;
            Mutex.Dispose();
            Mutex = null;
            Semaphore.Dispose();
            Semaphore = null;
        }
    }
}
