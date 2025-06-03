// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtenderMouseAction = NetExtender.Types.Mouse.MouseAction;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Events
{
    public delegate void MouseSystemMenuEventHandler(Object? sender, MouseSystemMenuEventArgs args);
    
    public class MouseSystemMenuEventArgs : MouseActionEventArgs
    {
        public Boolean IsTitleBar { get; init; } = true;
        public Boolean IsSystemMenu { get; init; }
        
        public MouseSystemMenuEventArgs(MouseDevice mouse, Int32 timestamp, NetExtenderMouseAction action)
            : base(mouse, timestamp, action)
        {
        }
        
        public MouseSystemMenuEventArgs(MouseDevice mouse, Int32 timestamp, NetExtenderMouseAction action, StylusDevice stylus)
            : base(mouse, timestamp, action, stylus)
        {
        }
        
        protected override void InvokeEventHandler(Delegate handler, Object? target)
        {
            ((MouseSystemMenuEventHandler) handler)(target, this);
        }
    }
}