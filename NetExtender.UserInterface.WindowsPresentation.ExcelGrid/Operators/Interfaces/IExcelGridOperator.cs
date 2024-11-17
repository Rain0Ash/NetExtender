using System;
using System.Collections.Generic;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces
{
    public interface IExcelGridOperator
    {
        public Int32 ItemCount { get; }
        public Int32 ColumnCount { get; }
        public Int32 RowCount { get; }
        public Boolean CanInsertRows { get; }
        public Boolean CanInsertColumns { get; }
        public Boolean CanDeleteRows { get; }
        public Boolean CanDeleteColumns { get; }
        
        public IEnumerable<ExcelColumnDefinition> AutoGenerateColumns();
        public void UpdatePropertyDefinitions();
        public Boolean CanSort(Int32 index);
        public Object? GetItem(ExcelCell cell);
        public Object? GetCellValue(ExcelCell cell);
        public Type GetPropertyType(ExcelCell cell);
        public Boolean TrySetCellValue(ExcelCell cell, Object? value);
        public ExcelCellDescriptor CreateCellDescriptor(ExcelCell cell);
        public Int32 InsertItem(Int32 index);
        public void InsertRows(Int32 index, Int32 count);
        public void InsertColumns(Int32 index, Int32 count);
        public void DeleteRows(Int32 index, Int32 count);
        public void DeleteColumns(Int32 index, Int32 count);
    }
}