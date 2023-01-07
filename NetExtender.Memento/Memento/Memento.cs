// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NetExtender.Types.Memento.Builder.Interfaces;
using NetExtender.Types.Memento.Interfaces;

namespace NetExtender.Types.Memento
{
    public class Memento<TSource> : IMemento<TSource> where TSource : class
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected List<IMementoItem<TSource>> Internal { get; } = new List<IMementoItem<TSource>>();
        protected Stack<MementoItemCollection<TSource>> Groups { get; } = new Stack<MementoItemCollection<TSource>>();
        protected IMementoBuilder<TSource>? Builder { get; }

        private Int32 _index = -1;
        protected Int32 Index
        {
            get
            {
                return _index;
            }
            set
            {
                Boolean undo = CanUndo;
                Boolean redo = CanRedo;

                _index = value;

                if (undo != CanUndo)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanUndo)));
                }

                if (redo != CanRedo)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanRedo)));
                }
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection<IMementoItem<TSource>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IMementoItem<TSource>>) Internal).IsReadOnly;
            }
        }

        public virtual Boolean CanUndo
        {
            get
            {
                return Index >= 0;
            }
        }

        public virtual Boolean CanRedo
        {
            get
            {
                return Index < Internal.Count - 1;
            }
        }

        public Memento()
            : this(null)
        {
        }

        public Memento(IMementoBuilder<TSource>? builder)
        {
            Builder = builder;
        }

        public virtual Boolean Contains(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.Contains(item);
        }

        void ICollection<IMementoItem<TSource>>.Add(IMementoItem<TSource> item)
        {
            Remember(item);
        }

        public virtual void Begin()
        {
            MementoItemCollection<TSource>? parent = null;
            if (Groups.Count > 0)
            {
                parent = Groups.Peek();
            }

            MementoItemCollection<TSource> child = new MementoItemCollection<TSource>();
            Groups.Push(child);

            if (parent is not null)
            {
                parent.Add(child);
                return;
            }

            Index++;
            Internal.Add(child);
        }

        public Boolean Remember(TSource item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            IMementoBuilder<TSource>? builder = Builder;

            if (builder is null)
            {
                throw new InvalidOperationException($"Builder for type {typeof(TSource)} isn't set");
            }

            IMementoItem<TSource> build = builder.Build(item);
            return Remember(build);
        }

        public virtual Boolean Remember(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            RemoveTop();

            if (Groups.Count > 0)
            {
                MementoItemCollection<TSource> group = Groups.Peek();
                group.Add(item);
                return true;
            }

            Index++;
            Internal.Add(item);

            return true;
        }

        public virtual void End()
        {
            Groups.Pop();
        }

        public virtual Boolean Remove(IMementoItem<TSource> item)
        {
            return Internal.Remove(item);
        }

        public virtual void Clear()
        {
            Internal.Clear();
            Groups.Clear();
            Index = -1;
        }

        public virtual Boolean Undo()
        {
            if (!CanUndo)
            {
                return false;
            }

            Internal[Index--].Swap();
            return true;
        }

        public virtual Boolean Redo()
        {
            if (!CanRedo)
            {
                return false;
            }

            Internal[++Index].Swap();
            return true;
        }

        protected virtual void RemoveTop()
        {
            while (Index < Internal.Count - 1)
            {
                Internal.RemoveAt(Internal.Count - 1);
            }
        }

        public void CopyTo(IMementoItem<TSource>[] array, Int32 arrayIndex)
        {
            Internal.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IMementoItem<TSource>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            Clear();
        }

        ~Memento()
        {
            Dispose(false);
        }
    }
}