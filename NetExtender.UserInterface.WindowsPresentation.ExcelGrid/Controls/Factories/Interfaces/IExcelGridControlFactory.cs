using System;
using System.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces
{
    public interface IExcelGridControlFactory
    {
        public Boolean Match(ExcelCellDescriptor? descriptor, Boolean exact);
        public FrameworkElement? FactoryDisplayControl(ExcelCellDescriptor descriptor, Boolean @readonly);
        public FrameworkElement? FactoryEditControl(ExcelCellDescriptor descriptor);
    }
}