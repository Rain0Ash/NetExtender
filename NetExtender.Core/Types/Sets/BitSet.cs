// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Sets
{
    public class BitSet : ISet
    {
        protected BitArray Bits { get; }

        public Int32 Count
        {
            get
            {
                return Bits.Count;
            }
        }

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
            Bits = new BitArray(count);
        }

        public Boolean Get(Int32 index)
        {
            return Bits.Get(index);
        }

        public void Set(Int32 index)
        {
            Bits.Set(index, true);
        }

        public Int32 NextSet(Int32 start)
        {
            Int32 offset = start;
            if (offset >= Count)
            {
                return -1;
            }

            Boolean bit = Bits.Get(offset);
            // locate non-empty slot
            while (!bit)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                bit = Bits.Get(offset);
            }

            return offset;
        }

        public void Not()
        {
            for (Int32 i = 0; i < Count; i++)
            {
                Bits[i] = !Bits[i];
            }
        }

        public void Set(BitSet other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            for (Int32 i = 0; i < Count && i < other.Count; i++)
            {
                Bits[i] = other[i];
            }
        }

        public void Or(BitSet other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            for (Int32 i = 0; i < Count && i < other.Count; i++)
            {
                Bits[i] |= other[i];
            }
        }

        public void And(BitSet other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            for (Int32 i = 0; i < Count && i < other.Count; i++)
            {
                Bits[i] &= other[i];
            }
        }

        public void Xor(BitSet other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            for (Int32 i = 0; i < Count && i < other.Count; i++)
            {
                Bits[i] ^= other[i];
            }
        }

        public void Clear()
        {
            Bits.SetAll(false);
        }

        public void Clear(Int32 index)
        {
            Bits.Set(index, false);
        }

        public Int32 NextClear(Int32 start)
        {
            Int32 offset = start;
            if (offset >= Count)
            {
                return -1;
            }

            Boolean bit = Bits.Get(offset);
            // locate non-empty slot
            while (bit)
            {
                if (++offset >= Count)
                {
                    return -1;
                }

                bit = Bits.Get(offset);
            }

            return offset;
        }

        public SByte[] ToSByteArray()
        {
            const Double bits = 8;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            SByte[] result = new SByte[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public Byte[] ToByteArray()
        {
            const Double bits = 8;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            Byte[] result = new Byte[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public Int16[] ToInt16Array()
        {
            const Double bits = 16;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            Int16[] result = new Int16[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public UInt16[] ToUInt16Array()
        {
            const Double bits = 16;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            UInt16[] result = new UInt16[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public Int32[] ToInt32Array()
        {
            const Double bits = 32;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            Int32[] result = new Int32[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public UInt32[] ToUInt32Array()
        {
            const Double bits = 32;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            UInt32[] result = new UInt32[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public Int64[] ToInt64Array()
        {
            const Double bits = 64;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            Int64[] result = new Int64[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        public UInt64[] ToUInt64Array()
        {
            const Double bits = 64;
            Int32 count = (Int32) Math.Ceiling(Count / bits);
            UInt64[] result = new UInt64[count];
            Bits.CopyTo(result, 0);
            return result;
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

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