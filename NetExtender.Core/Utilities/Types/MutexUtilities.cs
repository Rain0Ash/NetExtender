// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;
using NetExtender.Types.Threading;

namespace NetExtender.Utilities.Types
{
    public static class MutexUtilities
    {
        public static Boolean TryWaitOne(this Mutex mutex)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }
            
            try
            {
                return mutex.WaitOne();
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryWaitOne(this Mutex mutex, Int32 timeout)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryWaitOne(this Mutex mutex, Int32 timeout, Boolean exit)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout, exit);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean TryWaitOne(this Mutex mutex, TimeSpan timeout)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean TryWaitOne(this Mutex mutex, TimeSpan timeout, Boolean exit)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout, exit);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean TryWaitOne(this SemaphoreMutex mutex, Int32 timeout)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean TryWaitOne(this SemaphoreMutex mutex, TimeSpan timeout)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(timeout);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean TryReleaseMutex(this Mutex mutex)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                mutex.ReleaseMutex();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public static Boolean TryRelease(this SemaphoreMutex mutex)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                mutex.Release();
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public static Boolean Capture(this Mutex mutex)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(0);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static Boolean Capture(this SemaphoreMutex mutex)
        {
            if (mutex is null)
            {
                throw new ArgumentNullException(nameof(mutex));
            }

            try
            {
                return mutex.WaitOne(0);
            }
            catch (AbandonedMutexException)
            {
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private static ConcurrentDictionary<String, SemaphoreMutex> Mutexes { get; } = new ConcurrentDictionary<String, SemaphoreMutex>();

        public static Int32 GetMutexRegistrations(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex? mutex) ? mutex.Count : 0;
        }

        public static UInt64 GetMutexRegistrationsLong(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex? mutex) ? mutex.LongCount : 0;
        }

        public static Boolean Capture(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex? mutex) ? mutex.Capture() : CaptureRegisterMutex(name);
        }

        public static Boolean RegisterMutex(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (Mutexes.TryGetValue(name, out SemaphoreMutex? mutex))
            {
                mutex.Take();
                return true;
            }

            try
            {
                mutex = new SemaphoreMutex(true, name);

                if (Mutexes.TryAdd(name, mutex))
                {
                    return true;
                }

                mutex.Dispose();
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean CaptureRegisterMutex(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (Mutexes.TryGetValue(name, out SemaphoreMutex? mutex))
            {
                if (!mutex.Capture())
                {
                    return false;
                }

                mutex.Take();
                return true;
            }

            try
            {
                mutex = new SemaphoreMutex(true, name);

                if (mutex.Capture() && Mutexes.TryAdd(name, mutex))
                {
                    return true;
                }

                mutex.Dispose();
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean UnregisterMutex(String name, Boolean force)
        {
            return force ? ForceUnregisterMutex(name) : UnregisterMutex(name);
        }

        public static Boolean UnregisterMutex(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Mutexes.TryGetValue(name, out SemaphoreMutex? mutex))
            {
                return false;
            }

            mutex.Release();

            if (mutex.Disposed)
            {
                Mutexes.TryRemove(name, out _);
            }

            return true;
        }

        public static Boolean ForceUnregisterMutex(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Mutexes.TryRemove(name, out SemaphoreMutex? mutex))
            {
                return false;
            }

            mutex.Dispose();
            return true;
        }
    }
}