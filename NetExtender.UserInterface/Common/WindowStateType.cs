// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.UserInterface
{
    [SuppressMessage("ReSharper", "CA1069")]
    public enum WindowStateType : UInt32
    {
        Hide = 0,
        Normal = 1,
        Maximize = 3,
        Minimize = 6,
        Restore = 9,
        Show = 5,
        ShowMaximized = 3,
        ShowMinimized = 2,
        ShowNoActivate = 4,
        ShowMininimizedNoActive = 7,
        ShowNormalNoActivate = 8
    }
}