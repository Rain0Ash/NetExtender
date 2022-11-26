// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Workstation;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationFormsUserInterfaceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Window window)
        {
            SetPrimaryScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Window window, Byte percentage)
        {
            SetHandleScreenPercentageSize(window, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Window window, Byte width, Byte height)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            window.SetScreenPercentageSize(ScreenWrapper.PrimaryScreen, width, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Window window)
        {
            SetHandleScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize(this Window window, Byte percentage)
        {
            SetPrimaryScreenPercentageSize(window, percentage, percentage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize(this Window window, Byte width, Byte height)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            window.SetScreenPercentageSize(ScreenWrapper.FromHandle(window.GetHandle()), width, height);
        }
    }
}