// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localizations;

namespace NetExtender.GUI.WPF.ComboBoxes
{
    public class LocalizableComboBox : ImagedComboBox
    {
        public LocalizableComboBox()
        {
            Loaded += AddLocalization;
            Unloaded += RemoveLocalization;
        }

        private void AddLocalization(Object sender, EventArgs e)
        {
            Localization.LanguageChanged += OnUpdateText;
            OnUpdateText();
        }

        private void RemoveLocalization(Object sender, EventArgs e)
        {
            Localization.LanguageChanged -= OnUpdateText;
        }
        
        private void OnUpdateText()
        {
            UpdateText();
        }

        public virtual void UpdateText()
        {
            //override
        }
    }
}