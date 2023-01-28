// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Events;

namespace NetExtender.Types.HotKeys.Events
{
    public class WindowsHotKeyEventArgs : HandledEventArgs<Int32>
    {
        public WindowsHotKeyEventArgs(Int32 value)
            : base(value)
        {
        }

        public WindowsHotKeyEventArgs(Int32 value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}