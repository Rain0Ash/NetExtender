// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using NetExtender.Types.Notify;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;
using ReactiveUI;

namespace NetExtender.ReactiveUI.Utilities
{
    public static class ReactiveObjectUtilities
    {
        private static IStorage<IReactiveObject, PropertySubnotifier> Storage { get; } = new WeakStorage<IReactiveObject, PropertySubnotifier>();

        public static PropertySubnotifier<T> Register<T>(this T value) where T : class, IReactiveObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            void Changing(Object? _, PropertyChangingEventArgs args)
            {
                value.RaisePropertyChanged(args.PropertyName);
            }

            void Changed(Object? _, PropertyChangedEventArgs args)
            {
                value.RaisePropertyChanged(args.PropertyName);
            }

            return Storage.GetOrAdd(value, () => new PropertySubnotifier<T>(value, Changing, Changed)) as PropertySubnotifier<T> ?? throw new InvalidOperationException();
        }

        public static PropertySubnotifier<T> Register<T>(this T value, String when, params String?[]? properties) where T : class, IReactiveObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Register().Register(when, properties);
        }

        public static PropertySubnotifier<T> Register<T>(this T value, String when, IEnumerable<String?>? properties) where T : class, IReactiveObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Register().Register(when, properties);
        }

        public static PropertySubnotifier<T> Register<T, TProperty>(this T value, Expression<Func<T, TProperty>> when, params Expression<Func<T, Object?>>?[]? properties) where T : class, IReactiveObject
        {
            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            return value.Register().Register(when, properties);
        }

        public static Boolean TryGetSubnotifier<T>(this T value, [MaybeNullWhen(false)] out PropertySubnotifier<T> subnotifier) where T : class, IReactiveObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Storage.TryGetValue(value, out PropertySubnotifier? result))
            {
                subnotifier = default;
                return false;
            }

            subnotifier = result as PropertySubnotifier<T>;
            return subnotifier is not null;
        }
    }
}