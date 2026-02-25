// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Interfaces;
using NetExtender.WindowsPresentation.Types.Scopes.Interfaces;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types
{
    public class WindowsPresentationServiceProvider : WindowsPresentationServiceProviderBase
    {
        internal static readonly WindowsPresentationServiceProvider Internal = new WindowsPresentationServiceProvider();
        public static WindowsPresentationServiceProvider Instance
        {
            get
            {
                return Application.Current.Provider() ?? Internal;
            }
        }

        protected sealed override IServiceProvider ModelsProvider { get; }
        protected sealed override IServiceProvider WindowsProvider { get; }

        protected WindowsPresentationServiceProvider()
            : this(DependencyViewModelUtilities.GetProvider(), DependencyWindowUtilities.GetProvider())
        {
        }

        protected WindowsPresentationServiceProvider(IServiceProvider models, IServiceProvider windows)
        {
            ModelsProvider = models ?? throw new ArgumentNullException(nameof(models));
            WindowsProvider = windows ?? throw new ArgumentNullException(nameof(windows));
        }

        public Scope CreateScope()
        {
            if (!ReferenceEquals(ModelsProvider, WindowsProvider))
            {
                return new Scope(ModelsProvider.CreateScope(), WindowsProvider.CreateScope());
            }

            IServiceScope scope = ModelsProvider.CreateScope();
            return new Scope(scope, scope);
        }

        public Scope CreateAsyncScope()
        {
            if (!ReferenceEquals(ModelsProvider, WindowsProvider))
            {
                return new Scope(ModelsProvider.CreateAsyncScope(), WindowsProvider.CreateAsyncScope());
            }

            IAsyncServiceScope scope = ModelsProvider.CreateAsyncScope();
            return new Scope(scope, scope);
        }

        public sealed class Scope : WindowsPresentationServiceProviderBase, IAsyncViewModelServiceProviderScope, IAsyncWindowServiceProviderScope
        {
            private IServiceScope ModelsScope { get; }
            private IServiceScope WindowsScope { get; }

            protected override IServiceProvider ModelsProvider
            {
                get
                {
                    return ModelsScope.Provider;
                }
            }

            IViewModelServiceProvider IViewModelServiceProviderScope.Provider
            {
                get
                {
                    return Models;
                }
            }

            protected override IServiceProvider WindowsProvider
            {
                get
                {
                    return WindowsScope.Provider;
                }
            }

            IWindowServiceProvider IWindowServiceProviderScope.Provider
            {
                get
                {
                    return Windows;
                }
            }

            internal Scope(IServiceScope models, IServiceScope windows)
            {
                ModelsScope = models ?? throw new ArgumentNullException(nameof(models));
                WindowsScope = windows ?? throw new ArgumentNullException(nameof(windows));
            }

            public void Dispose()
            {
                if (ReferenceEquals(ModelsScope, WindowsScope))
                {
                    ModelsScope.Dispose();
                    return;
                }

                ModelsScope.Dispose();
                WindowsScope.Dispose();
            }

            public async ValueTask DisposeAsync()
            {
                if (ReferenceEquals(ModelsScope, WindowsScope))
                {
                    await ModelsScope.DisposeAsync();
                    return;
                }

                await ModelsScope.DisposeAsync();
                await WindowsScope.DisposeAsync();
            }
        }
    }

    public abstract class WindowsPresentationServiceProviderBase : IWindowServiceProvider, IViewModelServiceProvider
    {
        protected abstract IServiceProvider WindowsProvider { get; }
        protected abstract IServiceProvider ModelsProvider { get; }

        public IWindowServiceProvider Windows
        {
            get
            {
                return this;
            }
        }

        public IViewModelServiceProvider Models
        {
            get
            {
                return this;
            }
        }

        public virtual Object GetService(Type service)
        {
            throw new ImplicitImplementationNotSupportedException();
        }

        public virtual Object GetRequiredService(Type service)
        {
            return GetService(service) ?? throw new InvalidOperationException("Dependency not found.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object? IWindowServiceProvider.GetService(Type service)
        {
            return WindowsProvider.GetService(service);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object? IViewModelServiceProvider.GetService(Type service)
        {
            return ModelsProvider.GetService(service);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object? IWindowServiceProvider.GetRequiredService(Type service)
        {
            return WindowsProvider.GetService(service);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Object? IViewModelServiceProvider.GetRequiredService(Type service)
        {
            return ModelsProvider.GetService(service);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T? IWindowServiceProvider.Get<T>() where T : class
        {
            return (T?) Windows.GetService(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T? IViewModelServiceProvider.Get<T>() where T : class
        {
            return (T?) Models.GetService(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IWindowServiceProvider.Get<T>([MaybeNullWhen(false)] out T result)
        {
            result = Windows.Get<T>();
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IViewModelServiceProvider.Get<T>([MaybeNullWhen(false)] out T result)
        {
            result = Models.Get<T>();
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IWindowServiceProvider.Get<T>(Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Windows.Get<T>() ?? alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IViewModelServiceProvider.Get<T>(Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return Models.Get<T>() ?? alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IWindowServiceProvider.Get<T, TAlternate>()
        {
            return Windows.Get<T>() ?? new TAlternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IViewModelServiceProvider.Get<T, TAlternate>()
        {
            return Models.Get<T>() ?? new TAlternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IWindowServiceProvider.Require<T>()
        {
            return Windows.Get<T>() ?? throw new InvalidOperationException($"Dependency window '{typeof(T).Name}' not found.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IViewModelServiceProvider.Require<T>()
        {
            return Models.Get<T>() ?? throw new InvalidOperationException($"Dependency view model '{typeof(T).Name}' not found.");
        }
    }
}