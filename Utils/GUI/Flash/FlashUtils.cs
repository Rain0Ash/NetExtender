// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using NetExtender.Utils.WPF;
using NetExtender.Utils.Types;
using NetExtender.GUI.Common.Interfaces;

namespace NetExtender.Utils.GUI.Flash
{
    public static class FlashUtils
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [Flags]
        public enum Flashw : UInt32
        {
            /// <summary>
            /// Stop flash the window caption.
            /// </summary>
            Stop = 0,

            /// <summary>
            /// Flash the window caption.
            /// </summary>
            Caption = 1,

            /// <summary>
            /// Flash the taskbar button.
            /// </summary>
            Tray = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// </summary>
            All = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            Timer = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground.
            /// </summary>
            Notify = 15
        }

        private static FLASHWINFO GetFLASHWINFO(IntPtr handle, UInt32 flags, UInt32 count, UInt32 timeout)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        private static Boolean Flash(IntPtr handle, Flashw flags, UInt32 count = UInt32.MaxValue)
        {
            FLASHWINFO fi = GetFLASHWINFO(handle, flags.ToUInt32(), count, 0);
            return FlashWindowEx(ref fi);
        }

        private static Boolean Notify(IntPtr handle)
        {
            return Flash(handle, Flashw.Notify);
        }
        
        public static Boolean Notify(Form form)
        {
            return Notify(form.Handle);
        }

        public static Boolean Notify(Window window)
        {
            return Notify(window.GetHandle());
        }

        public static Boolean Notify(IGUIHandle handle)
        {
            return Notify(handle.Handle);
        }

        private static Boolean Start(IntPtr handle, UInt32 count)
        {
            return Flash(handle, Flashw.All, count);
        }

        public static Boolean Start(Form form, UInt32 count = UInt32.MaxValue)
        {
            return Start(form.Handle, count);
        }
        
        public static Boolean Start(Window window, UInt32 count = UInt32.MaxValue)
        {
            return Start(window.GetHandle(), count);
        }

        public static Boolean Start(IGUIHandle handle, UInt32 count = UInt32.MaxValue)
        {
            return Start(handle.Handle, count);
        }

        private static Boolean Stop(IntPtr handle)
        {
            return Flash(handle, Flashw.Stop);
        }

        public static Boolean Stop(Form form)
        {
            return Stop(form.Handle);
        }
        
        public static Boolean Stop(Window window)
        {
            return Stop(window.GetHandle());
        }

        public static Boolean Stop(IGUIHandle handle)
        {
            return Stop(handle.Handle);
        }
    }
}