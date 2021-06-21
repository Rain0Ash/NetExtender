// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NetExtender.Types.Native.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WinRectangle
    {
        public static implicit operator Rectangle(WinRectangle rectangle)
        {
            return rectangle.Rectangle();
        }

        private Int32 Left { get; init; }
        private Int32 Top { get; init; }
        private Int32 Right { get; init; }
        private Int32 Bottom { get; init; }

        public Rectangle Rectangle()
        {
            return new Rectangle(Left, Top, Right - Left, Bottom - Top);
        }
    }
}