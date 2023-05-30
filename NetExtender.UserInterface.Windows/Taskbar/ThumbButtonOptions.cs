// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.Windows.Taskbar
{
    [Flags]
    internal enum ThumbButtonOptions
    {
        Enabled = 0,
        Disabled = 1,
        DismissOnClick = 2,
        NoBackground = 4,
        Hidden = 8,
        NonInteractive = 16 // 0x00000010
    }
}