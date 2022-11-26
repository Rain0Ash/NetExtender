// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.UserInterface;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationUserInterfaceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle GetWindowRectangle(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return UserInterfaceUtilities.GetWindowRectangle(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ShowWindow(this Window window, WindowStateType state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return UserInterfaceUtilities.ShowWindow(window.GetHandle(), state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return UserInterfaceUtilities.BringToForegroundWindow(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize(this Window window, IScreen screen)
        {
            SetScreenPercentageSize(window, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize(this Window window, IScreen screen, Byte percentage)
        {
            SetScreenPercentageSize(window, screen, percentage, percentage);
        }

        public static void SetScreenPercentageSize(this Window window, IScreen screen, Byte width, Byte height)
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