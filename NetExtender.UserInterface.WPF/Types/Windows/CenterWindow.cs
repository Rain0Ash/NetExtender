// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;

namespace NetExtender.UserInterface.WPF.Windows
{
    public abstract class CenterWindow : FixedWindow
    {
        protected CenterWindow()
        {
            Started += SetSizeTo;
            Started += CenterTo;
        }
        
        protected virtual void SetSizeTo(Object sender, EventArgs args)
        {
        }
        
        protected void CenterTo(Object sender, EventArgs args)
        {
            CenterTo();
        }

        protected virtual void CenterTo()
        {
            CenterToScreen();
        }

        public void CenterToScreen()
        {
            Left = SystemParameters.PrimaryScreenWidth / 2 - Width / 2;
            Top = SystemParameters.PrimaryScreenHeight / 2 - Height / 2;
        }

        public void CenterToParent()
        {
            Window? parent = Owner ?? System.Windows.Application.Current.MainWindow;

            if (parent is null)
            {
                CenterToScreen();
                return;
            }
            
            Left = parent.Left + (parent.ActualWidth - ActualWidth) / 2;
            Top = parent.Top + (parent.ActualHeight - ActualHeight) / 2;
        }
    }
}