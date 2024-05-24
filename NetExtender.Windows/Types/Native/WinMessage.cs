// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetExtender.Windows;

namespace NetExtender.UserInterface.Windows.Types
{
    /// <summary>
    ///  Implements a Windows message.
    /// </summary>
    public readonly struct WinMessage : IEquatable<Int32>, IEquatable<WM>, IEquatable<WinMessage>, IComparable<Int32>, IComparable<WM>, IComparable<WinMessage>
    {
        public static Boolean operator ==(WinMessage first, WinMessage second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WinMessage first, WinMessage second)
        {
            return !first.Equals(second);
        }

        public IntPtr Handle { get; init; }

        public WM Message { get; init; }

        public IntPtr WParam { get; init; }

        public IntPtr LParam { get; init; }

        public IntPtr Result { get; init; }

        public WinMessage(IntPtr handle, Int32 message)
            : this(handle, (WM) message)
        {
            
        }

        public WinMessage(IntPtr handle, WM message)
            : this(handle, message, default, default, default)
        {
        }

        public WinMessage(IntPtr handle, Int32 message, IntPtr wparam, IntPtr lparam)
            : this(handle, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WinMessage(IntPtr handle, Int32 message, IntPtr wparam, IntPtr lparam, IntPtr result)
            : this(handle, (WM) message, wparam, lparam, result)
        {

        }

        public WinMessage(IntPtr handle, WM message, IntPtr wparam, IntPtr lparam)
            : this(handle, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WinMessage(IntPtr handle, WM message, IntPtr wparam, IntPtr lparam, IntPtr result)
        {
            Handle = handle;
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

        public Int32 CompareTo(Int32 other)
        {
            return CompareTo((WM) other);
        }

        public Int32 CompareTo(WM other)
        {
            return Comparer<WM>.Default.Compare(Message, other);
        }

        public Int32 CompareTo(WinMessage other)
        {
            return CompareTo(other.Message);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Handle, Message);
        }

        public override Boolean Equals(Object? other)
        {
            return other is WinMessage message && Equals(message);
        }

        public Boolean Equals(Int32 other)
        {
            return Equals((WM) other);
        }

        public Boolean Equals(WM other)
        {
            return EqualityComparer<WM>.Default.Equals(Message, other);
        }

        public Boolean Equals(WinMessage other)
        {
            return Handle == other.Handle &&
                   Message == other.Message &&
                   WParam == other.WParam &&
                   LParam == other.LParam &&
                   Result == other.Result;
        }
    }
}