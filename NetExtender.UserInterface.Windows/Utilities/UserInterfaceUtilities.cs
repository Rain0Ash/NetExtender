// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Windows.Types;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public enum WindowDisplayAffinity : Byte
    {
        None = 0,
        Monitor = 1,
        ExcludeFromCapture = 0x11
    }
    
    public static class UserInterfaceUtilities
    {
        public static UserInterfaceActionType Additional(this UserInterfaceActionType action, Byte additional)
        {
            return action | (UserInterfaceActionType) ((UInt64) UserInterfaceActionType.Additional << additional);
        }

        public const Int32 Distance = 5;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr handle, out UInt32 lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr handle, IntPtr processId);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Boolean AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Boolean BringWindowToTop(IntPtr handle);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr handle, UInt32 nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetClientRect(IntPtr handle, out WinRectangle rectangle);
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowRect(IntPtr handle, out WinRectangle rectangle);
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern Int32 GetWindowLong(IntPtr handle, Int32 index);
        
        [DllImport("user32.dll")]
        private static extern Int32 SetWindowLong(IntPtr handle, Int32 index, Int32 value);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowDisplayAffinity(IntPtr hWnd, out UInt32 affinity);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean SetWindowDisplayAffinity(IntPtr handle, UInt32 affinity);

        public static Boolean GetWindowDisplayAffinity(IntPtr handle, out WindowDisplayAffinity affinity)
        {
            if (handle == IntPtr.Zero || !GetWindowDisplayAffinity(handle, out UInt32 result) || result > Byte.MaxValue)
            {
                affinity = default;
                return false;
            }

            affinity = (WindowDisplayAffinity) result;
            return affinity switch
            {
                WindowDisplayAffinity.None => true,
                WindowDisplayAffinity.Monitor => true,
                WindowDisplayAffinity.ExcludeFromCapture => true,
                _ => false
            };
        }
        
        public static Boolean SetWindowDisplayAffinity(IntPtr handle, WindowDisplayAffinity affinity)
        {
            switch (affinity)
            {
                case WindowDisplayAffinity.None:
                case WindowDisplayAffinity.Monitor:
                case WindowDisplayAffinity.ExcludeFromCapture:
                    return handle != IntPtr.Zero && SetWindowDisplayAffinity(handle, (Byte) affinity);
                default:
                    throw new EnumUndefinedOrNotSupportedException<WindowDisplayAffinity>(affinity, nameof(affinity), null);
            }
        }

        public static Boolean? GetWindowSystemMenu(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            
            const Int32 gwlstyle = -16;
            const Int32 sysmenu = 0x80000;

            Int32 current = GetWindowLong(handle, gwlstyle);
            return (current & sysmenu) == sysmenu;
        }

        public static Boolean SetWindowSystemMenu(IntPtr handle, Boolean value)
        {
            const Int32 gwlstyle = -16;
            const Int32 sysmenu = 0x80000;
            
            if (handle == IntPtr.Zero)
            {
                return false;
            }
            
            Int32 current = GetWindowLong(handle, gwlstyle);

            if (value)
            {
                return SetWindowLong(handle, gwlstyle,  current | sysmenu) == 0;
            }
            
            return SetWindowLong(handle, gwlstyle,  current & ~sysmenu) == 0;
        }

        public static Rectangle GetClientRectangle(IntPtr handle)
        {
            if (!GetClientRect(handle, out WinRectangle rectangle))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }

            return rectangle;
        }

        public static Rectangle GetClientRectangle<T>(this T window) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return GetClientRectangle(window.Handle);
        }

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            if (!GetWindowRect(handle, out WinRectangle rectangle))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }

            return rectangle;
        }

        public static Rectangle GetWindowRectangle<T>(this T window) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return GetWindowRectangle(window.Handle);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr handle, Boolean bRevert = false);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Boolean GetMenuItemInfo(IntPtr hMenu, UInt32 uItem, Boolean fByPosition, [In, Out] MenuItemInfo lpmii);

        private static Boolean GetMenuItemInfoByCommand(IntPtr hMenu, UInt32 uItem, [In, Out] MenuItemInfo lpmii)
        {
            return GetMenuItemInfo(hMenu, uItem, false, lpmii);
        }

        private static Boolean GetMenuItemInfoByPosition(IntPtr hMenu, UInt32 uItem, [In, Out] MenuItemInfo lpmii)
        {
            return GetMenuItemInfo(hMenu, uItem, true, lpmii);
        }

        [DllImport("user32.dll")]
        private static extern Int32 GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean AppendMenu(IntPtr hMenu, UInt32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean InsertMenu(IntPtr hMenu, Int32 uPosition, UInt32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Boolean DeleteMenu(IntPtr hMenu, Int32 nPosition, Boolean uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 GetWindowText(IntPtr handle, StringBuilder text, Int32 count);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Boolean EnumThreadWindows(Int32 threadId, EnumThreadProc pfnEnum, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, String className, String windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, String windowText);

        [DllImport("user32.dll")]
        private static extern Int32 ShowWindow(IntPtr handle, Int32 nCmdShow);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr handle, out Int32 lpdwProcessId);

        private delegate Boolean EnumThreadProc(IntPtr handle, IntPtr lParam);

        [Flags]
        internal enum WindowsMenuItemMask
        {
            State = 1,
            Id = 2,
            SubMenu = 4,
            CheckMarks = 8,
            Type = 16,
            Data = 32,
            String = 64,
            Bitmap = 128,
            FType = 256
        }

        [Flags]
        internal enum WindowsMenuItemType : UInt32
        {
            String = 0,
            Bitmap = 4,
            MenuBarBreak = 32,
            MenuBreak = 64,
            OwnerDraw = 256,
            RadioCheck = 512,
            Separator = 2048,
            RightOrder = 8192,
            RightJustify = 16384
        }

        [Flags]
        internal enum WindowsMenuItemState : UInt32
        {
            None = 0,
            Enabled = None,
            Unchecked = None,
            Unhilite = None,
            Disabled = 3,
            Grayed = Disabled,
            Checked = 8,
            Hilite = 128,
            Default = 4096
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private readonly struct MenuItemInfo
        {
            public static Int32 SizeOf
            {
                get
                {
                    return Marshal.SizeOf(typeof(MenuItemInfo));
                }
            }

            public Int32 Size { get; init; }
            public WindowsMenuItemMask Mask { get; init; }
            public WindowsMenuItemType Type { get; init; }
            public WindowsMenuItemState State { get; init; }
            public UInt32 ID { get; init; }
            public IntPtr SubMenu { get; init; }
            public IntPtr BitmapChecked { get; init; }
            public IntPtr BitmapUnchecked { get; init; }
            public IntPtr ItemData { get; init; }
            public String? TypeData { get; init; }
            public UInt32 Length { get; init; }
            public IntPtr BitmapItem { get; init; }

            public Boolean IsDefault
            {
                get
                {
                    return Size == 0;
                }
            }
        }

        private static Boolean GetSystemMenuItem(IntPtr handle, UInt32 index, out MenuItemInfo info)
        {
            if (handle == IntPtr.Zero)
            {
                info = default;
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);

            if (menu == IntPtr.Zero)
            {
                info = default;
                return false;
            }

            info = new MenuItemInfo
            {
                Size = MenuItemInfo.SizeOf,
                Mask = WindowsMenuItemMask.String,
                Type = WindowsMenuItemType.String
            };

            return GetMenuItemInfoByPosition(menu, index, info);
        }

        private static IEnumerable<MenuItemInfo> GetSystemMenuItemsInternal(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                yield break;
            }

            IntPtr menu = GetSystemMenu(handle);

            if (menu == IntPtr.Zero)
            {
                yield break;
            }

            Int32 count = GetMenuItemCount(menu);
            for (UInt32 i = 0; i < count; i++)
            {
                MenuItemInfo info = new MenuItemInfo
                {
                    Size = MenuItemInfo.SizeOf,
                    Mask = WindowsMenuItemMask.String,
                    Type = WindowsMenuItemType.String
                };

                if (!GetMenuItemInfoByPosition(menu, i, info))
                {
                    yield return default;
                    continue;
                }

                yield return info;
            }
        }

        public static Boolean ContainsMenuSeparator(IntPtr handle, String title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return GetSystemMenuItemsInternal(handle).Any(item => item.TypeData == title);
        }

        public static Boolean AppendMenuSeparator(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && AppendMenu(menu, (Int32) WindowsMenuItemType.Separator, 0, String.Empty);
        }

        public static Boolean InsertMenuSeparator(IntPtr handle, Byte position)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && InsertMenu(menu, position, (UInt32) WindowsMenuItemType.Separator, 0, String.Empty);
        }
        
        private static Boolean ContainsMenu(IntPtr handle, String item)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            for (UInt32 i = 0; i < GetMenuItemCount(menu); i++)
            {
                MenuItemInfo info = new MenuItemInfo
                {
                    Size = MenuItemInfo.SizeOf,
                    Mask = WindowsMenuItemMask.Type,
                    TypeData = new String(' ', 256),
                    Length = 256
                };
                
                if (!GetMenuItemInfoByPosition(menu, i, info))
                {
                    continue;
                }

                if (info.TypeData.Contains(item))
                {
                    return true;
                }
            }
            
            return false;
        }

        public static Boolean AppendMenu(IntPtr handle, Int32 id, String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && AppendMenu(menu, (UInt32) WindowsMenuItemType.String, id, item);
        }

        public static Boolean InsertMenu(IntPtr handle, Byte position, Int32 id, String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && InsertMenu(menu, position, (UInt32) WindowsMenuItemType.String, id, item);
        }

        public static Boolean RemoveMenuAt(IntPtr handle, Byte position)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && DeleteMenu(menu, position, true);
        }

        public static Boolean RemoveMenuBy(IntPtr handle, Int32 command)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(handle);
            return menu != IntPtr.Zero && DeleteMenu(menu, command, false);
        }

        public static Boolean ResetSystemMenu(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            return GetSystemMenu(handle, true) == IntPtr.Zero;
        }

        public static Boolean ShowWindow(IntPtr handle, WindowStateType state)
        {
            return ShowWindow(handle, (UInt32) state);
        }

        public static Boolean ShowWindow<T>(this T window, WindowStateType state) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ShowWindow(window.Handle, state);
        }

        private static Boolean Bring(IntPtr handle)
        {
            BringWindowToTop(handle);
            ShowWindow(handle, 5);
            return true;
        }

        private static Boolean AttachAndBring(UInt32 thread, UInt32 application, IntPtr handle)
        {
            AttachThreadInput(thread, application, true);
            Bring(handle);
            AttachThreadInput(thread, application, false);
            return true;
        }

        public static Boolean BringToForegroundWindow(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            UInt32 thread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            UInt32 application = GetCurrentThreadId();

            return thread == application ? Bring(handle) : AttachAndBring(thread, application, handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground<T>(this T handle) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return BringToForegroundWindow(handle.Handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize<T>(this T window, IScreen screen) where T : IWindow
        {
            SetScreenPercentageSize(window, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize<T>(this T window, IScreen screen, Byte percentage) where T : IWindow
        {
            SetScreenPercentageSize(window, screen, percentage, percentage);
        }

        public static void SetScreenPercentageSize<T>(this T window, IScreen screen, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (width > 100)
            {
                width = 100;
            }

            if (height > 100)
            {
                height = 100;
            }

            window.Width = width / 100D * screen.WorkingArea.Width;
            window.Height = height / 100D * screen.WorkingArea.Height;
        }
    }
}