// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Concurrent;

namespace NetExtender.Utilities.Types
{
    public sealed class SyncRoot
    {
        private SyncRoot()
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SyncRoot Create()
        {
            return new SyncRoot();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDisposable? Lock()
        {
            ConcurrentLocker locker = new ConcurrentLocker(this);
            return locker.Lock() ? locker : null;
        }
    }

    public static class ConcurrentUtilities
    {
        public static SyncRoot SyncRoot
        {
            get
            {
                return SyncRoot.Create();
            }
        }

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