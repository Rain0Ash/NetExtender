// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.InteropServices;
using NetExtender.Interfaces.Common;
using NetExtender.Types.HotKeys;

namespace NetExtender.Windows.Utilities
{
    [Flags]
    public enum HotKeyModifierKeys
    {
        /// <summary>No modifiers are pressed.</summary>
        None = 0,

        /// <summary>The ALT key.</summary>
        Alt = 1,

        /// <summary>The CTRL key.</summary>
        Control = 2,

        /// <summary>The SHIFT key.</summary>
        Shift = 4,

        /// <summary>The Windows logo key.</summary>
        Windows = 8,
    }

    public static class WindowsHotKeyUtilities
    {
        [DllImport("user32.dll")]
        private static extern Boolean RegisterHotKey(IntPtr handle, Int32 id, Int32 modifiers, Int32 key);

        private static Boolean RegisterHotKey(IntPtr handle, Int32 id, Char key, HotKeyModifierKeys modifiers)
        {
            return handle != IntPtr.Zero && RegisterHotKey(handle, id, (Int32) modifiers, key);
        }

        private static ConcurrentDictionary<IntPtr, Int32> Counter { get; } = new ConcurrentDictionary<IntPtr, Int32>();

        public static Boolean RegisterHotKey(IntPtr handle, WindowsHotKeyAction hotkey, out Int32 id)
        {
            if (handle == IntPtr.Zero)
            {
                id = default;
                return false;
            }

            id = Counter.AddOrUpdate(handle, 0, (_, i) => ++i);
            return RegisterHotKey(handle, new WindowsHotKeyAction<Int32>(id, hotkey));
        }

        public static Boolean RegisterHotKey(IHandle handle, WindowsHotKeyAction hotkey, out Int32 id)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return RegisterHotKey(handle.Handle, hotkey, out id);
        }

        public static Boolean RegisterHotKey<T>(IntPtr handle, WindowsHotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            return handle != IntPtr.Zero && RegisterHotKey(handle, hotkey.Id.ToInt32(CultureInfo.InvariantCulture), hotkey.Key, hotkey.Modifier);
        }
        
        public static Boolean RegisterHotKey<T>(IHandle handle, WindowsHotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return RegisterHotKey(handle.Handle, hotkey);
        }

        public static Int32?[] RegisterHotKey(IntPtr handle, params WindowsHotKeyAction[] hotkeys)
        {
            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            if (handle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            Int32?[] array = new Int32?[hotkeys.Length];

            for (Int32 i = 0; i < hotkeys.Length; i++)
            {
                array[i] = RegisterHotKey(handle, hotkeys[i], out Int32 id) ? id : null;
            }

            return array;
        }

        public static Int32?[] RegisterHotKey(IHandle handle, params WindowsHotKeyAction[] hotkeys)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return RegisterHotKey(handle.Handle, hotkeys);
        }

        public static Int32?[] RegisterHotKey<T>(IntPtr handle, params WindowsHotKeyAction<T>[] hotkeys) where T : unmanaged, IConvertible
        {
            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            if (handle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            Int32?[] array = new Int32?[hotkeys.Length];

            for (Int32 i = 0; i < hotkeys.Length; i++)
            {
                array[i] = RegisterHotKey(handle, hotkeys[i]) ? hotkeys[i].Id.ToInt32(CultureInfo.InvariantCulture) : null;
            }

            return array;
        }
        
        public static Int32?[] RegisterHotKey<T>(IHandle handle, params WindowsHotKeyAction<T>[] hotkeys) where T : unmanaged, IConvertible
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return RegisterHotKey(handle.Handle, hotkeys);
        }
    }
}