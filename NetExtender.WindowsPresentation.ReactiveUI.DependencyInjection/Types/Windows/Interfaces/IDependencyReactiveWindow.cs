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