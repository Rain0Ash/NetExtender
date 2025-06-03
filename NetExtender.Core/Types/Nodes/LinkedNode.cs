// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Nodes.Interfaces;

namespace NetExtender.Types.Nodes
{
    public abstract class LinkedNode<T, TNode> : LinkedNode<TNode>, ILinkedNode<T, TNode> where TNode : LinkedNode<T, TNode>
    {
        protected virtual Boolean Unpack(TNode? node, [MaybeNullWhen(false)] out T value)
        {
            value = default;
            return false;
        }
        
        public virtual TNode? Find(T value)
        {
            return Find(value, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual TNode? Find(T value, IEqualityComparer<T>? comparer)
        {
            if (Head is not { } node)
            {
                return null;
            }
            
            if (value is not null)
            {
                comparer ??= EqualityComparer<T>.Default;
                while (node is not null)
                {
                    if (!Unpack(node, out T? result))
                    {
                        node = node.Next;
                        continue;
                    }
                    
                    if (comparer.Equals(value, result))
                    {
                        break;
                    }
                    
                    node = node.Next;
                    
                    if (ReferenceEquals(Head, node))
                    {
                        return null;
                    }
                }
                
                return node;
            }
            
            while (node is not null)
            {
                if (!Unpack(node, out T? result))
                {
                    node = node.Next;
                    continue;
                }
                
                if (result is null)
                {
                    break;
                }
                
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }
            
            return node;
        }

        public virtual TNode? Find(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Head is not { } node)
            {
                return null;
            }
            
            while (node is not null)
            {
                if (!Unpack(node, out T? result))
                {
                    node = node.Next;
                    continue;
                }
                
                if (predicate(result))
                {
                    break;
                }
                
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }
            
            return node;
        }

        public virtual TNode? FindLast(T value)
        {
            return FindLast(value, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual TNode? FindLast(T value, IEqualityComparer<T>? comparer)
        {
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            if (value is not null)
            {
                comparer ??= EqualityComparer<T>.Default;
                while (node is not null)
                {
                    if (!Unpack(node, out T? result))
                    {
                        node = node.Link;
                        continue;
                    }
                    
                    if (comparer.Equals(value, result))
                    {
                        break;
                    }
                    
                    node = node.Link;
                    
                    if (ReferenceEquals(node, link))
                    {
                        return null;
                    }
                }
                
                return node;
            }
            
            while (node is not null)
            {
                if (!Unpack(node, out T? result))
                {
                    node = node.Link;
                    continue;
                }
                
                if (result is null)
                {
                    break;
                }
                
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }
            
            return node;
        }

        public virtual TNode? FindLast(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            while (node is not null)
            {
                if (!Unpack(node, out T? result))
                {
                    node = node.Link;
                    continue;
                }
                
                if (predicate(result))
                {
                    break;
                }
                
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        public virtual TNode? FindPrevious(T value)
        {
            return FindPrevious(value, null);
        }
        
        public virtual TNode? FindPrevious(T value, IEqualityComparer<T>? comparer)
        {
            if (Previous is not { } current)
            {
                return null;
            }
            
            TNode? last = Last;
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Previous;
                    continue;
                }

                if (comparer.Equals(value, result))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));
            
            return null;
        }
        
        public virtual TNode? FindPrevious(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Previous is not { } current)
            {
                return null;
            }

            TNode? last = Last;
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Previous;
                    continue;
                }
                
                if (predicate(result))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));
            
