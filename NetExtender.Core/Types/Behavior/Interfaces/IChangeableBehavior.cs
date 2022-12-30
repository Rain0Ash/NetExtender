// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Initializer.Types.Behavior.Interfaces
{
    public interface IChangeableBehavior<out TOptions> : IReadableBehavior<TOptions>, ISaveableBehavior<TOptions>, IResetableBehavior<TOptions> where TOptions : unmanaged, Enum
    {
    }
    
    public interface IChangeableBehavior : IReadableBehavior, ISaveableBehavior, IResetableBehavior
    {
    }
}