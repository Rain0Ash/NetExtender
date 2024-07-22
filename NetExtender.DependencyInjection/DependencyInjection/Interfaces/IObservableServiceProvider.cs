using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IObservableServiceProvider : IDynamicServiceProvider, IObservableCollection<ServiceDescriptor>
    {
    }
}