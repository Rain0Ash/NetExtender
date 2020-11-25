// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Linq;
using NetExtender.GUI.WinForms.ListViews.Items;
using NetExtender.Interfaces;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class ValidableListView<T> : GenericListView<T>, IMultiValidable<T>
    {
        public Color InvalidColor { get; set; } = Color.Coral;

        public event EmptyHandler ValidateItemChanged;
        
        private Func<T, Boolean> _validateItem;
        
        public Func<T, Boolean> ValidateItem
        {
            get
            {
                return _validateItem;
            }
            set
            {
                if (_validateItem == value)
                {
                    return;
                }
                
                _validateItem = value;
                
                ValidateItemChanged?.Invoke();
            }
        }
        
        public Boolean IsValid
        {
            get
            {
                return Items.All(IsValidItem);
            }
        }

        public ValidableListView()
        {
            ValidateItemChanged += Refresh;
        }

        public virtual Boolean IsValidItem(T item)
        {
            return ValidateItem?.Invoke(item) ?? true;
        }

        public virtual Boolean IsValidItem(GenericListViewItem<T> lvitem)
        {
            return IsValidItem(lvitem.Item);
        }

        public Boolean IsValidIndex(Int32 index)
        {
            return IsValidItem(Items[index]);
        }
    }
}