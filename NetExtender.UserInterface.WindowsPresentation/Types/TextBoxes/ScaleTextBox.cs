using System;
using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Types.Bindings;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class ScaleTextBox : TextBox
    {
        public static readonly DependencyProperty FontScaleProperty = DependencyProperty.Register(nameof(FontScale), typeof(Double), typeof(ScaleTextBox), new FrameworkPropertyMetadata(DefaultFontScale, FrameworkPropertyMetadataOptions.AffectsRender, OnFontScaleChanged));
        private static Double DefaultFontSize { get; }
        private const Double DefaultFontScale = 0.8;
        
        static ScaleTextBox()
        {
            DefaultFontSize = new TextBox().FontSize;
        }
        
        private FontSizeScaleConverter FontScaleConverter { get; } = new FontSizeScaleConverter { Scale = DefaultFontScale };
        
        public Double FontScale
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Double) GetValue(FontScaleProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(FontScaleProperty, value);
            }
        }
        
        public ScaleTextBox()
        {
            SetBinding(FontSizeProperty, new OneWayBinding(nameof(ActualHeight), this) { Converter = FontScaleConverter } );
        }
        
        private static void OnFontScaleChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ScaleTextBox textbox)
            {
                return;
            }
            
            textbox.FontScaleConverter.Scale = args.NewValue is Double scale && Double.IsFinite(scale) && scale > 0 ? scale : DefaultFontScale;
        }
    }
}