using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using NetExtender.Types.Collections;

namespace NetExtender.UserInterface.WindowsPresentation.ExcelGrid
{
    public record ExcelSortDescriptor
    {
        public SortDescriptionCollection Descriptors { get; } = new SortDescriptionCollection();
        public ObservableCollection<FrameworkElement> Markers { get; } = new SuppressObservableCollection<FrameworkElement>();
    }
}