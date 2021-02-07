// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NetExtender.Utils.Types;
using NetExtender.Localizations;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public sealed class LocalizationComboBox : ImagedComboBox
    {
        private class CultureDropDownItem
        {
            public CultureInfo Info { get; }
            public DropDownItem Item { get; }

            public CultureDropDownItem([NotNull] CultureInfo info)
            {
                Info = info ?? throw new ArgumentNullException(nameof(info));
                Item = new DropDownItem(Info.GetNativeLanguageName(), Info.GetImage());
            }
        }
        
        private event TypeHandler<CultureInfo> LanguageChanged;

        public LocalizationComboBox()
        {
            BindingContext = new BindingContext();
            DataSource = GetItemsForDataSource();
            Localization.LanguageChanged += SetLanguage;
            SelectedIndexChanged += OnSelectedIndexChanged;
            LanguageChanged += Localization.UpdateLocalization;
            SetLanguage();
        }
        
        //TODO: autoupdate
        private static CultureDropDownItem[] GetItemsForDataSource()
        {
            return Localization.Supported.Select(culture => new CultureDropDownItem(culture)).ToArray();
        }

        public void SetLanguage()
        {
            SetLanguage(Localization.Culture);
        }

        public void SetLanguage([NotNull] CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            while (true)
            {
                Int32 order = Localization.Comparer.GetOrder(info);
                if (order.InRange(0, Items.Count, MathPositionType.Left))
                {
                    SelectedIndex = order;
                    return;
                }

                if (Equals(Localization.Default, info))
                {
                    throw new IndexOutOfRangeException($"Culture {info.GetNativeLanguageName()} out of range");
                }
                
                if (Equals(Localization.Basic, info))
                {
                    info = Localization.Default;
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

        private void OnSelectedIndexChanged(Object sender, EventArgs e)
        {
            LanguageChanged?.Invoke(Localization.Supported.ElementAt(SelectedIndex));
        }

        protected override void Dispose(Boolean disposing)
        {
            LanguageChanged = null;
            base.Dispose(disposing);
        }
    }
}