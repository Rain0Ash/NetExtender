using System;
using NetExtender.DependencyInjection.Events;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IChangeableServiceProvider : IServiceProvider
    {
        public event ServiceProviderChangedEventHandler? Changed;
        public Boolean IsStable { get; }
        public Boolean IsFinal { get; }
        
        public IServiceProvider? Rebuild();
        public void Final();
    }
}