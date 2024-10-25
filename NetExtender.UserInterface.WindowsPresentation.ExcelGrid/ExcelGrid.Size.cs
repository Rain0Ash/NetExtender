using System;
using System.Windows;
using NetExtender.Types.Reflection;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected GridLength GetColumnWidth(Int32 column)
        {
            return column >= 0 && column < ColumnDefinitions.Count && ColumnDefinitions[column] is ExcelColumnDefinition { Width: { Value: >= 0 } width } ? width : DefaultColumnWidth;
        }
        
        protected GridLength GetRowHeight(Int32 i)
        {
            return i >= 0 && i < RowDefinitions.Count && RowDefinitions[i] is ExcelRowDefinition { Height: { Value: >= 0 } height } ? height : DefaultRowHeight;
        }
        
        protected void AutoSizeAllColumns()
        {
            SheetGrid?.UpdateLayout();
            ColumnGrid.UpdateLayout();
            
            for (Int32 column = 0; column < Columns; column++)
            {
                AutoSizeColumn(column);
            }
        }
        
        private Boolean TrySetColumnWidth(Int32 column, GridLength width)
        {
            if (SheetGrid is not { } grid || column < 0 || column >= grid.ColumnDefinitions.Count)
            {
                return false;
            }
            
            grid.ColumnDefinitions[column].Width = width;
            return true;
        }
        
        protected void AutoSizeAllRows()
        {
            SheetGrid?.UpdateLayout();
            RowGrid.UpdateLayout();
            
            for (Int32 row = 0; row < Rows; row++)
            {
                AutoSizeRow(row);
            }
        }
        
        protected virtual Double? AutoSizeColumn(Int32 column)
        {
            if (SheetGrid is not { } grid || column < 0 || column >= Columns)
            {
                return null;
            }

            FrameworkElement? header = GetColumnElement(column);
            Double width = header is not null ? header.ActualWidth + header.Margin.Left + header.Margin.Right : 0;

            for (Int32 row = 0; row < grid.RowDefinitions.Count; row++)
            {
                if (GetElement(new ExcelCell(row, column)) is { } element)
                {
                    width = Math.Max(width, element.ActualWidth + element.Margin.Left + element.Margin.Right);
                }
            }

            TrySetColumnWidth(column, new GridLength(width));
            return width;
        }
        
        protected virtual void AutoSizeRow(Int32 row)
        {
            if (SheetGrid is not { } grid || row < 0 || row >= SheetGrid.RowDefinitions.Count)
            {
                return;
            }
            
            FrameworkElement? header = GetRowElement(row);
            Double height = header is not null ? header.ActualHeight + header.Margin.Top + header.Margin.Bottom : 0;

            for (Int32 column = 0; column < grid.ColumnDefinitions.Count; column++)
            {
                if (GetElement(new ExcelCell(row, column)) is { } element)
                {
                    height = Math.Max(height, element.ActualHeight + element.Margin.Top + element.Margin.Bottom);
                }
            }

            grid.RowDefinitions[row].Height = new GridLength((Int32) height + 2);
        }
        
        // ReSharper disable once CognitiveComplexity
        private void UpdateGridSize()
        {
            if (!IsLoaded || SheetGrid is not { } grid)
            {
                return;
            }
            
            using StackOverflowCounter overflow = new StackOverflowCounter(nameof(UpdateGridSize)) { Limit = 3 };
            
            if (!overflow)
            {
                return;
            }
            
            grid.UpdateLayout();
            ColumnGrid.UpdateLayout();
            RowGrid.UpdateLayout();

            for (Int32 row = 0; row < Rows; row++)
            {
                if (DefaultRowHeight == GridLength.Auto || GetRowHeight(row) == GridLength.Auto)
                {
                    AutoSizeRow(row);
                }
            }
            
            Double width = 0;
            Double stars = 0;
            for (Int32 column = 0; column < Columns; column++)
            {
                GridLength length = GetColumnWidth(column);
                if (length == GridLength.Auto)
                {
                    width += AutoSizeColumn(column) ?? 0;
                }
                else if (length.IsAbsolute)
                {
                    width += length.Value;
                }
                else if (length.IsStar)
                {
                    stars += length.Value;
                }
            }

            stars = Math.Max((SheetScrollViewer.ViewportWidth - width) / stars, 0);
            for (Int32 column = 0; column < Columns; column++)
            {
                if (GetColumnWidth(column) is { IsStar: true } length)
                {
                    TrySetColumnWidth(column, new GridLength(stars * length.Value));
                }
            }

            grid.UpdateLayout();

            for (Int32 column = 0; column < grid.ColumnDefinitions.Count; column++)
            {
                ColumnGrid.ColumnDefinitions[column].Width = new GridLength(grid.ColumnDefinitions[column].ActualWidth);
            }
        }
    }
}