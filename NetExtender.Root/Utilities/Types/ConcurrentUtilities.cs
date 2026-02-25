// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Concurrent;

namespace NetExtender.Utilities.Types
{
    public static class ConcurrentUtilities
    {
        public static IDisposable? Lock(this Object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                SyncRoot root => root.Lock(),
                _ when new ConcurrentLocker(value) is { } locker => locker.Lock() ? locker : null,
                _ => null
            };
        }
    }
}