using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Network.Interfaces
{
    public interface IAuthenticationTokenProvider<T> where T : IAuthenticationToken
    {
        public Task<T?> GetToken(HttpRequestMessage request, CancellationToken token);
        public Task<T?> RefreshToken(HttpRequestMessage request, T @default, CancellationToken token);
        public Task NotifySessionExpired(HttpRequestMessage request, T? @default, CancellationToken token);
    }
}