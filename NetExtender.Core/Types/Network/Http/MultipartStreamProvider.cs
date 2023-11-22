// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Network
{
    public abstract class MultipartStreamProvider
    {
        public Collection<HttpContent> Contents { get; } = new Collection<HttpContent>();

        public abstract Stream GetStream(HttpContent parent, HttpContentHeaders headers);

        public virtual Task ExecutePostProcessingAsync()
        {
            return ExecutePostProcessingAsync(CancellationToken.None);
        }

        public virtual Task ExecutePostProcessingAsync(CancellationToken token)
        {
            return token.IsCancellationRequested ? Task.FromCanceled(token) : Task.CompletedTask;
        }
    }
}