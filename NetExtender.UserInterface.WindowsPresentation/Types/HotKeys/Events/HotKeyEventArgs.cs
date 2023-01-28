// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Events;

namespace NetExtender.Types.HotKeys.Events
{
    public class HotKeyEventArgs : HandledEventArgs<HotKeyAction<Int32>>
    {
        public HotKeyEventArgs(HotKeyAction<Int32> value)
            : base(value)
        {
        }

        public HotKeyEventArgs(HotKeyAction<Int32> value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}