            return null;
        }
        
        public virtual TNode? FindLastPrevious(T value)
        {
            return FindLastPrevious(value, null);
        }
        
        public virtual TNode? FindLastPrevious(T value, IEqualityComparer<T>? comparer)
        {
            if (First is not { } current)
            {
                return null;
            }
            
            comparer ??= EqualityComparer<T>.Default;

            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Next;
                    continue;
                }

                if (comparer.Equals(value, result))
                {
                    return current;
                }

                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, this));

            return null;
        }
        
        public virtual TNode? FindLastPrevious(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (First is not { } current)
            {
                return null;
            }
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Next;
                    continue;
                }
                
                if (predicate(result))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, this));
            
            return null;
        }
        
        public virtual TNode? FindNext(T value)
        {
            return FindNext(value, null);
        }
        
        public virtual TNode? FindNext(T value, IEqualityComparer<T>? comparer)
        {
            if (Next is not { } current)
            {
                return null;
            }
            
            TNode? head = Head;
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Next;
                    continue;
                }

                if (comparer.Equals(value, result))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        public virtual TNode? FindNext(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Next is not { } current)
            {
                return null;
            }
            
            TNode? head = Head;
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Next;
                    continue;
                }
                
                if (predicate(result))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        public virtual TNode? FindLastNext(T value)
        {
            return FindLastNext(value, null);
        }
        
        public virtual TNode? FindLastNext(T value, IEqualityComparer<T>? comparer)
        {
            if (Last is not { } current)
            {
                return null;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Previous;
                    continue;
                }
                
                if (comparer.Equals(value, result))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, this));
            
            return null;
        }
        
        public virtual TNode? FindLastNext(Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Last is not { } current)
            {
                return null;
            }
            
            do
            {
                if (!Unpack(current, out T? result))
                {
                    current = current.Previous;
                    continue;
                }
                
                if (predicate(result))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, this));
            
            return null;
        }
        
        IEnumerator<ILinkedNode<T, TNode>> IEnumerable<ILinkedNode<T, TNode>>.GetEnumerator()
        {
            IEnumerable<TNode> source = this;
            foreach (TNode node in source)
            {
                yield return node;
            }
        }
    }
    
    public abstract class LinkedNode<TNode> : LinkedNode, ILinkedNode<TNode> where TNode : LinkedNode<TNode>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator TNode?(LinkedNode<TNode>? value)
        {
            return (TNode?) value;
        }
        
        protected abstract TNode? Head { get; }

        /// <inheritdoc cref="System.Collections.Generic.LinkedList{T}.First"/>
        public virtual TNode? First
        {
            get
            {
                return null;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedList{T}.First"/>
        ILinkedNode? ILinkedNode.First
        {
            get
            {
                return First;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedList{T}.Last"/>
        public virtual TNode? Last
        {
            get
            {
                return null;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedList{T}.Last"/>
        ILinkedNode? ILinkedNode.Last
        {
            get
            {
                return Last;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedListNode{T}.Previous"/>
        public virtual TNode? Previous
        {
            get
            {
                return Link is not null && !ReferenceEquals(this, Head) ? Link : null;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedListNode{T}.Previous"/>
        ILinkedNode? ILinkedNode.Previous
        {
            get
            {
                return Previous;
            }
        }
        
        private TNode? _next;
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedListNode{T}.Next"/>
        public virtual TNode? Next
        {
            get
            {
                return _next is not null && !ReferenceEquals(_next, Head) ? _next : null;
            }
            protected internal set
            {
                _next = value;
            }
        }
        
        /// <inheritdoc cref="System.Collections.Generic.LinkedListNode{T}.Next"/>
        ILinkedNode? ILinkedNode.Next
        {
            get
            {
                return Next;
            }
        }
        
        protected internal TNode? Link { get; set; }
        
        protected internal virtual void Invalidate()
        {
            _next = null;
            Link = null;
        }
        
        public virtual TNode? Find(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Head is not { } node)
            {
                return null;
            }
            
            while (node is not null && !predicate(node))
            {
                node = node.Next;
                
                if (ReferenceEquals(Head, node))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        public virtual TNode? FindLast(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Head?.Link is not { } link)
            {
                return null;
            }
            
            TNode? node = link;
            while (node is not null && !predicate(node))
            {
                node = node.Link;
                
                if (ReferenceEquals(node, link))
                {
                    return null;
                }
            }
            
            return node;
        }
        
        public virtual TNode? FindPrevious(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Previous is not { } current)
            {
                return null;
            }
            
            TNode? last = Last;
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));
            
            return null;
        }
        
        public virtual TNode? FindLastPrevious(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (First is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, this));
            
            return null;
        }
        
        public virtual TNode? FindNext(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Next is not { } current)
            {
                return null;
            }
            
            TNode? head = Head;
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        public virtual TNode? FindLastNext(Predicate<TNode> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Last is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, this));
            
            return null;
        }
        
        protected virtual IEnumerator<TNode> GetNodeEnumerator()
        {
            TNode? source = this;
            while (source is not null)
            {
                TNode node = source;
                yield return node;
                source = source.Next;
            }
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return GetNodeEnumerator();
        }

        IEnumerator<ILinkedNode<TNode>> IEnumerable<ILinkedNode<TNode>>.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator<ILinkedNode> ILinkedNode.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public abstract class LinkedNode
    {
    }
}