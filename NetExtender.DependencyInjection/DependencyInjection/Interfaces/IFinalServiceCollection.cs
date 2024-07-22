using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IFinalServiceCollection : IServiceCollection, ICollection
    {
        public new Int32 Count { get; }
        public Boolean IsFinal { get; }
        public void Final();
    }
}