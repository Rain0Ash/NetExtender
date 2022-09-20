// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;

namespace NetExtender.Types.Tasks.Interfaces
{
    public interface IAsyncStartable : IStartable
    {
        public Task StartAsync();

        public void RunSynchronously()
        {
            StartAsync().GetAwaiter().GetResult();
        }
        
        public new async void Start()
        {
            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await StartAsync().ConfigureAwait(false);
        }
    }
}