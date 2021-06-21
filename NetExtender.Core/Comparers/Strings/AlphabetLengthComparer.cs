// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Comparers
{
    public class AlphabetLengthComparer : IComparer, IComparer<String?>
    {
        public Int32 Compare(Object? x, Object? y)
        {
            return Compare(x?.ToString(), y?.ToString());
        }

        public Int32 Compare(String? x, String? y)
        {
            if (x is null || y is null)
            {
                return -2;
            }

            if (x.Length == y.Length)
            {
                return String.CompareOrdinal(x, y);
            }

            if (x.Length > y.Length)
            {
                return 1;
            }

            return -1;
        }
    }
}