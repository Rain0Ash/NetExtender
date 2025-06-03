// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Converters;
using NetExtender.WindowsPresentation.Utilities;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelGridControlFactory : IExcelGridControlFactory
    {
        protected Dictionary<Type, IValueConverter> Converters { get; } = new Dictionary<Type, IValueConverter>();
        protected List<IExcelGridControlFactory> Factories { get; } = new List<IExcelGridControlFactory>();
        
        public virtual Boolean Match(ExcelCellDescriptor? descriptor, Boolean exact)
        {
            return descriptor is not null && !exact;
        }

        public void RegisterValueConverter(Type instance, IValueConverter converter)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            
            Converters[instance] = converter ?? throw new ArgumentNullException(nameof(converter));
        }
        
        public void RegisterFactory(IExcelGridControlFactory? factory)
        {
            if (factory is not null && !Factories.Contains(factory))
            {
                Factories.Add(factory);
            }
        }
        
        protected IExcelGridControlFactory? FindFactory(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            for (Int32 i = Factories.Count - 1; i >= 0; i--)
            {
                IExcelGridControlFactory factory = Factories[i];
                if (factory.Match(descriptor, true))
                {
                    return factory;
                }
            }
            
            for (Int32 i = Factories.Count - 1; i >= 0; i--)
            {
                IExcelGridControlFactory factory = Factories[i];
                if (factory.Match(descriptor, false))
                {
                    return factory;
                }
            }
            
            return null;
        }
        
        public IValueConverter? GetValueConverter(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (descriptor.PropertyType is not { } type)
            {
                return null;
            }
            
            if (descriptor.Property?.Converter is { } converter)
            {
                return converter;
            }
            
            return Converters.Where(pair => type.IsAssignableFrom(pair.Key)).Values().FirstOrDefault();
        }
        
        protected virtual Boolean SetIsEnabledBinding(ExcelCellDescriptor descriptor, FrameworkElement element)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            
            if (descriptor.Property is not { } property || property.IsEnabledByProperty is null)
            {
                return false;
            }
            
            element.SetIsEnabledBinding(property.IsEnabledByProperty, property.IsEnabledByValue, property.IsEnabledBySource);
            return true;
        }
        
        protected virtual Boolean SetStyle(ExcelCellDescriptor descriptor, FrameworkElement element)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            
            if (descriptor.Property?.Style is not { } modifier)
            {
                return false;
            }
            
            Style style = element.Style is { } @base ? new Style(@base.TargetType, @base) : new Style(element.GetType());
            modifier.Invoke(element, style);
            element.Style = style;
            return true;
        }
        
        private static void FocusParentDataGrid(DependencyObject @object)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            DependencyObject? parent = VisualTreeHelper.GetParent(@object);
            while (parent is not null && parent is not ExcelGrid)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            (parent as UIElement)?.Focus();
        }
        
        public FrameworkElement? FactoryDisplayControl(ExcelCellDescriptor descriptor, Boolean @readonly)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            return FindFactory(descriptor)?.FactoryDisplayControl(descriptor, @readonly) ?? CreateDisplayControl(descriptor, @readonly);
        }
        
        protected virtual FrameworkElement? CreateDisplayControl(ExcelCellDescriptor descriptor, Boolean @readonly)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (descriptor.Property is ExcelTemplateColumnDefinition template)
            {
                return CreateTemplateControl(descriptor, template.EditCellTemplate);
            }
            
            if (descriptor.PropertyType is not { } type)
            {
                return CreateTextBlockControl(descriptor);
            }
            
            return CreateTypeDisplayControl(type, descriptor, @readonly) ?? CreateTextBlockControl(descriptor);
        }
        
        protected virtual FrameworkElement? CreateTypeDisplayControl(Type type, ExcelCellDescriptor descriptor, Boolean @readonly)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (Is(type, typeof(Boolean)))
            {
                return CreateCheckBoxControl(descriptor, @readonly);
            }
            
            if (Is(type, typeof(Color)))
            {
                return CreateColorPreviewControl(descriptor);
            }
            
            return null;
        }
        
        public FrameworkElement? FactoryEditControl(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (FindFactory(descriptor) is { } factory)
            {
                return factory.FactoryEditControl(descriptor);
            }
            
            if (descriptor.Property is not { } property || property.IsReadOnly)
            {
                return null;
            }
            
            if (CreateEditControl(descriptor) is not { } element)
            {
                return null;
            }
            
            element.HorizontalAlignment = HorizontalAlignment.Stretch;
            element.VerticalAlignment = VerticalAlignment.Stretch;
            return element;
        }
        
        protected virtual FrameworkElement? CreateEditControl(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (descriptor.Property is ExcelTemplateColumnDefinition template)
            {
                return CreateTemplateControl(descriptor, template.EditCellTemplate ?? template.CellTemplate);
            }
            
            if (descriptor.PropertyType is not { } type)
            {
                return descriptor.Property?.ItemsSourceProperty is not null ? CreateComboBox(descriptor) : CreateTextBox(descriptor);
            }
            
            if (Is(type, typeof(Boolean)))
            {
                return null;
            }
            
            if (Is(type, typeof(Color)))
            {
                return CreateColorPickerControl(descriptor);
            }
            
            if (!Is(type, typeof(Enum)))
            {
                return descriptor.Property?.ItemsSourceProperty is not null ? CreateComboBox(descriptor) : CreateTextBox(descriptor);
            }
            
            Type underlying = Nullable.GetUnderlyingType(descriptor.PropertyType) ?? descriptor.PropertyType;
            List<Object?> values = Enum.GetValues(underlying).Cast<Object?>().ToList();
            if (Nullable.GetUnderlyingType(type) is not null)
            {
                values.Insert(0, null);
            }
            
            return CreateComboBox(descriptor, values);
        }
        
        protected Binding CreateBinding(ExcelCellDescriptor descriptor)
        {
            return CreateBinding(descriptor, false);
        }
        
        protected Binding CreateBinding(ExcelCellDescriptor descriptor, Boolean @readonly)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            String? format = descriptor.Property?.FormatString;
            if (format is not null && !format.StartsWith("{"))
            {
                format = $"{{0:{format}}}";
            }
            
            Binding binding = new Binding(descriptor.BindingPath)
            {
                Mode = @readonly || descriptor.Property?.IsReadOnly is true || String.IsNullOrEmpty(descriptor.BindingPath) ? BindingMode.OneWay : BindingMode.TwoWay,
                Converter = GetValueConverter(descriptor),
                ConverterParameter = descriptor.Property?.ConverterParameter,
                StringFormat = format,
                ValidatesOnDataErrors = true,
                ValidatesOnExceptions = true,
                NotifyOnSourceUpdated = true
            };
            
            if (descriptor.Property?.ConverterCulture is { } culture)
            {
                binding.ConverterCulture = culture;
            }
            
            return binding;
        }
        
        protected Binding CreateOneWayBinding(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            Binding binding = CreateBinding(descriptor);
            binding.Mode = BindingMode.OneWay;
            return binding;
        }
        
        protected virtual FrameworkElement CreateCheckBoxControl(ExcelCellDescriptor descriptor, Boolean @readonly)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (descriptor.Property is { IsReadOnly: true } property)
            {
                ExcelCheckMark checkmark = new ExcelCheckMark
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = property.HorizontalAlignment,
                };
                
                checkmark.SetBinding(CheckMark.IsCheckedProperty, CreateBinding(descriptor, @readonly));
                SetStyle(descriptor, checkmark);
                return checkmark;
            }
            
            CheckBox checkbox = new CheckBox
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = descriptor.Property?.HorizontalAlignment ?? HorizontalAlignment.Left
            };
            
            checkbox.SetBinding(ToggleButton.IsCheckedProperty, CreateBinding(descriptor));
            SetIsEnabledBinding(descriptor, checkbox);
            SetStyle(descriptor, checkbox);
            return checkbox;
        }
        
        protected FrameworkElement CreateComboBox(ExcelCellDescriptor descriptor)
        {
            return CreateComboBox(descriptor, null);
        }
        
        protected virtual FrameworkElement CreateComboBox(ExcelCellDescriptor descriptor, List<Object?>? source)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            ComboBox combobox = new ComboBox
            {
                IsEditable = descriptor.Property?.IsEditable is true,
                Focusable = false,
                Margin = new Thickness(1, 1, 0, 0),
                HorizontalContentAlignment = descriptor.Property?.HorizontalAlignment ?? HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Padding = new Thickness(3, 0, 3, 0),
                BorderThickness = new Thickness(0)
            };
            
            if (source is not null || descriptor.Property?.ItemsSource is not null)
            {
                combobox.ItemsSource = source ?? descriptor.Property?.ItemsSource;
            }
            else
            {
                if (descriptor.Property?.ItemsSourceProperty is not null)
                {
                    combobox.SetBinding(ItemsControl.ItemsSourceProperty, new Binding(descriptor.Property.ItemsSourceProperty));
                }
            }
            
            combobox.DropDownOpened += (_, _) =>
            {
                combobox.Focusable = true;
            };
            
            combobox.DropDownClosed += (_, _) =>
            {
                combobox.Focusable = false;
                FocusParentDataGrid(combobox);
            };
            
            Binding binding = CreateBinding(descriptor);
            binding.NotifyOnSourceUpdated = true;
            combobox.SetBinding(descriptor.Property?.IsEditable is true ? ComboBox.TextProperty : Selector.SelectedValueProperty, binding);
            combobox.SelectedValuePath = descriptor.Property?.SelectedValuePath;
            combobox.DisplayMemberPath = descriptor.Property?.DisplayMemberPath;
            SetIsEnabledBinding(descriptor, combobox);
            SetStyle(descriptor, combobox);
            return combobox;
        }
        
        protected virtual FrameworkElement CreateTextBlockControl(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            TextBlock textblock = new ExcelTextBlock
            {
                HorizontalAlignment = descriptor.Property?.HorizontalAlignment ?? HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(4, 0, 4, 0)
            };
            
            Binding binding = CreateOneWayBinding(descriptor);
            
            if (descriptor.Property?.ItemsSource is not null)
            {
                if (!String.IsNullOrEmpty(descriptor.Property.DisplayMemberPath))
                {
                    binding.Path.Path += "." + descriptor.Property.DisplayMemberPath;
                }
            }
            
            textblock.SetBinding(TextBlock.TextProperty, binding);
            SetIsEnabledBinding(descriptor, textblock);
            SetStyle(descriptor, textblock);

            Border border = new Border { Child = textblock };
            SetStyle(descriptor, border);
            return border;
        }
        
        protected virtual FrameworkElement CreateTextBox(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            TextBox textbox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = descriptor.Property?.HorizontalAlignment ?? HorizontalAlignment.Left,
                MaxLength = descriptor.Property?.MaxLength ?? Int32.MaxValue,
                BorderThickness = new Thickness(0),
                Margin = new Thickness(1, 1, 0, 0)
            };
            
            static void OnTextBoxLoaded(Object sender, RoutedEventArgs args)
            {
                if (sender is not TextBox textbox)
                {
                    return;
                }
                
                textbox.CaretIndex = textbox.Text.Length;
                textbox.SelectAll();
            }
            
            textbox.Loaded += OnTextBoxLoaded;
            
            Binding binding = CreateBinding(descriptor);
            textbox.SetBinding(TextBox.TextProperty, binding);
            SetIsEnabledBinding(descriptor, textbox);
            SetStyle(descriptor, textbox);
            return textbox;
        }
        
        protected virtual FrameworkElement CreateColorPickerControl(ExcelCellDescriptor descriptor)
        {
            return CreateColorPreviewControl(descriptor);
        }
        
        protected virtual FrameworkElement CreateColorPreviewControl(ExcelCellDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                SnapsToDevicePixels = true,
                Width = 12,
                Height = 12
            };
            
            rectangle.SetBinding(FrameworkElement.DataContextProperty, CreateBinding(descriptor));
            
            SetIsEnabledBinding(descriptor, rectangle);
            Binding color = new Binding { Converter = new ColorToBrushConverter() };
            rectangle.SetBinding(Shape.FillProperty, color);
            
            SetStyle(descriptor, rectangle);
            
            Grid grid = new Grid
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            grid.Children.Add(rectangle);
            
            Border border = new Border { Child = grid };
            SetStyle(descriptor, border);
            return border;
        }
        
        protected virtual FrameworkElement? CreateTemplateControl(ExcelCellDescriptor descriptor, DataTemplate? template)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (template?.LoadContent() is not FrameworkElement content)
            {
                return null;
            }
            
            Binding binding = CreateBinding(descriptor);
            binding.Mode = BindingMode.OneWay;

            ContentControl control = new ContentControl
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Content = content
            };

            content.SetBinding(FrameworkElement.DataContextProperty, binding);
            SetIsEnabledBinding(descriptor, content);
            SetStyle(descriptor, content);
            return control;
        }
        
        protected static Boolean Is(Type first, Type second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            
            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            
            if (first.IsGenericType && second == first.GetGenericTypeDefinition())
            {
                return true;
            }
            
            foreach (Type @interface in first.GetInterfaces())
            {
                if (@interface.IsGenericType && second == @interface.GetGenericTypeDefinition() || second == @interface)
                {
                    return true;
                }
            }
            
            return Nullable.GetUnderlyingType(first) is { } underlying ? second.IsAssignableFrom(underlying) || second.IsAssignableFrom(first) : second.IsAssignableFrom(first);
        }
    }
}