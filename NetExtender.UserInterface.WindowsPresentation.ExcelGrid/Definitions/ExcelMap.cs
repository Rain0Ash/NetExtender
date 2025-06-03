// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public record ExcelMap
    {
        private const Int32 CellsCapacity = ColumnsCapacity * RowsCapacity;
        private const Int32 ColumnsCapacity = 10;
        private const Int32 RowsCapacity = 100;
        
        public Dictionary<Int32, FrameworkElement> Cells { get; } = new Dictionary<Int32, FrameworkElement>(CellsCapacity);
        public Dictionary<Int32, FrameworkElement> Columns { get; } = new Dictionary<Int32, FrameworkElement>(ColumnsCapacity);
        public Dictionary<Int32, FrameworkElement> Rows { get; } = new Dictionary<Int32, FrameworkElement>(RowsCapacity);
    }
}