// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.ComponentModel;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public interface IExcelGrid
    {
        public IObservableCollection<ExcelPropertyDefinition> ColumnDefinitions { get; }
        public IObservableCollection<ExcelPropertyDefinition> RowDefinitions { get; }
        
        public Int32 Columns { get; }
        public Int32 Rows { get; }
        public Boolean ItemsInColumns { get; }
        public Boolean ItemsInRows { get; }
        public Boolean CanInsert { get; }
        public Boolean CanDelete { get; }
        public IList? ItemsSource { get; }
        public ICollectionView? View { get; }
        public IList? ColumnHeadersSource { get; }
        public Func<Int32, Object> CreateColumnHeader { get; }
        public Func<Object>? CreateItem { get; }
        
        
        public ExcelPropertyDefinition? GetPropertyDefinition(ExcelCell cell);
        public Int32 FindSourceIndex(Int32 index);
    }
}