// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Utilities.Threading;
using NetExtender.Utilities.Types;
using NetExtender.Windows.Types;

namespace NetExtender.Windows.Utilities.IO
{
    public static class KeyboardLayoutUtilities
    {
        private const UInt32 SetKeyboardLayoutToProcess = 0x100;

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(UInt32 thread);

        [DllImport("user32.dll")]
        private static extern unsafe Int32 GetKeyboardLayoutList(Int32 number, IntPtr* lplist);

        [DllImport("user32.dll")]
        private static extern IntPtr ActivateKeyboardLayout(IntPtr handle, UInt32 flags);

        private static IntPtr ActivateKeyboardLayout(IntPtr handle, Boolean process)
        {
            return ActivateKeyboardLayout(handle, process ? SetKeyboardLayoutToProcess : 0);
        }

        [DllImport("user32.dll")]
        private static extern unsafe Boolean SystemParametersInfoW(UInt32 action, UInt32 uiParam, void* pvParam, UInt32 fWinIni);

        private static unsafe Boolean SystemParametersInfoW<T>(UInt32 action, ref T value) where T : unmanaged
        {
            fixed (void* pointer = &value)
            {
                return SystemParametersInfoW(action, 0, pointer, 0);
            }
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static KeyboardLayout[] KeyboardLayouts
        {
            get
            {
                static unsafe KeyboardLayout[] Internal()
                {
                    Int32 size = GetKeyboardLayoutList(0, null);

                    IntPtr[] pointers = new IntPtr[size];
                    fixed (IntPtr* pointer = pointers)
                    {
                        _ = GetKeyboardLayoutList(size, pointer);
                    }

                    return pointers.Select(KeyboardLayout.Create).ToArray();
                }

                return ThreadUtilities.STA(Internal);
            }
        }

        public static KeyboardLayout DefaultKeyboardLayout
        {
            get
            {
                const UInt32 GETDEFAULTINPUTLANG = 89U;
                IntPtr handle = IntPtr.Zero;
                SystemParametersInfoW(GETDEFAULTINPUTLANG, ref handle);
                return KeyboardLayout.Create(handle);
            }
        }

        public static KeyboardLayout KeyboardLayout
        {
            get
            {
                return ThreadUtilities.STA(() => KeyboardLayout.Create(GetKeyboardLayout(0)));
            }
            set
            {
                if (!SetKeyboardLayoutInternal(value, false))
                {
                    throw new ArgumentException("Keyboard layout is invalid", nameof(value));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean SetKeyboardLayoutInternal(KeyboardLayout layout, Boolean process)
        {
            IntPtr handle = (IntPtr) layout;
            if (handle == IntPtr.Zero)
            {
                handle = (IntPtr) DefaultKeyboardLayout;
            }

            return ThreadUtilities.STA(ActivateKeyboardLayout, handle, process) != IntPtr.Zero;
        }

        public static Boolean SetKeyboardLayout(KeyboardLayout layout)
        {
            return SetKeyboardLayout(layout, false);
        }

        public static Boolean SetKeyboardLayout(KeyboardLayout layout, Boolean process)
        {
            try
            {
                KeyboardLayout current = KeyboardLayout;

                if (current == layout)
                {
                    return true;
                }

                return SetKeyboardLayoutInternal(layout, process) && KeyboardLayout == layout;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean Next()
        {
            return Next(false);
        }

        public static Boolean Next(Boolean process)
        {
            return Next(1, process);
        }

        public static Boolean Next(Int32 count)
        {
            return Next(count, false);
        }

        public static Boolean Next(Int32 count, Boolean process)
        {
            if (count == 0)
            {
                count = 1;
            }

            try
            {
                KeyboardLayout[] layouts = KeyboardLayouts;

                if (layouts.Length <= 0)
                {
                    return false;
                }

                if (count % layouts.Length == 0)
                {
                    return true;
                }

                KeyboardLayout current = KeyboardLayout;

                Int32 index = layouts.IndexOf(current);

                if (index == -1)
                {
                    return false;
                }

                KeyboardLayout next = layouts[unchecked(index + count) % layouts.Length];
                SetKeyboardLayoutInternal(next, process);

                return KeyboardLayout == next;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}