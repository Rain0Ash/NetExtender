// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Initializer.Types.Behavior.Interfaces
{
    public interface IResetableBehavior<out TOptions> : IResetableBehavior, IBehavior<TOptions> where TOptions : unmanaged, Enum
    {
    }

    public interface IResetableBehavior : IBehavior
    {
        public Boolean Reset();
        public Task<Boolean> ResetAsync();
        public Task<Boolean> ResetAsync(CancellationToken token);
    }
}