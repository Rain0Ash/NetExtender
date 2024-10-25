using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelListOperator : ExcelGridOperator
    {
        public ExcelListOperator(ExcelGrid excel)
            : base(excel)
        {
        }

        protected override IEnumerable<ExcelColumnDefinition> GenerateColumnDefinitions(IList? source)
        {
            if (source is null)
            {
                yield break;
            }

            if (CollectionViewSource.GetDefaultView(source) is IItemProperties { ItemProperties: { Count: > 0 } items })
            {
                foreach (ItemPropertyInfo? info in items)
                {
                    if (info.Descriptor is not PropertyDescriptor { IsBrowsable: true } descriptor)
                    {
                        continue;
                    }

                    yield return new ExcelColumnDefinition
                    {
                        PropertyName = descriptor.Name,
                        Header = info.Name,
                        HorizontalAlignment = DefaultHorizontalAlignment,
                        Width = DefaultColumnWidth
                    };
                }

                yield break;
            }

            Type type = GetItemType(source);
            if (TypeDescriptor.GetProperties(type) is not { Count: > 0 } properties)
            {
                properties = GetPropertiesFromInstance(source, type);
            }
            
            if (properties.Count <= 0)
            {
                yield return new ExcelColumnDefinition
                {
                    Header = GetItemType(source).Name,
                    HorizontalAlignment = DefaultHorizontalAlignment,
                    Width = DefaultColumnWidth
                };
                
                yield break;
            }
            
            foreach (PropertyDescriptor descriptor in properties.OfType<PropertyDescriptor>().Where(static descriptor => descriptor.IsBrowsable))
            {
                yield return new ExcelColumnDefinition
                {
                    PropertyName = descriptor.Name,
                    Header = descriptor.Name,
                    HorizontalAlignment = DefaultHorizontalAlignment,
                    Width = DefaultColumnWidth
                };
            }
        }
        
        protected virtual PropertyDescriptorCollection GetPropertiesFromInstance(IList source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            foreach (Object? item in source)
            {
                if (item is not null && item.GetType() == type)
                {
                    return TypeDescriptor.GetProperties(item);
                }
            }

            return new PropertyDescriptorCollection(Array.Empty<PropertyDescriptor>());
        }
        
        public override Object? GetItem(ExcelCell cell)
        {
            if (Excel.ItemsSource is not { Count: > 0 } source)
            {
                return null;
            }

            Int32 index = GetItemIndex(cell);
            if (index >= 0 && index < source.Count)
            {
                return source[index];
            }

            return null;
        }
        
        public override String GetBindingPath(ExcelCell cell)
        {
            return GetPropertyDefinition(cell)?.PropertyName ?? $"[{GetItemIndex(cell)}]";
        }
        
        public override Boolean SetValue(ExcelCell cell, Object? value)
        {
            if (Excel.ItemsSource is not { } source || cell is not { Column: >= 0, Row: >= 0 })
            {
                return false;
            }
            
            source[GetItemIndex(cell)] = value;
            return true;
        }
        
        public override Int32 InsertItem(Int32 index)
        {
            if (Excel.ItemsSource is not { } source)
            {
                return -1;
            }
            
            Type type = GetItemType(source);
            
            try
            {
                Object? item = CreateItem(type);
                if (index < 0)
                {
                    return source.Add(item);
                }
                
                source.Insert(index, item);
                return index;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}