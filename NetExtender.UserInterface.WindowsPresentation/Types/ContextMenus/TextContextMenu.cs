using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using NetExtender.WindowsPresentation.Types.Bindings;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class TextContextMenu : TargetContextMenu
    {
        private static Object FullThickness { get; } = new Thickness(0, 0, 0, 0);
        private static Object IconThickness { get; } = new Thickness(-30, 0, 0, 0);
        private static Object CheckMarkThickness { get; } = new Thickness(0, 0, -40, 0);
        private static Object TextThickness { get; } = new Thickness(-30, 0, -40, 0);
        
        public static readonly DependencyProperty MenuItemMarginProperty = DependencyProperty.Register(nameof(MenuItemMargin), typeof(Thickness), typeof(TextContextMenu), new PropertyMetadata(TextThickness, ThicknessChanged));
        public static readonly DependencyProperty MenuItemBackgroundProperty = DependencyProperty.Register(nameof(MenuItemBackground), typeof(Brush), typeof(TextContextMenu), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty MenuItemAllowIconProperty = DependencyProperty.Register(nameof(MenuItemAllowIcon), typeof(Boolean), typeof(TextContextMenu), new PropertyMetadata(false, ThicknessChanged));
        public static readonly DependencyProperty MenuItemAllowCheckMarkProperty = DependencyProperty.Register(nameof(MenuItemAllowCheckMark), typeof(Boolean), typeof(TextContextMenu), new PropertyMetadata(false, ThicknessChanged));
        
        static TextContextMenu()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
            factory.SetValue(MarginProperty, new TwoWayBinding(nameof(MenuItemMargin), new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TextContextMenu), 1)));
            factory.SetValue(Panel.BackgroundProperty, new TwoWayBinding(nameof(MenuItemBackground), new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TextContextMenu), 1)));
            ItemsPanelProperty.OverrideMetadataDefaultValue<TextContextMenu, ItemsPanelTemplate>(new ItemsPanelTemplate { VisualTree = factory });
        }

        public Thickness MenuItemMargin
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Thickness) GetValue(MenuItemMarginProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(MenuItemMarginProperty, value);
            }
        }
        
        public Brush MenuItemBackground
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Brush) GetValue(MenuItemBackgroundProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(MenuItemBackgroundProperty, value);
            }
        }
        
        public Boolean MenuItemAllowIcon
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(MenuItemAllowIconProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(MenuItemAllowIconProperty, value);
            }
        }
        
        public Boolean MenuItemAllowCheckMark
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(MenuItemAllowCheckMarkProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(MenuItemAllowCheckMarkProperty, value);
            }
        }

        private static void ThicknessChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not TextContextMenu menu)
            {
                return;
            }
            
            Object? margin = menu.GetValue(MenuItemMarginProperty);
            if (!ReferenceEquals(margin, FullThickness) && !ReferenceEquals(margin, IconThickness) && !ReferenceEquals(margin, CheckMarkThickness) && !ReferenceEquals(margin, TextThickness))
            {
                return;
            }
            
            menu.SetValue(MenuItemMarginProperty, menu.MenuItemAllowIcon switch
            {
                true when menu.MenuItemAllowCheckMark => FullThickness,
                true => IconThickness,
                false when menu.MenuItemAllowCheckMark => CheckMarkThickness,
                false => TextThickness
            });
        }
    }
}