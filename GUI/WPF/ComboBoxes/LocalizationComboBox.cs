// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.GUI.WPF.ComboBoxes
{
#if WPF
    public class LocalizationComboBox : LocalizableComboBox
    {
        public LocalizationComboBox()
        {
            Started += OnStarted;
        }

        private void OnStarted(Object sender, EventArgs e)
        {
            Localization.LanguageChanged += SetLanguage;
            IsVisibleChanged += OnVisibleChanged;
            SelectionChanged += OnSelectedIndexChanged;

            ItemsSource = Localization.Supported;
            DisplayMemberPath = "CustomName";
            SelectedValuePath = "LCID";

            SetLanguage();
        }

        public void SetLanguage()
        {
            SetLanguage(Localization.Culture.LCID);
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

                if (Localization.Default.LCID == lcid)
                {
                    throw new IndexOutOfRangeException($"Culture {Localization.CultureByLCID.TryGetValue(lcid)?.CustomName ?? "Not exist"} out of range");
                }
                
                if (Localization.Basic.LCID == lcid)
                {
                    lcid = Localization.DefaultCulture.LCID;
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
            return Localization.CultureByLCID.FirstOr(
                x => String.Equals(x.Value.CustomName, SelectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase),
                new KeyValuePair<Int32, Culture>(Localization.DefaultCulture.LCID, Localization.DefaultCulture)).Key;
        }

        protected void OnSelectedIndexChanged(Object sender, SelectionChangedEventArgs e)
        {
            Localization.UpdateLocalization(GetLanguageLCID());
        }
    }
#endif
}