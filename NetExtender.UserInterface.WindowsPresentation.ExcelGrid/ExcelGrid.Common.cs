// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using NetExtender.UserInterface.WindowsPresentation.Types.Comparers;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected Int32 FindPropertyNameIndex(SortDescription description)
        {
            for (Int32 i = 0; i < PropertyDefinitions.Count; i++)
            {
                if (PropertyDefinitions[i].PropertyName == description.PropertyName)
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        protected Int32 FindNextColumn(Int32 row, Int32 column, Int32 delta)
        {
            Int32 columns = Columns;
            while (column >= 0 && column < columns - 1)
            {
                if (!TryGet(new ExcelCell(column, row), out Object? value) || String.IsNullOrEmpty(value?.ToString()))
                {
                    break;
                }
                
                column += delta;
            }
            
            return column;
        }
        
        protected Int32 FindNextRow(Int32 row, Int32 column, Int32 delta)
        {
            Int32 rows = Rows;
            while (row >= 0 && row < rows)
            {
                if (!TryGet(new ExcelCell(column, row), out Object? value) || String.IsNullOrEmpty(value?.ToString()))
                {
                    break;
                }
                
                row += delta;
            }
            
            return rows <= 0 || row < rows ? row : rows - 1;
        }
        
        public Int32 FindSourceIndex(Int32 index)
        {
            if (View is not { } view || ItemsSource is not { } source || index < 0 || index >= source.Count)
            {
                return index;
            }
            
            if (CustomSort is null && view.SortDescriptions.Count <= 0 || CustomSort is ISortDescriptionComparer { Descriptions.Count: <= 0 })
            {
                return index;
            }
            
            if (view.IsEmpty)
            {
                return -1;
            }
            
            if (view is CollectionView collection)
            {
                return source.IndexOf(collection.GetItemAt(index));
            }
            
            Int32 i = 0;
            foreach (Object? item in view)
            {
                if (i++ == index)
                {
                    return source.IndexOf(item);
                }
            }
            
            return -1;
        }
        
        public Int32 FindViewIndex(Int32 index)
        {
            if (View is not { } view || ItemsSource is not { } source || index < 0 || index >= source.Count)
            {
                return index;
            }
            
            if (CustomSort is null && view.SortDescriptions.Count <= 0 || CustomSort is ISortDescriptionComparer { Descriptions.Count: 0 })
            {
                return index;
            }
            
            if (view is CollectionView collection)
            {
                return collection.IndexOf(source[index]!);
            }
            
            Int32 result = 0;
            foreach (Object? item in view)
            {
                if (view == item)
                {
                    return result;
                }
                
                result++;
            }
            
            return -1;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected IEnumerable<Object> EnumerateItems(ExcelCellRange? range)
        {
            if (Operator is not { } @operator)
            {
                yield break;
            }

            if (range is null)
            {
                if (SelectionCollection is not { } selection)
                {
                    yield break;
                }

                IOrderedEnumerable<ExcelCell> order = ItemsInColumns ? selection.OrderBy(static cell => cell.Column).ThenBy(static cell => cell.Row) : selection.OrderBy(static cell => cell.Row).ThenBy(static cell => cell.Column);
                foreach (ExcelCell cell in order)
                {
                    if (@operator.GetItem(cell) is { } item)
                    {
                        yield return item;
                    }
                }

                yield break;
            }
            
            Int32 minimum = ItemsInColumns ? range.LeftColumn : range.TopRow;
            Int32 maximum = ItemsInColumns ? range.RightColumn : range.BottomRow;
            for (Int32 index = minimum; index <= maximum; index++)
            {
                ExcelCell cell = ItemsInColumns ? new ExcelCell(index, range.TopRow) : new ExcelCell(range.LeftColumn, index);
                
                if (@operator.GetItem(cell) is { } item)
                {
                    yield return item;
                }
            }
        }
        
        protected static Type? GetEnumType(Type? type)
        {
            if (type is null)
            {
                return null;
            }
            
            if (Nullable.GetUnderlyingType(type) is { } underlying)
            {
                type = underlying;
            }
            
            return typeof(Enum).IsAssignableFrom(type) ? type : null;
        }
        
        protected static Type? GetItemType(IEnumerable? source)
        {
            if (source is null)
            {
                return null;
            }
            
            try
            {
                return source.AsQueryable().ElementType;
            }
            catch (ArgumentException)
            {
                return source.GetType().GetInterfaces().FirstOrDefault(static type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))?.GetGenericArguments()[0];
            }
        }
        
        protected static Boolean IsIList(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (!typeof(IList).IsAssignableFrom(type))
            {
                return false;
            }
            
            if (ElementType(type) is not { } element || element == typeof(String))
            {
                return false;
            }
            
            if (element.IsGenericType && element.GetGenericTypeDefinition() == typeof(IList<>))
            {
                return true;
            }
            
            return ElementType(element) is not null;
        }
        
        protected static Type? ElementType(Type type)
        {
            foreach (Type @interface in type.GetInterfaces())
            {
                if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition() != typeof(IEnumerable<>))
                {
                    continue;
                }
                
                if (@interface.GetGenericArguments() is { Length: > 0 } args)
                {
                    return args[0];
                }
            }

            return null;
        }
    }
}