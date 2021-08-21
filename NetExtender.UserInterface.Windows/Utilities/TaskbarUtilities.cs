// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.UserInterface;
using NetExtender.UserInterface.Interfaces;
using NetExtender.UserInterface.Windows.Taskbar;
using NetExtender.UserInterface.Windows.Taskbar.Interfaces;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.UserInterface
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
    
    public static class TaskbarUtilities
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean FlashWindowEx(ref FlashInfo pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FlashInfo
        {
            public Int32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FlashInfo GetFlashInfo(IntPtr handle, UInt32 flags, UInt32 count, UInt32 timeout)
        {
            return new FlashInfo
            {
                cbSize = ReflectionUtilities.GetSize<FlashInfo>(),
                hwnd = handle,
                dwFlags = flags,
                uCount = count,
                dwTimeout = timeout
            };
        }
        
        private static ITaskbarList4? Taskbar { get; }
        
        static TaskbarUtilities()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            Taskbar = new CTaskbarList() as ITaskbarList4;
            Taskbar?.HrInit();
        }
        
        /// <summary>
        /// Displays or updates a progress bar hosted in a taskbar button of the given window handle 
        /// to show the specific percentage completed of the full operation.
        /// </summary>
        /// <param name="handle">The handle of the window whose associated taskbar button is being used as a progress indicator.
        /// This window belong to a calling process associated with the button's application and must be already loaded.</param>
        /// <param name="current">An application-defined value that indicates the proportion of the operation that has been completed at the time the method is called.</param>
        /// <param name="maximum">An application-defined value that specifies the value currentValue will have when the operation is complete.</param>
        private static Boolean SetProgressValue(IntPtr handle, UInt32 current, UInt32 maximum)
        {
            if (Taskbar is null)
            {
                return false;
            }

            if (handle == IntPtr.Zero)
            {
                return false;
            }

            Taskbar.SetProgressValue(handle, current, maximum);
            return true;
        }

        /// <summary>
        /// Sets the type and state of the progress indicator displayed on a taskbar button 
        /// of the given window handle 
        /// </summary>
        /// <param name="handle">The handle of the window whose associated taskbar button is being used as a progress indicator.
        /// This window belong to a calling process associated with the button's application and must be already loaded.</param>
        /// <param name="state">Progress state of the progress button</param>
        private static Boolean SetProgressState(IntPtr handle, TaskbarProgressBarState state)
        {
            if (Taskbar is null)
            {
                return false;
            }
            
            if (handle == IntPtr.Zero)
            {
                return false;
            }
            
            Taskbar.SetProgressState(handle, state);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Flash(IntPtr handle, FlashState flags, UInt32 count = UInt32.MaxValue)
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }
            
            FlashInfo info = GetFlashInfo(handle, flags.ToUInt32(), count, 0);
            return FlashWindowEx(ref info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(IntPtr handle)
        {
            return Flash(handle, FlashState.Notify);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Notify(handle.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartNotifyState(IntPtr handle)
        {
            return SetProgressState(handle, TaskbarProgressBarState.NoProgress) & Notify(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopNotifyState(IntPtr handle)
        {
            return SetProgressState(handle, TaskbarProgressBarState.NoProgress) & Stop(handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetNotifyStatus(IntPtr handle, TaskbarProgressBarState state)
        {
            return SetProgressState(handle, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetNotifyStatus(IntPtr handle, UInt32 value, UInt32 maximum, TaskbarProgressBarState state)
        {
            return SetProgressState(handle, state) & SetProgressValue(handle, value.Clamp(0, maximum), maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(IntPtr handle, UInt32 value, UInt32 maximum, TaskbarFlashState state)
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
        public static Boolean Notify<T>(this T handle, UInt32 value, UInt32 maximum, TaskbarFlashState state) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Notify(handle.Handle, value, maximum, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(IntPtr handle, UInt32 value, TaskbarFlashState state)
        {
            return Notify(handle, value, 100, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, UInt32 value, TaskbarFlashState state) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Notify(handle.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(IntPtr handle, Double value, TaskbarFlashState state)
        {
            return Notify(handle, (UInt32) value.Clamp(0, 1).Round(2) * 100, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, Double value, TaskbarFlashState state) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Notify(handle.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(IntPtr handle, TaskbarFlashState state)
        {
            return Notify(handle, 100, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify<T>(this T handle, TaskbarFlashState state) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Notify(handle.Handle, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(IntPtr handle)
        {
            return Start(handle, UInt32.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(IntPtr handle, UInt32 count)
        {
            return Flash(handle, FlashState.All, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start<T>(this T handle) where T : IUserInterfaceHandle
        {
            return Start(handle, UInt32.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start<T>(this T handle, UInt32 count) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Start(handle.Handle, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop(IntPtr handle)
        {
            return Flash(handle, FlashState.Stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop<T>(this T handle) where T : IUserInterfaceHandle
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return Stop(handle.Handle);
        }

        /// <summary>
        /// Sets the visibility of the taskbar.
        /// </summary>
        public static Boolean IsTaskbarVisible
        {
            set
            {
                SetTaskbarVisibility(value);
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(String lpClassName, String? lpWindowName);
        
        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hwnd, UInt32 nCmdShow);
        
        public static Boolean ShowWindow(IntPtr handle, WindowStateType state)
        {
            return ShowWindow(handle, (UInt32) state);
        }
        
        /// <summary>
        /// Hide or show the Windows taskbar and startmenu.
        /// </summary>
        private static void SetTaskbarVisibility(Boolean visible)
        {
            IntPtr taskbar = FindWindow("Shell_TrayWnd", null);

            if (taskbar != IntPtr.Zero)
            {
                ShowWindow(taskbar, visible ? WindowStateType.Show : WindowStateType.Hide);
            }
        }
    }
}