// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.UserInterface.Windows.Taskbar
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ThumbButton
    {
        /// <summary>WPARAM value for a THUMBBUTTON being clicked.</summary>
        public const Int32 Clicked = 6144;
        public readonly ThumbButtonMask Mask;
        public readonly UInt32 Id;
        public readonly UInt32 Bitmap;
        public IntPtr Icon;
        public readonly String Tip;
        public readonly ThumbButtonOptions Flags;
    }
}