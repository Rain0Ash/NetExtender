// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class AutoHideMenuItem : ParameterMenuItem
    {
        public static readonly DependencyProperty AutoHideProperty = DependencyProperty.Register(nameof(AutoHide), typeof(Boolean), typeof(AutoHideMenuItem), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, OnAutoHideChanged));
        public static readonly RoutedEvent AutoHideChangedEvent = EventManager.RegisterRoutedEvent(nameof(AutoHideChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AutoHideMenuItem));
        
        public Boolean AutoHide
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(AutoHideProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(AutoHideProperty, value);
            }
        }
        
        public event RoutedEventHandler AutoHideChanged
        {
            [System.Diagnostics.DebuggerStepThrough]
            add
            {
                AddHandler(AutoHideChangedEvent, value);
            }
            [System.Diagnostics.DebuggerStepThrough]
            remove
            {
                RemoveHandler(AutoHideChangedEvent, value);
            }
        }
        
        public AutoHideMenuItem()
        {
            Loaded += OnLoaded;
            IsEnabledChanged += OnIsEnabledChanged;
        }
        
        private void OnLoaded(Object sender, RoutedEventArgs args)
        {
            UpdateVisibility();
        }
        
        private void OnIsEnabledChanged(Object sender, DependencyPropertyChangedEventArgs args)
        {
            UpdateVisibility();
        }
        
        private void UpdateVisibility()
        {
            if (AutoHide)
            {
                SetCurrentValue(VisibilityProperty, IsEnabled ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        
        private static void OnAutoHideChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not AutoHideMenuItem item)
            {
                return;
            }
            
            item.UpdateVisibility();
            item.OnAutoHideChanged(new RoutedEventArgs(AutoHideChangedEvent));
        }
        
        protected virtual void OnAutoHideChanged(RoutedEventArgs args)
        {
            RaiseEvent(args);
        }
        
        public override String? ToString()
        {
            return Header is TextBlock block ? (String?) block.Text : (String?) (Header?.ToString() ?? Name);
        }
    }
}