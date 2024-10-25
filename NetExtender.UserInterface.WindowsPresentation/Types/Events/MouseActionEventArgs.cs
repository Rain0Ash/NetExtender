using System;
using System.Windows;
using System.Windows.Input;
using NetExtender.Utilities.UserInterface;
using NetExtenderMouseAction = NetExtender.Types.Mouse.MouseAction;

namespace NetExtender.UserInterface.WindowsPresentation.Types.Events
{
    public delegate void MouseActionEventHandler(Object? sender, MouseActionEventArgs args);
    
    public class MouseActionEventArgs : MouseButtonEventArgs
    {
        public NetExtenderMouseAction Action { get; }
        public Point? Position { get; init; }
        public Boolean Preview { get; init; }
        
        public Boolean IsNoClient
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.NoClient);
            }
        }
        
        public Boolean IsStandard
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.Standard);
            }
        }
        
        public Boolean IsMove
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.Move);
            }
        }
        
        public Boolean IsWheel
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.Wheel);
            }
        }
        
        public Boolean IsDoubleClick
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.DoubleClick);
            }
        }
        
        public Boolean IsButton
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.Button);
            }
        }
        
        public Boolean IsXButton
        {
            get
            {
                return Action.HasFlag(NetExtenderMouseAction.XButton);
            }
        }
        
        public MouseActionEventArgs(MouseDevice mouse, Int32 timestamp, NetExtenderMouseAction action)
            : base(mouse, timestamp, action.ToMouseButton())
        {
            Action = action;
        }
        
        public MouseActionEventArgs(MouseDevice mouse, Int32 timestamp, NetExtenderMouseAction action, StylusDevice stylus)
            : base(mouse, timestamp, action.ToMouseButton(), stylus)
        {
            Action = action;
        }
        
        protected override void InvokeEventHandler(Delegate handler, Object? target)
        {
            ((MouseActionEventHandler) handler)(target, this);
        }
    }
}