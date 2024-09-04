// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Notify
{
    public sealed class PropertySubnotifier<T> : PropertySubnotifier where T : class
    {
        private T Internal { get; }

        public PropertySubnotifier(T value, Action<Object?, PropertyChangedEventArgs>? changed)
            : this(value, null, changed)
        {
        }

        public PropertySubnotifier(T value, Action<Object?, PropertyChangingEventArgs>? changing)
            : this(value, changing, null)
        {
        }

        public PropertySubnotifier(T value, Action<Object?, PropertyChangingEventArgs>? changing, Action<Object?, PropertyChangedEventArgs>? changed)
            : base(changing, changed)
        {
            Internal = value ?? throw new ArgumentNullException(nameof(value));
            Subscribe(value as INotifyPropertyChanging);
            Subscribe(value as INotifyPropertyChanged);
        }

        public Boolean Contains(String when, String property)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Storage.TryGetValue(when, out LinkedList<String>? collection) && collection.Contains(property);
        }

        public Boolean Contains<TProperty>(Expression<Func<T, TProperty>> when, Expression<Func<T, Object?>> property)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            PropertyInfo? infowhen = when.GetPropertyInfo();

            if (infowhen is null)
            {
                throw new ArgumentException("Expression does not represent a property.", nameof(when));
            }

            PropertyInfo? info = property.GetPropertyInfo();

            if (info is null)
            {
                throw new ArgumentException("Expression does not represent a property.", nameof(property));
            }

            return Contains(infowhen.Name, info.Name);
        }

        public PropertySubnotifier<T> Register(String when, params String?[]? properties)
        {
            return Register(when, (IEnumerable<String?>?) properties);
        }

        public PropertySubnotifier<T> Register(String when, IEnumerable<String?>? properties)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            if (properties is null)
            {
                return this;
            }

            foreach (String? property in properties)
            {
                if (property is null)
                {
                    continue;
                }

                if (!Storage.TryGetValue(when, out LinkedList<String>? collection))
                {
                    collection = new LinkedList<String>();
                    Storage.Add(when, collection);
                }

                collection.AddLast(property);
            }

            return this;
        }

        public PropertySubnotifier<T> Register<TProperty>(Expression<Func<T, TProperty>> when, params Expression<Func<T, Object?>>?[]? properties)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            PropertyInfo? property = when.GetPropertyInfo();

            if (property is null)
            {
                throw new ArgumentException("Expression does not represent a property.", nameof(when));
            }

            return Register(property.Name, properties?.WhereNotNull().Select(ExpressionUtilities.GetPropertyInfo).WhereNotNull().Select(info => info.Name));
        }

        public PropertySubnotifier<T> Unregister(String when, params String?[]? properties)
        {
            return Unregister(when, (IEnumerable<String?>?) properties);
        }

        public PropertySubnotifier<T> Unregister(String when, IEnumerable<String?>? properties)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            if (properties is null)
            {
                return this;
            }

            if (!Storage.TryGetValue(when, out LinkedList<String>? collection))
            {
                return this;
            }

            foreach (String? property in properties)
            {
                if (property is null)
                {
                    continue;
                }

                collection.Remove(property);
            }

            return this;
        }

        public PropertySubnotifier<T> Unregister<TProperty>(Expression<Func<T, TProperty>> when, params Expression<Func<T, Object?>>?[]? properties)
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            PropertyInfo? property = when.GetPropertyInfo();

            if (property is null)
            {
                throw new ArgumentException("Expression does not represent a property.", nameof(when));
            }

            return Unregister(property.Name, properties?.WhereNotNull().Select(ExpressionUtilities.GetPropertyInfo).WhereNotNull().Select(info => info.Name));
        }
    }

    public abstract class PropertySubnotifier
    {
        protected Dictionary<String, LinkedList<String>> Storage { get; } = new Dictionary<String, LinkedList<String>>(4);

        private Action<Object?, PropertyChangingEventArgs>? Changing { get; }
        private Action<Object?, PropertyChangedEventArgs>? Changed { get; }

        protected PropertySubnotifier(Action<Object?, PropertyChangingEventArgs>? changing, Action<Object?, PropertyChangedEventArgs>? changed)
        {
            Changing = changing;
            Changed = changed;
        }

        protected void Subscribe(INotifyPropertyChanging? property)
        {
            if (property is not null)
            {
                property.PropertyChanging += OnPropertyChanging;
            }
        }

        protected void Subscribe(INotifyPropertyChanged? property)
        {
            if (property is not null)
            {
                property.PropertyChanged += OnPropertyChanged;
            }
        }

        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            String? name = args.PropertyName;

            if (name is null)
            {
                return;
            }

            if (!Storage.TryGetValue(name, out LinkedList<String>? collection))
            {
                return;
            }

            foreach (String? property in collection)
            {
                Changing?.Invoke(sender, new PropertyChanging(property));
            }
        }

        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            String? name = args.PropertyName;

            if (name is null)
            {
                return;
            }

            if (!Storage.TryGetValue(name, out LinkedList<String>? collection))
            {
                return;
            }

            foreach (String? property in collection)
            {
                Changed?.Invoke(sender, new PropertyChanged(property));
            }
        }
    }
}