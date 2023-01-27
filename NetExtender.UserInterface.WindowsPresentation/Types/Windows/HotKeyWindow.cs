// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Events;
using NetExtender.Types.HotKeys;
using NetExtender.UserInterface.Windows.Types;
using NetExtender.Utilities.UserInterface;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class HotKeyWindow : WndProcWindow
    {
        public event EventHandler<HotKeyEventArgs>? HotKey;

        protected override Boolean WndProc(ref WinMessage message)
        {
            if (message.Message != WM.HOTKEY)
            {
                return base.WndProc(ref message);
            }

            OnHotKey(new HotKeyEventArgs(message.WParam.ToInt32()));
            return base.WndProc(ref message);
        }

        protected virtual void OnHotKey(HotKeyEventArgs args)
        {
            HotKey?.Invoke(this, args);
        }

        public Boolean RegisterHotKey(HotKeyAction hotkey, out Int32 id)
        {
            return WindowsPresentationHotKeyUtilities.RegisterHotKey(this, hotkey, out id);
        }

        public Boolean RegisterHotKey<T>(HotKeyAction<T> hotkey) where T : unmanaged, IConvertible
        {
            return WindowsPresentationHotKeyUtilities.RegisterHotKey(this, hotkey);
        }

        public Boolean UnregisterHotKey(Int32 id)
        {
            return WindowsPresentationHotKeyUtilities.UnregisterHotKey(this, id);
        }

        public Boolean RegisterHotKey<T>(T id) where T : unmanaged, IConvertible
        {
            return WindowsPresentationHotKeyUtilities.UnregisterHotKey(this, id);
        }
    }
}