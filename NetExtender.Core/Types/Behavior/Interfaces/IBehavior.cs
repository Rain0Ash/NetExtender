// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Behavior.Interfaces
{
    public interface IBehavior<out TOptions> : IBehavior where TOptions : unmanaged, Enum
    {
        public TOptions Options { get; }
    }
    
    public interface IBehavior
    {
        public Boolean IsThreadSafe { get; }
    }
}