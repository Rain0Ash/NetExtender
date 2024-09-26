using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Interfaces
{
    public interface IViewModelServiceProvider : IServiceProvider
    {
        public new Object? GetService(Type service);
        public T? Get<T>() where T : class, IDependencyViewModel;
        public Boolean Get<T>([MaybeNullWhen(false)] out T result) where T : class, IDependencyViewModel;
        public T Get<T>(Func<T> alternate) where T : class, IDependencyViewModel;
        public T Get<T, TAlternate>() where T : class, IDependencyViewModel where TAlternate : T, new();
        public T Require<T>() where T : class, IDependencyViewModel;
    }
}