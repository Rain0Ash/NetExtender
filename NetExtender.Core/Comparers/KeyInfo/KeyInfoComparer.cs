// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Comparers.KeyInfo
{
    public class KeyInfoComparer : IEqualityComparer<ConsoleKeyInfo>
    {
        public Boolean Equals(ConsoleKeyInfo x, ConsoleKeyInfo y)
        {
            return x.Key == y.Key;
        }

        public Int32 GetHashCode(ConsoleKeyInfo key)
        {
            return key.KeyChar.GetHashCode();
        }
    }
}