// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public enum WindowCenterMode : Byte
    {
        None,
        Screen,
        Parent
    }
    
    public abstract class CenterWindow : FixedWindow
    {
        public WindowCenterMode Position { get; set; } = WindowCenterMode.Parent;
        public Boolean BringToFront { get; set; } = true;
        
        protected CenterWindow()
        {
            Started += SetSizeTo;
            Started += CenterTo;
            Started += DuplicateOwnerIcon;
            Started += SetTaskbarVisible;
            Loaded += BringToForeground;
        }
        
        protected virtual void SetSizeTo(Object? sender, EventArgs args)
        {
        }

        protected virtual void DuplicateOwnerIcon(Object? sender, RoutedEventArgs args)
        {
            Icon ??= Owner?.Icon ?? Application.Current.MainWindow?.Icon;
        }

        protected virtual void SetTaskbarVisible(Object? sender, RoutedEventArgs args)
        {
        }
        
        protected virtual void BringToForeground(Object? sender, RoutedEventArgs args)
        {
            if (!BringToFront)
            {
                return;
            }
            
            UserInterfaceUtilities.BringToForegroundWindow(Handle);
        }

        protected virtual void CenterTo(Object? sender, EventArgs args)
        {
            CenterTo();
        }
        
        public virtual Boolean CenterTo()
        {
            return Position switch
            {
                WindowCenterMode.Screen => CenterToScreen(),
                WindowCenterMode.Parent => CenterToParent(),
                _ => false
            };
        }

        public virtual Boolean CenterToScreen()
        {
            Left = SystemParameters.PrimaryScreenWidth / 2 - Width / 2;
            Top = SystemParameters.PrimaryScreenHeight / 2 - Height / 2;
            return true;
        }

        public virtual Boolean CenterToParent()
        {
            Window? parent = Owner ?? Application.Current.MainWindow;

            if (parent is null || parent == this || parent.WindowState == WindowState.Maximized)
            {
                return CenterToScreen();
            }
            
            Left = parent.Left + (parent.ActualWidth - ActualWidth) / 2;
            Top = parent.Top + (parent.ActualHeight - ActualHeight) / 2;
            return true;
        }
    }
}