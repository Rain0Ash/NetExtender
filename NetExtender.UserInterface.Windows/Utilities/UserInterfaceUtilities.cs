// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Types.Native.Windows;
using NetExtender.UserInterface;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public static class UserInterfaceUtilities
    {
        public static UserInterfaceActionType Additional(this UserInterfaceActionType action, Byte additional)
        {
            return action | (UserInterfaceActionType) ((UInt64) UserInterfaceActionType.Additional << additional);
        }

        public const Int32 Distance = 5;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, out UInt32 lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, IntPtr processId);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Boolean AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Boolean BringWindowToTop(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hwnd, UInt32 nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowRect(IntPtr hwnd, out WinRectangle lpRect);

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

        //TODO: добавить установку, проверку и удаление пунктов меню
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, Boolean bRevert = false);

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
        private static extern Int32 GetWindowText(IntPtr hwnd, StringBuilder text, Int32 count);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Boolean EnumThreadWindows(Int32 threadId, EnumThreadProc pfnEnum, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, String className, String windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, String windowText);

        [DllImport("user32.dll")]
        private static extern Int32 ShowWindow(IntPtr hwnd, Int32 nCmdShow);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, out Int32 lpdwProcessId);

        private delegate Boolean EnumThreadProc(IntPtr hwnd, IntPtr lParam);

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

        private static Boolean GetSystemMenuItem(IntPtr hwnd, UInt32 index, out MenuItemInfo info)
        {
            if (hwnd == IntPtr.Zero)
            {
                info = default;
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);

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

        private static IEnumerable<MenuItemInfo> GetSystemMenuItemsInternal(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                yield break;
            }

            IntPtr menu = GetSystemMenu(hwnd);

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

        public static Boolean ContainsMenuSeparator(IntPtr hwnd, String title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            return GetSystemMenuItemsInternal(hwnd).Any(item => item.TypeData == title);
        }

        public static Boolean AppendMenuSeparator(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && AppendMenu(menu, (Int32) WindowsMenuItemType.Separator, 0, String.Empty);
        }

        public static Boolean InsertMenuSeparator(IntPtr hwnd, Byte position)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && InsertMenu(menu, position, (UInt32) WindowsMenuItemType.Separator, 0, String.Empty);
        }

        public static Boolean AppendMenu(IntPtr hwnd, Int32 id, String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && AppendMenu(menu, (UInt32) WindowsMenuItemType.String, id, item);
        }

        public static Boolean InsertMenu(IntPtr hwnd, Byte position, Int32 id, String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && InsertMenu(menu, position, (UInt32) WindowsMenuItemType.String, id, item);
        }

        public static Boolean RemoveMenuAt(IntPtr hwnd, Byte position)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && DeleteMenu(menu, position, true);
        }

        public static Boolean RemoveMenuBy(IntPtr hwnd, Int32 command)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            IntPtr menu = GetSystemMenu(hwnd);
            return menu != IntPtr.Zero && DeleteMenu(menu, command, false);
        }

        public static Boolean ResetSystemMenu(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            return GetSystemMenu(hwnd, true) == IntPtr.Zero;
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

        private static Boolean Bring(IntPtr hwnd)
        {
            BringWindowToTop(hwnd);
            ShowWindow(hwnd, 5);
            return true;
        }

        private static Boolean AttachAndBring(UInt32 thread, UInt32 application, IntPtr hwnd)
        {
            AttachThreadInput(thread, application, true);
            Bring(hwnd);
            AttachThreadInput(thread, application, false);
            return true;
        }

        public static Boolean BringToForegroundWindow(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            UInt32 thread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            UInt32 application = GetCurrentThreadId();

            return thread == application ? Bring(hwnd) : AttachAndBring(thread, application, hwnd);
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