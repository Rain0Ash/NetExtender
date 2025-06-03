// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Utilities.Core;

namespace NetExtender.WindowsPresentation.Types.Dependency
{
    public delegate void PropertyChangedCallback<TProperty>(DependencyPropertyChangedEventArgs<TProperty> e);
    
    public static class DependencyProperty<T> where T : DependencyObject
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Register(expression, default, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> expression, TProperty? @default)
        {
            return Register(expression, @default, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> expression, Func<T, PropertyChangedCallback<TProperty>>? callback)
        {
            return Register(expression, default, callback);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> expression, TProperty? @default, Func<T, PropertyChangedCallback<TProperty>>? callback)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            String property = expression.GetMemberName();
            PropertyChangedCallback? changed = Convert(callback);
            return DependencyProperty.Register(property, typeof(TProperty), typeof(T), new PropertyMetadata(@default, changed));
        }

        [return: NotNullIfNotNull("callback")]
        private static PropertyChangedCallback? Convert<TProperty>(Func<T, PropertyChangedCallback<TProperty>?>? callback)
        {
            if (callback is null)
            {
                return null;
            }

            return (@object, args) =>
            {
                PropertyChangedCallback<TProperty>? changed = callback((T) @object);
                changed?.Invoke(args);
            };
        }
    }

    public readonly struct DependencyPropertyChangedEventArgs<T>
    {
        public static implicit operator DependencyPropertyChangedEventArgs<T>(DependencyPropertyChangedEventArgs value)
        {
            return new DependencyPropertyChangedEventArgs<T>(value);
        }
        
        public DependencyProperty Property { get; }
        public T NewValue { get; }
        public T OldValue { get; }

        private DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs args)
        {
            Property = args.Property;
            NewValue = (T) args.NewValue;
            OldValue = (T) args.OldValue;
        }
    }
}