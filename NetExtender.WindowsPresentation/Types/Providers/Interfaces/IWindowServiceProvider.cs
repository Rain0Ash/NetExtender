using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace NetExtender.WindowsPresentation.Types.Interfaces
{
    public interface IWindowServiceProvider : IServiceProvider
    {
        public new Object? GetService(Type service);
        public T? Get<T>() where T : Window;
        public Boolean Get<T>([MaybeNullWhen(false)] out T result) where T : Window;
        public T Get<T>(Func<T> alternate) where T : Window;
        public T Get<T, TAlternate>() where T : Window where TAlternate : T, new();
        public T Require<T>() where T : Window;
    }
}