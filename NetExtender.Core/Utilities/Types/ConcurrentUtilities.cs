// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Concurrent;

namespace NetExtender.Utilities.Types
{
    public static class ConcurrentUtilities
    {
        public static Object SyncRoot
        {
            get
            {
                return new Object();
            }
        }

        public static IDisposable? Lock(this Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ConcurrentLocker locker = new ConcurrentLocker(value);
            return locker.Lock() ? locker : null;
        }
    }
}