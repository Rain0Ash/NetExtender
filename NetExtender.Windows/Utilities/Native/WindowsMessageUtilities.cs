// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Mouse;
using NetExtender.Windows.Types;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Windows.Utilities.Native
{
    public static class WindowsMessageUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsNoClient(this WM value)
        {
            return value switch
            {
                WM.NCCREATE => true,
                WM.NCDESTROY => true,
                WM.NCCALCSIZE => true,
                WM.NCHITTEST => true,
                WM.NCPAINT => true,
                WM.NCACTIVATE => true,
                WM.NCMOUSEMOVE => true,
                WM.NCMOUSEHOVER => true,
                WM.NCLBUTTONDOWN => true,
                WM.NCLBUTTONUP => true,
                WM.NCLBUTTONDBLCLK => true,
                WM.NCRBUTTONDOWN => true,
                WM.NCRBUTTONUP => true,
                WM.NCRBUTTONDBLCLK => true,
                WM.NCMBUTTONDOWN => true,
                WM.NCMBUTTONUP => true,
                WM.NCMBUTTONDBLCLK => true,
                WM.NCXBUTTONDOWN => true,
                WM.NCXBUTTONUP => true,
                WM.NCXBUTTONDBLCLK => true,
                WM.NCMOUSELEAVE => true,
                WM.DWMNCRENDERINGCHANGED => true,
                _ => false
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWheel(this WM value)
        {
            return value switch
            {
                WM.MOUSEWHEEL => true,
                WM.MOUSEHWHEEL => true,
                _ => false
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWheel(this MouseAction value)
        {
            return value switch
            {
                MouseAction.VerticalWheel => true,
                MouseAction.HorizontalWheel => true,
                _ => false
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouse(this WM value)
        {
            return AsMouse(value) is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMouse(this WM value, out Boolean client)
        {
            Boolean result = AsMouse(value) is not null;
            client = !IsNoClient(value);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MouseAction? AsMouse(this WM value)
        {
            return value switch
            {
                WM.MOUSEMOVE => MouseAction.Move,
                WM.NCMOUSEMOVE => MouseAction.Move | MouseAction.NoClient,
                WM.MOUSEHOVER => MouseAction.Hover,
                WM.NCMOUSEHOVER => MouseAction.Hover | MouseAction.NoClient,
                WM.MOUSEWHEEL => MouseAction.VerticalWheel,
                WM.MOUSEHWHEEL => MouseAction.HorizontalWheel | MouseAction.NoClient,
                WM.LBUTTONDOWN => MouseAction.Left,
                WM.NCLBUTTONDOWN => MouseAction.Left | MouseAction.NoClient,
                WM.LBUTTONUP => MouseAction.Left,
                WM.NCLBUTTONUP => MouseAction.Left | MouseAction.NoClient,
                WM.LBUTTONDBLCLK => MouseAction.Left | MouseAction.DoubleClick,
                WM.NCLBUTTONDBLCLK => MouseAction.Left | MouseAction.DoubleClick | MouseAction.NoClient,
                WM.RBUTTONDOWN => MouseAction.Right,
                WM.NCRBUTTONDOWN => MouseAction.Right | MouseAction.NoClient,
                WM.RBUTTONUP => MouseAction.Right,
                WM.NCRBUTTONUP => MouseAction.Right | MouseAction.NoClient,
                WM.RBUTTONDBLCLK => MouseAction.Right | MouseAction.DoubleClick,
                WM.NCRBUTTONDBLCLK => MouseAction.Right | MouseAction.DoubleClick | MouseAction.NoClient,
                WM.MBUTTONDOWN => MouseAction.Middle,
                WM.NCMBUTTONDOWN => MouseAction.Middle | MouseAction.NoClient,
                WM.MBUTTONUP => MouseAction.Middle,
                WM.NCMBUTTONUP => MouseAction.Middle | MouseAction.NoClient,
                WM.MBUTTONDBLCLK => MouseAction.Middle | MouseAction.DoubleClick,
                WM.NCMBUTTONDBLCLK => MouseAction.Middle | MouseAction.DoubleClick | MouseAction.NoClient,
                WM.XBUTTONDOWN => MouseAction.XButton,
                WM.NCXBUTTONDOWN => MouseAction.XButton | MouseAction.NoClient,
                WM.XBUTTONUP => MouseAction.XButton,
                WM.NCXBUTTONUP => MouseAction.XButton | MouseAction.NoClient,
                WM.XBUTTONDBLCLK => MouseAction.XButton | MouseAction.DoubleClick,
                WM.NCXBUTTONDBLCLK => MouseAction.XButton | MouseAction.DoubleClick | MouseAction.NoClient,
                _ => null
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction? AsMouse(this WM value, WindowsMessage message)
        {
            return AsMouse(value)?.X(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction? AsMouse(this WM value, out Boolean client)
        {
            MouseAction? result = AsMouse(value);
            client = !IsNoClient(value);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction? AsMouse(this WM value, WindowsMessage message, out Boolean client)
        {
            return AsMouse(value, out client)?.X(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouse(this WM value)
        {
            return AsMouse(value) ?? throw new EnumUndefinedOrNotSupportedException<WM>(value, nameof(value), null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouse(this WM value, WindowsMessage message)
        {
            return ToMouse(value).X(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouse(this WM value, out Boolean client)
        {
            MouseAction result = ToMouse(value);
            client = !IsNoClient(value);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction ToMouse(this WM value, WindowsMessage message, out Boolean client)
        {
            return ToMouse(value, out client).X(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction X(this MouseAction value, WindowsMessage message)
        {
            return X(value, message.WParam);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe MouseAction X(this MouseAction value, IntPtr wparam)
        {
            if (!value.HasFlag(MouseAction.XButton))
            {
                return value;
            }
            
            value &= MouseAction.Modifiers;
            
            switch (sizeof(IntPtr))
            {
                case 4:
                {
                    Int16 button = wparam.ToInt32().High().High();
                    
                    if (!MathUtilities.IsZeroOrPowerOf2(unchecked((UInt32) button)))
                    {
                        return value;
                    }
                    
                    return value | (MouseAction) ((UInt32) MouseAction.XButton << (button.ILogB() + 1));
                }
                case 8:
                {
                    Int16 button = unchecked((Int32) wparam.ToInt64().Low()).High();
                    
                    if (!MathUtilities.IsZeroOrPowerOf2(unchecked((UInt32) button)))
                    {
                        return value;
                    }
                    
                    return value | (MouseAction) ((UInt32) MouseAction.XButton << (button.ILogB() + 1));
                }
                default:
                {
                    return value;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Point(this MouseAction value, WindowsMessage message)
        {
            return Point(value, message.LParam);
        }
        
        // ReSharper disable once UnusedParameter.Global
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Point Point(this MouseAction value, IntPtr lparam)
        {
            switch (sizeof(IntPtr))
            {
                case 4:
                {
                    Int32 result = lparam.ToInt32();
                    return new Point(result.High(), result.Low());
                }
                case 8:
                {
                    Int64 result = lparam.ToInt64();
                    return new Point(result.High(), unchecked((Int32) result.Low()));
                }
                default:
                {
                    return default;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction Point(this MouseAction value, WindowsMessage message, out Point point)
        {
            point = Point(value, message);
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MouseAction Point(this MouseAction value, IntPtr lparam, out Point point)
        {
            point = Point(value, lparam);
            return value;
        }
    }
}