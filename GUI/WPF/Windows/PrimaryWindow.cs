// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Utils.GUI;

namespace NetExtender.GUI.WPF.Windows
{
    public class PrimaryWindow : CenterWindow
    {
        public PrimaryWindow()
        {
            Loaded += BringToFront;
        }

        protected virtual void BringToFront(Object sender, RoutedEventArgs e)
        {
            this.BringToForeground();
        }
    }
}