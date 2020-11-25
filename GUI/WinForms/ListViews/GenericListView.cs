// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.GUI.WinForms.ListViews.Items;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class GenericListView<T> : MoveListView
    {
        public new ListViewGenericItemList<T> Items { get; }
        public new SelectedListViewItemList<T> SelectedItems { get; }

        public GenericListView()
        {
            Items = new ListViewGenericItemList<T>(base.Items);

            SelectedItems = new SelectedListViewItemList<T>(base.SelectedItems);
        }
    }
}