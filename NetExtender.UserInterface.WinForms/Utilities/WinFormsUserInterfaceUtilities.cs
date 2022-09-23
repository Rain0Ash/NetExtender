// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NetExtender.UserInterface;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Workstation;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsUserInterfaceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetWindowRectangle(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return UserInterfaceUtilities.GetWindowRectangle(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ShowWindow(this Form form, WindowStateType state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return UserInterfaceUtilities.ShowWindow(form.Handle, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return UserInterfaceUtilities.BringToForegroundWindow(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Form form)
        {
            SetPrimaryScreenPercentageSize(form, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Form form, Byte percentage)
        {
            SetPrimaryScreenPercentageSize(form, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Form form, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            SetScreenPercentageSize(form, ScreenWrapper.PrimaryScreen, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Form form)
        {
            SetHandleScreenPercentageSize(form, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Form form, Byte percentage)
        {
            SetHandleScreenPercentageSize(form, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Form form, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            SetScreenPercentageSize(form, ScreenWrapper.FromHandle(form.Handle), width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize(this Form form, IScreen screen)
        {
            SetScreenPercentageSize(form, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize(this Form form, IScreen screen, Byte percentage)
        {
            SetScreenPercentageSize(form, screen, percentage, percentage);
        }

        public static void SetScreenPercentageSize(this Form form, IScreen screen, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (width > 100)
            {
                width = 100;
            }

            if (height > 100)
            {
                height = 100;
            }

            form.Width = (Int32) (width / 100D * screen.WorkingArea.Width);
            form.Height = (Int32) (height / 100D * screen.WorkingArea.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>(this T window) where T : IWindow
        {
            SetPrimaryScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>(this T window, Byte percentage) where T : IWindow
        {
            SetPrimaryScreenPercentageSize(window, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>(this T window, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            window.SetScreenPercentageSize(ScreenWrapper.PrimaryScreen, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>(this T window) where T : IWindow
        {
            SetHandleScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>(this T window, Byte percentage) where T : IWindow
        {
            SetHandleScreenPercentageSize(window, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>(this T window, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            
            window.SetScreenPercentageSize(ScreenWrapper.FromHandle(window.Handle), width, height);
        }
    }
}