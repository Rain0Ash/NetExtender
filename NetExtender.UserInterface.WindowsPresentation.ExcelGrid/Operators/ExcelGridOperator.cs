using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using NetExtender.Types.Parsers;
using NetExtender.Types.Parsers.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces;
using NetExtender.Utilities.Types;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public abstract class ExcelGridOperator : IExcelGridOperator
    {
        protected ExcelGrid Excel { get; }
        private Dictionary<ExcelPropertyDefinition, PropertyDescriptor> Descriptors { get; } = new Dictionary<ExcelPropertyDefinition, PropertyDescriptor>();
        
        public HorizontalAlignment DefaultHorizontalAlignment { get; set; } = HorizontalAlignment.Center;
        public GridLength DefaultColumnWidth { get; set; } = new GridLength(1, GridUnitType.Star);
        
        public virtual Int32 ItemCount
        {
            get
            {
                return Excel.View?.Cast<Object>().Count() ?? 0;
            }
        }
        
        public virtual Int32 ColumnCount
        {
            get
            {
                return Excel.ItemsInRows ? Excel.PropertyDefinitions.Count : ItemCount;
            }
        }
        
        public virtual Int32 RowCount
        {
            get
            {
                return Excel.ItemsInRows ? ItemCount : Excel.PropertyDefinitions.Count;
            }
        }
        
        public virtual Boolean CanInsertRows
        {
            get
            {
                return Excel is { ItemsInRows: true, CanInsert: true, ItemsSource.IsFixedSize: false };
            }
        }
        
        public virtual Boolean CanInsertColumns
        {
            get
            {
                return Excel is { ItemsInColumns: true, CanInsert: true, ItemsSource.IsFixedSize: false };
            }
        }
        
        public virtual Boolean CanDeleteRows
        {
            get
            {
                return Excel is { CanDelete: true, ItemsInRows: true, ItemsSource.IsFixedSize: false };
            }
        }
        
        public virtual Boolean CanDeleteColumns
        {
            get
            {
                return Excel is { CanDelete: true, ItemsInColumns: true, ItemsSource.IsFixedSize: false };
            }
        }

        protected ExcelGridOperator(ExcelGrid excel)
        {
            Excel = excel ?? throw new ArgumentNullException(nameof(excel));
        }
        
        public virtual void AutoGenerateColumns()
        {
            Excel.ColumnDefinitions.Clear();
            foreach (ExcelColumnDefinition definition in GenerateColumnDefinitions(Excel.ItemsSource))
            {
                Excel.ColumnDefinitions.Add(definition);
                Excel.PropertyDefinitions.Add(definition);
            }
        }
        
        protected abstract IEnumerable<ExcelColumnDefinition> GenerateColumnDefinitions(IList? source);
        
        public virtual void UpdatePropertyDefinitions()
        {
            Descriptors.Clear();
            
            if (GetItemType(Excel.ItemsSource) is not { } type)
            {
                return;
            }
            
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (ExcelPropertyDefinition definition in Excel.PropertyDefinitions)
            {
                if (String.IsNullOrEmpty(definition.PropertyName))
                {
                    continue;
                }
                
                if (properties[definition.PropertyName] is not { } descriptor)
                {
                    continue;
                }
                
                SetPropertiesFromDescriptor(definition, descriptor);
                Descriptors[definition] = descriptor;
            }
        }
        
        public virtual Boolean CanSort(Int32 index)
        {
            return Excel.PropertyDefinitions[index].CanSort;
        }
        
        public abstract Object? GetItem(ExcelCell cell);
        
        [return: NotNullIfNotNull("source")]
        protected virtual Type? GetItemType(IList? source)
        {
            return source.BiggestCommonType();
        }
        
        protected virtual Int32 GetItemIndex(ExcelCell cell)
        {
            return Excel.ItemsInRows ? Excel.FindSourceIndex(cell.Row) : cell.Column;
        }
        
        protected virtual Object? CreateItem(Type? type)
        {
            if (type is null)
            {
                return null;
            }
            
            if (Excel.CreateItem is { } factory)
            {
                return factory.Invoke();
            }
            
            if (type == typeof(String))
            {
                return String.Empty;
            }
            
            if (type.IsValueType)
            {
                return ReflectionUtilities.Default(type)!;
            }
            
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#else
                return null;
#endif
            }
        }
        
        public virtual Object? GetCellValue(ExcelCell cell)
        {
            if (GetItem(cell) is { } item && GetPropertyDefinition(cell) is { } definition)
            {
                return GetPropertyDescriptor(definition, item, null) is { } descriptor ? descriptor.GetValue(item) : item;
            }
            
            return null;
        }
        
        public virtual Type GetPropertyType(ExcelCell cell)
        {
            ExcelPropertyDefinition? definition = GetPropertyDefinition(cell);
            Object? value = GetCellValue(cell);
            return GetPropertyType(definition, cell, value);
        }
        
        public virtual Type GetPropertyType(ExcelPropertyDefinition? definition, ExcelCell cell, Object? value)
        {
            PropertyDescriptor? descriptor = GetPropertyDescriptor(definition, null, cell);
            return descriptor?.PropertyType is not null ? descriptor.PropertyType : value?.GetType() ?? typeof(Object);
        }
        
        public abstract String GetBindingPath(ExcelCell cell);
        
        public virtual Object? GetDataContext(ExcelCell cell)
        {
            return GetPropertyDefinition(cell)?.PropertyName is not null ? GetItem(cell) : Excel.ItemsSource;
        }
        
        public abstract Boolean SetValue(ExcelCell cell, Object? value);
        
        public virtual Boolean TrySetCellValue(ExcelCell cell, Object? value)
        {
            if (Excel.ItemsSource is null)
            {
                return false;
            }
            
            if (GetItem(cell) is not { } item || GetPropertyDefinition(cell) is not { } definition || definition.IsReadOnly)
            {
                return false;
            }
            
            Type target = GetPropertyType(cell);
            if (!TryConvert(value, target, out Object? convert))
            {
                return false;
            }
            
            try
            {
                if (GetPropertyDescriptor(definition, item, null) is { } descriptor)
                {
                    descriptor.SetValue(item, convert);
                    return true;
                }
                
                SetValue(cell, convert);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public ExcelCellDescriptor CreateCellDescriptor(ExcelCell cell)
        {
            return new ExcelCellDescriptor
            {
                Property = GetPropertyDefinition(cell),
                PropertyType = GetPropertyType(cell),
                BindingPath = GetBindingPath(cell),
                BindingSource = GetDataContext(cell)
            };
        }
        
        protected virtual PropertyDescriptor? GetPropertyDescriptor(ExcelPropertyDefinition? definition, Object? item, ExcelCell? cell)
        {
            if (definition is null)
            {
                return null;
            }

            if (Descriptors.TryGetValue(definition, out PropertyDescriptor? descriptor))
            {
                return descriptor;
            }
            
            if (cell is not null)
            {
                item = GetItem(cell.Value);
            }
            
            return item is not null ? TypeDescriptor.GetProperties(item).OfType<PropertyDescriptor>().FirstOrDefault(property => property.Name == definition.PropertyName) : null;
        }
        
        public virtual ExcelPropertyDefinition? GetPropertyDefinition(ExcelCell cell)
        {
            return Excel.GetPropertyDefinition(cell);
        }
        
        protected static Type? MostGenericType(IList? source)
        {
            if (source is null)
            {
                return null;
            }
            
            Type[] generic = source.GetType().GetGenericArguments();
            Type? type = generic.Length > 0 ? generic[0] : null;
            return type is { IsGenericType: true } ? generic.Length > 0 ? type.GetGenericArguments()[0] : null : type;
        }
        
        protected static Type? Type(IList? source)
        {
            if (source is null)
            {
                return null;
            }
            
            Type? type = MostGenericType(source);
            if (type is not { IsInterface: true })
            {
                return type;
            }
            
            return source.Count > 0 ? source[0] is IList { Count: > 0 } row ? row[0]?.GetType() : type : null;
        }
        
        public abstract Int32 InsertItem(Int32 index);
        
        public virtual void InsertRows(Int32 index, Int32 count)
        {
            for (Int32 i = 0; i < count; i++)
            {
                InsertItem(index);
            }
        }
        
        public virtual void InsertColumns(Int32 index, Int32 count)
        {
            if (!Excel.ItemsInColumns)
            {
                return;
            }

            for (Int32 i = 0; i < count; i++)
            {
                InsertItem(index);
            }
        }
        protected virtual Boolean DeleteItem(Int32 index)
        {
            if (Excel.ItemsSource is not { Count: > 0 } source)
            {
                return false;
            }
            
            index = Excel.FindSourceIndex(index);
            if (index < 0 || index >= source.Count)
            {
                return false;
            }
            
            source.RemoveAt(index);
            return true;
        }
        
        public virtual void DeleteRows(Int32 index, Int32 count)
        {
            for (Int32 i = index + count - 1; i >= index; i--)
            {
                DeleteItem(i);
            }
        }
        
        public virtual void DeleteColumns(Int32 index, Int32 count)
        {
            if (!Excel.ItemsInColumns)
            {
                return;
            }
            
            for (Int32 i = index + count - 1; i >= index; i--)
            {
                DeleteItem(i);
            }
        }

        protected virtual void SetPropertiesFromDescriptor(ExcelPropertyDefinition definition, PropertyDescriptor descriptor)
        {
            if (definition is null)
            {
                throw new ArgumentNullException(nameof(definition));
            }
            
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (descriptor.GetAttributeValue<System.ComponentModel.ReadOnlyAttribute, Boolean>(static attribute => attribute.IsReadOnly) || descriptor.GetAttributeValue<NetExtender.Utilities.Core.ReadOnlyAttribute, Boolean>(a => a.IsReadOnly) || descriptor.IsReadOnly)
            {
                definition.IsReadOnly = true;
            }

            if (descriptor.GetAttributeValue<EditableAttribute, Boolean>(static editable => editable.AllowEdit))
            {
                definition.IsEditable = true;
            }
            
            definition.ItemsSourceProperty ??= descriptor.GetFirstAttributeOrDefault<ItemsSourcePropertyAttribute>()?.Property;
            definition.SelectedValuePath ??= descriptor.GetFirstAttributeOrDefault<SelectedValuePathAttribute>()?.Path;
            definition.DisplayMemberPath ??= descriptor.GetFirstAttributeOrDefault<DisplayMemberPathAttribute>()?.Path;
            definition.SetNullable(descriptor.GetFirstAttributeOrDefault<PropertyEnableByAttribute>());
        }
        
        private static Boolean TryConvert(Object? value, Type type, out Object? result)
        {
            return TryConvert(value, type, null, out result);
        }
        
        private static Boolean TryConvert(Object? value, Type type, IFormatProvider? provider, out Object? result)
        {
            try
            {
                if (value is null)
                {
                    result = ReflectionUtilities.Default(type);
                    return true;
                }
                
                if (value.GetType() == type)
                {
                    result = value;
                    return true;
                }
                
                if (type == typeof(String))
                {
                    result = value?.ToString();
                    return true;
                }
                
                if (type == typeof(Boolean) && value is String @string)
                {
                    result = @string.ToBoolean();
                    return true;
                }
                
                if (ConvertUtilities.TryChangeType(value, type, provider, out result))
                {
                    return true;
                }
                
                TypeConverter converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(value.GetType()))
                {
                    result = converter.ConvertFrom(value);
                    return true;
                }

                if (AutoParser.Get(value.GetType()) is IParser parser && parser.TryParse(value, provider, out result))
                {
                    return true;
                }

                result = null;
                return false;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}