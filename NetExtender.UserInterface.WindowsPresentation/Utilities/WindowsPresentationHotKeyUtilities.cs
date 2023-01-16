// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Windows;
using NetExtender.Types.HotKeys;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Windows.Utilities;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationHotKeyUtilities
    {
        public static Boolean RegisterHotKey(this Window window, HotKeyAction hotkey, out Int32 id)
        {
            return RegisterHotKey(window, (WindowsHotKeyAction) hotkey, out id);
        }
        
        public static Boolean RegisterHotKey(this Window window, WindowsHotKeyAction hotkey, out Int32 id)
        {
            return window switch
            {
                null => throw new ArgumentNullException(nameof(window)),
                IUserInterfaceHandle handle => WindowsHotKeyUtilities.RegisterHotKey(handle.Handle, hotkey, out id),
                _ => WindowsHotKeyUtilities.RegisterHotKey(window.GetHandle(), hotkey, out id)
            };
        }
        
        public static Boolean RegisterHotKey<T>(this Window window, HotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            return RegisterHotKey(window, (WindowsHotKeyAction<T>) hotkey);
        }
        
        public static Boolean RegisterHotKey<T>(this Window window, WindowsHotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            return window switch
            {
                null => throw new ArgumentNullException(nameof(window)),
                IUserInterfaceHandle handle => WindowsHotKeyUtilities.RegisterHotKey(handle.Handle, hotkey),
                _ => WindowsHotKeyUtilities.RegisterHotKey(window.GetHandle(), hotkey)
            };
        }

        public static Int32?[] RegisterHotKey(this Window window, params HotKeyAction[] hotkeys)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return RegisterHotKey(window, hotkeys.Select(hotkey => (WindowsHotKeyAction) hotkey).ToArray());
        }

        public static Int32?[] RegisterHotKey(this Window window, params WindowsHotKeyAction[] hotkeys)
        {
            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return window switch
            {
                null => throw new ArgumentNullException(nameof(window)),
                IUserInterfaceHandle handle => WindowsHotKeyUtilities.RegisterHotKey(handle.Handle, hotkeys),
                _ => WindowsHotKeyUtilities.RegisterHotKey(window.GetHandle(), hotkeys)
            };
        }
        
        public static Int32?[] RegisterHotKey<T>(this Window window, params HotKeyAction<T>[] hotkeys) where T : unmanaged, IConvertible
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }
            
            return RegisterHotKey(window, hotkeys.Select(hotkey => (WindowsHotKeyAction<T>) hotkey).ToArray());
        }
        
        public static Int32?[] RegisterHotKey<T>(this Window window, params WindowsHotKeyAction<T>[] hotkeys) where T : unmanaged, IConvertible
        {
            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return window switch
            {
                null => throw new ArgumentNullException(nameof(window)),
                IUserInterfaceHandle handle => WindowsHotKeyUtilities.RegisterHotKey(handle.Handle, hotkeys),
                _ => WindowsHotKeyUtilities.RegisterHotKey(window.GetHandle(), hotkeys)
            };
        }
    }
}