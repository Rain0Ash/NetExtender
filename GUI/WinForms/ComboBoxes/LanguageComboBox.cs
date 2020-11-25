// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localizations;

namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public class LanguageComboBox : FixedComboBox
    {
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

        protected virtual void UpdateText()
        {
            RefreshItems();
        }

        protected override void Dispose(Boolean disposing)
        {
            Localization.LanguageChanged -= OnUpdateText;
            base.Dispose(disposing);
        }
    }
}