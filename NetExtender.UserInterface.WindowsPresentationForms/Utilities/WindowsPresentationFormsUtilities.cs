// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;
using NetExtender.Utilities.UserInterface.Types;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationFormsUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ShowResultDialog(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return DialogResultUtilities.ToDialogResult(window.ShowDialog());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InterfaceDialogResult ShowInterfaceResultDialog(this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ShowResultDialog(window).ToInterfaceDialogResult();
        }

        public static MessageBoxResult ToMessageBoxResult(this DialogResult value)
        {
            return value switch
            {
                DialogResult.None => MessageBoxResult.None,
                DialogResult.OK => MessageBoxResult.OK,
                DialogResult.Cancel => MessageBoxResult.Cancel,
                DialogResult.Abort => MessageBoxResult.Cancel,
                DialogResult.Retry => MessageBoxResult.Cancel,
                DialogResult.Ignore => MessageBoxResult.Cancel,
                DialogResult.Yes => MessageBoxResult.Yes,
                DialogResult.No => MessageBoxResult.No,
                DialogResult.TryAgain => MessageBoxResult.Yes,
                DialogResult.Continue => MessageBoxResult.Yes,
                _ => throw new EnumUndefinedOrNotSupportedException<DialogResult>(value, nameof(value), null)
            };
        }

        public static DialogResult ToDialogResult(this MessageBoxResult value)
        {
            return value switch
            {
                MessageBoxResult.None => DialogResult.None,
                MessageBoxResult.OK => DialogResult.OK,
                MessageBoxResult.Cancel => DialogResult.Cancel,
                MessageBoxResult.Yes => DialogResult.Yes,
                MessageBoxResult.No => DialogResult.No,
                _ => throw new EnumUndefinedOrNotSupportedException<MessageBoxResult>(value, nameof(value), null)
            };
        }
    }
}