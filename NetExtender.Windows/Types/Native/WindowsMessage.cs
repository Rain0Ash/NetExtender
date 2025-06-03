// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NetExtender.Windows.Types
{
    /// <summary>
    ///  Implements a Windows message.
    /// </summary>
    public readonly struct WindowsMessage : IEquality<WindowsMessage>, IEquality<WM>, IEquality<Int32>, IFormattable
    {
        public static implicit operator WM(WindowsMessage message)
        {
            return message.Message;
        }
        
        public static Boolean operator ==(WindowsMessage first, WindowsMessage second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WindowsMessage first, WindowsMessage second)
        {
            return !first.Equals(second);
        }

        public IntPtr Handle { get; init; }

        public WM Message { get; init; }

        public IntPtr WParam { get; init; }

        public IntPtr LParam { get; init; }

        public IntPtr Result { get; init; }

        public WindowsMessage(IntPtr handle, Int32 message)
            : this(handle, (WM) message)
        {
            
        }

        public WindowsMessage(IntPtr handle, WM message)
            : this(handle, message, default, default, default)
        {
        }

        public WindowsMessage(IntPtr handle, Int32 message, IntPtr wparam, IntPtr lparam)
            : this(handle, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WindowsMessage(IntPtr handle, Int32 message, IntPtr wparam, IntPtr lparam, IntPtr result)
            : this(handle, (WM) message, wparam, lparam, result)
        {

        }

        public WindowsMessage(IntPtr handle, WM message, IntPtr wparam, IntPtr lparam)
            : this(handle, message, wparam, lparam, IntPtr.Zero)
        {
        }

        public WindowsMessage(IntPtr handle, WM message, IntPtr wparam, IntPtr lparam, IntPtr result)
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

        public Int32 CompareTo(WindowsMessage other)
        {
            return CompareTo(other.Message);
        }

        public Int32 CompareTo(WM other)
        {
            return Comparer<WM>.Default.Compare(Message, other);
        }

        public Int32 CompareTo(Int32 other)
        {
            return CompareTo((WM) other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Handle, Message);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                WindowsMessage value => Equals(value),
                WM value => Equals(value),
                Int32 value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(WindowsMessage other)
        {
            return Handle == other.Handle && Message == other.Message && WParam == other.WParam && LParam == other.LParam && Result == other.Result;
        }

        public Boolean Equals(WM other)
        {
            return EqualityComparer<WM>.Default.Equals(Message, other);
        }

        public Boolean Equals(Int32 other)
        {
            return Equals((WM) other);
        }

        public override String ToString()
        {
            return ToString((IFormatProvider?) null);
        }
        
        public String ToString(IFormatProvider? provider)
        {
            return $"{{ {nameof(Handle)}: {Handle.ToString(provider)}, {nameof(Message)}: {Message}, {nameof(WParam)}: {WParam.ToString(provider)}, {nameof(LParam)}: {LParam.ToString(provider)}, {nameof(Result)}: {Result.ToString(provider)} }}";
        }
        
        public String ToString(String? format)
        {
            return ToString(format, null);
        }
        
        public String ToString(String? format, IFormatProvider? provider)
        {
            if (format is null)
            {
                return ToString(provider);
            }
            
            return format
                .Replace("{HANDLE}", Handle.ToString(provider)).Replace("{H}", Handle.ToString(provider))
                .Replace("{MESSAGE}", Message.ToString()).Replace("{WM}", Message.ToString()).Replace("{M}", Message.ToString())
                .Replace("{WPARAM}", WParam.ToString(provider)).Replace("{W}", WParam.ToString(provider))
                .Replace("{LPARAM}", LParam.ToString(provider)).Replace("{L}", LParam.ToString(provider))
                .Replace("{RESULT}", Result.ToString(provider)).Replace("{R}", Result.ToString(provider));
        }
    }
}