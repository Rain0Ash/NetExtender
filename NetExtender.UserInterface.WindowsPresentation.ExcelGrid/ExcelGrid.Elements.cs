using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected void RefreshIfRequired()
        {
            RefreshIfRequired(false);
        }
        
        protected void RefreshIfRequired(Boolean force)
        {
            if (!force && ItemsSource is INotifyCollectionChanged)
            {
                return;
            }
            
            View?.Refresh();
        }
        
        protected void SelectAll()
        {
            SelectionCell = new ExcelCell(Rows - 1, Columns - 1);
            CurrentCell = new ExcelCell(0, 0);
            ScrollIntoView(CurrentCell);
        }
        
        protected virtual FrameworkElement? CreateDisplayControl(ExcelCell cell)
        {
            if (Operator?.CreateCellDescriptor(cell) is not { } descriptor || ControlFactory.FactoryDisplayControl(descriptor, IsReadOnly) is not { } element)
            {
                return null;
            }
            
            element.DataContext = descriptor.BindingSource;
            element.SourceUpdated += (_, _) => CurrentCellSourceUpdated(cell);
            return element;
        }
        
        protected virtual FrameworkElement? CreateEditControl(ExcelCell cell)
        {
            if (Operator?.CreateCellDescriptor(cell) is not { } descriptor || ControlFactory.FactoryEditControl(descriptor) is not { } element)
            {
                return null;
            }
            
            element.DataContext = descriptor.BindingSource;
            element.SourceUpdated += (_, _) => CurrentCellSourceUpdated(cell);
            return element;
        }
        
        private void AddInserterRow(Int32 rows)
        {
            if (SheetGrid is not { } grid)
            {
                return;
            }
            
            if (!CanInsertRows || AddItemHeader is null)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                RowGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                return;
            }
            
            grid.RowDefinitions.Add(new RowDefinition { Height = DefaultRowHeight });
            RowGrid.RowDefinitions.Add(new RowDefinition { Height = DefaultRowHeight });
            
            TextBlock cell = new TextBlock
            {
                Text = AddItemHeader,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            Border border = new Border
            {
                Background = Brushes.Transparent,
                BorderBrush = HeaderBorderBrush,
                BorderThickness = new Thickness(1, 0, 1, 1),
                Margin = new Thickness(0, 0, 0, 0)
            };
            
            border.MouseLeftButtonDown += AddItemCellMouseLeftButtonDown;
            Grid.SetRow(border, rows);
            
            cell.Padding = new Thickness(4, 2, 4, 2);
            Grid.SetRow(cell, rows);
            RowGrid.Children.Add(cell);
            RowGrid.Children.Add(border);
        }
        
        protected virtual void InsertDisplayControl(ExcelCell cell)
        {
            if (SheetGrid is not { } grid || CreateDisplayControl(cell) is not { } control)
            {
                return;
            }
            
            SetElementPosition(control, cell);
            
            if (CellInsertionIndex > grid.Children.Count)
            {
                grid.Children.Add(control);
            }
            else
            {
                grid.Children.Insert(CellInsertionIndex++, control);
            }
            
            Map.Cells.Add(cell.GetHashCode(), control);
        }
        
        protected virtual Boolean OpenComboBoxControl()
        {
            if (CurrentEditControl is not ComboBox combobox)
            {
                return false;
            }
            
            combobox.IsDropDownOpen = true;
            combobox.Focus();
            return true;
        }
        
        protected virtual void ShowEditControl()
        {
            RemoveEditControl();
            ExcelCell cell = CurrentCell;
            
            if (IsReadOnly || cell.Row >= Rows || cell.Column >= Columns)
            {
                return;
            }
            
            if (CurrentEditControl is not null)
            {
                return;
            }
            
            EditingCells = SelectedCells.ToList();
            
            if ((CurrentEditControl = CreateEditControl(cell)) is not { } edit)
            {
                return;
            }
            
            if (edit is TextBox editor)
            {
                editor.Visibility = Visibility.Hidden;
                editor.PreviewKeyDown += TextEditorPreviewKeyDown;
            }
            
            Grid.SetColumn(edit, CurrentCell.Column);
            Grid.SetRow(edit, CurrentCell.Row);
            
            SheetGrid?.Children.Add(edit);
            
            if (edit.Visibility == Visibility.Visible)
            {
                edit.Focus();
            }
        }
        
        protected virtual Boolean ShowTextBoxEditControl()
        {
            if (CurrentEditControl is null)
            {
                ShowEditControl();
            }
            
            if (CurrentEditControl is not TextBox editor)
            {
                return false;
            }
            
            editor.Visibility = Visibility.Visible;
            editor.Focus();
            editor.CaretIndex = editor.Text.Length;
            editor.SelectAll();
            return true;
        }
        
        private void RemoveEditControl()
        {
            if (CurrentEditControl is not { } control)
            {
                return;
            }
            
            if (control is TextBox editor)
            {
                editor.PreviewKeyDown -= TextEditorPreviewKeyDown;
            }
            
            SheetGrid?.Children.Remove(control);
            CurrentEditControl = null;
            Focus();
        }
        
        protected FrameworkElement? GetColumnElement(Int32 column)
        {
            return Map.Columns.TryGetValue(column, out FrameworkElement? element) ? element : null;
        }
        
        protected FrameworkElement? GetRowElement(Int32 row)
        {
            return Map.Rows.TryGetValue(row, out FrameworkElement? element) ? element : null;
        }
        
        protected Object GetColumnHeader(Int32 column)
        {
            return ItemsInRows && column >= 0 && column < PropertyDefinitions.Count && PropertyDefinitions[column].Header is { } header ? header : ExcelCell.ToColumn(column);
        }
        
        protected Object GetRowHeader(Int32 row)
        {
            return ItemsInColumns && row < RowDefinitions.Count && RowDefinitions[row].Header is { } header ? header : ExcelCell.ToRow(row);
        }
        
        protected FrameworkElement? GetElement(ExcelCell cell)
        {
            return Map.Cells.TryGetValue(cell.GetHashCode(), out FrameworkElement? element) ? element is Border border ? border.Child as FrameworkElement : element : null;
        }
        
        protected static void SetElementPosition(UIElement element, ExcelCell cell)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            
            Grid.SetColumn(element, cell.Column);
            Grid.SetRow(element, cell.Row);
        }
        
        protected Boolean HandleAutoInsert(ExcelCell cell)
        {
            if (!AutoInsert || !CanInsert)
            {
                return false;
            }
            
            if (cell.Row < Rows && cell.Column < Columns)
            {
                return false;
            }
            
            ExcelCell current = cell;
            if (cell.Row >= Rows)
            {
                Int32 index = Rows;
                Operator?.InsertRows(index, 1);
                View?.Refresh();
                index = FindViewIndex(index);
                current = new ExcelCell(index, cell.Column);
            }
            
            if (cell.Column >= Columns)
            {
                Int32 index = Columns;
                Operator?.InsertColumns(index, 1);
                View?.Refresh();
                index = FindViewIndex(index);
                current = new ExcelCell(cell.Row, index);
            }
            
            SelectionCell = current;
            CurrentCell = current;
            ScrollIntoView(current);
            return true;
        }
        
        protected virtual void InsertColumns()
        {
            if (Operator is not { } @operator)
            {
                return;
            }
            
            Int32 from = Math.Min(CurrentCell.Column, SelectionCell.Column);
            Int32 to = Math.Max(CurrentCell.Column, SelectionCell.Column);
            
            @operator.InsertColumns(from, to - from + 1);
            RefreshIfRequired(true);
        }
        
        protected virtual void InsertRows()
        {
            if (Operator is not { } @operator)
            {
                return;
            }
            
            Int32 from = Math.Min(CurrentCell.Row, SelectionCell.Row);
            Int32 to = Math.Max(CurrentCell.Row, SelectionCell.Row);
            
            @operator.InsertRows(from, to - from + 1);
            RefreshIfRequired();
        }
        
        protected virtual void DeleteColumns()
        {
            DeleteColumns(true);
        }
        
        protected virtual void DeleteColumns(Boolean scroll)
        {
            if (Operator is not { } @operator)
            {
                return;
            }
            
            Int32 from = Math.Min(CurrentCell.Column, SelectionCell.Column);
            Int32 to = Math.Max(CurrentCell.Column, SelectionCell.Column);
            @operator.DeleteColumns(from, to - from + 1);
            RefreshIfRequired(true);
            
            Int32 columns = Columns;
            Int32 maximum = columns > 0 ? columns - 1 : 0;
            if (CurrentCell.Column > maximum)
            {
                CurrentCell = new ExcelCell(maximum, CurrentCell.Column);
            }

            if (SelectionCell.Column > maximum)
            {
                SelectionCell = new ExcelCell(maximum, SelectionCell.Column);
            }
            
            if (scroll)
            {
                ScrollIntoView(CurrentCell);
            }
        }
        
        protected virtual void DeleteRows()
        {
            DeleteRows(true);
        }
        
        protected virtual void DeleteRows(Boolean scroll)
        {
            if (Operator is not { } @operator)
            {
                return;
            }
            
            Int32 from = Math.Min(CurrentCell.Row, SelectionCell.Row);
            Int32 to = Math.Max(CurrentCell.Row, SelectionCell.Row);
            @operator.DeleteRows(from, to - from + 1);
            RefreshIfRequired();
            
            Int32 rows = Rows;
            Int32 maximum = rows > 0 ? rows - 1 : 0;
            if (CurrentCell.Row > maximum)
            {
                CurrentCell = new ExcelCell(maximum, CurrentCell.Column);
            }

            if (SelectionCell.Row > maximum)
            {
                SelectionCell = new ExcelCell(maximum, SelectionCell.Column);
            }
            
            if (scroll)
            {
                ScrollIntoView(CurrentCell);
            }
        }
        
        public virtual void Clear()
        {
            foreach (ExcelCell cell in SelectedCells)
            {
                TrySet(cell, TryGet(cell, out _) && Operator?.GetPropertyType(cell) is { } type ? ReflectionUtilities.Default(type, false) : null);
            }
        }
    }
}