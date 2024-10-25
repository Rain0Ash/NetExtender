namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class DefaultExcelGrid : ExcelGrid
    {
        public DefaultExcelGrid()
        {
            CommandBindings.Clear();
            ColumnsContextMenu = null;
            RowsContextMenu = null;
            SheetContextMenu = null;
            AutoGenerateColumns = false;
            CanInsert = false;
            CanDelete = false;
            CanClear = false;
        }
    }
}