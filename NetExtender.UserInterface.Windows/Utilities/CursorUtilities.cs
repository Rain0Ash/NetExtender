// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.UserInterface
{
    public class CursorUtilities
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern Boolean GetCursorPos(out Point pt);
        
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern Boolean SetCursorPos(Int32 x, Int32 y);
        
        public static Point Position
        {
            get
            {
                return GetCursorPos(out Point position) ? position : throw new InvalidOperationException();
            }
            set
            {
                if (!SetCursorPos(value.X, value.Y))
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public static Point GetCursorPosition()
        {
            return Position;
        }        
        
        public static Point GetCursorPosition(Point size, Size resolution)
        {
            Point position = Position;
            return new Point(position.X * size.X / resolution.Width, position.Y * size.Y / resolution.Height);
        }
    }
}