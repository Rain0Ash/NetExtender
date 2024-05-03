// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Interop;
using NetExtender.UserInterface.Windows.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class WndProcWindow : SoundWindow
    {
        private HwndSource? Hwnd { get; set; }

        protected WndProcWindow()
        {
            Started += InitizalizeWndProc;
            Closed += DisposeWndProc;
        }

        protected virtual Boolean WndProc(ref WinMessage message)
        {
            return false;
        }

        // ReSharper disable once RedundantAssignment
        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            WinMessage message = new WinMessage(hwnd, msg, wParam, lParam);
            handled = WndProc(ref message);

            return IntPtr.Zero;
        }

        private void InitizalizeWndProc(Object? sender, RoutedEventArgs args)
        {
            Hwnd = HwndSource.FromHwnd(Handle);
            Hwnd?.AddHook(WndProc);
        }

        private void DisposeWndProc(Object? sender, EventArgs args)
        {
            Hwnd?.RemoveHook(WndProc);
            Hwnd?.Dispose();
        }
    }
}