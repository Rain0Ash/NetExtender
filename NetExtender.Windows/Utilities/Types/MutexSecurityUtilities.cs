// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.AccessControl;
using System.Threading;
using NetExtender.Types.Threading;

namespace NetExtender.Utilities.Types
{
    public static class MutexSecurityUtilities
    {
        public static MutexSecurity GetAccessControl(this SemaphoreMutex semaphore)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            Mutex mutex = (Mutex?) semaphore ?? throw new ObjectDisposedException(nameof(Mutex));
            return mutex.GetAccessControl();
        }

        public static void SetAccessControl(this SemaphoreMutex semaphore, MutexSecurity security)
        {
            if (semaphore is null)
            {
                throw new ArgumentNullException(nameof(semaphore));
            }

            if (security is null)
            {
                throw new ArgumentNullException(nameof(security));
            }

            Mutex mutex = (Mutex?) semaphore ?? throw new ObjectDisposedException(nameof(Mutex));
            mutex.SetAccessControl(security);
        }
    }
}