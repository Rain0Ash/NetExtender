// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Types.HotKeys;
using NetExtender.Types.HotKeys.Events;
using NetExtender.Utilities.UserInterface;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class HotKeyForm : Form
    {
        public event EventHandler<HotKeyEventArgs>? HotKey;
        
        protected HotKeyForm()
        {
            Load += RegisterHotKeys;
        }

        protected virtual void RegisterHotKeys(Object? sender, EventArgs args)
        {
        }

        protected override void WndProc(ref Message message)
        {
            if ((WM) message.Msg != WM.HOTKEY)
            {
                base.WndProc(ref message);
                return;
            }

            OnHotKey(message.WParam.ToInt32());
            base.WndProc(ref message);
        }

        private void OnHotKey(Int32 id)
        {
            if (HotKeyUtilities.TryGetHotKey(Handle, id, out WindowsHotKeyAction<Int32> action))
            {
                OnHotKey(new HotKeyEventArgs(action));
            }
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
        
        public Boolean UnregisterHotKey(Int32 id)
        {
            return WinFormsHotKeyUtilities.UnregisterHotKey(this, id);
        }

        public Boolean RegisterHotKey<T>(T id) where T : unmanaged, IConvertible
        {
            // ReSharper disable once InvokeAsExtensionMethod
            return WinFormsHotKeyUtilities.UnregisterHotKey(this, id);
        }
    }
}