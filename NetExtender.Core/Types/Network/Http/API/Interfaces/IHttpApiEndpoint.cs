using System;
using System.Net.Http;
using NetExtender.CQRS.Interfaces;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network.API.Interfaces
{
    public interface IHttpApiEndpoint<in TRequest, TResponse> : IHttpApiEndpoint where TRequest : IEntityCQRS
    {
    }

    public interface IHttpApiEndpoint : IEquatable<IHttpApiEndpoint>
    {
        public String? Controller { get; }
        public String? Name { get; }
        public HttpMethod Method { get; }
        public HttpMethodType MethodType { get; }
        public Uri Endpoint { get; }
        public HttpRequestHandler? Handler { get; }
        public String? Description { get; }

        internal Boolean IsEmpty
        {
            get
            {
                return false;
            }
        }
    }
}