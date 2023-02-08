// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Types.HotKeys;
using NetExtender.UserInterface.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsHotKeyUtilities
    {
        public static Boolean RegisterHotKey(this Form form, HotKeyAction hotkey, out Int32 id)
        {
            return RegisterHotKey(form, (WindowsHotKeyAction) hotkey, out id);
        }
        
        public static Boolean RegisterHotKey(this Form form, WindowsHotKeyAction hotkey, out Int32 id)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return HotKeyUtilities.RegisterHotKey(form.Handle, hotkey, out id);
        }
        
        public static Boolean RegisterHotKey<T>(this Form form, HotKeyAction<T> hotkey) where T : unmanaged, IComparable<T>, IConvertible
        {
            return RegisterHotKey(form, (WindowsHotKeyAction<T>) hotkey);
        }
        
        public static Boolean RegisterHotKey<T>(this Form form, WindowsHotKeyAction<T> hotkey) where T : unmanaged, IComparable<T>, IConvertible
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return HotKeyUtilities.RegisterHotKey(form.Handle, hotkey);
        }
        
        public static Int32?[] RegisterHotKey(this Form form, params HotKeyAction[] hotkeys)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return RegisterHotKey(form, hotkeys.Select(hotkey => (WindowsHotKeyAction) hotkey).ToArray());
        }
        
        public static Int32?[] RegisterHotKey(this Form form, params WindowsHotKeyAction[] hotkeys)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return HotKeyUtilities.RegisterHotKey(form.Handle, hotkeys);
        }
        
        public static Int32?[] RegisterHotKey<T>(this Form form, params HotKeyAction<T>[] hotkeys) where T : unmanaged, IComparable<T>, IConvertible
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }
            
            return RegisterHotKey(form, hotkeys.Select(hotkey => (WindowsHotKeyAction<T>) hotkey).ToArray());
        }
        
        public static Int32?[] RegisterHotKey<T>(this Form form, params WindowsHotKeyAction<T>[] hotkeys) where T : unmanaged, IComparable<T>, IConvertible
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (hotkeys is null)
            {
                throw new ArgumentNullException(nameof(hotkeys));
            }

            return HotKeyUtilities.RegisterHotKey(form.Handle, hotkeys);
        }
        
        public static Boolean UnregisterHotKey(this Form form, Int32 id)
        {
            return form switch
            {
                null => throw new ArgumentNullException(nameof(form)),
                IUserInterfaceHandle handle => HotKeyUtilities.UnregisterHotKey(handle.Handle, id),
                _ => HotKeyUtilities.UnregisterHotKey(form.Handle, id)
            };
        }
        
        public static Boolean UnregisterHotKey<T>(this Form form, T id) where T : unmanaged, IConvertible
        {
            return form switch
            {
                null => throw new ArgumentNullException(nameof(form)),
                IUserInterfaceHandle handle => HotKeyUtilities.UnregisterHotKey(handle.Handle, id),
                _ => HotKeyUtilities.UnregisterHotKey(form.Handle, id)
            };
        }
    }
}