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
        public static readonly DependencyProperty MenuItemMarginProperty = DependencyProperty.Register(nameof(MenuItemMargin), typeof(Thickness), typeof(TextContextMenu), new PropertyMetadata(new Thickness(-30, 0, -40, 0)));
        public static readonly DependencyProperty MenuItemBackgroundProperty = DependencyProperty.Register(nameof(MenuItemBackground), typeof(Brush), typeof(TextContextMenu), new PropertyMetadata(Brushes.White));
        
        static TextContextMenu()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
            factory.SetValue(MarginProperty, new TwoWayBinding(nameof(MenuItemMargin), new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TextContextMenu), 1)));
            factory.SetValue(Panel.BackgroundProperty, new TwoWayBinding(nameof(MenuItemBackground), new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TextContextMenu), 1)));
            ItemsPanelProperty.OverrideMetadataDefaultValue<TextContextMenu>(new ItemsPanelTemplate { VisualTree = factory });
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
    }
}