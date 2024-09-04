using System.Collections.Generic;
using System.Net;

namespace NetExtender.Types.Network.Interfaces
{
    public interface IDynamicHttpRequestHandler : IDictionary<HttpStatusCode, HttpExceptionHandler?>
    {
    }
}