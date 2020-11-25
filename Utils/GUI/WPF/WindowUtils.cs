// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace NetExtender.Utils.WPF
{
    public static class WindowUtils
    {
        public static IntPtr GetHandle(this Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        public static DialogResult ShowResultDialog(this Window window)
        {
            return ToDialogResult(window.ShowDialog());
        }
        
        public static DialogResult ToDialogResult(this Boolean? result)
        {
            return result switch
            {
                true => DialogResult.OK,
                false => DialogResult.Cancel,
                _ => DialogResult.None
            };
        }
    }
}