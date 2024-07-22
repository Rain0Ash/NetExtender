using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IDynamicServiceProvider : IChangeableServiceProvider, IServiceCollection, ICollection
    {
        public new Int32 Count { get; }
        public void Reset();
    }
}