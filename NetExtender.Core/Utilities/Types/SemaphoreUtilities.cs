// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;

namespace NetExtender.Utilities.Types
{
    public static class SemaphoreUtilities
    {
        public static Boolean TryWaitOne(this Semaphore semaphore, Int32 timeout)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                return semaphore.WaitOne(timeout);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryWaitOne(this Semaphore semaphore, Int32 timeout, Boolean exit)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                return semaphore.WaitOne(timeout, exit);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryWaitOne(this Semaphore semaphore, TimeSpan timeout)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                return semaphore.WaitOne(timeout);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryWaitOne(this Semaphore semaphore, TimeSpan timeout, Boolean exit)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                return semaphore.WaitOne(timeout, exit);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        
        public static Boolean TryRelease(this Semaphore semaphore)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                semaphore.Release();
                return true;
            }
            catch (SemaphoreFullException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        
        public static Boolean TryRelease(this Semaphore semaphore, Int32 count)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            try
            {
                semaphore.Release(count);
                return true;
            }
            catch (SemaphoreFullException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}