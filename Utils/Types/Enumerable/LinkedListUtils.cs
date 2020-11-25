// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Utils.Types
{
    public static class LinkedListUtils
    {
        public static void Swap<T>(this LinkedListNode<T> first, LinkedListNode<T> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            T tmp = first.Value;
            first.Value = second.Value;
            second.Value = tmp;
        }
    }
}