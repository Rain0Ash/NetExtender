// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Interop;
using NetExtender.UserInterface.Windows.Types;

namespace NetExtender.UserInterface.WPF.Windows
{
    public abstract class WndProcWindow : StartedWindow
    {
        private HwndSource? Hwnd { get; set; }

        protected WndProcWindow()
        {
            Started += InitizalizeWndProc;
            Closed += DisposeWndProc;
        }
        
        protected virtual void WndProc(ref WinMessage message)
        {
        }
        
        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            WinMessage message = new WinMessage(hwnd, msg, wParam, lParam);

            WndProc(ref message);

            return IntPtr.Zero;
        }

        private void InitizalizeWndProc(Object? sender, RoutedEventArgs args)
        {
            Hwnd = (HwndSource?) PresentationSource.FromDependencyObject(this);
            Hwnd?.AddHook(WndProc);
        }

        private void DisposeWndProc(Object? sender, EventArgs args)
        {
            Hwnd?.RemoveHook(WndProc);
            Hwnd?.Dispose();
        }
    }
}