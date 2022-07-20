// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Windows;
using NetExtender.Types.Concurrent.Stores;
using NetExtender.Types.Concurrent.Stores.Interfaces;

namespace NetExtender.WindowsPresentation.Utilities
{
    public static class WindowStoreUtilities
    {
        private static IConcurrentRegisterStore<Window> Store { get; } = new ConcurrentRegisterStore<Window>();

        public static Window? Window
        {
            get
            {
                return Store.Current;
            }
        }

        public static UInt64 Current
        {
            get
            {
                return Store.Value;
            }
        }

        public static UInt64 Register(Window window)
        {
            return Store.Register(window);
        }

        public static Window? Get(UInt64 seed)
        {
            return Store.TryGetKey(seed, out Window? window) ? window : null;
        }

        public static void Lock()
        {
            if (!Store.Lock())
            {
                throw new SynchronizationLockException();
            }
        }

        public static void Lock(TimeSpan timeout)
        {
            if (!Store.Lock(timeout))
            {
                throw new SynchronizationLockException();
            }
        }

        public static void Unlock()
        {
            if (!Store.Unlock())
            {
                throw new SynchronizationLockException();
            }
        }
    }
}