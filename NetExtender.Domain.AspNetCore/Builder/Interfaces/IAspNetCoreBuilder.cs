// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.AspNetCore.Builder.Interfaces
{
    public interface IAspNetCoreBuilder<out T> : IAspNetCoreBuilder, IApplicationBuilder<T> where T : class
    {
    }
    
    public interface IAspNetCoreBuilder
    {
        public ServiceAmbiguousHandler ServiceHandler
        {
            get
            {
                return ServiceAmbiguousHandler.New;
            }
        }
    }
}