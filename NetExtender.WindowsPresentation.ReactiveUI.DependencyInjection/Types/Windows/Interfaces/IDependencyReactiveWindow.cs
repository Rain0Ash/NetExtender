// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public interface IReactiveWindow : ITransientReactiveWindow
    {
    }
    
    public interface ITransientReactiveWindow : IDependencyReactiveWindow, ITransient
    {
    }

    public interface IScopedReactiveWindow : IDependencyReactiveWindow, IScoped
    {
    }

    public interface ISingletonReactiveWindow : IDependencyReactiveWindow, ISingleton
    {
    }

    public interface IDependencyReactiveWindow : IDependencyWindow, IDependencyService
    {
    }
}