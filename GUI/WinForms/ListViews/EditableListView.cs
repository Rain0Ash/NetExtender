// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Types;
using NetExtender.Events.Args;
using NetExtender.GUI.Common;
using NetExtender.GUI.WinForms.Forms;
using NetExtender.GUI.WinForms.ListViews.Items;
using NetExtender.GUI.WinForms.ToolStrips;
using NetExtender.GUI.WinForms.ToolStrips.Items;
using NetExtender.Native;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Types.Numerics;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class EditableListView<T> : EventListView<T>
    {
        public Boolean AllowContextMenu { get; set; } = true;
        public MouseButtons ContextMenuButton { get; } = MouseButtons.Right;

        private ActionType _actionType;
        public ActionType ActionType 
        {
            get
            {
                return _actionType;
            }
            set
            {
                if (_actionType == value)
                {
                    return;
                }
                
                _actionType = value;
                
                UpdateMenuButtons();
            }
        }
        
        private readonly FixedContextMenuStrip _menu = new FixedContextMenuStrip();

        public Boolean DoubleClickForChange { get; set; } = true;
        
        public Boolean DoubleEmptyClickForAdd { get; set; } = true;

        #region ToText
        
        public TextBoxForm ItemForm { get; set; } = new TextBoxForm();

        public String ItemFormTitle
        {
            get
            {
                return ItemForm.Text;
            }
            set
            {
                ItemForm.Text = value;
            }
        }
        
        public String ItemFormApplyButtonText
        {
            get
            {
                return ItemForm.ApplyButton.Text;
            }
            set
            {
                ItemForm.ApplyButton.Text = value;
            }
        }
        
        public String ItemFormCancelButtonText
        {
            get
            {
                return ItemForm.DenyButton.Text;
            }
            set
            {
                ItemForm.DenyButton.Text = value;
            }
        }

        protected String GetText(ActionType action)
        {
            return Buttons.TryGetValue(action)?.Text;
        }
        
        protected void SetText(String value, ActionType action)
        {
            ToolStripItem item = Buttons.TryGetValue(action);

            if (item is null)
            {
                return;
            }

            if (value is not null)
            {
                item.Text = value;
                return;
            }

            ActionType &= ~action;
        }
        
        public String SelectText
        {
            get
            {
                return GetText(ActionType.Select);
            }
            set
            {
                SetText(value, ActionType.Select);
            }
        }
        
        public String ViewText
        {
            get
            {
                return GetText(ActionType.View);
            }
            set
            {
                SetText(value, ActionType.View);
            }
        }
        
        public String CopyText
        {
            get
            {
                return GetText(ActionType.Copy);
            }
            set
            {
                SetText(value, ActionType.Copy);
            }
        }
        
        public String PasteText
        {
            get
            {
                return GetText(ActionType.Paste);
            }
            set
            {
                SetText(value, ActionType.Paste);
            }
        }
        
        public String CutText
        {
            get
            {
                return GetText(ActionType.Cut);
            }
            set
            {
                SetText(value, ActionType.Cut);
            }
        }
        
        public String AddText
        {
            get
            {
                return GetText(ActionType.Add);
            }
            set
            {
                SetText(value, ActionType.Add);
            }
        }
        
        public String RemoveText
        {
            get
            {
                return GetText(ActionType.Remove);
            }
            set
            {
                SetText(value, ActionType.Remove);
            }
        }

        public String ChangeText
        {
            get
            {
                return GetText(ActionType.Change);
            }
            set
            {
                SetText(value, ActionType.Change);
            }
        }

        #endregion

        protected readonly IIndexMap<ActionType, FixedToolStripMenuItem> Buttons = new IndexMap<ActionType, FixedToolStripMenuItem>
        {
            [ActionType.Select] = new FixedToolStripMenuItem("Select"),
            [ActionType.View] = new FixedToolStripMenuItem("View"){AllowOnMinItems = 1, AllowOnMaxItems = 1},
            [ActionType.Copy] = new FixedToolStripMenuItem("Copy"){AllowOnMinItems = 1},
            [ActionType.Paste] = new FixedToolStripMenuItem("Paste"),
            [ActionType.Cut] = new FixedToolStripMenuItem("Cut"){AllowOnMinItems = 1},
            [ActionType.Add] = new FixedToolStripMenuItem("Add"),
            [ActionType.Remove] = new FixedToolStripMenuItem("Remove"){AllowOnMinItems = 1},
            [ActionType.Change] = new FixedToolStripMenuItem("Change"){AllowOnMinItems = 1, AllowOnMaxItems = 1},
            [ActionType.ChangeStatus] = new FixedToolStripMenuItem("ChangeStatus"){AllowOnMinItems = 1},
            [ActionType.Replace] = new FixedToolStripMenuItem("Replace"),
            [ActionType.Reset] = new FixedToolStripMenuItem("Reset"),
            [ActionType.Additional1] = new FixedToolStripMenuItem("Additional1"),
            [ActionType.Additional2] = new FixedToolStripMenuItem("Additional2"),
            [ActionType.Additional3] = new FixedToolStripMenuItem("Additional3"),
        };
        
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.RBUTTONDOWN:
                case WM.RBUTTONUP:
                case WM.RBUTTONDBLCLK:
                    Point pointMousePos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                    ListViewHitTestInfo hit = HitTest(pointMousePos);

                    if (hit.Item is null)
                    {
                        SelectedIndices.Clear();
                        break;
                    }
                    
                    hit.Item.Selected = true;
                    hit.Item.Focused = true;
                    break;
            }
            
            base.WndProc(ref m);
        }

        private TypeHandler<TypeHandledEventArgs<ActionType>> _action;
        public event TypeHandler<TypeHandledEventArgs<ActionType>> Action
        {
            add
            {
                _action = value + _action;
            }
            remove
            {
                _action -= value;
            }
        }
        
        public EditableListView()
        {
            ActionType = ActionType.Basic;
            ValidateItemChanged += () => ItemForm.Validate = IsValidItemText;

            KeyDown += OnKeyDown;
            MouseDown += OpenContextMenu;
            _menu.ItemClicked += OnMenuActionClicked;
            ItemDoubleClick += DoubleClickChange;
            EmptyDoubleClick += DoubleEmptyClickAdd;
            
            Action += OnAction;
        }

        private void OnAction(TypeHandledEventArgs<ActionType> e)
        {
            if (e.Handled)
            {
                return;
            }

            Action action = e.Value switch
            {
                ActionType.Select => OnSelectAction,
                ActionType.View => OnViewAction,
                ActionType.Copy => OnCopyAction,
                ActionType.Paste => OnPasteAction,
                ActionType.Cut => OnCutAction,
                ActionType.Add => OnAddAction,
                ActionType.Remove => OnRemoveAction,
                ActionType.Edit => OnEditAction,
                ActionType.Change => OnChangeAction,
                ActionType.Reset => OnResetAction,
                _ => null
            };

            action?.Invoke();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (!ActionType.HasFlag(ActionType.Select))
            {
                SelectedIndices.Clear();
                return;
            }
            
            base.OnSelectedIndexChanged(e);
        }

        protected void UpdateMenuButtons(Int32 count = -1)
        {
            _menu.Items.Clear();

            Buttons.ForEachWhere(button => CheckMenuKey(button.Key, button.Value, count),
                button => _menu.Items.Add(button.Value));
        }

        protected virtual Boolean CheckMenuKey(ActionType key, FixedToolStripMenuItem item, Int32 count)
        {
            if (!ActionType.HasFlag(key))
            {
                return false;
            }

            if (key == ActionType.Select && !MultiSelect)
            {
                return false;
            }

            if (count >= 0 && !item.SelectedCountInRange(count))
            {
                return false;
            }

            return true;
        }
        
        protected virtual void OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }
            
            e.Handled = true;
            
            switch (e.KeyCode)
            {
                case Keys.Enter when SelectedItems.Count > 0:
                    ActionInvoke(ActionType.Change);
                    break;
                case Keys.A when ActionType.HasFlag(ActionType.Select) && e.Control:
                    ActionInvoke(ActionType.Select);
                    break;
                case Keys.E when ActionType.HasFlag(ActionType.View) && e.Control:
                    ActionInvoke(ActionType.View);
                    break;
                case Keys.C when ActionType.HasFlag(ActionType.Copy) && e.Control:
                    ActionInvoke(ActionType.Copy);
                    break;
                case Keys.V when ActionType.HasFlag(ActionType.Paste) && e.Control:
                    ActionInvoke(ActionType.Paste);
                    break;
                case Keys.X when ActionType.HasFlag(ActionType.Cut) && e.Control:
                    ActionInvoke(ActionType.Cut);
                    break;
                case Keys.Up when ActionType.HasFlag(ActionType.Swap) && e.Shift:
                    OnSwapAction(PointOffset.Up);
                    break;
                case Keys.Down when ActionType.HasFlag(ActionType.Swap) && e.Shift:
                    OnSwapAction(PointOffset.Down);
                    break;
                case Keys.Delete when ActionType.HasFlag(ActionType.Remove):
                    ActionInvoke(ActionType.Remove);
                    return;
                default:
                    e.Handled = false;
                    break;
            }
        }

        protected virtual void ActionInvoke(ActionType action)
        {
            _action?.Invoke(new TypeHandledEventArgs<ActionType>(action));
        }

        public virtual void OnSelectAction()
        {
            if (!MultiSelect)
            {
                return;
            }

            foreach (GenericListViewItem<T> lvitem in Items)
            {
                lvitem.Selected = true;
            }
        }
        
        public virtual void OnViewAction()
        {
        }
        
        public virtual void OnCopyAction()
        {
            try
            {
                if (SelectedItems.Count > 0)
                {
                    Clipboard.SetText(SelectedItems.Select(lvitem => lvitem.Text).Join("; "));
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public virtual void OnPasteAction()
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    OnAddAction(Clipboard.GetText());
                }
                else if (Clipboard.ContainsImage())
                {
                    OnAddAction(Clipboard.GetImage());
                }
                else if (Clipboard.ContainsAudio())
                {
                    OnAddAction(Clipboard.GetAudioStream());
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    OnAddAction(Clipboard.GetFileDropList());
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        public virtual void OnCutAction()
        {
            OnCopyAction();
            OnRemoveAction();
        }

        public virtual void OnSwapAction(PointOffset offset)
        {
            if (SelectedIndices.Count != 1)
            {
                return;
            }

            Int32 index = SelectedIndices[0];

            switch (offset)
            {
                case PointOffset.Up:
                    if (!IndexInItems(index - 1))
                    {
                        return;
                    }
                    
                    SwapItems(index, index - 1);

                    break;
                case PointOffset.Down:
                    if (!IndexInItems(index + 1))
                    {
                        return;
                    }
                    
                    SwapItems(index, index + 1);
                    
                    break;
                default:
                    break;
            }
        }
        
        public virtual void OnAddAction()
        {
            ItemForm.TextBox.Clear();

            if (ItemForm.ShowDialog() == DialogResult.OK)
            {
                OnAddAction(ItemForm.TextBox.Text);
            }
        }

        public virtual void OnAddAction(Object item)
        {
            switch (item)
            {
                case String str:
                    OnAddAction(str);
                    break;
                case GenericListViewItem<T> lvitem:
                    OnAddAction(lvitem);
                    break;
                case Stream stream:
                    OnAddAction(stream);
                    break;
                case Image image:
                    OnAddAction(image);
                    break;
                case StringCollection collection:
                    OnAddAction(collection);
                    break;
                default:
                    OnAddAction(item.ToString());
                    break;
            }
        }
        
        public virtual void OnAddAction(GenericListViewItem<T> lvitem)
        {
            TryInsert(SelectedIndices.OfType<Int32>().FirstOr(SelectedIndices.Count), lvitem);
        }
        
        public virtual void OnAddAction(Stream stream)
        {
            
        }
        
        public virtual void OnAddAction(Image image)
        {
            
        }

        public virtual void OnAddAction(String text)
        {
            while (true)
            {
                if (TryConvertToItem(text, out T item) && IsValidItem(item))
                {
                    TryInsert(SelectedIndices.OfType<Int32>().FirstOr(SelectedIndices.Count), item);
                }
                else
                {
                    ItemForm.TextBox.Clear();
                    ItemForm.TextBox.Text = text;

                    if (ItemForm.ShowDialog() == DialogResult.OK)
                    {
                        text = ItemForm.TextBox.Text;
                        continue;
                    }
                }

                break;
            }
        }

        public virtual void OnAddAction(StringCollection collection)
        {
            
        }
        
        public virtual void OnRemoveAction()
        {
            try
            {
                foreach (GenericListViewItem<T> lvitem in SelectedItems)
                {
                    Remove(lvitem);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public virtual void OnEditAction()
        {
        }

        public virtual void OnChangeAction()
        {
            if (SelectedItems.Count <= 0)
            {
                return;
            }

            OnChangeAction(SelectedItems[0]);
        }

        public virtual void OnChangeAction(GenericListViewItem<T> lvitem)
        {
            ItemForm.TextBox.Clear();
            ItemForm.TextBox.Text = lvitem.Text;
            if (ItemForm.ShowDialog() == DialogResult.OK && IsValidItemText(ItemForm.TextBox.Text))
            {
                lvitem.Text = ItemForm.TextBox.Text;
            }
        }
        
        public virtual void OnResetAction()
        {
        }
        
        private void DoubleClickChange(GenericListViewItem<T> lvitem, MouseEventArgs e)
        {
            if (DoubleClickForChange && ActionType.HasFlag(ActionType.Change))
            {
                OnChangeAction(lvitem);
            }
        }

        private void DoubleEmptyClickAdd(MouseEventArgs e)
        {
            if (DoubleEmptyClickForAdd && ActionType.HasFlag(ActionType.Add))
            {
                ActionInvoke(ActionType.Add);
            }
        }
        
        protected virtual void OpenContextMenu(Object sender, MouseEventArgs e)
        {
            if (!AllowContextMenu || e.Button != ContextMenuButton)
            {
                return;
            }

            UpdateMenuButtons(SelectedItems.Count);
            _menu.Show(Cursor.Position);
        }

        protected virtual void OnMenuActionClicked(Object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is not FixedToolStripMenuItem item)
            {
                return;
            }
            
            ActionType action = Buttons.TryGetKey(item);

            if (action != ActionType.None)
            {
                ActionInvoke(action);
            }
        }

        protected virtual Boolean TryConvertToItem(String str, out T item)
        {
            return str.TryConvert(out item);
        }

        protected virtual Boolean IsValidItemText(String str)
        {
            return TryConvertToItem(str, out T item) && IsValidItem(item);
        }
    }
}