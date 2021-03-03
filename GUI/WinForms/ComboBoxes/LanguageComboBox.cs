// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using NetExtender.Exceptions;
using NetExtender.Utils.Types;
using NetExtender.Localizations;

namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public sealed class LanguageComboBox : ImagedComboBox
    {
        private sealed class DropItem : DropDownItem
        {
            public static DropItem Create([NotNull] CultureInfo info)
            {
                return new DropItem(info ?? throw new ArgumentNullException(nameof(info)));
            }
            
            public CultureInfo Info { get; }

            public DropItem([NotNull] CultureInfo info)
                : base(info?.GetNativeLanguageName() ?? throw new ArgumentNullException(nameof(info)), info.GetImage())
            {
                Info = info;
            }
        }

        public LanguageComboBox()
        {
            HandleCreated += OnHandleCreated;
        }

        private void OnHandleCreated(Object? sender, EventArgs e)
        {
            BindingContext = new BindingContext();
            UpdateSource();
            Localization.SupportedLanguagesChanged += UpdateSource;
            Localization.Changed += SetLanguage;
            SelectedIndexChanged += OnSelectedIndexChanged;
            VisibleChanged += OnVisibleChanged;
        }

        private void OnSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (SelectedItem is not DropItem item)
            {
                return;
            }

            SetLanguage(item.Info);
        }

        private void UpdateSource()
        {
            DataSource = GetItemsForDataSource();
            SetLanguage();
        }

        private static DropItem[] GetItemsForDataSource()
        {
            return Localization.Supported.Select(DropItem.Create).ToArray();
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

            if (!info.IsCultureEquals(Localization.Culture))
            {
                Localization.Update(info);
            }

            if (SelectedItem is DropItem drop && info.IsCultureEquals(drop.Info))
            {
                return;
            }

            foreach (DropItem item in (IEnumerable<DropItem>) DataSource)
            {
                if (!info.IsCultureEquals(item.Info))
                {
                    continue;
                }
                
                SelectedItem = item;
                return;
            }

            throw new NotFoundException();
        }

        private void OnVisibleChanged(Object? sender, EventArgs e)
        {
            if (!Visible || Disposing || IsDisposed)
            {
                return;
            }

            SetLanguage();
        }
    }
}