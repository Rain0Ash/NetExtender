// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.Utils.GUI.Winforms.ListView;
using NetExtender.Utils.Types;
using NetExtender.Events.Args;
using NetExtender.GUI.Common;
using NetExtender.GUI.WinForms.Forms;
using NetExtender.GUI.WinForms.ListViews.Items;
using NetExtender.GUI.WinForms.ToolStrips.Items;
using NetExtender.Types.Drawing;
using NetExtender.Utils.IO;
using NetExtender.Watchers;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class PathListView : PathListView<FSWatcher>
    {
    }
    
    public class PathListView<T> : EditableListView<T> where T : FSWatcher
    {
        public event EmptyHandler PathTypeChanged;

        private PathType _pathType = PathType.All;

        public PathType PathType
        {
            get
            {
                return _pathType;
            }
            set
            {
                if (_pathType == value)
                {
                    return;
                }

                _pathType = value;
                PathTypeChanged?.Invoke();
                Refresh();
            }
        }
        
        public event EmptyHandler PathStatusChanged;

        private PathStatus _pathStatus = PathStatus.All;

        public PathStatus PathStatus
        {
            get
            {
                return _pathStatus;
            }
            set
            {
                if (_pathStatus == value)
                {
                    return;
                }

                _pathStatus = value;
                PathStatusChanged?.Invoke();
                Refresh();
            }
        }
        
        public String RecursiveText
        {
            get
            {
                return GetText(Common.ActionType.ChangeStatus);
            }
            set
            {
                SetText(value, Common.ActionType.ChangeStatus);
            }
        }

        public PathListView()
        {
            OverlapAllowed = false;
            ValidateItem = IsValidItem;
            RecursiveText = "Recursive";
            ActionType |= Common.ActionType.ChangeStatus;
            ItemForm = new PathTextBoxForm {TextBox = {Validate = IsValidItemText}};

            Action += OnAction;
        }

        private void OnAction(TypeHandledEventArgs<ActionType> e)
        {
            if (e.Handled || e.Value != Common.ActionType.ChangeStatus)
            {
                return;
            }

            OnRecursiveAction();
            e.Handled = true;
        }
        
        public override Boolean TryInsert(Int32 index, GenericListViewItem<T> lvitem)
        {
            if (lvitem.Item is not FSWatcher watcher)
            {
                lvitem.Item = (T) new FSWatcher(lvitem.Text, PathType, PathStatus);
            }

            return base.TryInsert(index, lvitem);
        }

        public override Boolean IsValidItem(T item)
        {
            return item.IsValid();
        }
        
        public override Boolean IsValidItem(GenericListViewItem<T> lvitem)
        {
            return lvitem.Item is FSWatcher ? IsValidItem(lvitem.Item) : PathUtils.IsValidPath(lvitem.Text, PathType, PathStatus);
        }

        protected override Boolean TryConvertToItem(String str, out T item)
        {
            try
            {
                T watcher = (T) new FSWatcher(str, PathType, PathStatus);

                item = watcher;
                return IsValidItem(watcher);
            }
            catch (ArgumentException)
            {
                item = default;
                return false;
            }
        }

        protected override Boolean IsValidItemText(String str)
        {
            return PathUtils.IsValidPath(str, PathType, PathStatus);
        }

        protected override void OnKeyDown(Object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R when ActionType.HasFlag(Common.ActionType.ChangeStatus) && e.Control:
                    e.Handled = true;
                    ActionInvoke(Common.ActionType.ChangeStatus);
                    break;
                default:
                    base.OnKeyDown(sender, e);
                    break;
            }
        }

        protected override void OnMenuActionClicked(Object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is not FixedToolStripMenuItem item)
            {
                return;
            }
            
            ActionType action = Buttons.TryGetKey(item);

            switch (action)
            {
                case Common.ActionType.ChangeStatus:
                    ActionInvoke(Common.ActionType.ChangeStatus);
                    break;
                default:
                    base.OnMenuActionClicked(sender, e);
                    break;
            }
        }

        public override void OnViewAction()
        {
            if (SelectedItems.Count <= 0)
            {
                return;
            }
            
            String path = PathUtils.GetFullPath(SelectedItems[0].Text);

            if (!PathUtils.IsValidPath(path, PathType.Folder, PathStatus.Exist))
            {
                return;
            }

            try
            {
                Process.Start("explorer.exe", $"\"{$@"{path}"}\"");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void OnChangeAction(GenericListViewItem<T> lvitem)
        {
            base.OnChangeAction(lvitem);

            if (lvitem.Item is FSWatcher watcher)
            {
                watcher.Path = lvitem.Text;
            }
        }
        
        public virtual void OnRecursiveAction()
        {
            foreach (GenericListViewItem<T> lvitem in SelectedItems)
            {
                if (lvitem.Item is FSWatcher watcher)
                {
                    watcher.IsRecursive = !watcher.IsRecursive;
                }
            }
        }

        protected override String GetItemImageKey(GenericListViewItem<T> lvitem)
        {
            if (lvitem.Item is FSWatcher watcher)
            {
                return lvitem.ImageList.GetOrSetImageKey(watcher.Icon);
            }

            return base.GetItemImageKey(lvitem);
        }
        
        protected override Color GetItemForeColor(GenericListViewItem<T> lvitem, DrawingData data)
        {
            if (lvitem.Item is FSWatcher watcher && watcher.IsRecursive)
            {
                return Color.Red;
            }
            
            return base.GetItemForeColor(lvitem, data);
        }
    }
}