// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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