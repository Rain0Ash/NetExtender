// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Immutable.Stacks
{
    public class ImmutableReversedStack<T> : IImmutableStack<T>
    {
        public static ImmutableReversedStack<T> Empty { get; } = new ImmutableReversedStack<T>(ImmutableList<T>.Empty);

        public Boolean IsEmpty
        {
            get
            {
                return Stack.IsEmpty;
            }
        }
        
        private ImmutableList<T> Stack { get; }
        
        private ImmutableReversedStack(ImmutableList<T> stack)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
        }
        
        public ImmutableReversedStack(IEnumerable<T> stack)
        {
            Stack = stack?.AsImmutableList() ?? throw new ArgumentNullException(nameof(stack));
        }

        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("This operation does not apply to an empty instance.");
            }
            
            return Stack[^1];
        }
        
        public ref readonly T PeekRef()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("This operation does not apply to an empty instance.");
            }

            return ref Stack.ItemRef(Stack.Count - 1);
        }
        
        public ImmutableReversedStack<T> Push(T value)
        {
            return new ImmutableReversedStack<T>(Stack.Add(value));
        }

        IImmutableStack<T> IImmutableStack<T>.Push(T value)
        {
            return Push(value);
        }
        
        public ImmutableReversedStack<T> Pop()
        {
            return Pop(out _);
        }

        IImmutableStack<T> IImmutableStack<T>.Pop()
        {
            return Pop();
        }
        
        public ImmutableReversedStack<T> Pop(out T value)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("This operation does not apply to an empty instance.");
            }

            value = Stack[^1];
            return new ImmutableReversedStack<T>(Stack.RemoveAt(Stack.Count - 1));
        }
        
        public ImmutableReversedStack<T> Clear()
        {
            return Empty;
        }
        
        IImmutableStack<T> IImmutableStack<T>.Clear()
        {
            return Clear();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}