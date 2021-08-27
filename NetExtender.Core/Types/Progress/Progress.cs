// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Progress.Interface;

namespace NetExtender.Types.Progress
{
    public class Progress<T1, T2> : Progress<(T1, T2)>, IProgress<T1, T2>
    {
    }
    
    public class Progress<T1, T2, T3> : Progress<(T1, T2, T3)>, IProgress<T1, T2, T3>
    {
    }
    
    public class Progress<T1, T2, T3, T4> : Progress<(T1, T2, T3, T4)>, IProgress<T1, T2, T3, T4>
    {
    }
    
    public class Progress<T1, T2, T3, T4, T5> : Progress<(T1, T2, T3, T4, T5)>, IProgress<T1, T2, T3, T4, T5>
    {
    }
}