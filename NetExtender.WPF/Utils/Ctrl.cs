// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utils.Windows.IO
{
    public static partial class KeyboardUtils
    {
        public static class Ctrl
        {
            public static Boolean IsCtrl
            {
                get
                {
                    return Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
                }
            }
            
            public static Boolean IsCtrlDown
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyDown, Key.LeftCtrl, Key.RightCtrl);
                }
            }
            
            public static Boolean IsCtrlToggled
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyToggled, Key.LeftCtrl, Key.RightCtrl);
                }
            }
            
            public static Boolean IsCtrlUp
            {
                get
                {
                    return IsKeyActive(Keyboard.IsKeyUp, Key.LeftCtrl, Key.RightCtrl);
                }
            }
        }
    }
}