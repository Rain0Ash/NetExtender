using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected virtual Boolean CanMouseMove(ExcelCell cell)
        {
            //Changed > to >=
            if (cell.Column < 0 || cell.Column >= Columns && (!CanInsertColumns || !EasyInsertByMouse))
            {
                return false;
            }
            
            if (cell.Row < 0 || cell.Row >= Rows && (!CanInsertRows || !EasyInsertByMouse))
            {
                return false;
            }
            
            return true;
        }
        
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            
            if (!IsMouseCaptured || SheetGrid is not { } grid)
            {
                return;
            }
            
            Point position = args.GetPosition(grid);
            ExcelCell cell = Get(position, AutoFillSelection is { Visibility: Visibility.Visible }, CurrentCell);
            
            if (!CanMouseMove(cell))
            {
                return;
            }
            
            if (AutoFillSelection is { } selection && selection.Visibility != Visibility.Visible)
            {
                SelectionCell = cell;
                ScrollIntoView(cell);
                return;
            }
            
            AutoFillCell = cell;
            if (AutoFiller.TryExtrapolate(cell, CurrentCell, SelectionCell, AutoFillCell, out Object? result))
            {
                AutoFillToolTip.Content = Format(cell, result);
            }
            
            Point point = args.GetPosition(AutoFillSelection);
            AutoFillToolTip.Placement = PlacementMode.Relative;
            AutoFillToolTip.HorizontalOffset = point.X + 8;
            AutoFillToolTip.VerticalOffset = point.Y + 8;
            ScrollIntoView(cell);
        }
        
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs args)
        {
            OnMouseUp(args);
            
            ReleaseMouseCapture();
            Mouse.OverrideCursor = null;
            
            if (AutoFillSelection is not { Visibility: Visibility.Visible })
            {
                return;
            }
            
            AutoFiller.AutoFill(CurrentCell, SelectionCell, AutoFillCell);
            
            AutoFillSelection.Visibility = Visibility.Hidden;
            AutoFillToolTip.IsOpen = false;
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            Focus();
            base.OnMouseLeftButtonDown(args);
            
            if (SheetGrid is not { } grid)
            {
                return;
            }

            Point position = args.GetPosition(grid);
            ExcelCell cell = Get(position);
            
            if (!CanMouseMove(cell))
            {
                return;
            }

            if (AutoFillSelection is { Visibility: Visibility.Visible })
            {
                AutoFillCell = cell;
                Mouse.OverrideCursor = AutoFillBox?.Cursor;
                AutoFillToolTip.IsOpen = true;
                
                CaptureMouse();
                args.Handled = true;
                return;
            }
            
            if (!HandleAutoInsert(cell))
            {
                if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    CurrentCell = cell;
                }
                
                SelectionCell = cell;
                ScrollIntoView(cell);
            }
            
            Mouse.OverrideCursor = SheetGrid?.Cursor;
            CaptureMouse();
            args.Handled = true;
        }
        
        protected virtual void SheetGridMouseDown(Object? sender, MouseButtonEventArgs args)
        {
            if (args.ClickCount != 2)
            {
                return;
            }
            
            ShowTextBoxEditControl();
            args.Handled = true;
        }
        
        protected virtual void TopLeftMouseDown(Object? sender, MouseButtonEventArgs args)
        {
            Focus();
            SelectAll();
            args.Handled = true;
        }
        
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs args)
        {
            base.OnPreviewMouseWheel(args);

            if (!Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                return;
            }

            Double step = 1 + args.Delta * 0.0004;
            
            TransformGroup transform = new TransformGroup();
            transform.Children.AddIfNotNull(LayoutTransform);
            transform.Children.Add(new ScaleTransform(step, step));
            
            LayoutTransform = transform;
            args.Handled = true;
        }
        
        protected virtual void ColumnGridMouseMove(Object? sender, MouseEventArgs args)
        {
            if (!ColumnGrid.IsMouseCaptured)
            {
                return;
            }
            
            Int32 column = Get(args.GetPosition(ColumnGrid)).Column;
            if (column >= 0)
            {
                SelectionCell = new ExcelCell(Rows - 1, column);
            }
        }
        
        protected virtual void ColumnGridMouseUp(Object? sender, MouseButtonEventArgs args)
        {
            ColumnGrid.ReleaseMouseCapture();
        }
        
        protected virtual void ColumnGridMouseDown(Object? sender, MouseButtonEventArgs args)
        {
            Focus();
            
            Int32 column = Get(args.GetPosition(ColumnGrid)).Column;
            if (column >= 0)
            {
                Boolean shift = (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.None;
                SelectionCell = new ExcelCell(Rows - 1, column);
                CurrentCell = shift ? new ExcelCell(0, CurrentCell.Column) : new ExcelCell(0, column);
                ScrollIntoView(SelectionCell);
            }
            
            if (args.ChangedButton == MouseButton.Left && CanSort)
            {
                ToggleSort();
            }
            
            ColumnGrid.CaptureMouse();
            args.Handled = true;
        }
        
        protected virtual void RowGridMouseMove(Object? sender, MouseEventArgs args)
        {
            if (!RowGrid.IsMouseCaptured)
            {
                return;
            }
            
            Int32 row = Get(args.GetPosition(RowGrid)).Row;
            if (row < 0)
            {
                return;
            }
            
            SelectionCell = new ExcelCell(row, Columns - 1);
            args.Handled = true;
        }
        
        protected virtual void RowGridMouseUp(Object? sender, MouseButtonEventArgs args)
        {
            RowGrid.ReleaseMouseCapture();
        }
        
        protected virtual void RowGridMouseDown(Object? sender, MouseButtonEventArgs args)
        {
            Focus();
            
            Int32 row = Get(args.GetPosition(RowGrid)).Row;
            ExcelCellRange range = SelectionRange;
            if (args.ChangedButton == MouseButton.Right && range.TopRow <= row && range.BottomRow >= row)
            {
                return;
            }
            
            if (row < 0)
            {
                RowGrid.CaptureMouse();
                args.Handled = true;
                return;
            }
            
            SelectionCell = new ExcelCell(row, Columns - 1);
            CurrentCell = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) ? new ExcelCell(CurrentCell.Row, 0) : new ExcelCell(row, 0);

            ScrollIntoView(SelectionCell);
            RowGrid.CaptureMouse();
            args.Handled = true;
        }
        
        protected virtual void AutoFillBoxMouseDown(Object? sender, MouseButtonEventArgs args)
        {
            if (AutoFillSelection is { } selection)
            {
                selection.Visibility = Visibility.Visible;
            }
        }
        
        protected virtual void AddItemCellMouseLeftButtonDown(Object? sender, MouseButtonEventArgs args)
        {
            Focus();
            
            Int32 index = InsertItem(-1);
            View?.Refresh();
            
            if (index < 0)
            {
                args.Handled = true;
                return;
            }
            
            index = FindViewIndex(index);
            ExcelCell cell = ItemsInRows ? new ExcelCell(index, 0) : new ExcelCell(0, index);
            SelectionCell = cell;
            CurrentCell = cell;
            ScrollIntoView(cell);
            args.Handled = true;
        }
        
        protected virtual void ColumnSplitterDoubleClick(Object? sender, MouseButtonEventArgs args)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                AutoSizeAllColumns();
            }
            
            if (sender is not GridSplitter splitter)
            {
                return;
            }
            
            Int32 column = Grid.GetColumn(splitter);
            AutoSizeColumn(column);
        }
        
        protected void ColumnSplitterChangeStarted(Object? sender, DragStartedEventArgs args)
        {
            ColumnSplitterChangeDelta(sender, null);
        }
        
        protected virtual void ColumnSplitterChangeDelta(Object? sender, DragDeltaEventArgs? args)
        {
            if (sender is not GridSplitter splitter)
            {
                return;
            }
            
            if (splitter.ToolTip is not ToolTip tooltip)
            {
                splitter.ToolTip = tooltip = new ToolTip
                {
                    IsOpen = true
                };
            }
            
            Int32 column = Grid.GetColumn(splitter);
            Double width = ColumnGrid.ColumnDefinitions[column].ActualWidth;

            tooltip.Content = $"{nameof(ColumnDefinition.Width)}: {width:0.#}";
            tooltip.Placement = PlacementMode.Relative;
            tooltip.PlacementTarget = ColumnGrid;
            tooltip.HorizontalOffset = Mouse.GetPosition(ColumnGrid).X;
            tooltip.VerticalOffset = splitter.ActualHeight + 4;
        }
        
        protected virtual void ColumnSplitterChangeCompleted(Object? sender, DragCompletedEventArgs args)
        {
            if (sender is not GridSplitter { ToolTip: ToolTip tooltip } splitter)
            {
                return;
            }
            
            tooltip.IsOpen = false;
            splitter.ToolTip = null;
        }
        
        protected virtual void RowSplitterDoubleClick(Object? sender, MouseButtonEventArgs args)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                AutoSizeAllRows();
            }
            
            if (sender is not GridSplitter splitter)
            {
                return;
            }
            
            Int32 row = Grid.GetRow(splitter);
            AutoSizeRow(row);
        }
        
        protected void RowSplitterChangeStarted(Object? sender, DragStartedEventArgs args)
        {
            RowSplitterChangeDelta(sender, null);
        }
        
        protected virtual void RowSplitterChangeDelta(Object? sender, DragDeltaEventArgs? args)
        {
            if (sender is not GridSplitter splitter)
            {
                return;
            }
            
            if (splitter.ToolTip is not ToolTip tooltip)
            {
                splitter.ToolTip = tooltip = new ToolTip
                {
                    IsOpen = true
                };
            }
            
            Int32 row = Grid.GetRow(splitter);
            Double height = RowGrid.RowDefinitions[row].ActualHeight;
            
            tooltip.Content = $"{nameof(RowDefinition.Height)}: {height:0.#}";
            tooltip.Placement = PlacementMode.Relative;
            tooltip.PlacementTarget = RowGrid;
            tooltip.HorizontalOffset = splitter.ActualWidth + 4;
            tooltip.VerticalOffset = Mouse.GetPosition(RowGrid).Y;
        }
        
        protected virtual void RowSplitterChangeCompleted(Object? sender, DragCompletedEventArgs args)
        {
            if (sender is not GridSplitter { ToolTip: ToolTip tooltip } splitter)
            {
                return;
            }
            
            tooltip.IsOpen = false;
            splitter.ToolTip = null;
        }
    }
}