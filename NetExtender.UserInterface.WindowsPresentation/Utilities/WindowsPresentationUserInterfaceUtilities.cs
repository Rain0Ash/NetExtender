// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.Utilities.UserInterface
{
    public enum InputDirection : Byte
    {
        Horizontal,
        Vertical
    }
    
    public static class WindowsPresentationUserInterfaceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToSymbol(this ListSortDirection value)
        {
            return value switch
            {
                ListSortDirection.Ascending => "▲",
                ListSortDirection.Descending => "▼",
                _ => throw new EnumUndefinedOrNotSupportedException<ListSortDirection>(value, nameof(value), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToInverseSymbol(this ListSortDirection value)
        {
            return value switch
            {
                ListSortDirection.Ascending => "▼",
                ListSortDirection.Descending => "▲",
                _ => throw new EnumUndefinedOrNotSupportedException<ListSortDirection>(value, nameof(value), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean? GetWindowSystemMenu(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return UserInterfaceUtilities.GetWindowSystemMenu(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetWindowSystemMenu(this Window window, Boolean value)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return UserInterfaceUtilities.SetWindowSystemMenu(window.GetHandle(), value);
        }
        
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
        public static Boolean GetWindowDisplayAffinity(this Window window, out WindowDisplayAffinity affinity)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            
            return UserInterfaceUtilities.GetWindowDisplayAffinity(window.GetHandle(), out affinity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetWindowDisplayAffinity(this Window window, WindowDisplayAffinity affinity)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            
            return UserInterfaceUtilities.SetWindowDisplayAffinity(window.GetHandle(), affinity);
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