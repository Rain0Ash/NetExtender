// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.Windows.Taskbar
{
    [Flags]
    public enum FlashState : UInt32
    {
        /// <summary>
        /// Stop flash the window caption.
        /// </summary>
        Stop = 0,

        /// <summary>
        /// Flash the window caption.
        /// </summary>
        Caption = 1,

        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        Tray = 2,

        /// <summary>
        /// Flash both the window caption and taskbar button.
        /// </summary>
        All = 3,

        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        Timer = 4,

        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        Notify = 15
    }
}