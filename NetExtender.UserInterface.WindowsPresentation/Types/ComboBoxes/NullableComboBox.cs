// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Types.ComboBoxes
{
    public class NullableComboBox : ComboBox
    {
        public static readonly DependencyProperty DefaultItemProperty = DependencyProperty.Register(nameof(DefaultItem), typeof(String), typeof(NullableComboBox), new FrameworkPropertyMetadata(default(String), OnDefaultChanged));

        public String? DefaultItem
        {
            get
            {
                return (String?) GetValue(DefaultItemProperty);
            }
            set
            {
                SetValue(DefaultItemProperty, value);
            }
        }

        protected Container Nullable { get; set; }
        protected ICollection? Values { get; private set; }
        private Func<Boolean>? Reload { get; set; }
        
        protected virtual View<T> Convert<T>(T value)
        {
            return value!;
        }

        public Maybe<T> Get<T>()
        {
            return GetSelectedItem<T>();
        }

        public Boolean Get<T>(out Maybe<T> result)
        {
            return TryGetSelectedItem(out result);
        }
        
        public Maybe<T> GetSelectedItem<T>()
        {
            return TryGetSelectedItem(out Maybe<T> result) ? result : default;
        }

        public Boolean TryGetSelectedItem<T>(out Maybe<T> result)
        {
            if (SelectedItem is not IView<T> current)
            {
                result = default;
                return false;
            }

            result = current.Maybe;
            return true;
        }
        
        public Boolean Set<T>(T value)
        {
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is not View<T> item || !item.Maybe.Equals(value))
                {
                    continue;
                }
                
                SelectedIndex = i;
                return true;
            }
            
            return false;
        }

        protected Boolean Update()
        {
            return Reload?.Invoke() ?? false;
        }

        protected virtual Boolean Update<T>()
        {
            DisplayMemberPath = nameof(View<T>.Display);
            SelectedValuePath = nameof(View<T>.Value);
            
            return Update<T, View<T>>();
        }

        protected Boolean Update<T, TView>() where TView : class, IView
        {
            File.WriteAllText("", "");
            
            if (Nullable.View is null)
            {
                Nullable = DefaultItem is not null ? new Container(new View<T>(DefaultItem)) { IsAuto = true } : default;
            }
            
            TView[] array = Values switch
            {
                TView[] values => Nullable.View is TView view ? values.Prepend(view).ToArray() : values,
                _ => Nullable.View is TView view ? new[] { view } : Array.Empty<TView>()
            };

            ItemsSource = array;
            SelectedIndex = array.Length > 0 ? 0 : -1;
            Reload = Update<T, TView>;
            return true;
        }

        public void SetItemsSource<T>(IEnumerable<View<T>> source)
        {
            SetItemsSource((View<T>?) null, source);
        }

        public void SetItemsSource<T>(View<T>? nullable, IEnumerable<View<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Nullable = new Container(nullable);
            Values = source.ToArray();
            Update<T>();
        }
        
        public void SetItemsSource<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SetItemsSource(source.Select(Convert));
        }

        public void SetItemsSource<T>(View<T>? nullable, IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (nullable is null)
            {
                SetItemsSource(source);
                return;
            }

            SetItemsSource(nullable, source.Select(Convert));
        }
        
        private static void OnDefaultChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not NullableComboBox combobox)
            {
                return;
            }

            if (combobox.Nullable.View is not null)
            {
                if (!combobox.Nullable.IsAuto)
                {
                    return;
                }
                
                if (args.NewValue is String value)
                {
                    combobox.Nullable.SetText(value);
                    return;
                }
            }

            combobox.Update();
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected readonly struct Container
        {
            public IView? View { get; }
            public Boolean IsAuto { get; init; }

            public Container(IView? view)
            {
                View = view;
                IsAuto = false;
            }

            public Boolean SetText(String value)
            {
                return IsAuto && (View?.SetText(value) ?? false);
            }
        }

        protected interface IView<T> : IView
        {
            public Maybe<T> Maybe { get; }
            public T? Value { get; }
        }

        protected interface IView : INotifyProperty
        {
            public Boolean HasValue { get; }

            public Boolean SetText(String value);
        }
        
        public class View<T> : View, IView<T>
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator T?(View<T>? value)
            {
                return value is not null ? value.Value : default;
            }

            [return: NotNullIfNotNull("value")]
            public static implicit operator View<T>?(String? value)
            {
                return value is not null ? new View<T>(value) : null;
            }

            [return: NotNullIfNotNull("value")]
            public static implicit operator View<T>?(T? value)
            {
                return value is not null ? new View<T>(value.ToString() ?? String.Empty, value) : null;
            }

            public Maybe<T> Maybe { get; }
            
            public sealed override Boolean HasValue
            {
                get
                {
                    return Maybe.HasValue;
                }
            }

            public T? Value
            {
                get
                {
                    return HasValue ? Maybe.Value : default;
                }
            }

            public View(String display)
                : base(display)
            {
                Maybe = default;
            }

            public View(String display, T value)
                : base(display)
            {
                Maybe = value;
            }
        }

        public abstract class View : ViewAbstraction
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator String?(View? value)
            {
                return value?.Display;
            }

            private String _display;
            public String Display
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
            
            protected View(String display)
            {
                _display = display ?? throw new ArgumentNullException(nameof(display));
            }

            public override Boolean SetText(String value)
            {
                Display = value ?? throw new ArgumentNullException(nameof(value));
                return true;
            }

            public override String ToString()
            {
                return Display;
            }
        }

        public abstract class ViewAbstraction : IView
        {
            public event PropertyChangingEventHandler? PropertyChanging;
            public event PropertyChangedEventHandler? PropertyChanged;
            
            public abstract Boolean HasValue { get; }
            public abstract Boolean SetText(String value);
        }
    }
}