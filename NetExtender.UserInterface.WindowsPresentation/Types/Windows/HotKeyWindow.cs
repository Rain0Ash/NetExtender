// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Types.HotKeys;
using NetExtender.Types.HotKeys.Events;
using NetExtender.Utilities.UserInterface;
using NetExtender.Windows;
using NetExtender.Windows.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public abstract class HotKeyWindow : ChromeWindow
    {
        public event EventHandler<HotKeyEventArgs>? HotKey;

        protected HotKeyWindow()
        {
            BeginConstructorInit();
            Started += RegisterHotKeys;
            EndConstructorInit();
        }

        protected virtual void RegisterHotKeys(Object sender, RoutedEventArgs args)
        {
        }

        protected override Boolean WndProc(ref WindowsMessage message)
        {
            if (message.Message != WM.HOTKEY)
            {
                return base.WndProc(ref message);
            }

            OnHotKey(message.WParam.ToInt32());
            return base.WndProc(ref message);
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
            return WindowsPresentationHotKeyUtilities.RegisterHotKey(this, hotkey, out id);
        }

        public Boolean RegisterHotKey<T>(HotKeyAction<T> hotkey) where T : unmanaged, IComparable<T>, IConvertible
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