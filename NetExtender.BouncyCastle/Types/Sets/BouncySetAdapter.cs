// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        public Boolean IsFixedSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsFixedSize;
            }
        }

        public Boolean IsReadOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Boolean IsSynchronized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsSynchronized;
            }
        }

        public Object SyncRoot
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.SyncRoot;
            }
        }

        public BouncySetAdapter(ISet @internal)
        {
            Internal = @internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(Object item)
        {
            Internal.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddAll(IEnumerable enumerable)
        {
            Internal.AddAll(enumerable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(Object item)
        {
            return Internal.Contains(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(Object item)
        {
            Internal.Remove(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAll(IEnumerable enumerable)
        {
            Internal.RemoveAll(enumerable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Internal.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ICollection.CopyTo(Array array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
    }
}