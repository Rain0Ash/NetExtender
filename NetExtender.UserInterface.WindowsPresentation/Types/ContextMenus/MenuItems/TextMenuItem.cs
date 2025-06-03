// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Types.Bindings;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class TextMenuItem : AutoHideMenuItem
    {
        public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(TextMenuItem), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty TextAlignmentProperty = TextBlock.TextAlignmentProperty.AddOwner(typeof(TextMenuItem), new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.None));
        
        protected static RelativeSource RelativeSource { get; } = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TextMenuItem), 1);
        protected static Binding TextBinding { get; } = new TwoWayBinding(nameof(Text), RelativeSource);
        protected static Binding TextAlignmentBinding { get; } = new TwoWayBinding(nameof(TextAlignment), RelativeSource);
        protected static Binding IsEnabledBinding { get; } = new TwoWayBinding(nameof(IsEnabled), RelativeSource);
        
        public String Text
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (String) GetValue(TextProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(TextProperty, value);
            }
        }
        
        public TextAlignment TextAlignment
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (TextAlignment) GetValue(TextAlignmentProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(TextAlignmentProperty, value);
            }
        }
        
        public TextMenuItem()
        {
            Loaded += OnLoaded;
        }
        
        private void OnLoaded(Object sender, RoutedEventArgs args)
        {
            Header ??= CreateHeader();
        }
        
        protected virtual Object CreateHeader()
        {
            TextBlock header = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            
            header.SetBinding(TextBlock.TextProperty, TextBinding);
            header.SetBinding(TextBlock.TextAlignmentProperty, TextAlignmentBinding);
            header.SetBinding(IsEnabledProperty, IsEnabledBinding);
            return header;
        }
    }
}