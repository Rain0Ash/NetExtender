using System;
using NetExtender.Utilities.UserInterface;
using WindowsMenuItemMask = NetExtender.Utilities.UserInterface.UserInterfaceUtilities.WindowsMenuItemMask;
using WindowsMenuItemType = NetExtender.Utilities.UserInterface.UserInterfaceUtilities.WindowsMenuItemType;

namespace NetExtender.UserInterface.Windows.Menu
{
    public class StringWindowsMenuItem : WindowsMenuItem
    {
        public String Title { get; }
        
        public StringWindowsMenuItem(Byte command, String title)
            : base(command, WindowsMenuItemMask.String, WindowsMenuItemType.String)
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