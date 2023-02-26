// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Comparers.Interfaces
{
    public interface IEqualityComparer<in T1, in T2>
    {
        public Boolean IsEquals(T1 x, T2 y)
        {
            return Equals(x, y);
        }

        public Boolean IsEquals(T2 x, T1 y)
        {
            return Equals(y, x);
        }

        public Boolean Equals(T1 x, T2 y);

        public Int32 GetHashCode(T1 obj);
    }
}