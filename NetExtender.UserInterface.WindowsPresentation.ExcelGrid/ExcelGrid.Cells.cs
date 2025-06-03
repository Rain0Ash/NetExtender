// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected ExcelCell Get(Point position)
        {
            return Get(position, false);
        }
        
        protected ExcelCell Get(Point position, Boolean autofill)
        {
            return Get(position, autofill, default);
        }
        
        protected ExcelCell Get(Point position, ExcelCell relative)
        {
            return Get(position, false, relative);
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual ExcelCell Get(Point position, Boolean autofill, ExcelCell relative)
        {
            if (SheetGrid is not { } grid)
            {
                return new ExcelCell(-1, -1);
            }
            
            Double width = 0;
            Int32 column = -1;
            Int32 row = -1;
            for (Int32 j = 0; j < grid.ColumnDefinitions.Count; j++)
            {
                Double previous = j - 1 >= 0 ? grid.ColumnDefinitions[j - 1].ActualWidth : 0;
                Double current = grid.ColumnDefinitions[j].ActualWidth;
                Double next = j + 1 < grid.ColumnDefinitions.Count ? SheetGrid.ColumnDefinitions[j + 1].ActualWidth : 0;
                
                (previous, next) = autofill ? relative.Column <= j ? (0D, next * 0.5) : (previous * 0.5, 0D) : (0, 0);
                
                if (position.X <= width - previous || position.X >= width + current + next)
                {
                    width += current;
                    continue;
                }
                
                column = j;
                break;
            }
            
            if (width > 0 && column < 0)
            {
                column = grid.ColumnDefinitions.Count - 1;
            }
            
            Double height = 0;
            for (Int32 i = 0; i < grid.RowDefinitions.Count; i++)
            {
                Double current = grid.RowDefinitions[i].ActualHeight;
                if (position.Y >= height + current)
                {
                    height += current;
                    continue;
                }
                
                row = i;
                break;
            }
            
            if (height > 0 && row < 0)
            {
                row = grid.RowDefinitions.Count - 1;
            }
            
            return column >= 0 && row >= 0 ? new ExcelCell(column, row) : new ExcelCell(-1, -1);
        }
        
        protected Point? Position(ExcelCell cell)
        {
            if (SheetGrid is not { } grid)
            {
                return null;
            }
            
            Double x = 0;
            Double y = 0;
            
            for (Int32 column = 0; column < Math.Min(cell.Column, grid.ColumnDefinitions.Count); column++)
            {
                x += grid.ColumnDefinitions[column].ActualWidth;
            }
            
            for (Int32 row = 0; row < Math.Min(cell.Row, grid.RowDefinitions.Count); row++)
            {
                y += grid.RowDefinitions[row].ActualHeight;
            }
            
            return new Point(x, y);
        }
        
        protected virtual String? Format(ExcelCell cell, Object? value)
        {
            if (ColumnDefinitions[cell.Column] is { Format: { } selector })
            {
                return selector(cell, value);
            }
            
            ExcelPropertyDefinition? definition = GetPropertyDefinition(cell);
            String? format = definition?.FormatString;
            
            if (String.IsNullOrEmpty(format))
            {
                return value?.ToString();
            }
            
            if (!format.Contains("{0"))
            {
                format = "{0:" + format + "}";
            }
            
            return String.Format(format, value);
        }
        
        protected virtual Boolean TryGet(ExcelCell cell, out Object? result)
        {
            if (Operator is not { } @operator)
            {
                result = default;
                return false;
            }
            
            result = @operator.GetCellValue(cell);
            return true;
        }
        
        protected virtual Boolean TryGet(ExcelCellRange? range, [MaybeNullWhen(false)] out Object?[,] result)
        {
            if (range is null)
            {
                result = default;
                return false;
            }
            
            result = new Object[range.Rows, range.Columns];
            for (Int32 row = 0; row < range.Rows; row++)
            {
                for (Int32 column = 0; column < range.Columns; column++)
                {
                    if (!TryGet(new ExcelCell(range.LeftColumn + column, range.TopRow + row), out Object? value))
                    {
                        result = default;
                        return false;
                    }

                    result[row, column] = value;
                }
            }

            return true;
        }
        
        protected virtual Boolean TryGet(ExcelCellRange? range, [MaybeNullWhen(false)] out String?[,] result)
        {
            if (range is null)
            {
                result = default;
                return false;
            }
            
            result = new String[range.Rows, range.Columns];
            for (Int32 row = 0; row < range.Rows; row++)
            {
                for (Int32 column = 0; column < range.Columns; column++)
                {
                    ExcelCell cell = new ExcelCell(range.LeftColumn + column, range.TopRow + row);
                    
                    if (!TryGet(cell, out Object? value))
                    {
                        result = default;
                        return false;
                    }
                    
                    result[row, column] = Format(cell, value);
                }
            }
            
            return true;
        }
        
        protected virtual Boolean TryGet(ExcelCellRange? range, Object?[,]? values, [MaybeNullWhen(false)] out String?[,] result)
        {
            if (range is null)
            {
                result = default;
                return false;
            }
            
            if (values is null)
            {
                return TryGet(range, out result);
            }
            
            result = new String[range.Rows, range.Columns];
            for (Int32 row = 0; row < range.Rows; row++)
            {
                for (Int32 column = 0; column < range.Columns; column++)
                {
                    result[row, column] = Format(new ExcelCell(range.LeftColumn + column, range.TopRow + row), values[row, column]);
                }
            }
            
            return true;
        }
        
        protected virtual Boolean TrySet(ExcelCell cell, Object? value)
        {
            if (Operator is not { } @operator)
            {
                return false;
            }
            
            Boolean successful = @operator.TrySetCellValue(cell, value);
            if (successful && ItemsSource is not INotifyCollectionChanged)
            {
                UpdateContent(cell);
            }
            
            return successful;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual Boolean TrySet(ExcelCellRange range, Object[,] values, out ExcelCellRange result)
        {
            if (range is null)
            {
                throw new ArgumentNullException(nameof(range));
            }
            
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            
            Int32 rows = values.GetUpperBound(0) + 1;
            Int32 columns = values.GetUpperBound(1) + 1;
            
            ExcelCell end = new ExcelCell(Math.Max(range.BottomRight.Column, range.LeftColumn + columns - 1), Math.Max(range.BottomRow, range.TopRow + rows - 1));
            result = new ExcelCellRange(range.TopLeft, end);
            
            if (Operator is not { } @operator)
            {
                return false;
            }
            
            SuppressCollectionNotify = true;
            
            Boolean insertcolumns = CanInsertColumns;
            Boolean insertrows = CanInsertRows;
            
            for (Int32 row = range.TopRow; row <= result.BottomRow; row++)
            {
                if (row >= Rows)
                {
                    if (!insertrows)
                    {
                        break;
                    }
                    
                    @operator.InsertRows(row, 1);
                }
                
                for (Int32 column = range.LeftColumn; column <= result.RightColumn; column++)
                {
                    if (column >= Columns)
                    {
                        if (!insertcolumns)
                        {
                            break;
                        }
                        
                        @operator.InsertColumns(column, 1);
                    }
                    
                    Object value = values[(row - result.TopRow) % rows, (column - result.LeftColumn) % columns];
                    TrySet(new ExcelCell(column, row), value);
                }
            }
            
            UpdateCollectionView();
            
            // TODO: only update changed cells (or rely on bindings)
            Update();
            SuppressCollectionNotify = false;
            return true;
        }
        
        private Boolean SetCheckInSelectedCells(Boolean value)
        {
            Boolean modified = false;
            foreach (ExcelCell cell in SelectedCells)
            {
                if (!TryGet(cell, out Object? current) || current is not Boolean)
                {
                    continue;
                }
                
                if (TrySet(cell, value))
                {
                    modified = true;
                }
            }
            
            return modified;
        }
        
        private Boolean ToggleCheckInCell()
        {
            if (GetElement(CurrentCell) is not { IsEnabled: true } || !TryGet(CurrentCell, out Object? current))
            {
                return false;
            }
            
            Boolean value = true;
            if (current is not Boolean state)
            {
                return SetCheckInSelectedCells(value);
            }
            
            value = state;
            value = !value;
            
            return SetCheckInSelectedCells(value);
        }
        
        // ReSharper disable once CognitiveComplexity
        protected void ChangeCurrentCell(Int32 Δrows, Int32 Δсolumns)
        {
            Int32 row = CurrentCell.Row;
            Int32 column = CurrentCell.Column;
            
            row += Δrows;
            column += Δсolumns;
            
            if (row < 0)
            {
                row = Rows - 1;
                column--;
            }
            
            if (row >= Rows && (!CanInsertRows || !EasyInsertByKeyboard))
            {
                column = column < Columns - 1 ? column + 1 : 0;
                row = 0;
            }
            
            if (column < 0)
            {
                column = 0;
                row = row >= 1 ? row - 1 : Rows - 1;
            }
            
            if (column >= Columns && (!CanInsertColumns || !EasyInsertByKeyboard))
            {
                column = 0;
                row = row < Rows - 1 || CanInsertRows && EasyInsertByKeyboard ? row + 1 : 0;
            }
            
            ExcelCell cell = new ExcelCell(column, row);
            
            if (HandleAutoInsert(cell))
            {
                return;
            }
            
            CurrentCell = cell;
            SelectionCell = cell;
            ScrollIntoView(cell);
        }
        
        protected Int32 InsertItem(Int32 index)
        {
            return InsertItem(index, true);
        }
        
        protected Int32 InsertItem(Int32 index, Boolean update)
        {
            if (Operator is not { } @operator)
            {
                return -1;
            }
            
            index = @operator.InsertItem(index);
            if (index >= 0 && update)
            {
                Update();
            }
            
            RefreshIfRequired(ItemsInColumns);
            return index;
        }
    }
}