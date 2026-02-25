// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;

namespace NetExtender
{
    public interface IGetter<out T>
    {
        public T Get();
    }

    public interface IAsyncGetter<T>
    {
        public ValueTask<T> GetAsync();
        public ValueTask<T> GetAsync(CancellationToken token);
    }
}