// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Comparers.Interfaces
{
    public interface IComparer<in T1, in T2>
    {
        public Int32 Compare(T1? x, T2? y);
    }
}