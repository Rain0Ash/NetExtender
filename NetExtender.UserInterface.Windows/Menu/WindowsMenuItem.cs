// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.UserInterface;
using WindowsMenuItemMask = NetExtender.Utilities.UserInterface.UserInterfaceUtilities.WindowsMenuItemMask;
using WindowsMenuItemType = NetExtender.Utilities.UserInterface.UserInterfaceUtilities.WindowsMenuItemType;
using WindowsMenuItemState = NetExtender.Utilities.UserInterface.UserInterfaceUtilities.WindowsMenuItemState;

namespace NetExtender.UserInterface.Windows.Menu
{
    public class WindowsMenuItem
    {
        public static WindowsMenuItem Close { get; } = new WindowsMenuItem(0xF060);
        public static WindowsMenuItem Size { get; } = new WindowsMenuItem(0xF000);
        public static WindowsMenuItem Move { get; } = new WindowsMenuItem(0xF010);
        public static WindowsMenuItem Minimize { get; } = new WindowsMenuItem(0xF020);
        public static WindowsMenuItem Maximize { get; } = new WindowsMenuItem(0xF030);
        public static WindowsMenuItem Restore { get; } = new WindowsMenuItem(0xF120);
        public static WindowsMenuItem Separator { get; } = new WindowsMenuItem((Int32) WindowsMenuItemType.Separator, WindowsMenuItemMask.Id, WindowsMenuItemType.Separator);
        
        public Int32 Command { get; }
        internal WindowsMenuItemMask Mask { get; }
        internal WindowsMenuItemType Type { get; }
        internal WindowsMenuItemState State { get; }

        private protected WindowsMenuItem(Int32 command)
            : this(command, WindowsMenuItemMask.String, WindowsMenuItemType.String)
        {
        }
        
        private protected WindowsMenuItem(Int32 command, WindowsMenuItemMask mask, WindowsMenuItemType type, WindowsMenuItemState state = WindowsMenuItemState.None)
        {
            Command = command;
            Mask = mask;
            Type = type;
            State = state;
        }

        protected internal virtual Boolean Append(IntPtr hwnd)
        {
            return UserInterfaceUtilities.AppendMenu(hwnd, Command, String.Empty);
        }

        protected internal virtual Boolean Insert(IntPtr hwnd, Byte position)
        {
            return UserInterfaceUtilities.InsertMenu(hwnd, position, Command, String.Empty);
        }
    }
}