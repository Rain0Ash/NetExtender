using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using NetExtender.Types.Collections;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public enum ExcelSelectionType
    {
        None,
        Row,
        Cell
    }
    
    public partial class ExcelGrid : Control, IExcelGrid, IWeakEventListener
    {
        protected Grid? SheetGrid { get; set; }
        protected Grid ColumnGrid { get; set; } = null!;
        protected Grid RowGrid { get; set; } = null!;
        
        public IExcelGridOperator? Operator { get; protected set; }
        
        public SuppressObservableCollection<ExcelPropertyDefinition> PropertyDefinitions { get; }
        
        public SuppressObservableCollection<ExcelPropertyDefinition> ColumnDefinitions
        {
            get
            {
                return PropertyDefinitions;
            }
        }
        
        IObservableCollection<ExcelPropertyDefinition> IExcelGrid.ColumnDefinitions
        {
            get
            {
                return ColumnDefinitions;
            }
        }
        
        public SuppressObservableCollection<ExcelPropertyDefinition> RowDefinitions
        {
            get
            {
                return PropertyDefinitions;
            }
        }
        
        IObservableCollection<ExcelPropertyDefinition> IExcelGrid.RowDefinitions
        {
            get
            {
                return RowDefinitions;
            }
        }
        
        public Int32 Columns
        {
            get
            {
                return SheetGrid is not null ? SheetGrid.ColumnDefinitions.Count : 0;
            }
        }
        
        public Int32 Rows
        {
            get
            {
                return SheetGrid is not null ? SheetGrid.RowDefinitions.Count - 1 : 0;
            }
        }
        
        public Boolean ItemsInColumns { get; protected set; }
        
        public Boolean ItemsInRows
        {
            get
            {
                return !ItemsInColumns;
            }
        }
        
        protected ExcelCell _autofillcell;
        public ExcelCell AutoFillCell
        {
            get
            {
                return _autofillcell;
            }
            set
            {
                _autofillcell = (ExcelCell) CoerceSelectionCell(this, value)!;
                SelectedCellsChanged();
            }
        }
        
        public IEnumerable<ExcelCell> SelectedCells
        {
            get
            {
                if (SelectionCollection is not null)
                {
                    foreach (ExcelCell cell in SelectionCollection.OrderBy(static cell => cell.Column).ThenBy(static cell => cell.Row))
                    {
                        yield return cell;
                    }
                    
                    yield break;
                }
                
                for (Int32 row = Math.Min(CurrentCell.Row, SelectionCell.Row); row <= Math.Max(CurrentCell.Row, SelectionCell.Row); row++)
                {
                    for (Int32 column = Math.Min(CurrentCell.Column, SelectionCell.Column); column <= Math.Max(CurrentCell.Column, SelectionCell.Column); column++)
                    {
                        yield return new ExcelCell(column, row);
                    }
                }
            }
        }
        
        protected ExcelCellRange? SelectionRange
        {
            get
            {
                return SelectionCollection is null ? new ExcelCellRange(CurrentCell, SelectionCell) : null;
            }
        }

        private readonly ObservableCollection<ExcelCell> _selection = new SuppressObservableCollection<ExcelCell>();
        private ObservableCollection<ExcelCell>? SelectionCollection
        {
            get
            {
                return _selection.Count > 0 ? _selection : null;
            }
        }

        public ICollectionView? View { get; protected set; }
        
        protected virtual Boolean CanInsertColumns
        {
            get
            {
                return !IsReadOnly && (Operator?.CanInsertColumns ?? false);
            }
        }
        
        protected virtual Boolean CanInsertRows
        {
            get
            {
                return !IsReadOnly && (Operator?.CanInsertRows ?? false);
            }
        }

        protected virtual Boolean CanDeleteColumns
        {
            get
            {
                return !IsReadOnly && (Operator?.CanDeleteColumns ?? false);
            }
        }
        
        protected virtual Boolean CanDeleteRows
        {
            get
            {
                return !IsReadOnly && (Operator?.CanDeleteRows ?? false);
            }
        }
        
        protected ExcelMap Map { get; } = new ExcelMap();
        protected ExcelSortDescriptor SortDescriptor { get; } = new ExcelSortDescriptor();
        protected ExcelAutoFiller AutoFiller { get; set; }
        
        protected ScrollViewer SheetScrollViewer { get; set; } = null!;
        protected ScrollViewer ColumnScrollViewer { get; set; } = null!;
        protected ScrollViewer RowScrollViewer { get; set; } = null!;
        protected Border? TopLeft { get; set; }
        protected Border? AutoFillBox { get; set; }
        protected Border? AutoFillSelection { get; set; }
        protected ToolTip AutoFillToolTip { get; set; } = null!;
        protected Border? Selection { get; set; }
        protected Border? SelectionBackground { get; set; }
        protected Border? CurrentBackground { get; set; }
        protected Border? ColumnSelectionBackground { get; set; }
        protected Border? RowSelectionBackground { get; set; }
        protected IList<ExcelCell>? EditingCells { get; set; }
        protected FrameworkElement? CurrentEditControl { get; set; }
        protected Int32 CellInsertionIndex { get; set; }
        protected Boolean EndKeyIsPressed { get; set; }
        protected IList? SynchronizationCollection { get; set; }
        protected INotifyCollectionChanged? NotifyCollection { get; set; }
        protected Boolean SuppressCollectionNotify { get; set; }
        
        public ExcelGrid()
        {
            PropertyDefinitions = new SuppressObservableCollection<ExcelPropertyDefinition>();
            PropertyDefinitions.CollectionChanged += PropertyDefinitionsChanged;
            AutoFiller = new ExcelAutoFiller(TryGet, TrySet);
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SheetScrollViewer = this.FindRequiredTemplate<ScrollViewer>(PART_SheetScrollViewer);
            SheetGrid = this.FindRequiredTemplate<Grid>(PART_SheetGrid);
            ColumnScrollViewer = this.FindRequiredTemplate<ScrollViewer>(PART_ColumnScrollViewer);
            ColumnGrid = this.FindRequiredTemplate<Grid>(PART_ColumnGrid);
            RowScrollViewer = this.FindRequiredTemplate<ScrollViewer>(PART_RowScrollViewer);
            RowGrid = this.FindRequiredTemplate<Grid>(PART_RowGrid);
            Selection = this.FindTemplate<Border>(PART_Selection);
            AutoFillBox = this.FindRequiredTemplate<Border>(PART_AutoFillBox);
            AutoFillSelection = this.FindTemplate<Border>(PART_AutoFillSelection);
            CurrentBackground = this.FindTemplate<Border>(PART_CurrentBackground);
            SelectionBackground = this.FindTemplate<Border>(PART_SelectionBackground);
            ColumnSelectionBackground = this.FindTemplate<Border>(PART_ColumnSelectionBackground);
            RowSelectionBackground = this.FindTemplate<Border>(PART_RowSelectionBackground);
            TopLeft = this.FindRequiredTemplate<Border>(PART_TopLeft);
            
            SheetScrollViewer.ScrollChanged += ScrollViewerScrollChanged;
            ColumnScrollViewer.ScrollChanged += ColumnScrollViewerScrollChanged;
            RowScrollViewer.ScrollChanged += RowScrollViewerScrollChanged;
            
            TopLeft.MouseDown += TopLeftMouseDown;
            AutoFillBox.MouseLeftButtonDown += AutoFillBoxMouseDown;
            ColumnGrid.MouseDown += ColumnGridMouseDown;
            ColumnGrid.MouseMove += ColumnGridMouseMove;
            ColumnGrid.MouseUp += ColumnGridMouseUp;
            RowGrid.MouseDown += RowGridMouseDown;
            RowGrid.MouseMove += RowGridMouseMove;
            RowGrid.MouseUp += RowGridMouseUp;
            SheetScrollViewer.SizeChanged += OnSheetSizeChanged;
            SheetGrid.MouseDown += SheetGridMouseDown;

            SheetScrollViewer.Loaded += OnSheetScrollViewerLoaded;

            AutoFillToolTip = new ToolTip
            {
                Placement = PlacementMode.Bottom,
                PlacementTarget = AutoFillSelection
            };

            Update();
            SelectedCellsChanged();
            Commands.Setup(this);
        }
        
        protected virtual IExcelGridOperator? CreateOperator()
        {
            if (ItemsSource is not { } source)
            {
                return null;
            }
            
            if (IsIList(source.GetType()))
            {
                return new ExcelMultiListOperator(this);
            }
            
            return WrapItems ? new ExcelWrapItemsOperator(this) : new ExcelListOperator(this);
        }
        
        public Boolean ReceiveWeakEvent(Type manager, Object? sender, EventArgs args)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            
            if (manager != typeof(CollectionChangedEventManager) || sender != NotifyCollection)
            {
                return false;
            }
            
            OnItemsCollectionChanged(args as NotifyCollectionChangedEventArgs);
            return true;
        }
        
        protected virtual ExcelPropertyDefinition? GetPropertyDefinition(ExcelCell cell)
        {
            Int32 index = ItemsInRows ? cell.Column : cell.Row;
            return index < PropertyDefinitions.Count ? PropertyDefinitions[index] : null;
        }
        
        ExcelPropertyDefinition? IExcelGrid.GetPropertyDefinition(ExcelCell cell)
        {
            return GetPropertyDefinition(cell);
        }
        
        private void PropertyDefinitionsChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            Update();
        }
        
        protected virtual void OnIsReadOnlyChanged(Boolean @readonly)
        {
            Update();
        }
        
        private void OnSheetSizeChanged(Object? sender, SizeChangedEventArgs args)
        {
            UpdateGridSize();
        }
        
        private void OnSheetScrollViewerLoaded(Object? sender, RoutedEventArgs args)
        {
            UpdateGridSize();
        }
        
        private void ScrollViewerScrollChanged(Object? sender, ScrollChangedEventArgs args)
        {
            ColumnScrollViewer.ScrollToHorizontalOffset(SheetScrollViewer.HorizontalOffset);
            RowScrollViewer.ScrollToVerticalOffset(SheetScrollViewer.VerticalOffset);
        }
        
        private void ColumnScrollViewerScrollChanged(Object? sender, ScrollChangedEventArgs args)
        {
            SheetScrollViewer.ScrollToHorizontalOffset(args.HorizontalOffset);
        }
        
        private void RowScrollViewerScrollChanged(Object? sender, ScrollChangedEventArgs args)
        {
            SheetScrollViewer.ScrollToVerticalOffset(args.VerticalOffset);
        }
        
        private void ItemsSourceChanged()
        {
            if (SynchronizationCollection is not null)
            {
                BindingOperations.DisableCollectionSynchronization(SynchronizationCollection);
                SynchronizationCollection = null;
            }
            
            if (NotifyCollection is not null)
            {
                CollectionChangedEventManager.RemoveListener(NotifyCollection, this);
                NotifyCollection = null;
            }
            
            if (ItemsSource is not null)
            {
                SynchronizationCollection = ItemsSource;
                BindingOperations.EnableCollectionSynchronization(SynchronizationCollection, this);
                CollectionViewSource source = new CollectionViewSource { Source = ItemsSource };
                View = source.View;
                UpdateCollectionView();
            }
            else
            {
                View = null;
            }
            
            Update();
            
            if (View is not { } view)
            {
                return;
            }
            
            CollectionChangedEventManager.AddListener(view, this);
            NotifyCollection = view;
        }
        
        private void OnItemsCollectionChanged(NotifyCollectionChangedEventArgs? args)
        {
            if (args is null)
            {
                return;
            }

            if (SuppressCollectionNotify)
            {
                return;
            }

            // TODO: update only changed rows/columns
            Dispatcher.Invoke(Update);
        }
        
        private void CurrentCellChanged()
        {
            SelectedCellsChanged();
        }
        
        private void SelectionCellChanged()
        {
            SelectedCellsChanged();
            ScrollIntoView(SelectionCell);
        }
        
        private void SelectedCellsChanged()
        {
            if (Selection is null)
            {
                return;
            }
            
            Int32 column = Math.Min(CurrentCell.Column, SelectionCell.Column);
            Int32 columnspan = Math.Abs(CurrentCell.Column - SelectionCell.Column) + 1;
            Int32 row = Math.Min(CurrentCell.Row, SelectionCell.Row);
            Int32 rowspan = Math.Abs(CurrentCell.Row - SelectionCell.Row) + 1;
            
            Border? border = Selection;
            if (border is not null)
            {
                Grid.SetColumn(border, column);
                Grid.SetColumnSpan(border, columnspan);
                Grid.SetRow(border, row);
                Grid.SetRowSpan(border, rowspan);
            }
            
            border = SelectionBackground;
            if (border is not null)
            {
                Grid.SetColumn(border, column);
                Grid.SetColumnSpan(border, columnspan);
                Grid.SetRow(border, row);
                Grid.SetRowSpan(border, rowspan);
            }
            
            border = ColumnSelectionBackground;
            if (border is not null)
            {
                Grid.SetColumn(border, column);
                Grid.SetColumnSpan(border, columnspan);
            }
            
            border = RowSelectionBackground;
            if (border is not null)
            {
                Grid.SetRow(border, row);
                Grid.SetRowSpan(border, rowspan);
            }
            
            border = CurrentBackground;
            if (border is not null)
            {
                Grid.SetColumn(border, CurrentCell.Column);
                Grid.SetRow(border, CurrentCell.Row);
            }
            
            UpdateSelectionVisibility();
            
            border = AutoFillBox;
            if (border is not null)
            {
                Grid.SetColumn(border, column + columnspan - 1);
                Grid.SetRow(border, row + rowspan - 1);
            }
            
            border = AutoFillSelection;
            if (border is not null)
            {
                Grid.SetColumn(border, Math.Min(CurrentCell.Column, AutoFillCell.Column));
                Grid.SetColumnSpan(border, Math.Abs(CurrentCell.Column - AutoFillCell.Column) + 1);
                Grid.SetRow(border, Math.Min(CurrentCell.Row, AutoFillCell.Row));
                Grid.SetRowSpan(border, Math.Abs(CurrentCell.Row - AutoFillCell.Row) + 1);
            }

            SelectedItems = EnumerateItems(SelectionRange).ToArray();
            ShowEditControl();
        }
        
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);
            
            if (args.Text.Length <= 0 || args.Text[0] < 32)
            {
                return;
            }
            
            if (CurrentEditControl is null)
            {
                ShowEditControl();
            }
            
            if (CurrentEditControl is not TextBox { IsEnabled: true } editor)
            {
                args.Handled = true;
                return;
            }
            
            ShowTextBoxEditControl();
            
            void Edit()
            {
                editor.Text = args.Text;
                editor.CaretIndex = editor.Text.Length;
            }
            
            Dispatcher.BeginInvoke(Edit, DispatcherPriority.Loaded);
            args.Handled = true;
        }
    }
}