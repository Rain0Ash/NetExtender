using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using NetExtender.Types.Entities;
using NetExtender.Types.Images;
using NetExtender.WindowsPresentation.Types.Bindings;

namespace NetExtender.UserInterface.WindowsPresentation.Types.ComboBoxes
{
    public class LocalizationSelectorComboBox : ComboBox
    {
        public LocalizationSelectorComboBox()
        {
            SelectedValuePath = nameof(LocalizationImageEntry<Any>.Identifier);
            Initialized += OnInitialized;
        }
        
        private void OnInitialized(Object? sender, EventArgs args)
        {
            ItemTemplate = CreateItemTemplate();
        }
        
        protected virtual DataTemplate CreateItemTemplate()
        {
            DataTemplate template = new DataTemplate();

            FrameworkElementFactory stackpanel = new FrameworkElementFactory(typeof(StackPanel));
            stackpanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            image.SetBinding(Image.SourceProperty, new OneWayBinding(nameof(LocalizationImageEntry<Any>.Image)));
            image.SetValue(Image.StretchProperty, Stretch.None);
            stackpanel.AppendChild(image);

            FrameworkElementFactory text = new FrameworkElementFactory(typeof(TextBlock));
            FrameworkElementFactory run = new FrameworkElementFactory(typeof(Run));
            run.SetBinding(Run.TextProperty, new OneWayBinding(nameof(LocalizationImageEntry<Any>.Name)) { StringFormat = " {0}" });
            text.AppendChild(run);

            stackpanel.AppendChild(text);

            template.VisualTree = stackpanel;
            return template;
        }
    }
}