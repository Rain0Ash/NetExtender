using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public abstract class ObservableCollectionAbstraction<T, TCollection> : IObservableCollectionAbstraction<T>, IReadOnlyObservableCollectionAbstraction<T>, ICollection where TCollection : ICollection<T>
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected TCollection Internal { get; private set; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return Internal is ICollection { IsSynchronized: true };
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Internal is ICollection collection ? collection.SyncRoot : this;
            }
        }
        
        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        protected ObservableCollectionAbstraction(TCollection collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
        }
        
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        protected virtual void OnPropertyChanging(PropertyChanging args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        protected virtual void OnPropertyChanged(PropertyChanged args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        protected virtual State Store()
        {
            return new State(this);
        }

        protected virtual void Handle(Step step, State? state)
        {
        }

        public virtual Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public virtual Boolean Add(T item)
        {
            if (Internal.Contains(item))
            {
                return false;
            }

            State state = Store();
            state.Invoke();
            state.PropertyChanging(nameof(Count));
            
            static Boolean Handler(ref TCollection @internal, T argument)
            {
                @internal.Add(argument);
                return true;
            }

            if (!state.Update(static (ref TCollection collection, T argument, ref List<T>? _, ref List<T>? _) => Handler(ref collection, argument), item))
            {
                return state.Fallback();
            }
            
            state.CollectionChanged(NotifyCollectionChangedAction.Add, item);
            state.PropertyChanged(nameof(Count));
            return state.Return();
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public virtual Boolean Remove(T item)
        {
            TCollection @internal = Internal;
            if (!@internal.Contains(item))
            {
                return false;
            }

            State state = Store();
            state.Invoke();
            state.PropertyChanging(nameof(Count));
            
            static Boolean Handler(ref TCollection @internal, T argument)
            {
                return @internal.Remove(argument);
            }

            if (!state.Update(static (ref TCollection collection, T argument, ref List<T>? _, ref List<T>? _) => Handler(ref collection, argument), item))
            {
                return state.Fallback();
            }

            state.CollectionChanged(NotifyCollectionChangedAction.Remove, item);
            state.PropertyChanged(nameof(Count));
            return state.Return();
        }

        public virtual void Clear()
        {
            TCollection @internal = Internal;
            if (@internal.Count <= 0)
            {
                return;
            }

            State state = Store();
            state.Invoke();
            state.PropertyChanging(nameof(Count));
            
            // ReSharper disable once RedundantAssignment
            static Boolean Handler(ref TCollection @internal, ref List<T>? old)
            {
                old = @internal.ToList();
                @internal.Clear();
                return true;
            }
            
            if (!state.Update(static (ref TCollection collection, T? _, ref List<T>? _, ref List<T>? old) => Handler(ref collection, ref old)))
            {
                state.Fallback();
                return;
            }

            state.CollectionChanged();
            state.PropertyChanged(nameof(Count));
            state.Return();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (Internal is ICollection collection)
            {
                collection.CopyTo(array, index);
            }

            if (array is not T[] generic)
            {
                throw new ArgumentException("Invalid type", nameof(array));
            }

            CopyTo(generic, index);
        }

        public virtual void CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, arrayIndex);
        }
        
        protected abstract TCollection CloneInternal();

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected enum Step : Byte
        {
            Invoke,
            Prepared,
            Changing,
            Updated,
            Collection,
            Changed,
            Unchanged,
            Fallback,
            Return
        }
        
        protected record State
        {
            public delegate Boolean Handler<in TArgument>(ref TCollection @internal, TArgument argument, ref List<T>? @new, ref List<T>? old);
            protected ObservableCollectionAbstraction<T, TCollection> Collection { get; }
            public String? Property { get; private set; }
            public TCollection Current { get; }
            public TCollection? Next { get; set; }
            public List<T>? NewItems { get; set; }
            public List<T>? OldItems { get; set; }

            public State(ObservableCollectionAbstraction<T, TCollection> collection)
            {
                Collection = collection;
                Current = collection.Internal;
            }

            protected virtual void Handle(Step step)
            {
                Collection.Handle(step, this);
                Property = null;
            }

            public virtual void Invoke()
            {
                Invoke(true);
            }

            public void Invoke(Boolean prepare)
            {
                Handle(Step.Invoke);

                if (prepare)
                {
                    Prepare();
                }
            }

            public virtual void Prepare()
            {
                Handle(Step.Prepared);
            }

            public void PropertyChanging(PropertyChanging args)
            {
                Property = args;
                Collection.OnPropertyChanging(args);
                Handle(Step.Changing);
            }

            protected virtual Boolean Update<TArgument>(TCollection @internal, TCollection reference, TArgument argument, List<T>? @new, List<T>? old)
            {
                if (!ReferenceEquals(@internal, reference))
                {
                    Update(reference);
                    return true;
                }
                
                Handle(Step.Updated);
                return true;
            }

            public Boolean Update<TArgument>(Handler<TArgument?>? handler)
            {
                return Update(handler, default);
            }

            public Boolean Update<TArgument>(Handler<TArgument>? handler, TArgument argument)
            {
                TCollection @internal = Collection.Internal;
                TCollection reference = @internal;

                List<T>? @new = null;
                List<T>? old = null;
                if (handler?.Invoke(ref reference, argument, ref @new, ref old) is not true)
                {
                    return false;
                }

                NewItems = @new;
                OldItems = old;

                return Update(@internal, reference, argument, @new, old);
            }

            public void Update()
            {
                if (Next is { } next)
                {
                    Update(next);
                }
            }

            public void Update(TCollection collection)
            {
                Collection.Internal = collection ?? throw new ArgumentNullException(nameof(collection));
                Next = Collection.Internal;
                Handle(Step.Updated);
            }

            public void CollectionChanged()
            {
                CollectionChanged((IList?) NewItems ?? Array.Empty<Object>(), (IList?) OldItems ?? Array.Empty<Object>());
            }

            public void CollectionChanged(NotifyCollectionChangedEventArgs args)
            {
                Collection.OnCollectionChanged(args);
                Handle(Step.Changing);
            }

            public void CollectionChanged(NotifyCollectionChangedAction action, Object? item)
            {
                CollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
            }

            public void CollectionChanged(IList newItems, IList oldItems)
            {
                CollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItems, oldItems));
            }

            public void PropertyChanged(PropertyChanged args)
            {
                Property = args;
                Collection.OnPropertyChanged(args);
                Handle(Step.Changed);
            }

            public Boolean Unchanged()
            {
                Handle(Step.Unchanged);
                return false;
            }

            public Boolean Fallback()
            {
                return Fallback(false);
            }

            public TArgument Fallback<TArgument>(TArgument argument)
            {
                Handle(Step.Fallback);
                return argument;
            }

            public Boolean Return()
            {
                return Return(true);
            }

            public TArgument Return<TArgument>(TArgument argument)
            {
                Handle(Step.Return);
                return argument;
            }
        }
    }
}