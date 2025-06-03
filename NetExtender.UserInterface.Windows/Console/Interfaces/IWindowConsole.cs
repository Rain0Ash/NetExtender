using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using NetExtender.Types.Console.Interfaces;
using NetExtender.Utilities.Windows;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.UserInterface.Console.Interfaces
{
    public interface IWindowConsole : IConsole
    {
        public new static IWindowConsole Default
        {
            get
            {
                return WindowConsoleHandler.Default;
            }
        }

        public Icon? Icon { get; set; }
        
        public Graphics CreateGraphics();
        public Boolean TryGetTitle([MaybeNullWhen(false)] String result);
        public Boolean TrySetTitle(String? result);
        public Boolean CenterToScreen();
        public Boolean CenterToScreen(IScreen screen);
        public Boolean CenterToScreen(DefaultMonitorType type);
    }
}