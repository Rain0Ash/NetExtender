// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public sealed class BitSet : ISet
    {
        private BitArray Bits { get; }

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
            Bits = new BitArray(count);
        }

        public void Clear()
        {
            Bits.SetAll(false);
        }

        public void Clear(Int32 index)
        {
            Bits.Set(index, false);
        }

        public void Set(Int32 index)
        {
            Bits.Set(index, true);
        }

        public Boolean Get(Int32 index)
        {
            return Bits.Get(index);
        }

        public Int32 NextSet(Int32 startFrom)
        {
            Int32 offset = startFrom;
            if (offset >= Count)
            {
                return -1;
            }

            Boolean res = Bits.Get(offset);
            // locate non-empty slot
            while (!res)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                res = Bits.Get(offset);
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

            Boolean res = Bits.Get(offset);
            // locate non-empty slot
            while (res)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                res = Bits.Get(offset);
            }

            return offset;
        }

        public void Or(BitSet other)
        {
            for (Int32 i = 0; i < other.Count; i++)
            {
                Bits[i] = other[i];
            }
        }

        public Int32[] ToIntArray()
        {
            Int32[] result = new Int32[Count / 32];
            Bits.CopyTo(result, 0);
            return result;
        }

        public Byte[] ToByteArray()
        {
            Byte[] result = new Byte[Count / 8];
            Bits.CopyTo(result, 0);
            return result;
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            Bits.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Bits.GetEnumerator();
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