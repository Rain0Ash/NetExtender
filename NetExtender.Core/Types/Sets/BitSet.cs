// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public sealed class BitSet : ISet
    {
        private readonly BitArray _bits;
        
        public Int32 Count { get; }
        
        Boolean ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        public BitSet(Int32 count)
        {
            Count = count;
            _bits = new BitArray(count);
        }

        public void Clear()
        {
            _bits.SetAll(false);
        }

        public void Clear(Int32 index)
        {
            _bits.Set(index, false);
        }

        public void Set(Int32 index)
        {
            _bits.Set(index, true);
        }

        public Boolean Get(Int32 index)
        {
            return _bits.Get(index);
        }

        public Int32 NextSet(Int32 startFrom)
        {
            Int32 offset = startFrom;
            if (offset >= Count)
            {
                return -1;
            }

            Boolean res = _bits.Get(offset);
            // locate non-empty slot
            while (!res)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                res = _bits.Get(offset);
            }

            return offset;
        }

        public Int32 NextClear(Int32 startFrom)
        {
            Int32 offset = startFrom;
            if (offset >= Count)
            {
                return -1;
            }

            Boolean res = _bits.Get(offset);
            // locate non-empty slot
            while (res)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                res = _bits.Get(offset);
            }

            return offset;
        }

        public void Or(BitSet other)
        {
            for (Int32 i = 0; i < other.Count; i++)
            {
                _bits[i] = other[i];
            }
        }

        public Int32[] ToIntArray()
        {
            Int32[] result = new Int32[Count / 32];
            _bits.CopyTo(result, 0);
            return result;
        }

        public Byte[] ToByteArray()
        {
            Byte[] result = new Byte[Count / 8];
            _bits.CopyTo(result, 0);
            return result;
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            _bits.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _bits.GetEnumerator();
        }
        
        public Boolean this[Int32 index]
        {
            get
            {
                return Get(index);
            }
        }
    }
}