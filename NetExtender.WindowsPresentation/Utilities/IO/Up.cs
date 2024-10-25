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
        public static class Up
        {
            public static Keys Keys
            {
                get
                {
                    return Keyboard.PrimaryDevice.GetKeys(KeyState.Up);
                }
            }

            public static Boolean Alt
            {
                get
                {
                    return KeyboardUtilities.Alt.IsAltUp;
                }
            }

            public static Boolean Shift
            {
                get
                {
                    return KeyboardUtilities.Shift.IsShiftUp;
                }
            }

            public static Boolean Control
            {
                get
                {
                    return KeyboardUtilities.Control.IsControlUp;
                }
            }

            public static Boolean Windows
            {
                get
                {
                    return KeyboardUtilities.Windows.IsWindowsUp;
                }
            }
        }
    }
}