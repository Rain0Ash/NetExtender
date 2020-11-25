// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NetExtender.Utils.Types;
using NetExtender.Cultures;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WPF.ComboBoxes
{
    public class LocalizationComboBox : LocalizableComboBox
    {
        public LocalizationComboBox()
        {
            Started += OnStarted;
        }

        private void OnStarted(Object sender, EventArgs e)
        {
            Localizations.Localization.LanguageChanged += SetLanguage;
            IsVisibleChanged += OnVisibleChanged;
            SelectionChanged += OnSelectedIndexChanged;

            ItemsSource = Localizations.Localization.Cultures;
            DisplayMemberPath = "CustomName";
            SelectedValuePath = "LCID";

            SetLanguage();
        }

        public void SetLanguage()
        {
            SetLanguage(Localizations.Localization.CurrentCulture.LCID);
        }

        public void SetLanguage(Int32 lcid)
        {
            while (true)
            {
                Int32 selectedIndex = Localizations.Localization.GetLanguageOrderID(lcid);
                if (selectedIndex.InRange(0, Items.Count, MathPositionType.Left))
                {
                    SelectedIndex = selectedIndex;
                    return;
                }

                if (Localizations.Localization.DefaultCulture.LCID == lcid)
                {
                    throw new IndexOutOfRangeException($"Culture {Localizations.Localization.CultureByLCID.TryGetValue(lcid)?.CustomName ?? "Not exist"} out of range");
                }
                
                if (Localizations.Localization.BasicCulture.LCID == lcid)
                {
                    lcid = Localizations.Localization.DefaultCulture.LCID;
                }
            }
        }

        protected void OnVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                SetLanguage();
            }
        }

        public Int32 GetLanguageLCID()
        {
            return Localizations.Localization.CultureByLCID.FirstOr(
                x => String.Equals(x.Value.CustomName, SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase),
                new KeyValuePair<Int32, Culture>(Localizations.Localization.DefaultCulture.LCID, Localizations.Localization.DefaultCulture)).Key;
        }

        protected void OnSelectedIndexChanged(Object sender, SelectionChangedEventArgs e)
        {
            Localizations.Localization.UpdateLocalization(GetLanguageLCID());
        }
    }
}