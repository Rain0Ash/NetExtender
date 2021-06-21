// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.UserInterface
{
    public static class MessageBoxUtils
    {
        public const MessageBoxButtons DefaultMessageBoxButtons = MessageBoxButtons.OK;
        public const MessageBoxIcon DefaultMessageBoxIcon = MessageBoxIcon.Warning;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text)
        {
            return ToMessageBox(text, DefaultMessageBoxButtons);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title)
        {
            return ToMessageBox(text, title, DefaultMessageBoxButtons);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxIcon icon)
        {
            return ToMessageBox(text, DefaultMessageBoxButtons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxIcon icon)
        {
            return ToMessageBox(text, title, DefaultMessageBoxButtons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, buttons, DefaultMessageBoxIcon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, title, buttons, DefaultMessageBoxIcon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, buttons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, title, buttons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text.GetString(), null, buttons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text.GetString(), title?.GetString(), buttons, icon);
        }
    }
}