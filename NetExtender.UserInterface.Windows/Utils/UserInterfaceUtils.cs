// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Core.Workstation.Interfaces;
using NetExtender.Types.Native.Windows;
using NetExtender.UserInterface;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utils.Static;

namespace NetExtender.Utils.UserInterface
{
    public static class UserInterfaceUtils
    {
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
                WindowsInteropUtils.ThrowLastWin32Exception();
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
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, Boolean bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean AppendMenu(IntPtr hMenu, Int32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean InsertMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Int32 DeleteMenu(IntPtr hMenu, Int32 nPosition, Int32 wFlags);
        
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

            window.Width = width / 100d * screen.WorkingArea.Width;
            window.Height = height / 100d * screen.WorkingArea.Height;
        }
    }
}