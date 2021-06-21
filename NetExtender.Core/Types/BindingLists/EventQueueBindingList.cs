// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Utils.Numerics;

namespace NetExtender.Types.BindingLists
{
    public class EventQueueBindingList<T> : EventBindingList<T>
    {
        private Int32 _maximumLength = Int32.MaxValue;

        public Int32 MaximumLength
        {
            get
            {
                return _maximumLength;
            }
            set
            {
                Int32 val = value.ToRange();
                if (_maximumLength == val)
                {
                    return;
                }

                _maximumLength = val;

                if (Count <= val)
                {
                    return;
                }

                RemoveRange(Reversed ? 0 : val, Count - val);
            }
        }

        public Boolean Reversed { get; set; } = false;

        public void Enqueue(T item)
        {
            Add(item);
        }

        public T Dequeue()
        {
            return Pop();
        }

        public new void Add(T item)
        {
            if (Count >= MaximumLength)
            {
                RemoveAt(Count - 1);
            }

            base.Add(item);
        }

        public T Pop(Int32 index = 0)
        {
            index = index.ToRange();
            if (index > Count - 1)
            {
                index = Count - 1;
            }

            T item = this[index];
            RemoveAt(index);
            return item;
        }

        public T Get(Int32 index)
        {
            return this.ElementAtOrDefault(index);
        }
    }
}