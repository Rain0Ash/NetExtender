using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.History
{
    public abstract partial class TimeHistoryCollection<T>
    {
        public readonly struct Node : IStruct<Node>, INode, IComparable
        {
            public static implicit operator T(Node node)
            {
                return node.Value;
            }
            
            public static implicit operator DateTime(Node node)
            {
                return node.Time.LocalDateTime;
            }

            public static implicit operator DateTimeOffset(Node node)
            {
                return node.Time;
            }

            public static implicit operator (DateTimeOffset Time, T Value)(Node node)
            {
                return (node, node);
            }

            public static Boolean operator ==(Node first, Node second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Node first, Node second)
            {
                return !(first == second);
            }

            internal TimeHistoryCollection<T> History { get; }

            private NodeComparer Comparer
            {
                get
                {
                    return History.Comparer;
                }
            }

            private readonly DateTimeOffset? _time;
            public DateTimeOffset Time
            {
                get
                {
                    return _time ?? default;
                }
            }

            public T Value { get; }

            internal Boolean IsUniversal
            {
                get
                {
                    return !IsEmpty && !_time.HasValue;
                }
            }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return History is null;
                }
            }

            internal Node(TimeHistoryCollection<T> history, T value)
                : this(history, value, DateTimeOffset.Now)
            {
            }

            internal Node(TimeHistoryCollection<T> history, T value, DateTimeOffset time)
                : this(history, value, (DateTimeOffset?) time)
            {
            }

            private Node(TimeHistoryCollection<T> history, T value, DateTimeOffset? time)
            {
                History = history ?? throw new ArgumentNullException(nameof(history));
                Value = value;
                _time = time;
            }

            internal static Node Universal(TimeHistoryCollection<T> history, T value)
            {
                return new Node(history, value, null);
            }

            internal Boolean Is(TimeHistoryCollection<T>? value)
            {
                return value is not null && ReferenceEquals(History, value);
            }

            internal Boolean Is(Node node)
            {
                return Is(node.History);
            }

            public Int32 CompareTo(Object? other)
            {
                return other switch
                {
                    null => IsEmpty ? 0 : CompareTo(default(T)),
                    T value => CompareTo(value),
                    DateTime value => CompareTo(value),
                    DateTimeOffset value => CompareTo(value),
                    Node value => CompareTo(value),
                    _ => 0
                };
            }

            public Int32 CompareTo(T? other)
            {
                return Comparer.Compare(Value, other);
            }

            public Int32 CompareTo(DateTime other)
            {
                return Comparer.Compare(Time, other);
            }

            public Int32 CompareTo(DateTimeOffset other)
            {
                return Comparer.Compare(Time, other);
            }

            public Int32 CompareTo(Node other)
            {
                return Comparer.Compare(Time, other.Time);
            }

            public override Int32 GetHashCode()
            {
                return Comparer.GetHashCode(this);
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    T value => Equals(value),
                    DateTime value => Equals(value),
                    DateTimeOffset value => Equals(value),
                    Node value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(T? other)
            {
                return Comparer.Equals(Value, other);
            }

            public Boolean Equals(DateTime other)
            {
                return Comparer.Equals(Time, other);
            }

            public Boolean Equals(DateTimeOffset other)
            {
                return Comparer.Equals(Time, other);
            }

            public Boolean Equals(Node other)
            {
                return Comparer.Equals(this, other);
            }

            public override String ToString()
            {
                return !IsEmpty ? $"{{ {nameof(Time)}: {Time}, {nameof(Value)}: {Value} }}" : String.Empty;
            }
        }

        protected class NodeComparer : Comparer<Node>, IComparer<DateTime>, IComparer<DateTimeOffset>, IEqualityComparer<DateTime>, IEqualityComparer<DateTimeOffset>, IEqualityComparer<Node>
        {
            public new static NodeComparer Default
            {
                get
                {
                    return Seal.Default;
                }
            }

            private IComparer<T> Comparer { get; }

            private NodeComparer(IComparer<T>? comparer)
            {
                Comparer = comparer ?? Comparer<T>.Default;
            }

            [return: NotNullIfNotNull("comparer")]
            public static NodeComparer? Create(IComparer<T>? comparer)
            {
                return comparer is not null ? new Seal(comparer) : null;
            }

            public virtual Int32 Compare(T? x, T? y)
            {
                try
                {
                    return Comparer.Compare(x, y);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            public virtual Int32 Compare(DateTime x, DateTime y)
            {
                return x.CompareTo(y);
            }

            public virtual Int32 Compare(DateTimeOffset x, DateTimeOffset y)
            {
                return x.CompareTo(y);
            }

            public override Int32 Compare(Node x, Node y)
            {
                Int32 compare = x.IsUniversal || y.IsUniversal ? 0 : Compare(x.Time, y.Time);
                return compare != 0 ? compare : Compare(x.Value, y.Value);
            }

            public virtual Int32 GetHashCode(T? value)
            {
                return HashCode.Combine(value);
            }

            public virtual Boolean Equals(T? x, T? y)
            {
                try
                {
                    return Comparer.Compare(x, y) == 0;
                }
                catch (Exception)
                {
                    return EqualityComparer<T>.Default.Equals(x, y);
                }
            }

            public virtual Int32 GetHashCode(DateTime value)
            {
                return value.GetHashCode();
            }

            public virtual Boolean Equals(DateTime x, DateTime y)
            {
                return x == y;
            }

            public virtual Int32 GetHashCode(DateTimeOffset value)
            {
                return value.GetHashCode();
            }

            public virtual Boolean Equals(DateTimeOffset x, DateTimeOffset y)
            {
                return x == y;
            }

            public virtual Int32 GetHashCode(Node value)
            {
                return HashCode.Combine(value.History, value.Value);
            }

            public virtual Boolean Equals(Node x, Node y)
            {
                return x.Is(y) && (x.IsUniversal || y.IsUniversal || Equals(x.Time, y.Time)) && Equals(x.Value, y.Value);
            }

            private sealed class Seal : NodeComparer
            {
                // ReSharper disable once MemberHidesStaticFromOuterClass
                public new static NodeComparer Default { get; } = new NodeComparer(null);
                
                public Seal(IComparer<T>? comparer)
                    : base(comparer)
                {
                }
            }
        }
    }
}