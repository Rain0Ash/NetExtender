// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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