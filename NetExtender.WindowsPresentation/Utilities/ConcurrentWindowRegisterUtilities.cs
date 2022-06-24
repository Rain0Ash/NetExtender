// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Threading;
using System.Windows;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities
{
    public static class ConcurrentWindowRegisterUtilities
    {
        private static IStore<Window, UInt64> Store { get; } = new WeakStoreStructWrapper<Window, UInt64>();
        public static UInt64 Current { get; private set; }
        
        public static Window? Window
        {
            get
            {
                return Get(Current);
            }
        }
        
        private static UInt64 Count { get; set; }

        public static UInt64 Register(Window window)
        {
            lock (Store)
            {
                return Current = Store.GetOrAdd(window, () => unchecked(++Count));
            }
        }

        public static Window? Get(UInt64 seed)
        {
            lock (Store)
            {
                (Window? key, _) = Store.AsEnumerable().SingleOrDefault(pair => pair.Value == seed);
                return key;
            }
        }

        public static void Lock()
        {
            Lock(Time.Second.Three);
        }

        public static void Lock(TimeSpan timeout)
        {
            if (!Monitor.TryEnter(Store, timeout))
            {
                throw new SynchronizationLockException();
            }
        }

        public static void Unlock()
        {
            Monitor.Exit(Store);
        }
    }
}