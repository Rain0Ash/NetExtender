// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Types.Threading
{
    public sealed class SemaphoreMutex : IDisposable
    {
        public static explicit operator Mutex?(SemaphoreMutex? mutex)
        {
            return mutex?.Mutex;
        }

        private Mutex? Mutex { get; set; }

        private UInt64 _count = 1;

        public Int32 Count
        {
            get
            {
                UInt64 count = LongCount;
                if (count > Int32.MaxValue)
                {
                    throw new OverflowException();
                }

                return (Int32) count;
            }
        }

        public UInt64 LongCount
        {
            get
            {
                return _count;
            }
        }

        public Boolean Disposed
        {
            get
            {
                return Mutex is null;
            }
        }

        public SemaphoreMutex()
            : this(new Mutex())
        {
        }

        public SemaphoreMutex(Boolean initiallyOwned)
            : this(new Mutex(initiallyOwned))
        {
        }

        public SemaphoreMutex(Boolean initiallyOwned, String? name)
            : this(new Mutex(initiallyOwned, name))
        {
        }

        public SemaphoreMutex(Boolean initiallyOwned, String? name, out Boolean createdNew)
            : this(new Mutex(initiallyOwned, name, out createdNew))
        {
        }

        private SemaphoreMutex(Mutex mutex)
        {
            Mutex = mutex ?? throw new ArgumentNullException(nameof(mutex));
        }

        public UInt64 Take()
        {
            if (Mutex is null)
            {
                throw new ObjectDisposedException(nameof(Mutex));
            }

            Interlocked.Increment(ref _count);
            return LongCount;
        }

        public UInt64 Release()
        {
            if (Mutex is null)
            {
                throw new ObjectDisposedException(nameof(Mutex));
            }

            if (LongCount <= 1)
            {
                Dispose();
                return LongCount;
            }

            Interlocked.Decrement(ref _count);
            return LongCount;
        }

        public Boolean WaitOne()
        {
            return Mutex?.WaitOne() ?? throw new ObjectDisposedException(nameof(Mutex));
        }

        public Boolean WaitOne(TimeSpan timeout)
        {
            return Mutex?.WaitOne(timeout) ?? throw new ObjectDisposedException(nameof(Mutex));
        }

        public Boolean WaitOne(Int32 timeout)
        {
            return Mutex?.WaitOne(timeout) ?? throw new ObjectDisposedException(nameof(Mutex));
        }

        public void Сlose()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Mutex is null)
            {
                return;
            }

            try
            {
                Mutex.ReleaseMutex();
            }
            catch (Exception)
            {
                //ignored
            }

            Mutex.Dispose();
            Mutex = null;
            _count = 0;
        }
    }
}