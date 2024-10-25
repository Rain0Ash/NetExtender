// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using NetExtender.Windows;
using NetExtender.Windows.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public abstract class WndProcWindow : SoundWindow
    {
        private HwndSource? Hwnd { get; set; }

        protected WndProcWindow()
        {
            Started += InitializeWndProc;
            Closed += DisposeWndProc;
        }
        
        private void InitializeWndProc(Object? sender, RoutedEventArgs args)
        {
            Hwnd = HwndSource.FromHwnd(Handle);
            Hwnd?.AddHook(WndProc);
        }
        
        private void DisposeWndProc(Object? sender, EventArgs args)
        {
            Hwnd?.RemoveHook(WndProc);
            Hwnd?.Dispose();
        }

        protected virtual Boolean WndProc(ref WindowsMessage message)
        {
            return false;
        }

        // ReSharper disable once RedundantAssignment
        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            WindowsMessage message = new WindowsMessage(hwnd, msg, wParam, lParam);
            handled = WndProc(ref message);

            return IntPtr.Zero;
        }
        
        [DllImport("user32.dll")]
        private static extern Int32 SendMessage(IntPtr handle, Int32 message, IntPtr wparam, IntPtr lparam);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Int32 SendMessage(IntPtr handle, WM message, IntPtr wparam, IntPtr lparam)
        {
            return SendMessage(handle, (Int32) message, wparam, lparam);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Int32 SendMessage(IntPtr handle, WindowsMessage message)
        {
            return SendMessage(handle, message.Message, message.WParam, message.LParam);
        }
    }
}