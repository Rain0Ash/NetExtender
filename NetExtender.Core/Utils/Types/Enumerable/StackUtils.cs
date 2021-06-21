// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Utils.Types
{
    public static class StackUtils
    {
        /// <summary>
        /// Pushes a range of items into a stack
        /// </summary>
        /// <typeparam name="T">The type of items in the stack</typeparam>
        /// <param name="stack">The stack to push into</param>
        /// <param name="items">The items to push</param>
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T?> items)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (T? item in items)
            {
                stack.Push(item!);
            }
        }
    }
}