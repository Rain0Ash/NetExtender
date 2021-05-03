// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using NetExtender.Utils.WPF;
using NetExtender.Utils.Types;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.Core;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.GUI.Flash
{
    public enum TaskbarFlashState
    {
        None,
        Flash,
        Normal,
        Indeterminate,
        Pause,
        Error
    }
    
    public static class FlashUtils
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public Int32 cbSize;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FLASHWINFO GetFLASHWINFO(IntPtr handle, UInt32 flags, UInt32 count, UInt32 timeout)
        {
            return new FLASHWINFO
            {
                cbSize = ReflectionUtils.GetSize<FLASHWINFO>(),
                hwnd = handle,
                dwFlags = flags,
                uCount = count,
                dwTimeout = timeout
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Flash(IntPtr handle, Flashw flags, UInt32 count = UInt32.MaxValue)
        {
            FLASHWINFO fi = GetFLASHWINFO(handle, flags.ToUInt32(), count, 0);
            return FlashWindowEx(ref fi);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Notify(IntPtr handle)
        {
            return Flash(handle, Flashw.Notify);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form)
        {
            return Notify(form.Handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window)
        {
            return Notify(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle) where T : IGUIHandle
        {
            return Notify(handle.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean StartNotifyState(IntPtr handle)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, handle);
            return Notify(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean StopNotifyState(IntPtr handle)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, handle);
            return Stop(handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean SetNotifyStatus(IntPtr handle, TaskbarProgressBarState state)
        {
            TaskbarManager.Instance.SetProgressState(state, handle);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean SetNotifyStatus(IntPtr handle, Int32 value, Int32 maximum, TaskbarProgressBarState state)
        {
            if (maximum < 0)
            {
                maximum = 0;
            }
            
            TaskbarManager.Instance.SetProgressState(state, handle);
            TaskbarManager.Instance.SetProgressValue(value.ToRange(0, maximum), maximum, handle);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Notify(IntPtr handle, Int32 value, Int32 maximum, TaskbarFlashState state)
        {
            return state switch
            {
                TaskbarFlashState.None => StopNotifyState(handle),
                TaskbarFlashState.Flash => StartNotifyState(handle),
                TaskbarFlashState.Normal => SetNotifyStatus(handle, value, maximum, TaskbarProgressBarState.Normal),
                TaskbarFlashState.Indeterminate => SetNotifyStatus(handle, TaskbarProgressBarState.Indeterminate),
                TaskbarFlashState.Pause => SetNotifyStatus(handle, value, maximum, TaskbarProgressBarState.Paused),
                TaskbarFlashState.Error => SetNotifyStatus(handle, value, maximum, TaskbarProgressBarState.Error),
                _ => throw new NotSupportedException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, Int32 value, Int32 maximum, TaskbarFlashState state)
        {
            return Notify(form.Handle, value, maximum, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, Int32 value, Int32 maximum, TaskbarFlashState state)
        {
            return Notify(window.GetHandle(), value, maximum, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, Int32 value, Int32 maximum, TaskbarFlashState state) where T : IGUIHandle
        {
            return Notify(handle.Handle, value, maximum, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Notify(IntPtr handle, Int32 value, TaskbarFlashState state)
        {
            return Notify(handle, value, 100, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, Int32 value, TaskbarFlashState state)
        {
            return Notify(form.Handle, value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, Int32 value, TaskbarFlashState state)
        {
            return Notify(window.GetHandle(), value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, Int32 value, TaskbarFlashState state) where T : IGUIHandle
        {
            return Notify(handle.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Notify(IntPtr handle, Double value, TaskbarFlashState state)
        {
            return Notify(handle, (Int32) value.ToRange(0, 1).Round(2) * 100, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, Double value, TaskbarFlashState state)
        {
            return Notify(form.Handle, value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, Double value, TaskbarFlashState state)
        {
            return Notify(window.GetHandle(), value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, Double value, TaskbarFlashState state) where T : IGUIHandle
        {
            return Notify(handle.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Notify(IntPtr handle, TaskbarFlashState state)
        {
            return Notify(handle, 100, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, TaskbarFlashState state)
        {
            return Notify(form.Handle, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, TaskbarFlashState state)
        {
            return Notify(window.GetHandle(), state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, TaskbarFlashState state) where T : IGUIHandle
        {
            return Notify(handle.Handle, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Start(IntPtr handle, UInt32 count)
        {
            return Flash(handle, Flashw.All, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Form form, UInt32 count = UInt32.MaxValue)
        {
            return Start(form.Handle, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Window window, UInt32 count = UInt32.MaxValue)
        {
            return Start(window.GetHandle(), count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start<T>(this T handle, UInt32 count = UInt32.MaxValue) where T : IGUIHandle
        {
            return Start(handle.Handle, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Stop(IntPtr handle)
        {
            return Flash(handle, Flashw.Stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop(this Form form)
        {
            return Stop(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop(this Window window)
        {
            return Stop(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop<T>(this T handle) where T : IGUIHandle
        {
            return Stop(handle.Handle);
        }
    }
}