using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class FilterComboBox : ComboBox
    {
        public static readonly DependencyProperty IsCaseSensitiveProperty = DependencyProperty.Register(nameof(IsCaseSensitive), typeof(Boolean), typeof(FilterComboBox), new UIPropertyMetadata(false));
        public static readonly DependencyProperty DropDownOnFocusProperty = DependencyProperty.Register(nameof(DropDownOnFocus), typeof(Boolean), typeof(FilterComboBox), new UIPropertyMetadata(true));

        [Description("The way the combo box treats the case sensitivity of typed text")]
        [Category("AutoFiltered ComboBox")]
        [DefaultValue(true)]
        public Boolean IsCaseSensitive
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(IsCaseSensitiveProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(IsCaseSensitiveProperty, value);
            }
        }
        
        [Description("The way the combo box behaves when it receives focus")]
        [Category("AutoFiltered ComboBox")]
        [DefaultValue(true)]
        public Boolean DropDownOnFocus
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(DropDownOnFocusProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(DropDownOnFocusProperty, value);
            }
        }
        
        private Int32 SilenceEventsCount { get; set; }
        private ICollectionView? CollectionView { get; set; }
        private String? CurrentText  { get; set; }
        private Boolean IsTextSaved { get; set; }
        private Int32 TextStart { get; set; }
        private Int32 TextLength { get; set; }
        private Boolean KeyboardSelectionGuard { get; set; }

        public FilterComboBox()
        {
            DependencyPropertyDescriptor? text = DependencyPropertyDescriptor.FromProperty(TextProperty, typeof(FilterComboBox));
            text.AddValueChanged(this, OnTextChanged);
            RegisterIsCaseSensitiveChangeNotification();
        }
        
        private void RegisterIsCaseSensitiveChangeNotification()
        {
            DependencyPropertyDescriptor.FromProperty(IsCaseSensitiveProperty, typeof(FilterComboBox)).AddValueChanged(this, OnIsCaseSensitiveChanged);
        }

        protected virtual void OnIsCaseSensitiveChanged(Object? sender, EventArgs args)
        {
            if (IsCaseSensitive)
            {
                IsTextSearchEnabled = false;
            }

            RefreshFilter();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EditableTextBox.SelectionChanged += EditableTextBoxSelectionChanged;
            ItemsPopup.Focusable = true;
        }

        private TextBox EditableTextBox
        {
            get
            {
                return (TextBox) GetTemplateChild("PART_EditableTextBox")!;
            }
        }

        private Popup ItemsPopup
        {
            get
            {
                return (Popup) GetTemplateChild("PART_Popup")!;
            }
        }

        private ScrollViewer? ItemsScrollViewer
        {
            get
            {
                Border? border = ItemsPopup.FindName("DropDownBorder") as Border;
                return border?.Child as ScrollViewer;
            }
        }

        private void EditableTextBoxSelectionChanged(Object? sender, RoutedEventArgs args)
        {
            TextBox? textbox = (TextBox) args.OriginalSource;
            Int32 start = textbox.SelectionStart;
            Int32 length = textbox.SelectionLength;

            if (SilenceEventsCount > 0)
            {
                return;
            }

            TextStart = start;
            TextLength = length;
            RefreshFilter();
            ScrollItemsToTop();
        }

        private void RestoreSavedText()
        {
            Text = IsTextSaved ? CurrentText : "";
            EditableTextBox.SelectAll();
        }

        private void ClearFilter()
        {
            TextLength = 0;
            TextStart = 0;
            RefreshFilter();
            Text = "";
            ScrollItemsToTop();
        }

        private void SilenceEvents()
        {
            ++SilenceEventsCount;
        }

        private void UnsilenceEvents()
        {
            if (SilenceEventsCount > 0)
            {
                --SilenceEventsCount;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (ItemsSource == null)
            {
                return;
            }

            if (DropDownOnFocus)
            {
                IsDropDownOpen = true;
            }
        }

        protected override void OnPreviewLostKeyboardFocus(
            KeyboardFocusChangedEventArgs e)
        {
            if (Text.Length == 0)
            {
                RestoreSavedText();
            }
            else if (SelectedItem != null)
            {
                CurrentText = SelectedItem.ToString();
            }

            base.OnPreviewLostKeyboardFocus(e);
        }

        private void ScrollItemsToTop()
        {
            ScrollViewer? viewer = ItemsScrollViewer;
            viewer?.ScrollToTop();
        }

        private void RefreshFilter()
        {
            if (ItemsSource == null)
            {
                return;
            }

            CollectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            CollectionView.Refresh();
            IsDropDownOpen = true;
        }

        private Boolean FilterPredicate(Object? value)
        {
            if (value is null)
            {
                return false;
            }

            if (Text.Length == 0)
            {
                return true;
            }

            String? prefix = Text;
            if (TextLength > 0 && TextStart + TextLength == Text.Length)
            {
                prefix = prefix.Substring(0, TextStart);
            }

            return value.ToString()?.StartsWith(prefix, !IsCaseSensitive, CultureInfo.CurrentCulture) ?? false;
        }

        protected override void OnItemsSourceChanged(IEnumerable? old, IEnumerable? @new)
        {
            if (@new is not null)
            {
                CollectionView = CollectionViewSource.GetDefaultView(@new);
                CollectionView.Filter += FilterPredicate;
            }

            if (old is not null)
            {
                CollectionView = CollectionViewSource.GetDefaultView(old);
                CollectionView.Filter -= FilterPredicate;
            }

            base.OnItemsSourceChanged(old, @new);
        }

        private void OnTextChanged(Object? sender, EventArgs e)
        {
            if (!IsTextSaved)
            {
                CurrentText = Text;
                IsTextSaved = true;
            }

            if (IsTextSearchEnabled || SilenceEventsCount != 0)
            {
                return;
            }

            RefreshFilter();

            if (Text.Length <= 0)
            {
                return;
            }

            Int32 prefix = Text.Length;
            CollectionView = CollectionViewSource.GetDefaultView(ItemsSource);
            foreach (Object? item in CollectionView)
            {
                Int32 text = item.ToString()?.Length ?? 0;
                SelectedItem = item;

                SilenceEvents();
                EditableTextBox.Text = item?.ToString() ?? String.Empty;
                EditableTextBox.Select(prefix, text - prefix);
                UnsilenceEvents();
                break;
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Tab:
                    IsDropDownOpen = false;
                    break;
                case Key.Escape:
                    KeyboardSelectionGuard = false;
                    UnsilenceEvents();
                    ClearFilter();
                    IsDropDownOpen = true;
                    return;
                case Key.Down:
                case Key.Up:
                    IsDropDownOpen = true;
                    if (!KeyboardSelectionGuard)
                    {
                        KeyboardSelectionGuard = true;
                        SilenceEvents();
                    }

                    break;
                default:
                    break;
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    KeyboardSelectionGuard = false;
                    UnsilenceEvents();
                    ClearFilter();
                    IsDropDownOpen = true;
                    return;
                default:
                    break;
            }

            base.OnKeyDown(e);
        }
    }
}