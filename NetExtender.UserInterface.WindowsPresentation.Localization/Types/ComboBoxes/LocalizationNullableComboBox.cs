// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.UserInterface.WindowsPresentation.Types.ComboBoxes;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Localization.Types.ComboBoxes
{
    /*public class LocalizationNullableComboBox : NullableComboBox
    {
        protected override void Update<T>()
        {
            DisplayMemberPath = nameof(View<T>.Display) + "." + nameof(View<T>.Display.Current);
            SelectedValuePath = nameof(View<T>.Value);

            Update<T, View<T>>();
        }
        
        public virtual void SetItemsSource<T>(IEnumerable<View<T>> source)
        {
            SetItemsSource(null, source);
        }
        
        public void SetItemsSource<T>(View<T>? nullable, IEnumerable<View<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Nullable = nullable;
            Values = source.ToArray();
            Update<T>();
        }
        
        public new class View<T> : View, IView<T>
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator T?(View<T>? value)
            {
                return value is not null ? value.Value : default;
            }
            
            public T Value { get; }

            public View(ILocalizationPropertyInfo display)
                : base(display)
            {
                Value = default!;
            }

            public View(ILocalizationPropertyInfo display, T value)
                : base(display)
            {
                Value = value;
            }
        }
        
        public new class View : ViewAbstraction
        {
            private ILocalizationPropertyInfo _display;
            public ILocalizationPropertyInfo Display
            {
                get
                {
                    return _display;
                }
                set
                {
                    this.RaiseAndSetIfChanged(ref _display, value);
                }
            }

            protected View(ILocalizationPropertyInfo display)
            {
                _display = display ?? throw new ArgumentNullException(nameof(display));
            }
        }
    }*/
}