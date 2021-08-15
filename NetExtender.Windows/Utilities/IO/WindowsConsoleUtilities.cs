// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.IO
{
    public static class WindowsConsoleUtilities
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(Int32 nStdHandle);
        
        public static IntPtr ConsoleInputHandle { get; } = GetStdHandle(-10);
        public static IntPtr ConsoleOutputHandle { get; } = GetStdHandle(-11);
        public static IntPtr ConsoleErrorHandle { get; } = GetStdHandle(-12);
    }
}