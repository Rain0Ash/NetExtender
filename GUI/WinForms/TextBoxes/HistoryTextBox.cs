// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class HistoryTextBox : FixedTextBox
    {
        private Boolean _ignoreChange = true;
        private List<String> _storageUndo;
        private List<String> _storageRedo;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _storageUndo = new List<String>();
            _storageRedo = new List<String>();
            _ignoreChange = false;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (_ignoreChange)
            {
                return;
            }

            ClearUndo();
            if (_storageUndo.Count > 2048)
            {
                _storageUndo.RemoveAt(0);
            }

            if (_storageRedo.Count > 2048)
            {
                _storageRedo.RemoveAt(0);
            }

            _storageUndo.Add(Text);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z when (ModifierKeys & Keys.Control) == Keys.Control:
                    ClearUndo();
                    _ignoreChange = true;
                    Undo();
                    _ignoreChange = false;
                    break;
                case Keys.Y when (ModifierKeys & Keys.Control) == Keys.Control:
                    ClearUndo();
                    _ignoreChange = true;
                    Redo();
                    _ignoreChange = false;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        public void Redo()
        {
            if (_storageRedo.Count <= 0)
            {
                return;
            }

            _ignoreChange = true;
            Text = _storageRedo[_storageRedo.Count - 1];
            _storageUndo.Add(Text);
            _storageRedo.RemoveAt(_storageRedo.Count - 1);
            _ignoreChange = false;
        }

        public new void Undo()
        {
            if (_storageUndo.Count <= 0)
            {
                return;
            }

            _ignoreChange = true;
            _storageRedo.Add(Text);
            Text = _storageUndo[_storageUndo.Count - 1];
            _storageUndo.RemoveAt(_storageUndo.Count - 1);
            _ignoreChange = false;
        }
    }
}