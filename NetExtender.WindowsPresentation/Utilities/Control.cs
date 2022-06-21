// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        public static class Control
        {
            public static Boolean IsControl
            {
                get
                {
                    return Keyboard.Modifiers == ModifierKeys.Control;
                }
            }
            
            public static Boolean HasControl
            {
                get
                {
                    return Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
                }
            }
            
            public static Boolean IsControlDown
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyDown, Key.LeftCtrl, Key.RightCtrl);
                }
            }
            
            public static Boolean IsControlToggled
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyToggled, Key.LeftCtrl, Key.RightCtrl);
                }
            }
            
            public static Boolean IsControlUp
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyUp, Key.LeftCtrl, Key.RightCtrl);
                }
            }
        }
    }
}