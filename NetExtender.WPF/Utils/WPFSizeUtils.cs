// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;

namespace NetExtender.Utils.Types
{
    public static class WPFSizeUtils
    {
        public static System.Drawing.Size ToIntSize(this Size size)
        {
            return new System.Drawing.Size((Int32) size.Width, (Int32) size.Height);
        }

        public static Size ToDoubleSize(this System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }
        
        public static Double GetAspectRatio(this Size size)
        {
            return DrawingUtils.GetAspectRatio(size.Width, size.Height);
        }
    }
}