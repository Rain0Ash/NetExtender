using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IRequiredServiceProvider : IServiceProvider, ISupportRequiredService
    {
    }
}