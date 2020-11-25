// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Localizations;

namespace NetExtender.GUI.WinForms.CheckBoxes
{
    public class MultiTextLocalizationCheckBox : LocalizationCheckBox
    {
        public LocaleStrings CheckedText { get; set; }
        public LocaleStrings IndeterminateText { get; set; }
        public LocaleStrings UncheckedText { get; set; }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            LocalizationText = CheckState switch
            {
                CheckState.Unchecked => UncheckedText,
                CheckState.Checked => CheckedText,
                CheckState.Indeterminate => IndeterminateText,
                _ => LocalizationText
            } ?? LocalizationText;
        }
    }
}