using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NetExtender.Types.Collections;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public abstract class ObservableSet<T, TSet> : ObservableCollectionAbstraction<T, TSet>, ISet, IObservableSet<T>, IReadOnlyObservableSet<T> where TSet : ISet<T>
    {
        protected ObservableSet(TSet set)
            : base(set)
        {
        }

        protected override State Store()
        {
            return new State(this);
        }

        protected sealed override void Handle(Step step, ObservableCollectionAbstraction<T, TSet>.State? state)
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
            base.Handle(step, state);
        }

        public override Boolean Add(T item)
        {
            if (Internal.Contains(item))
            {
                return false;
            }

            State state = Store();
            state.Invoke();
            state.PropertyChanging(nameof(Count));
            
            static Boolean Handler(ref TSet @internal, T argument)
            {
                return @internal.Add(argument);
            }

            if (!state.Update(static (ref TSet set, T argument, ref List<T>? _, ref List<T>? _) => Handler(ref set, argument), item))
            {
                return state.Fallback();
            }
            
            state.CollectionChanged(NotifyCollectionChangedAction.Add, item);
            state.PropertyChanged(nameof(Count));
            return state.Return();
        }

        public virtual Boolean IsSubsetOf(IEnumerable<T> other)
        {
            return Internal.IsSubsetOf(other);
        }

        public virtual Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            return Internal.IsProperSubsetOf(other);
        }

        public virtual Boolean IsSupersetOf(IEnumerable<T> other)
        {
            return Internal.IsSupersetOf(other);
        }

        public virtual Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            return Internal.IsProperSupersetOf(other);
        }

        public virtual Boolean Overlaps(IEnumerable<T> other)
        {
            return Internal.Overlaps(other);
        }

        public virtual Boolean SetEquals(IEnumerable<T> other)
        {
            return Internal.SetEquals(other);
        }

        public virtual void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            TSet @internal = Internal;
            TSet set = CloneInternal();
            set.UnionWith(other);

            if (set.Count == @internal.Count)
            {
                return;
            }

            State state = Store();
            state.Invoke(false);
            state.Next = set;
            state.NewItems = set.Where(item => !@internal.Contains(item)).ToList();
            state.Prepare();
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            state.Return();
        }

        public virtual void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            TSet @internal = Internal;
            TSet set = CloneInternal();
            set.IntersectWith(other);

            if (set.Count == @internal.Count)
            {
                return;
            }

            State state = Store();
            state.Invoke(false);
            state.Next = set;
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Prepare();
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            state.Return();
        }

        public virtual void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            TSet @internal = Internal;
            TSet set = CloneInternal();
            set.ExceptWith(other);

            if (set.Count == @internal.Count)
            {
                return;
            }

            State state = Store();
            state.Invoke(false);
            state.Next = set;
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Prepare();
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            state.Return();
        }

        public virtual void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            TSet @internal = Internal;
            TSet set = CloneInternal();
            set.SymmetricExceptWith(other);

            State state = Store();
            state.Invoke(false);
            state.Next = set;
            state.NewItems = set.Where(item => !@internal.Contains(item)).ToList();
            state.OldItems = @internal.Where(item => !set.Contains(item)).ToList();
            state.Prepare();

            if (state.NewItems.Count <= 0 && state.OldItems.Count <= 0)
            {
                state.Unchanged();
                return;
            }
            
            state.PropertyChanging(nameof(Count));
            state.Update();
            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            state.Return();
        }

        protected new record State : ObservableCollectionAbstraction<T, TSet>.State
        {
            public State(ObservableSet<T, TSet> set)
                : base(set)
            {
            }
        }
    }
}