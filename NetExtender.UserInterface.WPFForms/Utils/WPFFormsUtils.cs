// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Forms;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.UserInterface
{
    public static class WPFFormsUtils
    {
        public static DialogResult ShowResultDialog(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return DialogResultUtils.ToDialogResult(window.ShowDialog());
        }
        
        public static MessageBoxResult ToMessageBoxResult(this DialogResult result)
        {
            return result switch
            {
                DialogResult.None => MessageBoxResult.None,
                DialogResult.OK => MessageBoxResult.OK,
                DialogResult.Cancel => MessageBoxResult.Cancel,
                DialogResult.Abort => MessageBoxResult.Cancel,
                DialogResult.Retry => MessageBoxResult.Cancel,
                DialogResult.Ignore => MessageBoxResult.Cancel,
                DialogResult.Yes => MessageBoxResult.Yes,
                DialogResult.No => MessageBoxResult.No,
                _ => throw new NotSupportedException()
            };
        }
        
        public static DialogResult ToDialogResult(this MessageBoxResult result)
        {
            return result switch
            {
                MessageBoxResult.None => DialogResult.None,
                MessageBoxResult.OK => DialogResult.OK,
                MessageBoxResult.Cancel => DialogResult.Cancel,
                MessageBoxResult.Yes => DialogResult.Yes,
                MessageBoxResult.No => DialogResult.No,
                _ => throw new NotSupportedException()
            };
        }
    }
}