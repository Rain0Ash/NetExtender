// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.UserInterface.Console.Interfaces;

namespace NetExtender.Domains.WindowsPresentation.Builder.Interfaces
{
    public interface IWindowsPresentationBuilder<out T> : IWindowsPresentationBuilder, IApplicationBuilder<T> where T : Window
    {
    }
    
    public interface IWindowsPresentationBuilder : IApplicationBuilder
    {
        public new IWindowConsole Console { get; }
    }
}