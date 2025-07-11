// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IObservableServiceProvider : IDynamicServiceProvider, IObservableCollection<ServiceDescriptor>
    {
    }
}