// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        public static class Shift
        {
            public static Boolean IsShift
            {
                get
                {
                    return Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
                }
            }

            public static Boolean IsShiftDown
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyDown, Key.LeftShift, Key.RightShift);
                }
            }

            public static Boolean IsShiftToggled
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyToggled, Key.LeftShift, Key.RightShift);
                }
            }

            public static Boolean IsShiftUp
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyUp, Key.LeftShift, Key.RightShift);
                }
            }
        }
    }
}