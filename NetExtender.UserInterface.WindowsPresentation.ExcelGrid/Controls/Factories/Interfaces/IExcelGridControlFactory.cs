// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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