// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NetExtender.Utils.Static
{
    public static class InteropUtils
    {
        public static void ThrowLastWin32Exception()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}