// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;

namespace NetExtender.GUI.WPF.Windows
{
    public abstract class ChildWindow : CenterWindow
    {
        protected ChildWindow()
        {
            Started += SetTaskbarVisible;
            Started += DuplicateOwnerIcon;
        }

        private void SetTaskbarVisible(Object sender, RoutedEventArgs e)
        {
            //ShowInTaskbar = Application.Current.MainWindow == this;
        }
        
        private void DuplicateOwnerIcon(Object sender, RoutedEventArgs e)
        {
            Icon ??= Owner?.Icon ?? Application.Current.MainWindow?.Icon;
        }

        protected override void CenterTo()
        {
            CenterToParent();
        }
    }
}