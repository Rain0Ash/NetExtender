// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class ParameterMenuItem : MenuItem
    {
        public static readonly DependencyProperty AllowRightClickProperty = DependencyProperty.Register(nameof(AllowRightClick), typeof(Boolean), typeof(ParameterMenuItem), new PropertyMetadata(false));
        
        public Boolean AllowRightClick
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(AllowRightClickProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(AllowRightClickProperty, value);
            }
        }
        protected static Binding CommandParameterBinding { get; } = new Binding();
        
        public ParameterMenuItem()
        {
            PreviewMouseRightButtonDown += OnPreviewMouseRightButton;
            PreviewMouseRightButtonUp += OnPreviewMouseRightButton;
            SetBinding(CommandParameterProperty, CommandParameterBinding);
        }
        
        private void OnPreviewMouseRightButton(Object? sender, MouseButtonEventArgs args)
        {
            args.Handled |= !AllowRightClick;
        }
    }
}