// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        public static class Win
        {
            public static Boolean IsWin
            {
                get
                {
                    return Keyboard.Modifiers.HasFlag(ModifierKeys.Windows);
                }
            }
            
            public static Boolean IsWinDown
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyDown, Key.LWin, Key.RWin);
                }
            }
            
            public static Boolean IsWinToggled
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyToggled, Key.LWin, Key.RWin);
                }
            }
            
            public static Boolean IsWinUp
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyUp, Key.LWin, Key.RWin);
                }
            }
        }
    }
}