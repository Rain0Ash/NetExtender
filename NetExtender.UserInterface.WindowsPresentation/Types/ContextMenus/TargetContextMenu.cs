using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class TargetContextMenu : ContextMenu
    {
        public static readonly DependencyProperty AutoHideProperty = DependencyProperty.RegisterAttached(nameof(AutoHideMenuItem.AutoHide), typeof(Boolean), typeof(ContextMenu), new PropertyMetadata(true));
        protected static Binding TargetBinding { get; } = new Binding(nameof(PlacementTarget) + "." + nameof(DataContext)) { RelativeSource = RelativeSource.Self };
        
        public TargetContextMenu()
        {
            SetBinding(DataContextProperty, TargetBinding);
        }
    }
}