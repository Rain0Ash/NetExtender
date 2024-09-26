using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.WindowsPresentation.ReactiveUI
{
    public interface ITransientReactiveViewModel : IDependencyReactiveViewModel, ITransient
    {
    }
    
    public interface IScopedReactiveViewModel : IDependencyReactiveViewModel, IScoped
    {
    }
    
    public interface ISingletonReactiveViewModel : IDependencyReactiveViewModel, ISingleton
    {
    }
    
    public interface IDependencyReactiveViewModel : IDependencyViewModel, IDependencyService
    {
    }
}