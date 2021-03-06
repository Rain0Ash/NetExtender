// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utils.IO
{
    public static partial class KeyboardUtils
    {
        public static class Alt
        {
            public static Boolean IsAlt
            {
                get
                {
                    return Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);
                }
            }
            
            public static Boolean IsAltDown
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyDown, Key.LeftAlt, Key.RightAlt);
                }
            }
            
            public static Boolean IsAltToggled
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyToggled, Key.LeftAlt, Key.RightAlt);
                }
            }
            
            public static Boolean IsAltUp
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyUp, Key.LeftAlt, Key.RightAlt);
                }
            }
        }
    }
}