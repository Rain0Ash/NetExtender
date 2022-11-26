// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Org.BouncyCastle.Utilities.Collections;

namespace NetExtender.Types.Sets
{
    public class BouncySetAdapter : Interfaces.ISet, ISet
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator BouncySetAdapter?(HashSet? value)
        {
            return value is not null ? new BouncySetAdapter(value) : null;
        }

        private ISet Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Internal.IsEmpty;
            }
        }

        public Boolean IsFixedSize
        {
            get
            {
                return Internal.IsFixedSize;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Boolean IsSynchronized
        {
            get
            {
                return Internal.IsSynchronized;
            }
        }

        public Object SyncRoot
        {
            get
            {
                return Internal.SyncRoot;
            }
        }

        public BouncySetAdapter(ISet @internal)
        {
            Internal = @internal;
        }

        public void Add(Object item)
        {
            Internal.Add(item);
        }

        public void AddAll(IEnumerable enumerable)
        {
            Internal.AddAll(enumerable);
        }

        public Boolean Contains(Object item)
        {
            return Internal.Contains(item);
        }

        public void Remove(Object item)
        {
            Internal.Remove(item);
        }

        public void RemoveAll(IEnumerable enumerable)
        {
            Internal.RemoveAll(enumerable);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
}