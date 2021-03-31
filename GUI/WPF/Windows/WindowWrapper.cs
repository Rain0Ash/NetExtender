// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Forms;
using NetExtender.Utils.WPF;
using NetExtender.GUI.Common.Interfaces;

namespace NetExtender.GUI.WPF.Windows
{
    public class WindowWrapper : IWindow
    {
        public static implicit operator WindowWrapper(Window window)
        {
            return new WindowWrapper(window);
        }
        
        public static implicit operator Window(WindowWrapper wrapper)
        {
            return wrapper._window;
        }
        
        public event FormClosingEventHandler WindowClosing
        {
            add
            {
                if (_window is IWindow window)
                {
                    window.WindowClosing += value;
                }
            }
            remove
            {
                if (_window is IWindow window)
                {
                    window.WindowClosing -= value;
                }
            }
        }
        
        public IntPtr Handle
        {
            get
            {
                return _window.GetHandle();
            }
        }

        public String Name
        {
            get
            {
                return _window.Name;
            }
            set
            {
                _window.Name = value;
            }
        }

        public String Title
        {
            get
            {
                return _window.Title;
            }
            set
            {
                _window.Title = value;
            }
        }

        public Boolean ShowInTaskbar
        {
            get
            {
                return _window.ShowInTaskbar;
            }
            set
            {
                _window.ShowInTaskbar = value;
            }
        }

        public Boolean TopMost
        {
            get
            {
                return _window.Topmost;
            }
            set
            {
                _window.Topmost = value;
            }
        }

        public Double Top
        {
            get
            {
                return _window.Top;
            }
            set
            {
                _window.Top = value;
            }
        }

        public Double Width
        {
            get
            {
                return _window.Width;
            }
            set
            {
                _window.Width = value;
            }
        }

        public Double Height
        {
            get
            {
                return _window.Height;
            }
            set
            {
                _window.Height = value;
            }
        }

        private readonly Window _window;

        public WindowWrapper(Window window)
        {
            _window = window;
        }
        
        public void Show()
        {
            _window.Show();
        }

        public DialogResult ShowDialog()
        {
            return _window.ShowResultDialog();
        }

        public Boolean Activate()
        {
            return _window.Activate();
        }
    }
}