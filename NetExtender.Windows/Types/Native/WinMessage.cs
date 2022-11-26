// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using NetExtender.Windows;

namespace NetExtender.UserInterface.Windows.Types
{
    /// <summary>
    ///  Implements a Windows message.
    /// </summary>
    public readonly struct WinMessage
    {
        public static Boolean operator ==(WinMessage first, WinMessage second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WinMessage first, WinMessage second)
        {
            return !first.Equals(second);
        }

        public IntPtr HWnd { get; init; }

        public WM Message { get; init; }

        public IntPtr WParam { get; init; }

        public IntPtr LParam { get; init; }

        public IntPtr Result { get; init; }

        public WinMessage(IntPtr hwnd, Int32 message, IntPtr wparam, IntPtr lparam)
            : this(hwnd, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WinMessage(IntPtr hwnd, Int32 message, IntPtr wparam, IntPtr lparam, IntPtr result)
            : this(hwnd, (WM) message, wparam, lparam, result)
        {

        }

        public WinMessage(IntPtr hwnd, WM message, IntPtr wparam, IntPtr lparam)
            : this(hwnd, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WinMessage(IntPtr hwnd, WM message, IntPtr wparam, IntPtr lparam, IntPtr result)
        {
            HWnd = hwnd;
            Message = message;
            WParam = wparam;
            LParam = lparam;
            Result = result;
        }

        /// <summary>
        ///  Gets the <see cref='LParam'/> value, and converts the value to an object.
        /// </summary>
        public Object? GetLParam(Type cls)
        {
            return Marshal.PtrToStructure(LParam, cls);
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj is not WinMessage message)
            {
                return false;
            }

            return HWnd == message.HWnd &&
                   Message == message.Message &&
                   WParam == message.WParam &&
                   LParam == message.LParam &&
                   Result == message.Result;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(HWnd, Message);
        }
    }
}