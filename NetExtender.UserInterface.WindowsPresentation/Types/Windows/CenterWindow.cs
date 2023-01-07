// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Media;
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
        public Size? Scale { get; set; }

        protected virtual Size Screen
        {
            get
            {
                return new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
            }
        }

        protected CenterWindow()
        {
            Started += SetSizeTo;
            Started += CenterTo;
            Started += ScaleTo;
            Started += DuplicateOwnerIcon;
            Started += SetTaskbarVisible;
            Loaded += BringToForeground;
        }

        protected virtual void SetSizeTo(Object? sender, RoutedEventArgs args)
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

        protected virtual void CenterTo(Object? sender, RoutedEventArgs args)
        {
            CenterTo();
        }

        protected virtual void ScaleTo(Object? sender, RoutedEventArgs args)
        {
            ScaleTo();
        }

        public Boolean CenterTo()
        {
            return CenterTo(Position);
        }

        public virtual Boolean CenterTo(WindowCenterMode position)
        {
            return position switch
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

        public void ScaleTo()
        {
            if (Scale is Size scale)
            {
                ScaleTo(scale);
            }
        }
        
        public void ScaleTo(Size scale)
        {
            ScaleTo(scale.Width, scale.Height);
        }

        public virtual void ScaleTo(Double width, Double height)
        {
            if (width <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(width), width, null);
            }
            
            if (height <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(height), height, null);
            }

            Size screen = Screen;
            LayoutTransform = new ScaleTransform(screen.Width / width, screen.Height / height);
        }
    }
}