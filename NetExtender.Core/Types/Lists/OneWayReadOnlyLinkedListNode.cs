// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.LinkedLists
{
    public readonly struct OneWayReadOnlyLinkedListNode<T> : ILinkedListNode<T>, IEnumerable<OneWayReadOnlyLinkedListNode<T>>, IEnumerable<ILinkedListNode<T>>, IStruct<OneWayReadOnlyLinkedListNode<T>>
    {
        public static implicit operator OneWayReadOnlyLinkedListNode<T>(LinkedListNode<T>? value)
        {
            return value is not null ? new OneWayReadOnlyLinkedListNode<T>(value) : default;
        }
        
        public static implicit operator LinkedListNode<T>?(OneWayReadOnlyLinkedListNode<T> value)
        {
            return value.Internal;
        }
        
        public static Boolean operator ==(OneWayReadOnlyLinkedListNode<T> first, OneWayReadOnlyLinkedListNode<T> second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(OneWayReadOnlyLinkedListNode<T> first, OneWayReadOnlyLinkedListNode<T> second)
        {
            return !(first == second);
        }
        
        internal LinkedListNode<T>? Internal { get; }
        
        public T Value
        {
            get
            {
                return Internal is not null ? Internal.Value : throw new InvalidOperationException();
            }
            set
            {
                if (Internal is null)
                {
                    throw new InvalidOperationException();
                }

                Internal.Value = value;
            }
        }
        
        public ref T ValueRef
        {
            get
            {
                if (Internal is null)
                {
                    throw new InvalidOperationException();
                }
                
                return ref Internal.ValueRef;
            }
        }
        
        public OneWayReadOnlyLinkedListNode<T>? First
        {
            get
            {
                return Internal?.List?.First;
            }
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.First
        {
            get
            {
                return First;
            }
        }
        
        ILinkedListNode? ILinkedListNode.First
        {
            get
            {
                return First;
            }
        }
        
        ILinkedNode? ILinkedNode.First
        {
            get
            {
                return First;
            }
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Last
        {
            get
            {
                return Internal?.List?.Last;
            }
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Last
        {
            get
            {
                return Last;
            }
        }
        
        ILinkedListNode? ILinkedListNode.Last
        {
            get
            {
                return Last;
            }
        }
        
        ILinkedNode? ILinkedNode.Last
        {
            get
            {
                return Last;
            }
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Previous
        {
            get
            {
                return null;
            }
        }
        
        ILinkedListNode? ILinkedListNode.Previous
        {
            get
            {
                return null;
            }
        }
        
        ILinkedNode? ILinkedNode.Previous
        {
            get
            {
                return null;
            }
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Next
        {
            get
            {
                return Internal?.Next;
            }
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Next
        {
            get
            {
                return Next;
            }
        }
        
        ILinkedListNode? ILinkedListNode.Next
        {
            get
            {
                return Next;
            }
        }
        
        ILinkedNode? ILinkedNode.Next
        {
            get
            {
                return Next;
            }
        }
        
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal is null;
            }
        }
        
        public OneWayReadOnlyLinkedListNode(LinkedListNode<T> value)
        {
            Internal = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Find(T value)
        {
            return Internal.Find(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Find(T value)
        {
            return Find(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Find(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.Find(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Find(T value, IEqualityComparer<T>? comparer)
        {
            return Find(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Find(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.Find(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Find(Predicate<T> predicate)
        {
            return Find(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? Find(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.Find(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.Find(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Find(node => predicate(node));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLast(T value)
        {
            return Internal.FindLast(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(T value)
        {
            return FindLast(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.FindLast(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(T value, IEqualityComparer<T>? comparer)
        {
            return FindLast(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLast(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLast(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(Predicate<T> predicate)
        {
            return FindLast(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLast(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLast(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLast(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return FindLast(node => predicate(node));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindPrevious(T value)
        {
            return Internal.FindPrevious(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(T value)
        {
            return FindPrevious(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.FindPrevious(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return FindPrevious(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindPrevious(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindPrevious(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(Predicate<T> predicate)
        {
            return FindPrevious(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindPrevious(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindPrevious(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindPrevious(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return FindPrevious(node => predicate(node));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastPrevious(T value)
        {
            return Internal.FindLastPrevious(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(T value)
        {
            return FindLastPrevious(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.FindLastPrevious(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(T value, IEqualityComparer<T>? comparer)
        {
            return FindLastPrevious(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastPrevious(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLastPrevious(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(Predicate<T> predicate)
        {
            return FindLastPrevious(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastPrevious(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLastPrevious(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastPrevious(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return FindLastPrevious(node => predicate(node));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindNext(T value)
        {
            return Internal.FindNext(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(T value)
        {
            return FindNext(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindNext(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.FindNext(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(T value, IEqualityComparer<T>? comparer)
        {
            return FindNext(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindNext(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindNext(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(Predicate<T> predicate)
        {
            return FindNext(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindNext(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindNext(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindNext(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return FindNext(node => predicate(node));
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastNext(T value)
        {
            return Internal.FindLastNext(value) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(T value)
        {
            return FindLastNext(value);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastNext(T value, IEqualityComparer<T>? comparer)
        {
            return Internal.FindLastNext(value, comparer) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(T value, IEqualityComparer<T>? comparer)
        {
            return FindLastNext(value, comparer);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastNext(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLastNext(predicate) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(Predicate<T> predicate)
        {
            return FindLastNext(predicate);
        }
        
        public OneWayReadOnlyLinkedListNode<T>? FindLastNext(Predicate<OneWayReadOnlyLinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return Internal.FindLastNext(node => predicate(node)) is { } result ? new OneWayReadOnlyLinkedListNode<T>(result) : null;
        }
        
        ILinkedListNode<T>? ILinkedListNode<T>.FindLastNext(Predicate<ILinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return FindLastNext(node => predicate(node));
        }
        
        public override Int32 GetHashCode()
        {
            return Internal?.GetHashCode() ?? 0;
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Internal is null,
                T value => Equals(value),
                LinkedListNode<T> value => Equals(value),
                OneWayLinkedListNode<T> value => Equals(value),
                TwoWayLinkedListNode<T> value => Equals(value),
                OneWayReadOnlyLinkedListNode<T> value => Equals(value),
                TwoWayReadOnlyLinkedListNode<T> value => Equals(value),
                ILinkedListNode<T> value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(T? other)
        {
            return Internal is not null && EqualityComparer<T>.Default.Equals(Internal.ValueRef, other);
        }

        public Boolean Equals(LinkedListNode<T>? other)
        {
            return Internal is null && other is null || Internal is not null && Internal.Equals(other);
        }

        public Boolean Equals(ILinkedListNode<T>? other)
        {
            return Internal is null && other is null || Internal is not null && other is not null && Equals(other.Value);
        }
        
        Boolean ILinkedListNode.Equals(ILinkedListNode? other)
        {
            return Equals(other as ILinkedListNode<T>);
        }

        public Boolean Equals(OneWayLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }

        public Boolean Equals(TwoWayLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }

        public Boolean Equals(OneWayReadOnlyLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }

        public Boolean Equals(TwoWayReadOnlyLinkedListNode<T> other)
        {
            return Equals(other.Internal);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        IEnumerator<OneWayReadOnlyLinkedListNode<T>> IEnumerable<OneWayReadOnlyLinkedListNode<T>>.GetEnumerator()
        {
            LinkedListNode<T>? source = Internal;
            while (source is not null)
            {
                yield return source;
                source = source.Next;
            }
        }
        
        IEnumerator<ILinkedListNode<T>> IEnumerable<ILinkedListNode<T>>.GetEnumerator()
        {
            LinkedListNode<T>? source = Internal;
            while (source is not null)
            {
                OneWayReadOnlyLinkedListNode<T> node = source;
                yield return node;
                source = source.Next;
            }
        }

        IEnumerator<ILinkedNode> ILinkedNode.GetEnumerator()
        {
            LinkedListNode<T>? source = Internal;
            while (source is not null)
            {
                OneWayReadOnlyLinkedListNode<T> node = source;
                yield return node;
                source = source.Next;
            }
        }

        IEnumerator<ILinkedListNode> ILinkedListNode.GetEnumerator()
        {
            LinkedListNode<T>? source = Internal;
            while (source is not null)
            {
                OneWayReadOnlyLinkedListNode<T> node = source;
                yield return node;
                source = source.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}