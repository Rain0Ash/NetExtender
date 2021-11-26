// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface
{
    public enum WindowStateType : UInt32
    {
        Hide = 0,
        Normal = 1,
        ShowMinimized = 2,
        ShowMaximized = 3,
        ShowNoActivate = 4,
        Maximize = ShowMaximized,
        Show = 5,
        Minimize = 6,
        ShowMininimizedNoActive = 7,
        ShowNormalNoActivate = 8,
        Restore = 9
    }
}