using System;
using System.Windows;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.WindowsPresentation.Builder.Interfaces
{
    public interface IWindowsPresentationBuilder<out T> : IWindowsPresentationBuilder, IApplicationBuilder<T> where T : Window
    {
    }
    
    public interface IWindowsPresentationBuilder : IApplicationBuilder
    {
        public Boolean IsConsoleVisible { get; }
    }
}