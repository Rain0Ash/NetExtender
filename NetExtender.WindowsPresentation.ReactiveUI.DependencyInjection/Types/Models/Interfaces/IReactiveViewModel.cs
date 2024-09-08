using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.WindowsPresentation.ReactiveUI.Interfaces
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
    
    public interface IDependencyReactiveViewModel : IDependencyService, IDependencyViewModel
    {
    }
}