// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Behavior.Interfaces
{
    public interface IReadableBehavior<out TOptions> : IReadableBehavior, IBehavior<TOptions> where TOptions : unmanaged, Enum
    {
    }

    public interface IReadableBehavior : IBehavior
    {
        public Boolean Read();
        public Task<Boolean> ReadAsync();
        public Task<Boolean> ReadAsync(CancellationToken token);
    }
}