// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.UserInterface.Utilities;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class WindowWrapper : IWindow
    {
        public static implicit operator WindowWrapper(Window window)
        {
            return new WindowWrapper(window);
        }

        public static implicit operator Window(WindowWrapper wrapper)
        {
            return wrapper.Window;
        }

        public event InterfaceClosingEventHandler? WindowClosing;

        public IntPtr Handle
        {
            get
            {
                return Window.GetHandle();
            }
        }

        public String Name
        {
            get
            {
                return Window.Name;
            }
            set
            {
                Window.Name = value;
            }
        }

        public String Title
        {
            get
            {
                return Window.Title;
            }
            set
            {
                Window.Title = value;
            }
        }

        public Boolean ShowInTaskbar
        {
            get
            {
                return Window.ShowInTaskbar;
            }
            set
            {
                Window.ShowInTaskbar = value;
            }
        }

        public Boolean TopMost
        {
            get
            {
                return Window.Topmost;
            }
            set
            {
                Window.Topmost = value;
            }
        }

        public Double Top
        {
            get
            {
                return Window.Top;
            }
            set
            {
                Window.Top = value;
            }
        }

        public Double Width
        {
            get
            {
                return Window.Width;
            }
            set
            {
                Window.Width = value;
            }
        }

        public Double Height
        {
            get
            {
                return Window.Height;
            }
            set
            {
                Window.Height = value;
            }
        }

        private Window Window { get; }

        public WindowWrapper(Window window)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));
            Window.Closing += OnClosing;
        }

        private void OnClosing(Object? sender, CancelEventArgs args)
        {
            InterfaceClosingEventArgs closing = new InterfaceClosingEventArgs(InterfaceCloseReason.WindowClosing, args.Cancel);
            WindowClosing?.Invoke(sender, closing);

            if (closing.Cancel)
            {
                args.Cancel = closing.Cancel;
            }
        }

        public void Show()
        {
            Window.Show();
        }

        public InterfaceDialogResult ShowDialog()
        {
            return InterfaceDialogResultUtilities.ToInterfaceDialogResult(Window.ShowDialog());
        }

        public Boolean Activate()
        {
            return Window.Activate();
        }
    }
}