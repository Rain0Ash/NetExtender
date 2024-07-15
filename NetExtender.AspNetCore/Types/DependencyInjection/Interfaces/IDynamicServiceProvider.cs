using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.DependencyInjection.Interfaces
{
    public interface IDynamicServiceProvider : IServiceCollection, IServiceProvider, ICollection
    {
        public new Int32 Count { get; }
        
        public IServiceProvider Rebuild();
        public void Reset();
    }
}