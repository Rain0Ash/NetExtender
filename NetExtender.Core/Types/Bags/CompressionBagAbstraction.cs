namespace NetExtender.Types.Bags
{
    /*public abstract class CompressionBagAbstraction<T> : ICompressionBag<T>, IReadOnlyCompressionBag<T>, INotifyCollectionChanged where T : notnull
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        
        public abstract Constraint Constraints { get; }
        
        ICounter64<T> ICompressionBag<T>.Constraints
        {
            get
            {
                return Constraints;
            }
        }

        IReadOnlyCounter64<T> IReadOnlyCompressionBag<T>.Constraints
        {
            get
            {
                return Constraints;
            }
        }
        
        public abstract IReadOnlyList<T> Bag { get; }
        protected Decimal Budget { get; set; }
        protected Func<T, Decimal> Converter { get; }

        public Int32 Count
        {
            get
            {
                
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        protected CompressionBagAbstraction(Func<T, Decimal> converter)
        {
            Converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        protected virtual void Update()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public virtual Boolean TryGetValue(Int32 index, out T result)
        {
            
        }

        protected Boolean Build([MaybeNullWhen(false)] out ImmutableList<T> result)
        {
            if (!Build(out ImmutableSortedCounter<T, Int64> counter))
            {
                result = ImmutableList<T>.Empty;
                return false;
            }

            T[]? array = counter.ToItemsArray();
            result = array?.ToImmutableList();
            return result is not null;
        }

        protected virtual Boolean Build(out ImmutableSortedCounter<T, Int64> result)
        {
            SortedCounter64<T> counter = new SortedCounter64<T>(Constraints.Comparer);
            
            Decimal budget = Budget;
            foreach ((T item, Int64 quantity) in Constraints)
            {
                Decimal cost = Converter(item);
                Int64 afford = cost > 0 ? (Int64) Math.Min(budget / cost, quantity) : 0;

                counter.Add(item, afford);
                budget -= cost * afford;
            }

            result = ImmutableSortedCounter.CreateRange(counter.Comparer, counter);
            return result.Count > 0;
        }
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            Decimal budget = Budget;
            
            foreach ((T item, Int64 quantity) in Constraints)
            {
                Decimal cost = Converter(item);
                Decimal afford = cost > 0 ? Math.Min(budget / cost, quantity) : 0;

                for (Int32 i = 0; i < afford; i++)
                {
                    if (budget < cost)
                    {
                        break;
                    }

                    budget -= cost;
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract class Constraint : ICounter64<T>, IReadOnlyCounter64<T>
        {
            protected abstract CompressionBagAbstraction<T> Bag { get; }
            protected abstract SortedCounter64<T> Internal { get; }

            public IComparer<T> Comparer
            {
                get
                {
                    return Internal.Comparer;
                }
            }
            
            public Int32 Count
            {
                get
                {
                    return Internal.Count;
                }
            }

            Boolean ICollection<KeyValuePair<T, Int64>>.IsReadOnly
            {
                get
                {
                    return ((ICollection<KeyValuePair<T, Int64>>) Internal).IsReadOnly;
                }
            }

            public IEnumerable<T> Keys
            {
                get
                {
                    return Internal.Keys;
                }
            }

            ICollection<T> IDictionary<T, Int64>.Keys
            {
                get
                {
                    return ((IDictionary<T, Int64>) Internal).Keys;
                }
            }

            public IEnumerable<Int64> Values
            {
                get
                {
                    return Internal.Values;
                }
            }

            ICollection<Int64> IDictionary<T, Int64>.Values
            {
                get
                {
                    return ((IDictionary<T, Int64>) Internal).Values;
                }
            }

            Boolean IDictionary<T, Int64>.ContainsKey(T key)
            {
                return Contains(key);
            }

            Boolean IReadOnlyDictionary<T, Int64>.ContainsKey(T key)
            {
                return Contains(key);
            }

            public Boolean Contains(T item)
            {
                return Internal.Contains(item);
            }

            public Boolean Contains(T item, Int64 count)
            {
                return Internal.Contains(item, count);
            }

            public Boolean TryGetValue(T item, out Int64 value)
            {
                return Internal.TryGetValue(item, out value);
            }

            Boolean ICollection<KeyValuePair<T, Int64>>.Contains(KeyValuePair<T, Int64> item)
            {
                return Contains(item.Key, item.Value);
            }

            Int64 ICounter<T, Int64>.Add(T item)
            {
                return Add(item, 1);
            }

            public Int64 Add(T item, Int64 value)
            {
                value = Internal.Add(item, value);
                Bag.Update();
                return value;
            }
            
            void IDictionary<T, Int64>.Add(T key, Int64 value)
            {
                Add(key, value);
            }

            void ICollection<KeyValuePair<T, Int64>>.Add(KeyValuePair<T, Int64> item)
            {
                Add(item.Key, item.Value);
            }

            Boolean ICounter<T, Int64>.TryAdd(T item)
            {
                return TryAdd(item, 1, out _);
            }

            public Boolean TryAdd(T item, Int64 count)
            {
                return TryAdd(item, count, out _);
            }

            Boolean ICounter<T, Int64>.TryAdd(T item, out Int64 result)
            {
                return TryAdd(item, 1, out result);
            }

            protected Boolean TryAdd(T item, Int64 count, out Int64 result)
            {
                if (!Internal.TryAdd(item, count, out result))
                {
                    return false;
                }

                Bag.Update();
                return true;
            }

            Boolean ICounter<T, Int64>.TryAdd(T item, Int64 count, out Int64 result)
            {
                return TryAdd(item, count, out result);
            }

            Boolean ICounter<T, Int64>.AddRange(IEnumerable<T> source)
            {
                if (!Internal.AddRange(source))
                {
                    return false;
                }
                
                Bag.Update();
                return true;
            }

            public Boolean AddRange(IEnumerable<KeyValuePair<T, Int64>> source)
            {
                if (!Internal.AddRange(source))
                {
                    return false;
                }
                
                Bag.Update();
                return true;
            }
            
            Boolean IDictionary<T, Int64>.Remove(T key)
            {
                return Remove(key, 1);
            }

            Boolean ICounter<T, Int64>.Remove(T item)
            {
                return Remove(item, 1);
            }

            public Boolean Remove(T item, Int64 count)
            {
                return Remove(item, count, out _);
            }

            Boolean ICounter<T, Int64>.Remove(T item, out Int64 result)
            {
                return Remove(item, 1, out result);
            }

            public Boolean Remove(T item, Int64 count, out Int64 result)
            {
                if (!Internal.Remove(item, count, out result))
                {
                    return false;
                }

                Bag.Update();
                return true;
            }

            Boolean ICollection<KeyValuePair<T, Int64>>.Remove(KeyValuePair<T, Int64> item)
            {
                return Remove(item.Key, item.Value);
            }

            Boolean ICounter<T, Int64>.RemoveRange(IEnumerable<T> source)
            {
                if (!Internal.RemoveRange(source))
                {
                    return false;
                }
                
                Bag.Update();
                return true;
            }

            public Boolean RemoveRange(IEnumerable<KeyValuePair<T, Int64>> source)
            {
                if (!Internal.RemoveRange(source))
                {
                    return false;
                }
                
                Bag.Update();
                return true;
            }

            public void Clear()
            {
                Internal.Clear();
                Bag.Update();
            }

            public Boolean Clear(T item)
            {
                if (!Internal.Clear(item))
                {
                    return false;
                }

                Bag.Update();
                return true;
            }

            public Boolean Clear(IEnumerable<T> source)
            {
                if (!Internal.Clear(source))
                {
                    return false;
                }

                Bag.Update();
                return true;
            }

            public void CopyTo(KeyValuePair<T, Int64>[] array, Int32 arrayIndex)
            {
                Internal.CopyTo(array, arrayIndex);
            }

            public KeyValuePair<T, Int64>[] ToArray()
            {
                return Internal.ToArray();
            }

            public T[]? ToItemsArray()
            {
                return Internal.ToItemsArray();
            }

            public T[]? ToItemsArray(Int32 length)
            {
                return Internal.ToItemsArray(length);
            }

            public IEnumerable<T> Enumerate()
            {
                return Internal.Enumerate();
            }

            public IEnumerator<KeyValuePair<T, Int64>> GetEnumerator()
            {
                return Internal.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Int64 this[T item]
            {
                get
                {
                    return Internal[item];
                }
                set
                {
                    Internal[item] = value;
                }
            }
        }
    }*/
}