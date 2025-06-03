// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NetExtender.Types.Exceptions;
using NetExtender.Windows;
using NetExtender.Windows.Utilities.Native;
using NetExtenderMouseAction = NetExtender.Types.Mouse.MouseAction;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationMouseUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseButton(this WM value)
        {
            return AsMouseButton(value) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseButton(this WM value, out Boolean client)
        {
            Boolean result = AsMouseButton(value) is not null;
            client = !value.IsNoClient();
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MouseButton? AsMouseButton(this WM value)
        {
            return value switch
            {
                WM.LBUTTONDOWN => MouseButton.Left,
                WM.NCLBUTTONDOWN => MouseButton.Left,
                WM.LBUTTONUP => MouseButton.Left,
                WM.NCLBUTTONUP => MouseButton.Left,
                WM.LBUTTONDBLCLK => MouseButton.Left,
                WM.NCLBUTTONDBLCLK => MouseButton.Left,
                WM.RBUTTONDOWN => MouseButton.Right,
                WM.NCRBUTTONDOWN => MouseButton.Right,
                WM.RBUTTONUP => MouseButton.Right,
                WM.NCRBUTTONUP => MouseButton.Right,
                WM.RBUTTONDBLCLK => MouseButton.Right,
                WM.NCRBUTTONDBLCLK => MouseButton.Right,
                WM.MBUTTONDOWN => MouseButton.Middle,
                WM.NCMBUTTONDOWN => MouseButton.Middle,
                WM.MBUTTONUP => MouseButton.Middle,
                WM.NCMBUTTONUP => MouseButton.Middle,
                WM.MBUTTONDBLCLK => MouseButton.Middle,
                WM.NCMBUTTONDBLCLK => MouseButton.Middle,
                WM.XBUTTONDOWN => MouseButton.XButton1,
                WM.NCXBUTTONDOWN => MouseButton.XButton2,
                WM.XBUTTONUP => MouseButton.XButton1,
                WM.NCXBUTTONUP => MouseButton.XButton2,
                WM.XBUTTONDBLCLK => MouseButton.XButton1,
                WM.NCXBUTTONDBLCLK => MouseButton.XButton2,
                _ => null
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton? AsMouseButton(this WM value, out Boolean client)
        {
            MouseButton? result = AsMouseButton(value);
            client = !value.IsNoClient();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton ToMouseButton(this WM value)
        {
            return AsMouseButton(value) ?? throw new EnumUndefinedOrNotSupportedException<WM>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton ToMouseButton(this WM value, out Boolean client)
        {
            MouseButton result = ToMouseButton(value);
            client = !value.IsNoClient();
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseAction(this WM value)
        {
            return AsMouseAction(value) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseAction(this WM value, out Boolean client)
        {
            Boolean result = AsMouseAction(value) is not null;
            client = !value.IsNoClient();
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MouseAction? AsMouseAction(this WM value)
        {
            return value switch
            {
                WM.MOUSEWHEEL => MouseAction.WheelClick,
                WM.MOUSEHWHEEL => MouseAction.WheelClick,
                WM.LBUTTONDOWN => MouseAction.LeftClick,
                WM.NCLBUTTONDOWN => MouseAction.LeftClick,
                WM.LBUTTONUP => MouseAction.LeftClick,
                WM.NCLBUTTONUP => MouseAction.LeftClick,
                WM.LBUTTONDBLCLK => MouseAction.LeftDoubleClick,
                WM.NCLBUTTONDBLCLK => MouseAction.LeftDoubleClick,
                WM.RBUTTONDOWN => MouseAction.RightClick,
                WM.NCRBUTTONDOWN => MouseAction.RightClick,
                WM.RBUTTONUP => MouseAction.RightClick,
                WM.NCRBUTTONUP => MouseAction.RightClick,
                WM.RBUTTONDBLCLK => MouseAction.RightDoubleClick,
                WM.NCRBUTTONDBLCLK => MouseAction.RightDoubleClick,
                WM.MBUTTONDOWN => MouseAction.MiddleClick,
                WM.NCMBUTTONDOWN => MouseAction.MiddleClick,
                WM.MBUTTONUP => MouseAction.MiddleClick,
                WM.NCMBUTTONUP => MouseAction.MiddleClick,
                WM.MBUTTONDBLCLK => MouseAction.MiddleDoubleClick,
                WM.NCMBUTTONDBLCLK => MouseAction.MiddleDoubleClick,
                _ => null
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction? AsMouseAction(this WM value, out Boolean client)
        {
            MouseAction? result = AsMouseAction(value);
            client = !value.IsNoClient();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouseAction(this WM value)
        {
            return AsMouseAction(value) ?? throw new EnumUndefinedOrNotSupportedException<WM>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouseAction(this WM value, out Boolean client)
        {
            MouseAction result = ToMouseAction(value);
            client = !value.IsNoClient();
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseAction(this NetExtenderMouseAction value)
        {
            return AsMouseAction(value) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseAction(this NetExtenderMouseAction value, out Boolean client)
        {
            return AsMouseAction(value, out client) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MouseAction? AsMouseAction(this NetExtenderMouseAction value)
        {
            return (value & (~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick)) switch
            {
                NetExtenderMouseAction.VerticalWheel & ~NetExtenderMouseAction.Modifiers => MouseAction.WheelClick,
                NetExtenderMouseAction.VerticalWheel & ~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick => MouseAction.WheelClick,
                NetExtenderMouseAction.HorizontalWheel & ~NetExtenderMouseAction.Modifiers => MouseAction.WheelClick,
                NetExtenderMouseAction.HorizontalWheel & ~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick => MouseAction.WheelClick,
                NetExtenderMouseAction.Left & ~NetExtenderMouseAction.Modifiers => MouseAction.LeftClick,
                NetExtenderMouseAction.Left & ~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick => MouseAction.LeftDoubleClick,
                NetExtenderMouseAction.Right & ~NetExtenderMouseAction.Modifiers => MouseAction.RightClick,
                NetExtenderMouseAction.Right & ~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick => MouseAction.RightDoubleClick,
                NetExtenderMouseAction.Middle & ~NetExtenderMouseAction.Modifiers => MouseAction.MiddleClick,
                NetExtenderMouseAction.Middle & ~NetExtenderMouseAction.Modifiers | NetExtenderMouseAction.DoubleClick => MouseAction.MiddleDoubleClick,
                _ => null
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction? AsMouseAction(this NetExtenderMouseAction value, out Boolean client)
        {
            MouseAction? result = AsMouseAction(value);
            client = value.HasFlag(NetExtenderMouseAction.NoClient);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouseAction(this NetExtenderMouseAction value)
        {
            return AsMouseAction(value) ?? throw new EnumUndefinedOrNotSupportedException<NetExtenderMouseAction>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouseAction(this NetExtenderMouseAction value, out Boolean client)
        {
            return AsMouseAction(value, out client) ?? throw new EnumUndefinedOrNotSupportedException<NetExtenderMouseAction>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseButton(this NetExtenderMouseAction value)
        {
            return AsMouseButton(value) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouseButton(this NetExtenderMouseAction value, out Boolean client)
        {
            return AsMouseButton(value, out client) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MouseButton? AsMouseButton(this NetExtenderMouseAction value)
        {
            return (value & ~NetExtenderMouseAction.Modifiers) switch
            {
                NetExtenderMouseAction.Left & ~NetExtenderMouseAction.Modifiers => MouseButton.Left,
                NetExtenderMouseAction.Right & ~NetExtenderMouseAction.Modifiers => MouseButton.Right,
                NetExtenderMouseAction.Middle & ~NetExtenderMouseAction.Modifiers => MouseButton.Middle,
                NetExtenderMouseAction.XButton1 & ~NetExtenderMouseAction.Modifiers => MouseButton.XButton1,
                NetExtenderMouseAction.XButton2 & ~NetExtenderMouseAction.Modifiers => MouseButton.XButton2,
                _ => null
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton? AsMouseButton(this NetExtenderMouseAction value, out Boolean client)
        {
            MouseButton? result = AsMouseButton(value);
            client = value.HasFlag(NetExtenderMouseAction.NoClient);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton ToMouseButton(this NetExtenderMouseAction value)
        {
            return AsMouseButton(value) ?? throw new EnumUndefinedOrNotSupportedException<NetExtenderMouseAction>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseButton ToMouseButton(this NetExtenderMouseAction value, out Boolean client)
        {
            return AsMouseButton(value, out client) ?? throw new EnumUndefinedOrNotSupportedException<NetExtenderMouseAction>(value, nameof(value), null);
        }
    }
}