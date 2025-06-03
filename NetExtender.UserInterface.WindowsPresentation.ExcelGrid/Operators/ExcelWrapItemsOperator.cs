// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelWrapItemsOperator : ExcelListOperator
    {
        public override Int32 ColumnCount
        {
            get
            {
                Int32 count = Excel.ColumnDefinitions.Count;
                return count == 0 || Excel.ItemsInRows ? count : (Excel.ItemsSource?.Count ?? 0) / count;
            }
        }
        
        public override Int32 RowCount
        {
            get
            {
                Int32 count = Excel.RowDefinitions.Count;
                return count != 0 && Excel.ItemsInRows ? (Excel.ItemsSource?.Count ?? 0) / count : count;
            }
        }
        
        public ExcelWrapItemsOperator(IExcelGrid excel)
            : base(excel)
        {
        }
        
        public override Boolean CanSort(Int32 index)
        {
            return false;
        }
        
        protected override Int32 GetItemIndex(ExcelCell cell)
        {
            return Excel.ItemsInRows ? cell.Row * Excel.Columns + cell.Column : cell.Column * Excel.Rows + cell.Row;
        }
    }
}