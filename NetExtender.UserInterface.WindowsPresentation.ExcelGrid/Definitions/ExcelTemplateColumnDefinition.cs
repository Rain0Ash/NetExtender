using System.Windows;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public class ExcelTemplateColumnDefinition : ExcelColumnDefinition
    {
        private DataTemplate? _cell;
        public DataTemplate? CellTemplate
        {
            get
            {
                return _cell;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _cell, value);
            }
        }
        
        private DataTemplate? _editcell;
        public DataTemplate? EditCellTemplate
        {
            get
            {
                return _editcell;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _editcell, value);
            }
        }
    }
}