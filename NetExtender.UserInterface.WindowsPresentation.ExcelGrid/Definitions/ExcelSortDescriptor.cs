// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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