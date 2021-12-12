// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Events;

namespace NetExtender.Types.Dispatchers
{
    public delegate void DispatcherShutdownStateEventHandler(Object? sender, DispatcherShutdownStateEventArgs args);
    
    public class DispatcherShutdownStateEventArgs : HandledEventArgs<DispatcherShutdownState>
    {
        public DispatcherShutdownStateEventArgs(DispatcherShutdownState value)
            : base(value)
        {
        }

        public DispatcherShutdownStateEventArgs(DispatcherShutdownState value, Boolean handled)
            : base(value, handled)
        {
        }
    }
}