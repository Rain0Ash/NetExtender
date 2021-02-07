// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace NetExtender.GUI.WPF.Windows
{
    public abstract class WndProcWindow : StartedWindow
    {
        private HwndSource _hwnd;

        protected WndProcWindow()
        {
            Started += InitizalizeWndProc;
            Closed += DisposeWndProc;
        }
        
        protected virtual void WndProc(ref Message m)
        {
        }
        
        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {
            Message m = new Message
            {
                HWnd = hwnd,
                Msg = msg,
                WParam = wParam,
                LParam = lParam,
                Result = IntPtr.Zero
            };

            WndProc(ref m);

            return IntPtr.Zero;
        }

        private void InitizalizeWndProc(Object sender, RoutedEventArgs e)
        {
            _hwnd = (HwndSource) PresentationSource.FromDependencyObject(this);
            _hwnd?.AddHook(WndProc);
        }

        private void DisposeWndProc(Object sender, EventArgs e)
        {
            _hwnd?.RemoveHook(WndProc);
            _hwnd?.Dispose();
        }
    }
}