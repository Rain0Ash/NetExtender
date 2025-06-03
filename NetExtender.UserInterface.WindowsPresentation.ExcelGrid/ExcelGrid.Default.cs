// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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