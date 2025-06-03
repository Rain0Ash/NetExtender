// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using NetExtender.UserInterface.WindowsPresentation.ExcelGrid.Interfaces;
using NetExtender.Utilities.UserInterface;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    [ContentProperty(nameof(ItemsSource))]
    [DefaultProperty(nameof(ItemsSource))]
    [TemplatePart(Name = PART_Grid, Type = typeof(Grid))]
    [TemplatePart(Name = PART_SheetScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = PART_SheetGrid, Type = typeof(Grid))]
    [TemplatePart(Name = PART_ColumnScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = PART_ColumnGrid, Type = typeof(Grid))]
    [TemplatePart(Name = PART_RowScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = PART_RowGrid, Type = typeof(Grid))]
    [TemplatePart(Name = PART_Selection, Type = typeof(Border))]
    [TemplatePart(Name = PART_AutoFillBox, Type = typeof(Border))]
    [TemplatePart(Name = PART_AutoFillSelection, Type = typeof(Border))]
    [TemplatePart(Name = PART_CurrentBackground, Type = typeof(Border))]
    [TemplatePart(Name = PART_SelectionBackground, Type = typeof(Border))]
    [TemplatePart(Name = PART_ColumnSelectionBackground, Type = typeof(Border))]
    [TemplatePart(Name = PART_RowSelectionBackground, Type = typeof(Border))]
    [TemplatePart(Name = PART_TopLeft, Type = typeof(Border))]
    public partial class ExcelGrid
    {
        private const String PART_Grid = nameof(PART_Grid);
        private const String PART_SheetScrollViewer = nameof(PART_SheetScrollViewer);
        private const String PART_SheetGrid = nameof(PART_SheetGrid);
        private const String PART_ColumnScrollViewer = nameof(PART_ColumnScrollViewer);
        private const String PART_ColumnGrid = nameof(PART_ColumnGrid);
        private const String PART_RowScrollViewer = nameof(PART_RowScrollViewer);
        private const String PART_RowGrid = nameof(PART_RowGrid);
        private const String PART_Selection = nameof(PART_Selection);
        private const String PART_AutoFillBox = nameof(PART_AutoFillBox);
        private const String PART_AutoFillSelection = nameof(PART_AutoFillSelection);
        private const String PART_CurrentBackground = nameof(PART_CurrentBackground);
        private const String PART_SelectionBackground = nameof(PART_SelectionBackground);
        private const String PART_ColumnSelectionBackground = nameof(PART_ColumnSelectionBackground);
        private const String PART_RowSelectionBackground = nameof(PART_RowSelectionBackground);
        private const String PART_TopLeft = nameof(PART_TopLeft);

        public static readonly DependencyProperty AutoGenerateColumnsProperty = DependencyProperty.Register(nameof(AutoGenerateColumns), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty AutoInsertProperty = DependencyProperty.Register(nameof(AutoInsert), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty IsAutoFillProperty = DependencyProperty.Register(nameof(IsAutoFill), typeof(Boolean), typeof(ExcelGrid), new PropertyMetadata(true));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(false, OnIsReadOnlyChanged));
        public static readonly DependencyProperty WrapItemsProperty = DependencyProperty.Register(nameof(WrapItems), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(false));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IList), typeof(ExcelGrid), new UIPropertyMetadata(null, ItemsSourceChanged));
        public static readonly DependencyProperty DefaultHorizontalAlignmentProperty = DependencyProperty.Register(nameof(DefaultHorizontalAlignment), typeof(HorizontalAlignment), typeof(ExcelGrid), new UIPropertyMetadata(HorizontalAlignment.Center));
        public static readonly DependencyProperty ColumnHeadersSourceProperty = DependencyProperty.Register(nameof(ColumnHeadersSource), typeof(IList), typeof(ExcelGrid), new UIPropertyMetadata(null, Reload));
        public static readonly DependencyProperty ColumnHeadersFormatStringProperty = DependencyProperty.Register(nameof(ColumnHeadersFormatString), typeof(String), typeof(ExcelGrid), new UIPropertyMetadata(null, Reload));
        public static readonly DependencyProperty RowHeadersSourceProperty = DependencyProperty.Register(nameof(RowHeadersSource), typeof(IList), typeof(ExcelGrid), new UIPropertyMetadata(null, Reload));
        public static readonly DependencyProperty RowHeadersFormatStringProperty = DependencyProperty.Register(nameof(RowHeadersFormatString), typeof(String), typeof(ExcelGrid), new UIPropertyMetadata(null, Reload));
        public static readonly DependencyProperty CanResizeColumnsProperty = DependencyProperty.Register(nameof(CanResizeColumns), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CanResizeRowsProperty = DependencyProperty.Register(nameof(CanResizeRows), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CanInsertProperty = DependencyProperty.Register(nameof(CanInsert), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CanDeleteProperty = DependencyProperty.Register(nameof(CanDelete), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CanClearProperty = DependencyProperty.Register(nameof(CanClear), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty CustomSortProperty = DependencyProperty.Register(nameof(CustomSort), typeof(IComparer), typeof(ExcelGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty InputDirectionProperty = DependencyProperty.Register(nameof(InputDirection), typeof(InputDirection), typeof(ExcelGrid), new UIPropertyMetadata(InputDirection.Vertical));
        public static readonly DependencyProperty CurrentCellProperty = DependencyProperty.Register(nameof(CurrentCell), typeof(ExcelCell), typeof(ExcelGrid), new FrameworkPropertyMetadata(new ExcelCell(0, 0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CurrentCellChanged, CoerceCurrentCell));
        public static readonly DependencyProperty SelectionCellProperty = DependencyProperty.Register(nameof(SelectionCell), typeof(ExcelCell), typeof(ExcelGrid), new UIPropertyMetadata(new ExcelCell(0, 0), SelectionCellChanged, CoerceSelectionCell));
        public static readonly DependencyProperty SelectionTypeProperty = DependencyProperty.Register(nameof(SelectionType), typeof(ExcelSelectionType), typeof(ExcelGrid), new PropertyMetadata(ExcelSelectionType.None));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof(SelectedItems), typeof(IEnumerable), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ControlFactoryProperty = DependencyProperty.Register(nameof(ControlFactory), typeof(IExcelGridControlFactory), typeof(ExcelGrid), new UIPropertyMetadata(new ExcelGridControlFactory()));
        public static readonly DependencyProperty CreateColumnHeaderProperty = DependencyProperty.Register(nameof(CreateColumnHeader), typeof(Func<Int32, Object>), typeof(ExcelGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty CreateItemProperty = DependencyProperty.Register(nameof(CreateItem), typeof(Func<Object>), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty AddItemHeaderProperty = DependencyProperty.Register(nameof(AddItemHeader), typeof(String), typeof(ExcelGrid), new UIPropertyMetadata("+"));
        public static readonly DependencyProperty ItemHeaderPathProperty = DependencyProperty.Register(nameof(ItemHeaderPath), typeof(String), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ColumnHeaderHeightProperty = DependencyProperty.Register(nameof(ColumnHeaderHeight), typeof(GridLength), typeof(ExcelGrid), new UIPropertyMetadata(new GridLength(20)));
        public static readonly DependencyProperty RowHeaderWidthProperty = DependencyProperty.Register(nameof(RowHeaderWidth), typeof(GridLength), typeof(ExcelGrid), new UIPropertyMetadata(new GridLength(40)));
        public static readonly DependencyProperty DefaultColumnWidthProperty = DependencyProperty.Register(nameof(DefaultColumnWidth), typeof(GridLength), typeof(ExcelGrid), new UIPropertyMetadata(new GridLength(1, GridUnitType.Star)));
        public static readonly DependencyProperty DefaultRowHeightProperty = DependencyProperty.Register(nameof(DefaultRowHeight), typeof(GridLength), typeof(ExcelGrid), new UIPropertyMetadata(new GridLength(20)));
        public static readonly DependencyProperty SheetContextMenuProperty = DependencyProperty.Register(nameof(SheetContextMenu), typeof(ContextMenu), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ColumnsContextMenuProperty = DependencyProperty.Register(nameof(ColumnsContextMenu), typeof(ContextMenu), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty RowsContextMenuProperty = DependencyProperty.Register(nameof(RowsContextMenu), typeof(ContextMenu), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty MultiChangeInChangedColumnOnlyProperty = DependencyProperty.Register(nameof(MultiChangeInChangedColumnOnly), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(false));
        public static readonly DependencyProperty GridLineBrushProperty = DependencyProperty.Register(nameof(GridLineBrush), typeof(Brush), typeof(ExcelGrid), new UIPropertyMetadata(new SolidColorBrush(Color.FromRgb(218, 220, 221))));
        public static readonly DependencyProperty HeaderBorderBrushProperty = DependencyProperty.Register(nameof(HeaderBorderBrush), typeof(Brush), typeof(ExcelGrid), new UIPropertyMetadata(new SolidColorBrush(Color.FromRgb(177, 181, 186))));
        public static readonly DependencyProperty AlternatingRowsBackgroundProperty = DependencyProperty.Register(nameof(AlternatingRowsBackground), typeof(Brush), typeof(ExcelGrid), new UIPropertyMetadata(null));
        public static readonly DependencyProperty EasyInsertByMouseProperty = DependencyProperty.Register(nameof(EasyInsertByMouse), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty EasyInsertByKeyboardProperty = DependencyProperty.Register(nameof(EasyInsertByKeyboard), typeof(Boolean), typeof(ExcelGrid), new UIPropertyMetadata(true));
        public static readonly DependencyProperty IsMoveAfterEnterProperty = DependencyProperty.Register(nameof(IsMoveAfterEnter), typeof(Boolean), typeof(ExcelGrid), new PropertyMetadata(true));
        
        private static VisibilityToValueConverter HorizontalScrollBarVisibilityConverter { get; } = new VisibilityToValueConverter { Visible = SystemParameters.HorizontalScrollBarHeight, Hidden = default(Double), Collapsed = default(Double) };
        private static VisibilityToValueConverter VerticalScrollBarVisibilityConverter { get; } = new VisibilityToValueConverter { Visible = SystemParameters.VerticalScrollBarWidth, Hidden = default(Double), Collapsed = default(Double) };
        
        static ExcelGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExcelGrid), new FrameworkPropertyMetadata(typeof(ExcelGrid)));
            IsEnabledProperty.OverrideMetadata(typeof(ExcelGrid), new FrameworkPropertyMetadata(HandleIsEnabledChanged));
        }
        
        public Boolean AutoGenerateColumns
        {
            get
            {
                return (Boolean) GetValue(AutoGenerateColumnsProperty);
            }
            set
            {
                SetValue(AutoGenerateColumnsProperty, value);
            }
        }
        
        public Boolean AutoInsert
        {
            get
            {
                return (Boolean) GetValue(AutoInsertProperty);
            }
            set
            {
                SetValue(AutoInsertProperty, value);
            }
        }
        
        public Boolean IsAutoFill
        {
            get
            {
                return (Boolean) GetValue(IsAutoFillProperty);
            }
            set
            {
                SetValue(IsAutoFillProperty, value);
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return (Boolean) GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }
        
        public Boolean WrapItems
        {
            get
            {
                return (Boolean) GetValue(WrapItemsProperty);
            }
            set
            {
                SetValue(WrapItemsProperty, value);
            }
        }
        
        public IList? ItemsSource
        {
            get
            {
                return (IList) GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        
        public HorizontalAlignment DefaultHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment) GetValue(DefaultHorizontalAlignmentProperty);
            }
            set
            {
                SetValue(DefaultHorizontalAlignmentProperty, value);
            }
        }
        
        public IList? ColumnHeadersSource
        {
            get
            {
                return (IList?) GetValue(ColumnHeadersSourceProperty);
            }
            set
            {
                SetValue(ColumnHeadersSourceProperty, value);
            }
        }
        
        public String ColumnHeadersFormatString
        {
            get
            {
                return (String) GetValue(ColumnHeadersFormatStringProperty);
            }
            set
            {
                SetValue(ColumnHeadersFormatStringProperty, value);
            }
        }
        
        public IList? RowHeadersSource
        {
            get
            {
                return (IList?) GetValue(RowHeadersSourceProperty);
            }
            set
            {
                SetValue(RowHeadersSourceProperty, value);
            }
        }
        
        public String RowHeadersFormatString
        {
            get
            {
                return (String) GetValue(RowHeadersFormatStringProperty);
            }
            set
            {
                SetValue(RowHeadersFormatStringProperty, value);
            }
        }
        
        public Boolean CanResizeColumns
        {
            get
            {
                return (Boolean) GetValue(CanResizeColumnsProperty);
            }
            set
            {
                SetValue(CanResizeColumnsProperty, value);
            }
        }
        
        public Boolean CanResizeRows
        {
            get
            {
                return (Boolean) GetValue(CanResizeRowsProperty);
            }
            set
            {
                SetValue(CanResizeRowsProperty, value);
            }
        }
        
        public Boolean CanInsert
        {
            get
            {
                return (Boolean) GetValue(CanInsertProperty);
            }
            set
            {
                SetValue(CanInsertProperty, value);
            }
        }
        
        public Boolean CanDelete
        {
            get
            {
                return (Boolean) GetValue(CanDeleteProperty);
            }
            set
            {
                SetValue(CanDeleteProperty, value);
            }
        }
        
        public Boolean CanClear
        {
            get
            {
                return (Boolean) GetValue(CanClearProperty);
            }
            set
            {
                SetValue(CanClearProperty, value);
            }
        }
        
        public IComparer? CustomSort
        {
            get
            {
                return (IComparer?) GetValue(CustomSortProperty);
            }
            set
            {
                SetValue(CustomSortProperty, value);
            }
        }
        
        public InputDirection InputDirection
        {
            get
            {
                return (InputDirection) GetValue(InputDirectionProperty);
            }
            set
            {
                SetValue(InputDirectionProperty, value);
            }
        }
        
        public ExcelCell CurrentCell
        {
            get
            {
                return (ExcelCell) GetValue(CurrentCellProperty);
            }
            set
            {
                SetValue(CurrentCellProperty, value);
            }
        }
        
        public ExcelCell SelectionCell
        {
            get
            {
                return (ExcelCell) GetValue(SelectionCellProperty);
            }
            set
            {
                SetValue(SelectionCellProperty, value);
                SelectionCollection?.Clear();
            }
        }
        
        public ExcelSelectionType SelectionType
        {
            get
            {
                return (ExcelSelectionType) GetValue(SelectionTypeProperty);
            }
            set
            {
                SetValue(SelectionTypeProperty, value);
            }
        }
        
        public IEnumerable SelectedItems
        {
            get
            {
                return (IEnumerable) GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }
        
        public IExcelGridControlFactory ControlFactory
        {
            get
            {
                return (IExcelGridControlFactory) GetValue(ControlFactoryProperty);
            }
            set
            {
                SetValue(ControlFactoryProperty, value);
            }
        }
        
        public Func<Int32, Object> CreateColumnHeader
        {
            get
            {
                return (Func<Int32, Object>) GetValue(CreateColumnHeaderProperty);
            }
            set
            {
                SetValue(CreateColumnHeaderProperty, value);
            }
        }
        
        public Func<Object>? CreateItem
        {
            get
            {
                return (Func<Object>?) GetValue(CreateItemProperty);
            }
            set
            {
                SetValue(CreateItemProperty, value);
            }
        }
        
        public String? AddItemHeader
        {
            get
            {
                return (String?) GetValue(AddItemHeaderProperty);
            }
            set
            {
                SetValue(AddItemHeaderProperty, value);
            }
        }
        
        public String ItemHeaderPath
        {
            get
            {
                return (String) GetValue(ItemHeaderPathProperty);
            }
            set
            {
                SetValue(ItemHeaderPathProperty, value);
            }
        }
        
        public GridLength ColumnHeaderHeight
        {
            get
            {
                return (GridLength) GetValue(ColumnHeaderHeightProperty);
            }
            set
            {
                SetValue(ColumnHeaderHeightProperty, value);
            }
        }
        
        public GridLength RowHeaderWidth
        {
            get
            {
                return (GridLength) GetValue(RowHeaderWidthProperty);
            }
            set
            {
                SetValue(RowHeaderWidthProperty, value);
            }
        }
        
        public GridLength DefaultColumnWidth
        {
            get
            {
                return (GridLength) GetValue(DefaultColumnWidthProperty);
            }
            set
            {
                SetValue(DefaultColumnWidthProperty, value);
            }
        }
        
        public GridLength DefaultRowHeight
        {
            get
            {
                return (GridLength) GetValue(DefaultRowHeightProperty);
            }
            set
            {
                SetValue(DefaultRowHeightProperty, value);
            }
        }
        
        public ContextMenu? SheetContextMenu
        {
            get
            {
                return (ContextMenu?) GetValue(SheetContextMenuProperty);
            }
            set
            {
                SetValue(SheetContextMenuProperty, value);
            }
        }
        
        public ContextMenu? ColumnsContextMenu
        {
            get
            {
                return (ContextMenu?) GetValue(ColumnsContextMenuProperty);
            }
            set
            {
                SetValue(ColumnsContextMenuProperty, value);
            }
        }
        
        public ContextMenu? RowsContextMenu
        {
            get
            {
                return (ContextMenu?) GetValue(RowsContextMenuProperty);
            }
            set
            {
                SetValue(RowsContextMenuProperty, value);
            }
        }
        
        public Boolean MultiChangeInChangedColumnOnly
        {
            get
            {
                return (Boolean) GetValue(MultiChangeInChangedColumnOnlyProperty);
            }
            set
            {
                SetValue(MultiChangeInChangedColumnOnlyProperty, value);
            }
        }
        
        public Brush GridLineBrush
        {
            get
            {
                return (Brush) GetValue(GridLineBrushProperty);
            }
            set
            {
                SetValue(GridLineBrushProperty, value);
            }
        }
        
        public Brush HeaderBorderBrush
        {
            get
            {
                return (Brush) GetValue(HeaderBorderBrushProperty);
            }
            set
            {
                SetValue(HeaderBorderBrushProperty, value);
            }
        }
        
        public Brush AlternatingRowsBackground
        {
            get
            {
                return (Brush) GetValue(AlternatingRowsBackgroundProperty);
            }
            set
            {
                SetValue(AlternatingRowsBackgroundProperty, value);
            }
        }
        
        public Boolean EasyInsertByMouse
        {
            get
            {
                return (Boolean) GetValue(EasyInsertByMouseProperty);
            }
            set
            {
                SetValue(EasyInsertByMouseProperty, value);
            }
        }
        
        public Boolean EasyInsertByKeyboard
        {
            get
            {
                return (Boolean) GetValue(EasyInsertByKeyboardProperty);
            }
            set
            {
                SetValue(EasyInsertByKeyboardProperty, value);
            }
        }
        
        public Boolean IsMoveAfterEnter
        {
            get
            {
                return (Boolean) GetValue(IsMoveAfterEnterProperty);
            }
            set
            {
                SetValue(IsMoveAfterEnterProperty, value);
            }
        }
        
        protected static void HandleIsEnabledChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel)
            {
                return;
            }
            
            excel.UpdateSelectionVisibility();
        }
        
        protected static void OnIsReadOnlyChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel || args.NewValue is not Boolean @readonly)
            {
                return;
            }
            
            excel.OnIsReadOnlyChanged(@readonly);
        }
        
        protected static void CurrentCellChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel)
            {
                return;
            }
            
            excel.CurrentCellChanged();
        }
        
        private static Object? CoerceCurrentCell(DependencyObject? sender, Object? value)
        {
            if (sender is not ExcelGrid excel || value is not ExcelCell cell)
            {
                return null;
            }
            
            Int32 row = cell.Row;
            Int32 column = cell.Column;
            
            if (excel.AutoInsert)
            {
                column = Clamp(column, 0, excel.Columns - 1 + (excel.CanInsertColumns ? 1 : 0));
                row = Clamp(row, 0, excel.Rows - 1 + (excel.CanInsertRows ? 1 : 0));
            }
            else
            {
                column = Clamp(column, 0, excel.Columns - 1);
                row = Clamp(row, 0, excel.Rows - 1);
            }
            
            return new ExcelCell(column, row);
        }
        
        protected static void SelectionCellChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel)
            {
                return;
            }
            
            excel.SelectionCellChanged();
        }
        
        private static Object? CoerceSelectionCell(DependencyObject? sender, Object? value)
        {
            if (sender is not ExcelGrid excel || value is not ExcelCell cell)
            {
                return null;
            }
            
            Int32 column = Clamp(cell.Column, 0, excel.Columns - 1);
            Int32 row = Clamp(cell.Row, 0, excel.Rows - 1);
            return new ExcelCell(column, row);
        }

        protected static void ItemsSourceChanged(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel)
            {
                return;
            }
            
            excel.ItemsSourceChanged();
        }
        
        private static Int32 Clamp(Int32 value, Int32 min, Int32 max)
        {
            Int32 result = value;
            if (result > max)
            {
                result = max;
            }
            
            if (result < min)
            {
                result = min;
            }
            
            return result;
        }
        
        protected static void Reload(DependencyObject? sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is not ExcelGrid excel)
            {
                return;
            }
            
            excel.Update();
        }
    }
}