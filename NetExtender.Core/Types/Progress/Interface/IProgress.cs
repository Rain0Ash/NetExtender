// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Core.Types.Progress.Interface
{
    public interface IProgress<T1, T2> : IProgress<(T1, T2)>
    {
    }
    
    public interface IProgress<T1, T2, T3> : IProgress<(T1, T2, T3)>
    {
    }
    
    public interface IProgress<T1, T2, T3, T4> : IProgress<(T1, T2, T3, T4)>
    {
    }
    
    public interface IProgress<T1, T2, T3, T4, T5> : IProgress<(T1, T2, T3, T4, T5)>
    {
    }
}