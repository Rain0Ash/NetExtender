using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        public virtual void Copy()
        {
            Copy("\t");
        }
        
        public virtual void Copy(String? separator)
        {
            ExcelCellRange range = SelectionRange;
            
            if (!TryGet(range, out Object?[,]? values))
            {
                return;
            }
            
            DataObject data = new DataObject();
            if (TryGet(range, values, out String?[,]? strings))
            {
                String text = ConvertToCsv(strings, separator, true);
                data.SetText(text);
            }

            if (values.Cast<Object>().IsSerializable())
            {
                try
                {
                    data.SetData(typeof(ExcelGrid), values);
                }
                catch (Exception)
                {
                }
            }
            
            Clipboard.SetDataObject(data);
        }
        
        public virtual void Cut()
        {
            if (IsReadOnly)
            {
                Copy();
                return;
            }
            
            Copy();
            Clear();
        }
        
        public virtual void Paste()
        {
            if (IsReadOnly)
            {
                return;
            }
            
            Object[,]? values = GetClipboardData();
            
            if (values is null || !TrySet(SelectionRange, values, out ExcelCellRange range))
            {
                return;
            }
            
            SelectionCell = range.BottomRight;
            CurrentCell = range.TopLeft;
            
            ScrollIntoView(CurrentCell);
        }
        
        protected virtual Object[,]? GetClipboardData()
        {
            Object[,]? values = null;
            
            if (Clipboard.GetDataObject() is { } data)
            {
                values = data.GetData(typeof(ExcelGrid)) as Object[,];
            }
            
            if (values is not null || !Clipboard.ContainsText())
            {
                return values;
            }
            
            String text = Clipboard.GetText().Trim();
            return TextToArray(text);
        }
        
        private static Char[] Trim { get; } = " \r\n\t".ToCharArray();
        
        protected virtual Object[,]? TextToArray(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Int32 rows = 0;
            Int32 columns = 0;
            String[] lines = value.Split('\n');
            foreach (String line in lines)
            {
                rows++;
                String[] fields = line.Split('\t');
                if (fields.Length > columns)
                {
                    columns = fields.Length;
                }
            }
            
            if (rows <= 0 || columns <= 0)
            {
                return null;
            }
            
            Int32 row = 0;
            Object[,] result = new Object[rows, columns];
            foreach (String line in lines)
            {
                String[] fields = line.Split('\t');

                Int32 column = 0;
                foreach (String field in fields)
                {
                    result[row, column] = field.Trim(Trim);
                    column++;
                }

                row++;
            }

            return result;
        }
        
        protected internal class Commands
        {
            public static ICommand SortAscending { get; } = new RoutedCommand(nameof(SortAscending), typeof(ExcelGrid));
            public static ICommand SortDescending { get; } = new RoutedCommand(nameof(SortDescending), typeof(ExcelGrid));
            public static ICommand ClearSort { get; } = new RoutedCommand(nameof(ClearSort), typeof(ExcelGrid));
            public static ICommand InsertColumns { get; } = new RoutedCommand(nameof(InsertColumns), typeof(ExcelGrid));
            public static ICommand InsertRows { get; } = new RoutedCommand(nameof(InsertRows), typeof(ExcelGrid));
            public static ICommand DeleteColumns { get; } = new RoutedCommand(nameof(DeleteColumns), typeof(ExcelGrid));
            public static ICommand DeleteRows { get; } = new RoutedCommand(nameof(DeleteRows), typeof(ExcelGrid));

            private static IEnumerable<CommandBinding> Create()
            {
                yield return new CommandBinding(SortAscending, ExecuteSortAscending, CanExecuteSortAscending);
                yield return new CommandBinding(SortDescending, ExecuteSortDescending, CanExecuteSortDescending);
                yield return new CommandBinding(ClearSort, ExecuteClearSort, CanExecuteClearSort);
                yield return new CommandBinding(InsertColumns, ExecuteInsertColumns, CanExecuteInsertColumns);
                yield return new CommandBinding(InsertRows, ExecuteInsertRows, CanExecuteInsertRows);
                yield return new CommandBinding(DeleteColumns, ExecuteDeleteColumns, CanExecuteDeleteColumns);
                yield return new CommandBinding(DeleteRows, ExecuteDeleteRows, CanExecuteDeleteRows);

                yield return new CommandBinding(ApplicationCommands.Copy, ExecuteCopy, CanExecuteCopy);
                yield return new CommandBinding(ApplicationCommands.Cut, ExecuteCut, CanExecuteCut);
                yield return new CommandBinding(ApplicationCommands.Paste, ExecutePaste, CanExecutePaste);
                yield return new CommandBinding(ApplicationCommands.Delete, ExecuteClear, CanExecuteClear);
            }

            private static void CanExecuteSortAscending(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanSort: true };
            }
            
            private static void ExecuteSortAscending(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Sort(ListSortDirection.Ascending);
            }
            
            private static void CanExecuteSortDescending(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanSort: true };
            }
            
            private static void ExecuteSortDescending(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Sort(ListSortDirection.Descending);
            }
            
            private static void CanExecuteClearSort(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanSort: true };
            }
            
            private static void ExecuteClearSort(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.ClearSort();
            }
            
            private static void CanExecuteInsertColumns(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanInsertColumns: true };
            }
            
            private static void ExecuteInsertColumns(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.InsertColumns();
            }
            
            private static void CanExecuteInsertRows(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanInsertRows: true };
            }
            
            private static void ExecuteInsertRows(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.InsertRows();
            }
            
            private static void CanExecuteDeleteColumns(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanDeleteColumns: true };
            }
            
            private static void ExecuteDeleteColumns(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.DeleteColumns();
            }
            
            private static void CanExecuteDeleteRows(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanDeleteRows: true };
            }
            
            private static void ExecuteDeleteRows(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.DeleteRows();
            }
            
            private static void CanExecuteCopy(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid;
            }
            
            private static void ExecuteCopy(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Copy();
            }
            
            private static void CanExecuteCut(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid;
            }
            
            private static void ExecuteCut(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Cut();
            }
            
            private static void CanExecutePaste(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid;
            }
            
            private static void ExecutePaste(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Paste();
            }
            
            private static void CanExecuteClear(Object? sender, CanExecuteRoutedEventArgs args)
            {
                args.CanExecute = sender is ExcelGrid { CanClear: true };
            }
            
            private static void ExecuteClear(Object? sender, ExecutedRoutedEventArgs args)
            {
                (sender as ExcelGrid)?.Clear();
            }
            
            public static void Setup(ExcelGrid excel)
            {
                if (excel is null)
                {
                    throw new ArgumentNullException(nameof(excel));
                }

                excel.CommandBindings.AddRange(Create());
            }
        }
    }
}