// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Types.Events;
using NetExtender.Types.HotKeys;
using NetExtender.Utilities.UserInterface;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class HotKeyForm : Form
    {
        public event EventHandler<HotKeyEventArgs>? HotKey;

        protected override void WndProc(ref Message message)
        {
            if ((WM) message.Msg != WM.HOTKEY)
            {
                base.WndProc(ref message);
                return;
            }

            OnHotKey(new HotKeyEventArgs(message.WParam.ToInt32()));
            base.WndProc(ref message);
        }

        protected virtual void OnHotKey(HotKeyEventArgs args)
        {
            HotKey?.Invoke(this, args);
        }

        public Boolean RegisterHotKey(HotKeyAction hotkey, out Int32 id)
        {
            return WinFormsHotKeyUtilities.RegisterHotKey(this, hotkey, out id);
        }

        public Boolean RegisterHotKey<T>(HotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            return WinFormsHotKeyUtilities.RegisterHotKey(this, hotkey);
        }
    }
}