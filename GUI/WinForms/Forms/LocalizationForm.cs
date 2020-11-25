// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;
using NetExtender.Localizations;

namespace NetExtender.GUI.WinForms.Forms
{
    public abstract class LocalizationForm : FlashForm, ILocalizable
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            //override
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Localization.LanguageChanged -= OnUpdateText;
            }
            
            base.Dispose(disposing);
        }
    }
}