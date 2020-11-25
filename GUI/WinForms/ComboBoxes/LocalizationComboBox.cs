// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Types;
using NetExtender.Cultures;
using NetExtender.Localizations;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public sealed class LocalizationComboBox : ImagedComboBox
    {
        private event TypeHandler<Int32> LanguageChanged;

        public LocalizationComboBox()
        {
            BindingContext = new BindingContext();
            DataSource = GetItemsForDataSource();
            Localization.LanguageChanged += SetLanguage;
            LanguageChanged += Localization.UpdateLocalization;
            SetLanguage();
        }

        protected override void UpdateText()
        {
            //pass
        }

        private static DropDownItem[] GetItemsForDataSource()
        {
            return Localization.Cultures
                .Select(culture => new DropDownItem(culture.CustomName) {Image = culture.Image}).ToArray();
        }

        public void SetLanguage()
        {
            SetLanguage(Localization.CurrentCulture.LCID);
        }

        public void SetLanguage(Int32 lcid)
        {
            while (true)
            {
                Int32 selectedIndex = Localization.GetLanguageOrderID(lcid);
                if (selectedIndex.InRange(0, Items.Count, MathPositionType.Left))
                {
                    SelectedIndex = selectedIndex;
                    return;
                }

                if (Localization.DefaultCulture.LCID == lcid)
                {
                    throw new IndexOutOfRangeException($"Culture {(Localization.CultureByLCID.TryGetValue(lcid, new Culture(lcid){CustomName = "NotExist"}).CustomName)} out of range");
                }
                
                if (Localization.BasicCulture.LCID == lcid)
                {
                    lcid = Localization.DefaultCulture.LCID;
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !Disposing)
            {
                SetLanguage();
            }
        }

        public Int32 GetLanguageLCID()
        {
            return Localization.CultureByLCID.FirstOr(
                x => String.Equals(x.Value.CustomName, Text, StringComparison.CurrentCultureIgnoreCase),
                new KeyValuePair<Int32, Culture>(Localization.DefaultCulture.LCID, Localization.DefaultCulture)).Key;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            LanguageChanged?.Invoke(GetLanguageLCID());
        }

        protected override void Dispose(Boolean disposing)
        {
            LanguageChanged = null;
            base.Dispose(disposing);
        }
    }
}