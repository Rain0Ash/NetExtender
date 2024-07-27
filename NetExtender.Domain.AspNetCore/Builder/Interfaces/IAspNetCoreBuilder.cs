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