// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.Windows.Menu
{
    public class StringWindowsMenuItem : WindowsMenuItem
    {
        public String Title { get; }
        
        public StringWindowsMenuItem(Byte command, String title)
            : base(command, UserInterfaceUtilities.WindowsMenuItemMask.String, UserInterfaceUtilities.WindowsMenuItemType.String)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        protected internal override Boolean Append(IntPtr hwnd)
        {
            return UserInterfaceUtilities.AppendMenu(hwnd, Command, Title);
        }

        protected internal override Boolean Insert(IntPtr hwnd, Byte position)
        {
            return UserInterfaceUtilities.InsertMenu(hwnd, position, Command, Title);
        }
    }
}