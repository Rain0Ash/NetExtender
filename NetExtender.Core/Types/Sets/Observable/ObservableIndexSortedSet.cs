// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Sets
{
    /*public sealed class ObservableIndexSortedSet<T> : ObservableIndexSortedSetAbstraction<T>
    {
        public ObservableIndexSortedSet()
        {
        }

        public ObservableIndexSortedSet(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableIndexSortedSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableIndexSortedSet(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        private ObservableIndexSortedSet(IndexSortedSet<T> set)
            : base(set)
        {
        }
        
        public override ObservableIndexSortedSet<T> GetViewBetween(T? lower, T? upper)
        {
            return new ObservableIndexSortedSet<T>(Internal.GetViewBetween(lower, upper));
        }
    }

    public abstract class ObservableIndexSortedSetAbstraction<T> : ObservableIndexSortedSet<T, IndexSortedSet<T>>
    {
        public ObservableIndexSortedSetAbstraction()
            : base(new IndexSortedSet<T>())
        {
        }

        public ObservableIndexSortedSetAbstraction(IComparer<T>? comparer)
            : base(new IndexSortedSet<T>(comparer))
        {
        }

        public ObservableIndexSortedSetAbstraction(IEnumerable<T> collection)
            : base(collection is not null ? new IndexSortedSet<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableIndexSortedSetAbstraction(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection is not null ? new IndexSortedSet<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        protected ObservableIndexSortedSetAbstraction(IndexSortedSet<T> set)
            : base(set)
        {
        }
        
        protected sealed override IndexSortedSet<T> CloneInternal()
        {
            return new IndexSortedSet<T>(Internal, Internal.Comparer);
        }
    }

    public abstract class ObservableIndexSortedSet<T, TSet> : ObservableSet<T, TSet> where TSet : IndexSortedSet<T>
    {
        public virtual IComparer<T> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        public T? Min
        {
            get
            {
                return Internal.Min;
            }
        }

        public T? Max
        {
            get
            {
                return Internal.Max;
            }
        }

        protected ObservableIndexSortedSet(TSet set)
            : base(set)
        {
        }

        protected override State Store()
        {
            return new State(this);
        }

        protected sealed override void Handle(Step step, ObservableSet<T, TSet>.State? state)
        {
            if (state is State @new)
            {
                Handle(step, @new);
                return;
            }
            
            base.Handle(step, state);
        }
        
        protected virtual void Handle(Step step, State? state)
        {
            if (state is null)
            {
                base.Handle(step, state);
                return;
            }

            switch (step)
            {
                case Step.Changing:
                {
                    if (state.Min)
                    {
                        state.PropertyChanging(nameof(Min));
                    }
                    
                    if (state.Max)
                    {
                        state.PropertyChanging(nameof(Max));
                    }
                    
                    goto default;
                }
                case Step.Collection:
                {
                    if (state.Min)
                    {
                        state.PropertyChanged(nameof(Min));
                    }
                    
                    if (state.Max)
                    {
                        state.PropertyChanged(nameof(Max));
                    }
                    
                    goto default;
                }
                default:
                    base.Handle(step, state);
                    return;
            }
        }

        public Boolean ContainsIndex(Int32 index)
        {
            return Internal.ContainsIndex(index);
        }

        public Int32 IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }

        public T? ValueOf(Int32 index)
        {
            return Internal.ValueOf(index);
        }

        public Boolean ValueOf(Int32 index, [MaybeNullWhen(false)] out T value)
        {
            return Internal.ValueOf(index, out value);
        }

        public virtual Boolean TryGetValue(T equal, [MaybeNullWhen(false)] out T actual)
        {
            return Internal.TryGetValue(equal, out actual);
        }

        public abstract ObservableSet<T, TSet> GetViewBetween(T? lower, T? upper);

        public virtual Int32 RemoveWhere(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            TSet @internal = Internal;
            TSet set = CloneInternal();
            Int32 count = set.RemoveWhere(match);

            if (count <= 0)
            {
                return 0;
            }

            State state = Store();
            state.Next = set;
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Invoke();
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            return state.Return(count);
        }

        public virtual IEnumerable<T> Reverse()
        {
            return Internal.Reverse();
        }

        public virtual void CopyTo(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array);
        }

        public virtual void CopyTo(T[] array, Int32 index, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index, count);
        }

        public sealed override IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        public T this[Int32 index]
        {
            get
            {
                return Internal[index];
            }
            set
            {
                State state = Store();
                state.OldItems = new List<T>(1) { Internal[index] };
                state.NewItems = new List<T>(1) { value };
                state.Invoke();
                Internal[index] = value;
                state.CollectionChanged();
                state.Return();
            }
        }
        
        //TODO: пофиксить. сломается при отсутствии изменения Next. Не забыть про обычный SortedSet
        protected new record State : ObservableSet<T, TSet>.State
        {
            public Boolean Min { get; set; }
            public Boolean Max { get; set; }

            public override TSet? Next
            {
                get
                {
                    return base.Next;
                }
                set
                {
                    if (value is null)
                    {
                        base.Next = value;
                        return;
                    }
                    
                    Min = value.Comparer.Compare(Current.Min, value.Min) != 0 || !EqualityComparer<T>.Default.Equals(Current.Min, value.Min);
                    Max = value.Comparer.Compare(Current.Max, value.Max) != 0 || !EqualityComparer<T>.Default.Equals(Current.Max, value.Max);
                    base.Next = value;
                }
            }

            public State(ObservableIndexSortedSet<T, TSet> set)
                : base(set)
            {
            }
        }
    }*/
}