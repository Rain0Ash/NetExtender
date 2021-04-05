// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;
using JetBrains.Annotations;
using NetExtender.Threading;

namespace NetExtender.Utils.Types
{
    public static class MutexUtils
    {
        public static Boolean CaptureMutex([NotNull] this Mutex mutex)
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
        }
        
        public static Boolean CaptureMutex([NotNull] this SemaphoreMutex mutex)
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
        }
        
        private static ConcurrentDictionary<String, SemaphoreMutex> Mutexes { get; } = new ConcurrentDictionary<String, SemaphoreMutex>();

        public static Int32 GetMutexRegistrations([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex mutex) ? mutex.Count : 0;
        }
        
        public static UInt64 GetMutexRegistrationsLong([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex mutex) ? mutex.LongCount : 0;
        }
        
        public static Boolean CaptureMutex([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Mutexes.TryGetValue(name, out SemaphoreMutex mutex) ? mutex.CaptureMutex() : CaptureRegisterMutex(name);
        }
        
        public static Boolean RegisterMutex([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (Mutexes.TryGetValue(name, out SemaphoreMutex mutex))
            {
                mutex.Capture();
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

        public static Boolean CaptureRegisterMutex([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (Mutexes.TryGetValue(name, out SemaphoreMutex mutex))
            {
                if (!mutex.CaptureMutex())
                {
                    return false;
                }

                mutex.Capture();
                return true;
            }

            try
            {
                mutex = new SemaphoreMutex(true, name);

                if (mutex.CaptureMutex() && Mutexes.TryAdd(name, mutex))
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

        public static Boolean UnregisterMutex([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Mutexes.TryGetValue(name, out SemaphoreMutex mutex))
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
        
        public static Boolean ForceUnregisterMutex([NotNull] String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Mutexes.TryRemove(name, out SemaphoreMutex mutex))
            {
                return false;
            }

            mutex.Dispose();
            return true;
        }
    }
}