// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Behavior.Interfaces
{
    public interface ISaveableBehavior<out TOptions> : ISaveableBehavior, IBehavior<TOptions> where TOptions : unmanaged, Enum
    {
    }

    public interface ISaveableBehavior : IBehavior
    {
        public Boolean Save();
        public Task<Boolean> SaveAsync();
        public Task<Boolean> SaveAsync(CancellationToken token);
    }
}