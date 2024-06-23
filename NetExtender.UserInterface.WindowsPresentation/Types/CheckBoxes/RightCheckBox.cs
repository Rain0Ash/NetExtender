using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Types.CheckBoxes
{
    public class RightCheckBox : CheckBox
    {
        private static Style Right { get; }
        public static readonly DependencyProperty IsRightProperty = DependencyProperty.Register(nameof(IsRight), typeof(Boolean), typeof(RightCheckBox), new PropertyMetadata(true, OnRightChanged));
        
        static RightCheckBox()
        {
            Right = new Style(typeof(Path));
            Right.Setters.Add(new Setter(FlowDirectionProperty, FlowDirection.LeftToRight));
        }
        
        public Boolean IsRight
        {
            get
            {
                return (Boolean) GetValue(IsRightProperty);
            }
            set
            {
                SetValue(IsRightProperty, value);
            }
        }
        
        public RightCheckBox()
        {
            Loaded += OnLoaded;
        }
        
        private void OnLoaded(Object? sender, RoutedEventArgs args)
        {
            UpdateFlowDirection();
        }
        
        private void UpdateFlowDirection()
        {
            if (IsRight)
            {
                FlowDirection = FlowDirection.RightToLeft;
                Resources.AddStyle(Right);
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
                Resources.RemoveStyle(Right);
            }
        }
        
        private static void OnRightChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not RightCheckBox checkbox)
            {
                return;
            }
            
            checkbox.UpdateFlowDirection();
        }
    }
}