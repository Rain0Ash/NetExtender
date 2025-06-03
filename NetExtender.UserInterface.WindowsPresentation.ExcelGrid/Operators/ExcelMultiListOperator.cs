// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelMultiListOperator : ExcelGridOperator
    {
        public override Boolean CanInsertColumns
        {
            get
            {
                return true;
            }
        }
        
        public override Boolean CanDeleteColumns
        {
            get
            {
                return true;
            }
        }
        
        public ExcelMultiListOperator(IExcelGrid excel)
            : base(excel)
        {
        }
        
        protected override IEnumerable<ExcelColumnDefinition> AutoGenerateColumns(IList? source)
        {
            if (source is null)
            {
                yield break;
            }
            
            Type type = Type(source) ?? typeof(Object);
            Int32 columns = source.Cast<IList>().FirstOrDefault()?.Count ?? 0;
            for (Int32 i = 0; i < columns; i++)
            {
                yield return new ExcelColumnDefinition
                {
                    Header = type.Name,
                    HorizontalAlignment = DefaultHorizontalAlignment,
                    Width = DefaultColumnWidth
                };
            }
        }
        
        public override Boolean CanSort(Int32 index)
        {
            return false;
        }
        
        public override Object? GetItem(ExcelCell cell)
        {
            if (Excel.ItemsSource is not { } source || cell.Row < 0 || cell.Column < 0 || cell.Row >= source.Count)
            {
                return null;
            }
            
            if (source[cell.Row] is not IList row || cell.Column >= row.Count)
            {
                return null;
            }

            return ((IList?) source[cell.Row])?[cell.Column];
        }
        
        // ReSharper disable once CognitiveComplexity
        public override Int32 InsertItem(Int32 index)
        {
            if (Excel.ItemsSource is not { } source)
            {
                return -1;
            }

            Type type = GetItemType(source);
            
            try
            {
                if (CreateItem(type) is not IList list || Type(list) is not { } element)
                {
                    return -1;
                }

                if (Excel.ItemsInRows)
                {
                    for (Int32 i = 0; i < Excel.Columns; i++)
                    {
                        list.Add(CreateItem(element));
                    }
                    
                    if (index < 0)
                    {
                        return list.Add(list);
                    }
                    
                    list.Insert(index, list);
                    return index;
                }

                foreach (IList row in list.OfType<IList>())
                {
                    Object? item = CreateItem(element);
                    if (index < 0)
                    {
                        index = row.Add(item);
                        continue;
                    }

                    row.Insert(index, item);
                }
                
                return index;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        public override String GetBindingPath(ExcelCell cell)
        {
            return $"[{cell.Row}][{cell.Column}]";
        }
        
        public override Boolean SetValue(ExcelCell cell, Object? value)
        {
            if (Excel.ItemsSource is not { } source || cell.Row < 0 || cell.Column < 0 || cell.Row >= source.Count)
            {
                return false;
            }
            
            if (source[cell.Row] is not IList row || cell.Column >= row.Count)
            {
                return false;
            }
            
            row[cell.Column] = value;
            return true;
        }
        
        protected virtual Int32 InsertColumnHeader(Int32 index)
        {
            if (Excel.ColumnHeadersSource is null)
            {
                return -1;
            }
            
            Object item = Excel.CreateColumnHeader(index);
            if (index < 0 || index >= Excel.ColumnHeadersSource.Count)
            {
                return Excel.ColumnHeadersSource.Add(item);
            }

            Excel.ColumnHeadersSource.Insert(index, item);
            return index;
        }
        
        public override void InsertColumns(Int32 index, Int32 count)
        {
            for (Int32 i = 0; i < count; i++)
            {
                InsertColumnHeader(index + i);
            }
            
            base.InsertColumns(index, count);
        }
        
        protected override Boolean DeleteItem(Int32 index)
        {
            if (Excel.ItemsSource is not { } source || index < 0 || index >= source.Count)
            {
                return false;
            }
            
            if (!Excel.ItemsInColumns)
            {
                source.RemoveAt(index);
                return true;
            }
            
            foreach (IList row in Excel.ItemsSource.OfType<IList>().Where(row => index < row.Count))
            {
                row.RemoveAt(index);
            }
            
            return true;
        }
        
        public override void DeleteColumns(Int32 index, Int32 count)
        {
            if (Excel.ColumnHeadersSource is not { } source)
            {
                base.DeleteColumns(index, count);
                return;
            }
            
            for (Int32 i = index + count - 1; i >= index; i--)
            {
                source.RemoveAt(i);
            }

            base.DeleteColumns(index, count);
        }
    }
}