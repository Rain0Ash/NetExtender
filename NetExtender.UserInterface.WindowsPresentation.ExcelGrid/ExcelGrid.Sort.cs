using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public partial class ExcelGrid
    {
        protected Boolean CanSort
        {
            get
            {
                Int32 index = ItemsInRows ? CurrentCell.Column : CurrentCell.Row;
                return View is not null && Operator is { } @operator && @operator.CanSort(index);
            }
        }
        
        private void Sort(ListSortDirection direction)
        {
            Sort(direction, false);
        }
        
        private void Sort(ListSortDirection direction, Boolean append)
        {
            Int32 index = ItemsInRows ? CurrentCell.Column : CurrentCell.Row;
            
            if (PropertyDefinitions[index].PropertyName is not { } property)
            {
                return;
            }
            
            try
            {
                if (!append)
                {
                    SortDescriptor.Descriptors.Clear();
                }
                
                SortDescriptor.Descriptors.Add(new SortDescription(property, direction));
                UpdateCollectionView();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        
        protected void ToggleSort()
        {
            ToggleSort(false);
        }
        
        protected virtual void ToggleSort(Boolean append)
        {
            if (GetPropertyDefinition(CurrentCell) is not { CanSort: true, PropertyName: { } property })
            {
                return;
            }
            
            SortDescription? current = SortDescriptor.Descriptors.FirstOrNullable(description => description.PropertyName == property);
            
            if (!append)
            {
                SortDescriptor.Descriptors.Clear();
            }
            
            try
            {
                if (!current.HasValue)
                {
                    SortDescriptor.Descriptors.Add(new SortDescription(property, ListSortDirection.Ascending));
                }
                else if (current.Value.Direction == ListSortDirection.Ascending)
                {
                    SortDescriptor.Descriptors.Add(new SortDescription(property, ListSortDirection.Descending));
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            
            UpdateCollectionView();
        }
        
        protected void UpdateSortDescriptionMarkers()
        {
            foreach (FrameworkElement element in SortDescriptor.Markers)
            {
                ColumnGrid.Children.Remove(element);
            }
            
            SortDescriptor.Markers.Clear();
            
            if (!ItemsInRows)
            {
                return;
            }
            
            foreach (SortDescription description in SortDescriptor.Descriptors)
            {
                Int32 index = FindPropertyNameIndex(description);
                
                if (index < 0)
                {
                    continue;
                }
                
                TextBlock block = new TextBlock
                {
                    Text = description.Direction.ToInverseSymbol(),
                    Foreground = Brushes.DarkGray,
                    Margin = new Thickness(0, 0, 4, 0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                Grid.SetColumn(block, index);
                ColumnGrid.Children.Add(block);
                SortDescriptor.Markers.Add(block);
            }
        }
        
        protected void ClearSort()
        {
            try
            {
                SortDescriptor.Descriptors.Clear();
                UpdateCollectionView();
            }
            catch (Exception)
            {
            }
        }
    }
}