// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Interfaces;
using NetExtender.Localizations;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.GUI.WinForms.CheckBoxes
{
    public class LocalizationCheckBox : CheckBox, ILocalizable
    {
        private IString _text;
        public IString LocalizationText
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;

                OnUpdateText();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Localization.LanguageChanged += OnUpdateText;
            OnUpdateText();
        }

        private void OnUpdateText()
        {
            SuspendLayout();
            UpdateText();
            ResumeLayout(false);
            PerformLayout();
        }

        public virtual void UpdateText()
        {
            if (LocalizationText is not null)
            {
                Text = LocalizationText.ToString();
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            Localization.LanguageChanged -= OnUpdateText;
            base.Dispose(disposing);
        }
    }
}