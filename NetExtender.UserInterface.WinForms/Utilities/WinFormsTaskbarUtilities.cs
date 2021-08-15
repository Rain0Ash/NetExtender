// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NetExtender.Utilities.UserInterface
{
    public static class WinFormsTaskbarUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Notify(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, UInt32 value, UInt32 maximum, TaskbarFlashState state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Notify(form.Handle, value, maximum, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, UInt32 value, TaskbarFlashState state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Notify(form.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, Double value, TaskbarFlashState state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Notify(form.Handle, value, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Notify(this Form form, TaskbarFlashState state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Notify(form.Handle, state);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Start(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Start(this Form form, UInt32 count)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Start(form.Handle, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Stop(this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return TaskbarUtilities.Stop(form.Handle);
        }
    }
}