// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using NetExtender.WindowsPresentation.Types;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class Down
        {
            public static Keys Keys
            {
                get
                {
                    return Keyboard.PrimaryDevice.GetKeys(KeyState.Down);
                }
            }

            public static Boolean Alt
            {
                get
                {
                    return KeyboardUtilities.Alt.IsAltDown;
                }
            }

            public static Boolean Shift
            {
                get
                {
                    return KeyboardUtilities.Shift.IsShiftDown;
                }
            }

            public static Boolean Control
            {
                get
                {
                    return KeyboardUtilities.Control.IsControlDown;
                }
            }

            public static Boolean Windows
            {
                get
                {
                    return KeyboardUtilities.Windows.IsWindowsDown;
                }
            }
        }
    }
}