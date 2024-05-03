using System;
using System.Windows.Input;
using NetExtender.Types.Events;

namespace NetExtender.UserInterface.WindowsPresentation.Types.DataGrids
{
    public delegate void ItemSelectedEventHandler(Object sender, ItemSelectedEventArgs args);

    public class ItemSelectedEventArgs : HandledEventArgs<Object?>
    {
        public ItemSelectedEventArgs(Object? value)
            : base(value)
        {
        }

        public ItemSelectedEventArgs(Object? value, Boolean handled)
            : base(value, handled)
        {
        }
    }
    
    public class FixedDataGrid : System.Windows.Controls.DataGrid
    {
        public event ItemSelectedEventHandler? ItemSelected;

        private Object? LastItem { get; set; }
        
        public FixedDataGrid()
        {
            PreviewKeyDown += OnPreviewKeyDown;
            PreviewMouseLeftButtonDown += SelectLastItem;
            PreviewMouseDoubleClick += MouseDoubleClick;
        }

        private void OnPreviewKeyDown(Object? sender, KeyEventArgs args)
        {
            if (args.Handled)
            {
                return;
            }

            if (args.Key != Key.Enter)
            {
                return;
            }

            Object? item = SelectedItem = CurrentItem;
            args.Handled = true;
            ItemSelected?.Invoke(this, new ItemSelectedEventArgs(item));
        }

        private void SelectLastItem(Object? sender, MouseButtonEventArgs args)
        {
            if (args.Handled)
            {
                return;
            }

            LastItem = SelectedItem;
        }

        private new void MouseDoubleClick(Object? sender, MouseButtonEventArgs args)
        {
            args.Handled = true;
            Object? item = LastItem;
            
            if (item is null)
            {
                item = CurrentItem = LastItem = SelectedItem;
                ItemSelected?.Invoke(this, new ItemSelectedEventArgs(item));
                return;
            }
            
            if (!ReferenceEquals(item, SelectedItem) && !Equals(item, SelectedItem))
            {
                LastItem = SelectedItem;
                return;
            }

            item = CurrentItem = SelectedItem;
            ItemSelected?.Invoke(this, new ItemSelectedEventArgs(item));
        }
    }
}