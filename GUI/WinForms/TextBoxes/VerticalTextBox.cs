// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class VerticalTextBox : HidenTextBox
    {
        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        public new event EmptyHandler TextAlignChanged;

        public new ContentAlignment TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                if (_textAlign == value)
                {
                    return;
                }

                _textAlign = value;
                TextAlignChanged?.Invoke();
            }
        }

        public VerticalTextBox()
        {
            TextAlignChanged += OnTextAlignChanged;
        }

        protected virtual void OnTextAlignChanged()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(Boolean disposing)
        {
            TextAlignChanged = null;
            base.Dispose(disposing);
        }
    }
}