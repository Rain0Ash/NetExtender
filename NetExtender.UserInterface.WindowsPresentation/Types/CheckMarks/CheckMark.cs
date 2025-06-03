// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

using System.Windows;
using System.Windows.Controls;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class CheckMark : Control
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked), typeof(Boolean), typeof(CheckMark), new PropertyMetadata(false));
        public static readonly RoutedEvent IsCheckedChangedEvent = EventManager.RegisterRoutedEvent(nameof(IsCheckedChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CheckMark));
        
        public Boolean IsChecked
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(IsCheckedProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }
        
        public event RoutedEventHandler IsCheckedChanged
        {
            [System.Diagnostics.DebuggerStepThrough]
            add
            {
                AddHandler(IsCheckedChangedEvent, value);
            }
            [System.Diagnostics.DebuggerStepThrough]
            remove
            {
                RemoveHandler(IsCheckedChangedEvent, value);
            }
        }
    }
}