// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using NetExtender.WindowsPresentation.Types.Exceptions;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationTemplateUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? FindTemplate(this Control control, String name)
        {
            return TryFindTemplate(control, name, out Object? result) ? result : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object FindRequiredTemplate(this Control control, String name)
        {
            return FindTemplate(control, name) ?? throw new TemplateNotFoundException(name, control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryFindTemplate(this Control control, String? name, [MaybeNullWhen(false)] out Object result)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            return TryFindTemplate(control.Template, name, control, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FindTemplate<T>(this Control control, String name) where T : DependencyObject
        {
            return FindTemplate(control, name) as T;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindRequiredTemplate<T>(this Control control, String name) where T : DependencyObject
        {
            return FindTemplate<T>(control, name) ?? throw new TemplateNotFoundException<T>(name, control);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryFindTemplate<T>(this Control control, String? name, [MaybeNullWhen(false)] out T result) where T : DependencyObject
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            
            return TryFindTemplate(control.Template, name, control, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? FindTemplate(this ControlTemplate template, String name, FrameworkElement parent)
        {
            return TryFindTemplate(template, name, parent, out Object? result) ? result : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object FindRequiredTemplate(this ControlTemplate template, String name, FrameworkElement parent)
        {
            return FindTemplate(template, name, parent) ?? throw new TemplateNotFoundException(name, parent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryFindTemplate(this ControlTemplate template, String? name, FrameworkElement parent, [MaybeNullWhen(false)] out Object result)
        {
            if (template is null)
            {
                throw new ArgumentNullException(nameof(template));
            }
            
            if (name is null)
            {
                result = null;
                return false;
            }

            try
            {
                result = template.FindName(name, parent);
                return result is not null;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FindTemplate<T>(this ControlTemplate template, String name, FrameworkElement parent) where T : DependencyObject
        {
            return FindTemplate(template, name, parent) as T;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindRequiredTemplate<T>(this ControlTemplate template, String name, FrameworkElement parent) where T : DependencyObject
        {
            return FindTemplate<T>(template, name, parent) ?? throw new TemplateNotFoundException<T>(name, parent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryFindTemplate<T>(this ControlTemplate template, String? name, FrameworkElement parent, [MaybeNullWhen(false)] out T result) where T : DependencyObject
        {
            result = TryFindTemplate(template, name, parent, out Object? @object) ? @object as T : default;
            return result is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
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
    }
}