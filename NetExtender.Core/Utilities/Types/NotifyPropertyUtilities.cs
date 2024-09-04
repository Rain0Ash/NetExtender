// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Notify;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class NotifyPropertyUtilities
    {
        private static IStorage<Object, PropertySubnotifier> Storage { get; } = new WeakStorage<Object, PropertySubnotifier>();

        public static PropertySubnotifier<T> RegisterSubnotifier<T>(T value, Action<Object?, PropertyChangedEventArgs>? changed) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Storage.GetOrAdd(value, () => new PropertySubnotifier<T>(value, changed)) as PropertySubnotifier<T> ?? throw new InvalidOperationException();
        }

        public static PropertySubnotifier<T> RegisterSubnotifier<T>(T value, Action<Object?, PropertyChangingEventArgs>? changing) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Storage.GetOrAdd(value, () => new PropertySubnotifier<T>(value, changing)) as PropertySubnotifier<T> ??throw new InvalidOperationException();
        }

        public static PropertySubnotifier<T> RegisterSubnotifier<T>(T value, Action<Object?, PropertyChangingEventArgs>? changing, Action<Object?, PropertyChangedEventArgs>? changed) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Storage.GetOrAdd(value, () => new PropertySubnotifier<T>(value, changing, changed)) as PropertySubnotifier<T> ?? throw new InvalidOperationException();
        }

        public static Boolean TryGetSubnotifier<T>(T value, [MaybeNullWhen(false)] out PropertySubnotifier<T> subnotifier) where T : class
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