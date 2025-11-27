using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers
{
    public abstract class KeyValueComparer<TKey, TValue> : IComparer<(TKey, TValue)>, IComparer<Tuple<TKey, TValue>>, IComparer<KeyValuePair<TKey, TValue>>
    {
        public static KeyValueComparer<TKey, TValue> DefaultKey
        {
            get
            {
                return Key.Default;
            }
        }
        
        public static KeyValueComparer<TKey, TValue> DefaultValue
        {
            get
            {
                return Value.Default;
            }
        }

        public sealed class Key : KeyValueComparer<TKey, TValue>
        {
            public static KeyValueComparer<TKey, TValue> Default { get; } = new Key(Comparer<TKey>.Default, Comparer<TValue>.Default);
            
            public IComparer<TKey> Comparer { get; }
            public IComparer<TValue>? Next { get; }

            public Key(IComparer<TKey>? comparer)
                : this(comparer, null)
            {
            }

            public Key(IComparer<TKey>? comparer, IComparer<TValue>? next)
            {
                Comparer = comparer ?? Comparer<TKey>.Default;
                Next = next;
            }
            
            public override Int32 Compare((TKey, TValue) x, (TKey, TValue) y)
            {
                Int32 compare = Comparer.Compare(x.Item1, y.Item1);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Item2, y.Item2);
                }
                
                return compare;
            }

            public override Int32 Compare(Tuple<TKey, TValue>? x, Tuple<TKey, TValue>? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (x is null)
                {
                    return -1;
                }

                if (y is null)
                {
                    return 1;
                }
                
                Int32 compare = Comparer.Compare(x.Item1, y.Item1);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Item2, y.Item2);
                }
                
                return compare;
            }

            public override Int32 Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                Int32 compare = Comparer.Compare(x.Key, y.Key);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Value, y.Value);
                }
                
                return compare;
            }
        }
        
        public sealed class Value : KeyValueComparer<TKey, TValue>
        {
            public static KeyValueComparer<TKey, TValue> Default { get; } = new Key(Comparer<TKey>.Default, Comparer<TValue>.Default);
            
            public IComparer<TValue> Comparer { get; }
            public IComparer<TKey>? Next { get; }

            public Value(IComparer<TValue>? comparer)
                : this(comparer, null)
            {
            }

            public Value(IComparer<TValue>? comparer, IComparer<TKey>? next)
            {
                Comparer = comparer ?? Comparer<TValue>.Default;
                Next = next;
            }
            
            public override Int32 Compare((TKey, TValue) x, (TKey, TValue) y)
            {
                Int32 compare = Comparer.Compare(x.Item2, y.Item2);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Item1, y.Item1);
                }
                
                return compare;
            }

            public override Int32 Compare(Tuple<TKey, TValue>? x, Tuple<TKey, TValue>? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (x is null)
                {
                    return -1;
                }

                if (y is null)
                {
                    return 1;
                }
                
                Int32 compare = Comparer.Compare(x.Item2, y.Item2);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Item1, y.Item1);
                }
                
                return compare;
            }

            public override Int32 Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                Int32 compare = Comparer.Compare(x.Value, y.Value);
                
                if (compare == 0 && Next is not null)
                {
                    return Next.Compare(x.Key, y.Key);
                }
                
                return compare;
            }
        }

        public abstract Int32 Compare((TKey, TValue) x, (TKey, TValue) y);
        public abstract Int32 Compare(Tuple<TKey, TValue>? x, Tuple<TKey, TValue>? y);
        public abstract Int32 Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y);
    }
}