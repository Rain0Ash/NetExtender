// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Media;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation
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
            BeginConstructorInit();
            Started += SetSizeTo;
            Started += CenterTo;
            Started += ScaleTo;
            Started += DuplicateOwnerIcon;
            Started += SetTaskbarVisible;
            Loaded += BringToForeground;
            EndConstructorInit();
        }

        private void SetSizeTo(Object? sender, EventArgs args)
        {
            SetSizeTo();
        }

        private void DuplicateOwnerIcon(Object? sender, EventArgs args)
        {
            DuplicateOwnerIcon();
        }

        private void SetTaskbarVisible(Object? sender, EventArgs args)
        {
            SetTaskbarVisible();
        }

        private void BringToForeground(Object? sender, EventArgs args)
        {
            if (BringToFront)
            {
                BringToForeground();
            }
        }

        private void CenterTo(Object? sender, EventArgs args)
        {
            CenterTo();
        }

        private void ScaleTo(Object? sender, EventArgs args)
        {
            ScaleTo();
        }

        public virtual void SetSizeTo()
        {
        }

        public virtual void DuplicateOwnerIcon()
        {
            Icon ??= Owner?.Icon ?? Application.Current.MainWindow?.Icon;
        }

        public virtual void SetTaskbarVisible()
        {
        }

        public virtual void BringToForeground()
        {
            UserInterfaceUtilities.BringToForegroundWindow(Handle);
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
            Window? parent = Owner;

            if (parent is null || parent == this || parent.WindowState != WindowState.Normal)
            {
                return CenterToScreen();
            }

            Left = parent.Left + (parent.ActualWidth - ActualWidth) / 2;
            Top = parent.Top + (parent.ActualHeight - ActualHeight) / 2;
            return true;
        }

        public void ScaleTo()
        {
            if (Scale is { } scale)
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