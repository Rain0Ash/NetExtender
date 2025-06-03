// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using NetExtender.UserInterface.WindowsPresentation.Types.Comparers;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.UserInterface;
using NetExtender.WindowsPresentation.Types.Bindings;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        public virtual void ScrollIntoView(ExcelCell cell)
        {
            if (SheetGrid is not { } grid)
            {
                return;
            }
            
            grid.UpdateLayout();
            
            if (Position(cell) is not { } current || Position(new ExcelCell(cell.Column + 1, cell.Row + 1)) is not { } next)
            {
                return;
            }
            
            if (current.X < SheetScrollViewer.HorizontalOffset)
            {
                SheetScrollViewer.ScrollToHorizontalOffset(current.X);
            }
            
            if (next.X > SheetScrollViewer.HorizontalOffset + SheetScrollViewer.ViewportWidth)
            {
                SheetScrollViewer.ScrollToHorizontalOffset(Math.Max(next.X - SheetScrollViewer.ViewportWidth, 0));
            }
            
            if (current.Y < SheetScrollViewer.VerticalOffset)
            {
                SheetScrollViewer.ScrollToVerticalOffset(current.Y);
            }
            
            if (next.Y > SheetScrollViewer.VerticalOffset + SheetScrollViewer.ViewportHeight)
            {
                SheetScrollViewer.ScrollToVerticalOffset(Math.Max(next.Y - SheetScrollViewer.ViewportHeight, 0));
            }
        }
        
        private volatile Boolean _update;
        public void Update()
        {
            try
            {
                if (SheetGrid is null || _update)
                {
                    return;
                }
                
                _update = true;
                ClearContent();
                
                if (ItemsSource is null || CreateOperator() is not { } @operator)
                {
                    return;
                }
                
                Operator = @operator;
                if (AutoGenerateColumns && ColumnDefinitions.Count <= 0)
                {
                    ObservableCollectionUtilities.AddRange(ColumnDefinitions, @operator.AutoGenerateColumns());
                }
                
                @operator.UpdatePropertyDefinitions();
                ItemsInColumns = PropertyDefinitions.OfType<ExcelRowDefinition>().FirstOrDefault() is not null;
                
                (Int32 columns, Int32 rows) = (@operator.ColumnCount, @operator.RowCount);
                Visibility visibility = rows >= 0 ? Visibility.Visible : Visibility.Hidden;
                RowScrollViewer.Visibility = ColumnScrollViewer.Visibility = SheetScrollViewer.Visibility = visibility;
                
                if (TopLeft is not null)
                {
                    TopLeft.Visibility = visibility;
                }
                
                if (rows < 0)
                {
                    return;
                }
                
                UpdateColumns(columns);
                UpdateRows(rows);
                UpdateCells(columns, rows);
                UpdateSortDescriptionMarkers();
                
                UpdateSelectionVisibility();
                ShowEditControl();
                
                Dispatcher.BeginInvoke(new Action(UpdateGridSize), DispatcherPriority.Loaded);
            }
            finally
            {
                _update = false;
            }
        }
        
        protected virtual void UpdateCollectionView()
        {
            if (!Dispatcher.CheckAccess())
            {
                Debug.WriteLine("Updating collection view on non-dispatcher thread.");
            }
            
            if (View is not { } view)
            {
                return;
            }
            
            if (view is ListCollectionView list)
            {
                list.CustomSort = CustomSort;
            }
            
            SortDescriptionCollection descriptions = view.SortDescriptions;
            if (CustomSort is ISortDescriptionComparer comparer)
            {
                descriptions = comparer.Descriptions;
                view.SortDescriptions.Clear();
            }
            
            descriptions.Clear();
            
            try
            {
                foreach (SortDescription description in SortDescriptor.Descriptors)
                {
                    descriptions.Add(description);
                }
            }
            catch (InvalidOperationException)
            {
                descriptions.Clear();
            }
            
            view.Refresh();
            UpdateSortDescriptionMarkers();
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual void UpdateColumns(Int32 columns)
        {
            if (SheetGrid is not { } grid)
            {
                return;
            }
            
            ColumnDefinition definition;
            Binding binding;
            for (Int32 column = 0; column < columns; column++)
            {
                GridLength width = ColumnWidth(column);
                definition = new ColumnDefinition { Width = width };
                grid.ColumnDefinitions.Add(definition);
                binding = new TwoWayBinding(nameof(ColumnDefinition.Width), definition) { Source = definition };
                
                definition = new ColumnDefinition();
                ColumnGrid.ColumnDefinitions.Add(definition);
                definition.SetBinding(ColumnDefinition.WidthProperty, binding);
            }

            definition = new ColumnDefinition();
            ColumnGrid.ColumnDefinitions.Add(definition);
            
            binding = new Binding(nameof(ScrollViewer.ComputedVerticalScrollBarVisibility))
            {
                Source = SheetScrollViewer,
                Converter = VerticalScrollBarVisibilityConverter,
                ConverterParameter = SystemParameters.VerticalScrollBarWidth
            };
            
            definition.SetBinding(ColumnDefinition.WidthProperty, binding);

            ColumnGrid.ContextMenu = ColumnsContextMenu;
            ColumnGrid.Children.AddIfNotNull(ColumnSelectionBackground);

            for (Int32 column = 0; column < columns; column++)
            {
                Object header = GetColumnHeader(column);
                ExcelCell cell = new ExcelCell(ItemsInRows ? column : -1, ItemsInRows ? -1 : column);
                ExcelPropertyDefinition? property = GetPropertyDefinition(cell);

                Border border = new Border
                {
                    BorderBrush = HeaderBorderBrush,
                    BorderThickness = new Thickness(0, 1, 1, 1),
                    Margin = new Thickness(0, 0, column < columns - 1 ? -1 : 0, 0)
                };
                
                Grid.SetColumn(border, column);
                ColumnGrid.Children.Add(border);

                FrameworkElement element = header as FrameworkElement ?? new TextBlock
                {
                    Text = header.ToString() ?? "-",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = ItemsInRows && property is not null ? property.HorizontalAlignment : HorizontalAlignment.Center,
                    Padding = new Thickness(4, 2, 4, 2)
                };

                if (property?.Tooltip is not null)
                {
                    ToolTipService.SetToolTip(element, property.Tooltip);
                }

                if (ColumnHeadersSource is not null && ItemsInRows)
                {
                    element.DataContext = ColumnHeadersSource;
                    element.SetBinding(TextBlock.TextProperty, new Binding($"[{column}]") { StringFormat = ColumnHeadersFormatString });
                }

                Grid.SetColumn(element, column);
                ColumnGrid.Children.Add(element);
                Map.Columns[column] = element;
            }

            for (Int32 column = 0; column < columns; column++)
            {
                if (!CanResizeColumns)
                {
                    continue;
                }
                
                GridSplitter splitter = new GridSplitter
                {
                    ResizeDirection = GridResizeDirection.Columns,
                    Background = Brushes.Transparent,
                    Width = 5,
                    RenderTransform = new TranslateTransform(3, 0),
                    Focusable = false,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                
                splitter.MouseDoubleClick += ColumnSplitterDoubleClick;
                splitter.DragStarted += ColumnSplitterChangeStarted;
                splitter.DragDelta += ColumnSplitterChangeDelta;
                splitter.DragCompleted += ColumnSplitterChangeCompleted;
                
                Grid.SetColumn(splitter, column);
                ColumnGrid.Children.Add(splitter);
            }
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual void UpdateRows(Int32 rows)
        {
            if (SheetGrid is not { } grid)
            {
                return;
            }
            
            RowGrid.Children.AddIfNotNull(RowSelectionBackground);

            for (Int32 row = 0; row < rows; row++)
            {
                RowDefinition sheet = new RowDefinition { Height = RowHeight(row) };
                grid.RowDefinitions.Add(sheet);

                RowDefinition definition = new RowDefinition();
                definition.SetBinding(RowDefinition.HeightProperty, new TwoWayBinding(nameof(RowDefinition.Height), sheet));
                RowGrid.RowDefinitions.Add(definition);
            }

            for (Int32 row = 0; row < rows; row++)
            {
                Object header = GetRowHeader(row);

                Border border = new Border
                {
                    BorderBrush = HeaderBorderBrush,
                    BorderThickness = new Thickness(1, 0, 1, 1),
                    Margin = new Thickness(0, 0, 0, -1)
                };

                Grid.SetRow(border, row);
                RowGrid.Children.Add(border);

                FrameworkElement cell = header as FrameworkElement ?? new TextBlock
                {
                    Text = header.ToString() ?? "-",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Padding = new Thickness(4, 2, 4, 2)
                };

                if (ItemHeaderPath is not null && ItemsInRows && Operator is { } @operator)
                {
                    cell.DataContext = @operator.GetItem(new ExcelCell(-1, row));
                    cell.SetBinding(TextBlock.TextProperty, new Binding(ItemHeaderPath));
                }

                if (RowHeadersSource is not null && ItemsInRows)
                {
                    cell.DataContext = RowHeadersSource;
                    cell.SetBinding(TextBlock.TextProperty, new Binding($"[{row}]") { StringFormat = RowHeadersFormatString });
                }

                Grid.SetRow(cell, row);
                RowGrid.Children.Add(cell);
                Map.Rows[row] = cell;
            }

            AddInserterRow(rows);

            RowGrid.ContextMenu = RowsContextMenu;

            RowDefinition scroll = new RowDefinition();
            RowGrid.RowDefinitions.Add(scroll);

            scroll.SetBinding(RowDefinition.HeightProperty, new Binding(nameof(ScrollViewer.ComputedHorizontalScrollBarVisibility))
            {
                Source = SheetScrollViewer,
                Converter = HorizontalScrollBarVisibilityConverter,
                ConverterParameter = SystemParameters.HorizontalScrollBarHeight
            });
            
            if (!CanResizeRows)
            {
                return;
            }

            for (Int32 row = 0; row < rows; row++)
            {
                GridSplitter splitter = new GridSplitter
                {
                    ResizeDirection = GridResizeDirection.Rows,
                    Background = Brushes.Transparent,
                    Height = 5,
                    RenderTransform = new TranslateTransform(0, 3),
                    Focusable = false,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                
                splitter.MouseDoubleClick += RowSplitterDoubleClick;
                splitter.DragStarted += RowSplitterChangeStarted;
                splitter.DragDelta += RowSplitterChangeDelta;
                splitter.DragCompleted += RowSplitterChangeCompleted;
                
                Grid.SetRow(splitter, row);
                RowGrid.Children.Add(splitter);
            }
        }
        
        private void CurrentCellSourceUpdated(ExcelCell cell)
        {
            if (EditingCells?.ToArray() is not { } edit || !Enumerable.Contains(edit, cell) || !TryGet(cell, out Object? value))
            {
                return;
            }
            
            foreach (ExcelCell current in edit)
            {
                if (MultiChangeInChangedColumnOnly && current.Column != cell.Column)
                {
                    continue;
                }
                
                if (current.Equals(cell))
                {
                    continue;
                }
                
                TrySet(current, value);
            }
        }
        
        // ReSharper disable once CognitiveComplexity
        private void UpdateCells(Int32 columns, Int32 rows)
        {
            if (SheetGrid is not { } grid)
            {
                return;
            }
            
            grid.ContextMenu = SheetContextMenu;

            grid.Children.AddIfNotNull(SelectionBackground);
            grid.Children.AddIfNotNull(CurrentBackground);

            for (Int32 row = 1; row <= rows; row++)
            {
                Border border = new Border
                {
                    BorderBrush = GridLineBrush,
                    BorderThickness = new Thickness(0, 1, 0, 0)
                };

                if (row < rows && AlternatingRowsBackground is not null && (row & 1) == 1)
                {
                    border.Background = AlternatingRowsBackground;
                }

                Grid.SetColumn(border, 0);
                if (columns > 0)
                {
                    Grid.SetColumnSpan(border, columns);
                }

                Grid.SetRow(border, row);
                grid.Children.Add(border);
            }

            if (rows > 0)
            {
                for (Int32 column = 0; column < columns; column++)
                {
                    if (column == 0 && columns > 1)
                    {
                        continue;
                    }

                    Border border = new Border
                    {
                        BorderBrush = GridLineBrush,
                        BorderThickness = new Thickness(column > 0 ? 1 : 0, 0, column == columns - 1 ? 1 : 0, 0)
                    };

                    Grid.SetRow(border, 0);
                    Grid.SetRowSpan(border, rows);
                    Grid.SetColumn(border, column);
                    grid.Children.Add(border);
                }
            }

            CellInsertionIndex = grid.Children.Count;

            for (Int32 row = 0; row < rows; row++)
            {
                for (Int32 column = 0; column < columns; column++)
                {
                    InsertDisplayControl(new ExcelCell(column, row));
                }
            }
            
            grid.Children.AddIfNotNull(Selection);
            grid.Children.AddIfNotNull(AutoFillBox);
            grid.Children.AddIfNotNull(AutoFillSelection);
        }
        
        // ReSharper disable once CognitiveComplexity
        private void UpdateSelectionVisibility()
        {
            Boolean enabled = IsEnabled;
            if (CurrentBackground is not null)
            {
                CurrentBackground.Visibility = enabled && CurrentCell.Column < Columns && CurrentCell.Row < Rows ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (AutoFillBox is not null)
            {
                AutoFillBox.Visibility = enabled && IsAutoFill && CurrentCell.Column < Columns && CurrentCell.Row < Rows ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (SelectionBackground is not null)
            {
                SelectionBackground.Visibility = enabled && CurrentCell.Column < Columns && CurrentCell.Row < Rows ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (ColumnSelectionBackground is not null)
            {
                ColumnSelectionBackground.Visibility = enabled && CurrentCell.Column < Columns ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (RowSelectionBackground is not null)
            {
                RowSelectionBackground.Visibility = enabled && CurrentCell.Row < Rows ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (Selection is not null)
            {
                Selection.Visibility = enabled && CurrentCell.Column < Columns && CurrentCell.Row < Rows ? Visibility.Visible : Visibility.Hidden;
            }
            
            if (TopLeft is not null && RowSelectionBackground is not null)
            {
                Boolean all = Math.Abs(CurrentCell.Column - SelectionCell.Column) + 1 == Columns && Math.Abs(CurrentCell.Row - SelectionCell.Row) + 1 == Rows;
                TopLeft.Background = enabled && all ? RowSelectionBackground.Background : RowGrid.Background;
            }
        }
        
        protected void UpdateContent(ExcelCell cell)
        {
            if (GetElement(cell) is not { } element)
            {
                InsertDisplayControl(cell);
                return;
            }
            
            SheetGrid?.Children.Remove(element);
            CellInsertionIndex--;
            Map.Cells.Remove(cell.GetHashCode());
            InsertDisplayControl(cell);
        }
        
        protected void ClearContent()
        {
            ClearGrid(SheetGrid);
            ClearGrid(ColumnGrid);
            ClearGrid(RowGrid);
            
            CellInsertionIndex = 0;
            Map.Columns.Clear();
            Map.Cells.Clear();
        }
        
        protected void ClearGrid(Grid? grid)
        {
            if (grid is null)
            {
                return;
            }
            
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid.Children.Clear();
        }
    }
}