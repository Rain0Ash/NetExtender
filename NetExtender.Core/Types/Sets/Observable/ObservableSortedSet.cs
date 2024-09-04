using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Sets
{
    public sealed class ObservableSortedSet<T> : ObservableSortedSetAbstraction<T>
    {
        public ObservableSortedSet()
        {
        }

        public ObservableSortedSet(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableSortedSet(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableSortedSet(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        private ObservableSortedSet(SortedSetCollection<T> set)
            : base(set)
        {
        }
        
        public override ObservableSortedSet<T> GetViewBetween(T? lower, T? upper)
        {
            return new ObservableSortedSet<T>(Internal.GetViewBetween(lower, upper));
        }
    }

    public abstract class ObservableSortedSetAbstraction<T> : ObservableSortedSet<T, SortedSetCollection<T>>
    {
        public ObservableSortedSetAbstraction()
            : base(new SortedSetCollection<T>())
        {
        }

        public ObservableSortedSetAbstraction(IComparer<T>? comparer)
            : base(new SortedSetCollection<T>(comparer))
        {
        }

        public ObservableSortedSetAbstraction(IEnumerable<T> collection)
            : base(collection is not null ? new SortedSetCollection<T>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        public ObservableSortedSetAbstraction(IEnumerable<T> collection, IComparer<T>? comparer)
            : base(collection is not null ? new SortedSetCollection<T>(collection, comparer) : throw new ArgumentNullException(nameof(collection)))
        {
        }

        protected ObservableSortedSetAbstraction(SortedSetCollection<T> set)
            : base(set)
        {
        }
        
        protected sealed override SortedSetCollection<T> CloneInternal()
        {
            return new SortedSetCollection<T>(Internal, Internal.Comparer);
        }
    }

    public abstract class ObservableSortedSet<T, TSet> : ObservableSet<T, TSet> where TSet : SortedSet<T>
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

        protected ObservableSortedSet(TSet set)
            : base(set)
        {
        }

        protected override State Factory()
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
                case Step.Prepared:
                {
                    if (state.Min.HasDifference())
                    {
                        state.PropertyChanging(nameof(Min));
                    }
                    
                    if (state.Max.HasDifference())
                    {
                        state.PropertyChanging(nameof(Max));
                    }
                    
                    goto default;
                }
                case Step.Collection:
                {
                    if (state.Min.HasDifference())
                    {
                        state.PropertyChanged(nameof(Min));
                    }
                    
                    if (state.Max.HasDifference())
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

            State state = Factory();
            state.Invoke(false);
            state.Next = set;
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Prepare();
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

        public virtual void CopyTo(T[] array, Int32 arrayIndex, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, arrayIndex, count);
        }

        public new virtual SortedSet<T>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        protected new record State : ObservableSet<T, TSet>.State
        {
            public State<T?> Min { get; protected set; }
            public State<T?> Max { get; protected set; }

            public State(ObservableSortedSet<T, TSet> set)
                : base(set)
            {
            }

            public override void Prepare()
            {
                Min = new State<T?>(Current.Min);
                Max = new State<T?>(Current.Max);
                
                if (Next is { } next)
                {
                    Min = Min.Update(next.Min);
                    Max = Max.Update(next.Max);
                }
                
                base.Prepare();
            }

            protected override Boolean Update<TArgument>(TSet @internal, TSet reference, TArgument argument, List<T>? @new, List<T>? old)
            {
                if (Next is not null)
                {
                    return base.Update(@internal, reference, argument, @new, old);
                }
                
                Min = Min.Update(reference.Min);
                Max = Max.Update(reference.Max);
                
                return base.Update(@internal, reference, argument, @new, old);
            }
        }
    }
}