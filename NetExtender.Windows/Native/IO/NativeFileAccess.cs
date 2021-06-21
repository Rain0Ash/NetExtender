// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Windows.IO
{
    [Flags]
    public enum NativeFileAccess : UInt32
    {
        GenericRead = 0x80000000,
        GenericWrite = 0x40000000
    }
}