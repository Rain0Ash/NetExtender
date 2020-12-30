// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using NetExtender.GUI;
using NetExtender.Utils.Types;
using NetExtender.Utils.WPF;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.GUI.WinForms.Forms;

namespace NetExtender.Utils.GUI
{
    public static class GUIUtils
    {
        public const Int32 Distance = 5;
        
        public static DialogResult ToMessageBox(this Object str, Object caption = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            return System.Windows.Forms.MessageBox.Show(str.GetString(), caption?.GetString(), buttons, icon);
        }

        public static DialogResult ToMessageForm(this Object str, Object title = null, Image icon = null, Image messageIcon = null, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return new MessageForm(str.GetString(), title?.GetString(), icon, messageIcon, buttons).ShowDialog();
        }

        public static Boolean ToBoolean(this MessageBoxResult value)
        {
            return value switch
            {
                MessageBoxResult.None => false,
                MessageBoxResult.OK => true,
                MessageBoxResult.Cancel => false,
                MessageBoxResult.Yes => true,
                MessageBoxResult.No => false,
                _ => throw new NotSupportedException()
            };
        }
        
        public static Boolean ToBoolean(this DialogResult value)
        {
            return value switch
            {
                DialogResult.None => false,
                DialogResult.OK => true,
                DialogResult.Cancel => false,
                DialogResult.Abort => false,
                DialogResult.Retry => true,
                DialogResult.Ignore => false,
                DialogResult.Yes => true,
                DialogResult.No => false,
                _ => throw new NotSupportedException()
            };
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out UInt32 lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetCurrentThreadId();

        [DllImport("user32.dll")] 
        private static extern IntPtr GetForegroundWindow(); 

        [DllImport("user32.dll")]
        private static extern Boolean AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach); 

        [DllImport("user32.dll", SetLastError = true)] 
        private static extern Boolean BringWindowToTop(IntPtr hWnd); 

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Boolean BringWindowToTop(HandleRef hWnd); 

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, UInt32 nCmdShow);

        public static Boolean ShowWindow(IntPtr handle, WindowStateType state)
        {
            return ShowWindow(handle, (UInt32) state);
        }
        
        public static Boolean ShowWindow(this IWindow window, WindowStateType state)
        {
            return ShowWindow(window.Handle, state);
        }

        private static Boolean Bring(IntPtr hWnd)
        {
            BringWindowToTop(hWnd);
            ShowWindow(hWnd, 5);
            return true;
        }

        private static Boolean AttachAndBring(UInt32 thread, UInt32 application, IntPtr hWnd)
        {
            AttachThreadInput(thread, application, true);
            Bring(hWnd);
            AttachThreadInput(thread, application, false);
            return true;
        }
        
        private static Boolean BringToForegroundWindow(IntPtr hWnd)
        {
            UInt32 thread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            UInt32 application = GetCurrentThreadId();

            return thread == application ? Bring(hWnd) : AttachAndBring(thread, application, hWnd);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground(this Form form)
        {
            return BringToForegroundWindow(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground(this Window window)
        {
            return BringToForegroundWindow(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground(IGUIHandle window)
        {
            return BringToForegroundWindow(window.Handle);
        }
    }
}