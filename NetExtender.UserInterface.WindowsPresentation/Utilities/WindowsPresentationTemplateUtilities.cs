using System;
using System.Windows;
using System.Windows.Controls;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationTemplateUtilities
    {
        private sealed class TemplateGeneratorControl : ContentControl
        {
            public static readonly DependencyProperty FactoryProperty = DependencyProperty.Register(nameof(Factory), typeof(Func<Object>), typeof(TemplateGeneratorControl), new PropertyMetadata(null, FactoryChanged));

            public Func<Object>? Factory
            {
                get
                {
                    return (Func<Object>?) GetValue(FactoryProperty);
                }
                set
                {
                    SetValue(FactoryProperty, value);
                }
            }
            
            private static void FactoryChanged(DependencyObject instance, DependencyPropertyChangedEventArgs args)
            {
                if (instance is null)
                {
                    throw new ArgumentNullException(nameof(instance));
                }

                TemplateGeneratorControl control = (TemplateGeneratorControl) instance;
                Func<Object>? factory = (Func<Object>?) args.NewValue;
                control.Content = factory?.Invoke();
            }
        }

        public static DataTemplate CreateDataTemplate(Func<Object> template)
        {
            if (template is null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TemplateGeneratorControl));
            factory.SetValue(TemplateGeneratorControl.FactoryProperty, template);

            return new DataTemplate(typeof(DependencyObject))
            {
                VisualTree = factory
            };
        }

        public static ControlTemplate CreateControlTemplate(Type type, Func<Object> template)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (template is null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TemplateGeneratorControl));
            factory.SetValue(TemplateGeneratorControl.FactoryProperty, template);

            return new ControlTemplate(type)
            {
                VisualTree = factory
            };
        }
    }
}