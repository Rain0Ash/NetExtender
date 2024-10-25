using System;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelWrapItemsOperator : ExcelListOperator
    {
        public override Int32 ColumnCount
        {
            get
            {
                Int32 count = Excel.PropertyDefinitions.Count;
                return count == 0 || Excel.ItemsInRows ? count : (Excel.ItemsSource?.Count ?? 0) / count;
            }
        }
        
        public override Int32 RowCount
        {
            get
            {
                Int32 count = Excel.PropertyDefinitions.Count;
                return count != 0 && Excel.ItemsInRows ? (Excel.ItemsSource?.Count ?? 0) / count : count;
            }
        }
        
        public ExcelWrapItemsOperator(ExcelGrid excel)
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