// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NetExtender.Utils.UserInterface
{
    public static class WPFTaskbarUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Notify(window.GetHandle());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, UInt32 value, UInt32 maximum, TaskbarFlashState state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Notify(window.GetHandle(), value, maximum, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, UInt32 value, TaskbarFlashState state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Notify(window.GetHandle(), value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, Double value, TaskbarFlashState state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Notify(window.GetHandle(), value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Window window, TaskbarFlashState state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Notify(window.GetHandle(), state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Start(window.GetHandle(), UInt32.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Window window, UInt32 count)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Start(window.GetHandle(), count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return TaskbarUtils.Stop(window.GetHandle());
        }
    }
}