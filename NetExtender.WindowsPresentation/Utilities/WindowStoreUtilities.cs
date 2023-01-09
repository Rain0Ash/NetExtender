// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using NetExtender.Types.Concurrent.Stores;
using NetExtender.Types.Concurrent.Stores.Interfaces;

namespace NetExtender.WindowsPresentation.Utilities
{
    public static class WindowStoreUtilities<TWindow> where TWindow : Window
    {
        public static TWindow? Window
        {
            get
            {
                return Get();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow? Get()
        {
            return WindowStoreUtilities.Get<TWindow>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow? Get(UInt64 seed)
        {
            return WindowStoreUtilities.Get<TWindow>(seed);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow Require()
        {
            return WindowStoreUtilities.Require<TWindow>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow Require(UInt64 seed)
        {
            return WindowStoreUtilities.Require<TWindow>(seed);
        }
    }

    public static class WindowStoreUtilities
    {
        private static IConcurrentRegisterStore<Window> Store { get; } = new ConcurrentRegisterStore<Window>();

        public static Window? Window
        {
            get
            {
                return Get();
            }
        }

        public static UInt64 Seed
        {
            get
            {
                return Store.Value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Register(Window window)
        {
            return Store.Register(window);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Window? Get()
        {
            return Store.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Window? Get(UInt64 seed)
        {
            return Store.TryGetKey(seed, out Window? window) ? window : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow? Get<TWindow>() where TWindow : Window
        {
            return Convert<TWindow>(Window);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow? Get<TWindow>(UInt64 seed) where TWindow : Window
        {
            return Convert<TWindow>(Get(seed));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Window Require(UInt64 seed)
        {
            return Get(seed) ?? throw new InvalidOperationException("Window not in concurrent store");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow Require<TWindow>() where TWindow : Window
        {
            return Get<TWindow>() ?? throw new InvalidOperationException($"Window of type {typeof(TWindow)} not in concurrent store");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TWindow Require<TWindow>(UInt64 seed) where TWindow : Window
        {
            return Get<TWindow>(seed) ?? throw new InvalidOperationException($"Window of type {typeof(TWindow)} not in concurrent store");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("window")]
        private static TWindow? Convert<TWindow>(Window? window) where TWindow : Window
        {
            return window switch
            {
                null => null,
                TWindow result => result,
                _ => throw new InvalidOperationException($"Concurrent window is not of type {typeof(TWindow)}")
            };
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