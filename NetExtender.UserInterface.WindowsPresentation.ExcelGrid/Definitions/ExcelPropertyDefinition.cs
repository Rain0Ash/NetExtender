using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public delegate String? ExcelFormat(ExcelCell cell, Object? value);
    public delegate void ExcelElementStyle(FrameworkElement element, Style style);
    public delegate void ExcelElementContextMenu(ContextMenu menu, FrameworkElement element);
    
    public abstract class ExcelPropertyDefinition : DependencyObject, INotifyProperty
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        #region Grid
        
        private String? _formatString;
        public String? FormatString
        {
            get
            {
                return _formatString;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _formatString, value);
            }
        }
        
        private ExcelFormat? _format;
        public ExcelFormat? Format
        {
            get
            {
                return _format;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _format, value);
            }
        }
        
        private Object? _header;
        public Object? Header
        {
            get
            {
                return _header;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _header, value);
            }
        }
        
        private Object? _tooltip;
        public Object? Tooltip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _tooltip, value);
            }
        }
        
        private HorizontalAlignment _horizontalAlignment;
        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return _horizontalAlignment;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _horizontalAlignment, value);
            }
        }
        
        private String? _propertyName;
        public String? PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _propertyName, value);
            }
        }
        
        private Boolean _canSort = true;
        public Boolean CanSort
        {
            get
            {
                return _canSort;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _canSort, value);
            }
        }
        
        #endregion
        
        #region Factory
        
        private IValueConverter? _converter;
        public IValueConverter? Converter
        {
            get
            {
                return _converter;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _converter, value);
            }
        }
        
        private CultureInfo? _converterCulture;
        public CultureInfo? ConverterCulture
        {
            get
            {
                return _converterCulture;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _converterCulture, value);
            }
        }
        
        private Object? _converterParameter;
        public Object? ConverterParameter
        {
            get
            {
                return _converterParameter;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _converterParameter, value);
            }
        }
        
        private Boolean _isReadOnly;
        public Boolean IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isReadOnly, value);
            }
        }
        
        private Boolean _isEditable;
        public Boolean IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isEditable, value);
            }
        }
        
        private IEnumerable? _itemsSource;
        public IEnumerable? ItemsSource
        {
            get
            {
                return _itemsSource;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _itemsSource, value);
            }
        }
        
        private String? _itemsSourceProperty;
        public String? ItemsSourceProperty
        {
            get
            {
                return _itemsSourceProperty;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _itemsSourceProperty, value);
            }
        }
        
        private String? _selectedValuePath;
        public String? SelectedValuePath
        {
            get
            {
                return _selectedValuePath;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedValuePath, value);
            }
        }
        
        private String? _displayMemberPath;
        public String? DisplayMemberPath
        {
            get
            {
                return _displayMemberPath;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _displayMemberPath, value);
            }
        }
        
        private Int32 _maxLength = Int32.MaxValue;
        public Int32 MaxLength
        {
            get
            {
                return _maxLength;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _maxLength, value);
            }
        }
        
        private String? _enabledByProperty;
        public String? IsEnabledByProperty
        {
            get
            {
                return _enabledByProperty;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _enabledByProperty, value);
            }
        }
        
        private Object? _enabledByValue;
        public Object? IsEnabledByValue
        {
            get
            {
                return _enabledByValue;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _enabledByValue, value);
            }
        }
        
        private Object? _enabledBySource;
        public Object? IsEnabledBySource
        {
            get
            {
                return _enabledBySource;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _enabledBySource, value);
            }
        }
        
        private ExcelElementStyle? _style;
        public ExcelElementStyle? Style
        {
            get
            {
                return _style;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _style, value);
            }
        }
        
        private ExcelElementContextMenu? _menu;
        public ExcelElementContextMenu? ContextMenu
        {
            get
            {
                return _menu;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _menu, value);
            }
        }
        
        #endregion
        
        public void Set(PropertyEnableByAttribute? attribute)
        {
            if (attribute is null)
            {
                return;
            }
            
            IsEnabledByProperty = attribute.Property;
            IsEnabledByValue = attribute.Value;
        }
        
        public void SetNullable(PropertyEnableByAttribute? attribute)
        {
            if (attribute is null)
            {
                return;
            }
            
            IsEnabledByProperty ??= attribute.Property;
            IsEnabledByValue ??= attribute.Value;
        }
    }
}