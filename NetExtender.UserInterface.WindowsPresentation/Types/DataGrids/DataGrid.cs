// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Data;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class DataGrid : ViewDataGrid
    {
        public DataGrid()
        {
            Columns.CollectionChanged += Bind;
        }

        protected virtual void Bind(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems is null)
            {
                return;
            }

            foreach (Object? item in args.NewItems)
            {
                if (item is not DataGridTextColumn { View: null } column)
                {
                    continue;
                }

                Binding? binding = column.GetBinding(DataGridTextColumn.HeaderProperty);

                if (binding is null)
                {
                    continue;
                }

                TextBlock block = new CenterTextBlock();
                block.SetBinding(TextBlock.TextProperty, binding);
                column.View = block;
            }
        }
    }
}