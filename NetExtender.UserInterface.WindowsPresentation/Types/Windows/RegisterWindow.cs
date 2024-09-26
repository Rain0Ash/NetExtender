// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public class RegisterWindow : DependencyWindow
    {
        public UInt64 Id { get; }

        protected RegisterWindow()
        {
            WindowStorageUtilities.Lock();
            Id = WindowStorageUtilities.Register(this);
            Initialized += OnInitialized;
        }

        private void OnInitialized(Object? sender, EventArgs args)
        {
            Initialized -= OnInitialized;
            WindowStorageUtilities.Unlock();
        }

        public new virtual Boolean Activate()
        {
            return base.Activate();
        }

        public new virtual void Show()
        {
            base.Show();
        }

        public new virtual Boolean? ShowDialog()
        {
            return base.ShowDialog();
        }

        public new virtual void Hide()
        {
            base.Hide();
        }

        public new virtual void Close()
        {
            base.Close();
        }
    }
}