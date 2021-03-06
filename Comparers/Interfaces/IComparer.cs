// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Comparers.Interfaces
{
    public interface IComparer<T1, T2>
    {
        public Int32 Comparing(T1? x, T2? y)
        {
            return Compare(x, y);
        }

        public Int32 Comparing(T2? x, T1? y)
        {
            return Compare(y, x);
        }
        
        public Int32 Compare(T1? x, T2? y);
    }
}