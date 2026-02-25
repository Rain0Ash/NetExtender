// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;

namespace NetExtender
{
    public interface ISetter<in T>
    {
        public void Set(T value);
    }

    public interface IAsyncSetter<in T>
    {
        public ValueTask SetAsync(T value);
        public ValueTask SetAsync(T value, CancellationToken token);
    }
}