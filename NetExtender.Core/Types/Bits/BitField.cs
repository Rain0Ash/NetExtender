// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NetExtender.Types.Bits
{
    /// <summary>
    /// A class that represents a BitString with named fields. A field is a range
    /// of contiguous bits in a BitString.  A named field means you can use a name
    /// to address/identify a particular field. To use this class you must do the
    /// following:
    /// 1) Use AddField() to define/add your named fields in the order they
    /// appear inside the BitString
    /// 2) Call a version of InitBitField() to initialize a blank object or
    /// one that is initialized with a particular set of bits.
    /// </summary>
    public class BitField
    {
        /// <summary>
        /// A class that represents a field in a BitString.  A field is
        /// a range of contiguous bits in a BitString
        /// </summary>
        private class BitFieldDefinition
        {
            /// <summary>
            /// The starting location for a field in a BitString
            /// </summary>
            public Int32 Offset { get; set; }

            /// <summary>
            /// The length of a field in a BitString
            /// </summary>
            public Int32 Length { get; set; }
        }


        private BitString _bits;
        private readonly OrderedDictionary _fields;


        /// <summary>
        /// Returns a BitString that the BitField represents
        /// </summary>
        [Description("The BitString representing the BitField")]
        public BitString BitString
        {
            get
            {
                return new BitString(_bits);
            }
        }

        /// <summary>
        /// An array of 32-bit words that stores the bits of 1's and 0's
        /// of the BitString
        /// </summary>
        [Description("The array of 32-bit words storing the BitString")]
        public UInt32[] Buffer
        {
            get
            {
                // Return a copy of the internal buffer that stores
                // the bits of the BitString
                return _bits.Buffer;
            }
        }

        /// <summary>
        /// The number of 32-bit words used to store the BitString
        /// </summary>
        [Description("Number of 32-bit words storing the BitString")]
        public Int32 StorageSize
        {
            get
            {
                return _bits.StorageSize;
            }
        }

        /// <summary>
        /// The number of 1's and 0's that the BitString represents
        /// </summary>
        [Description("Number of 1's and 0's that the BitString represents")]
        public Int32 Length
        {
            get
            {
                return _bits.Length;
            }
        }


        /// <summary>
        /// Initiates an empty BitField
        /// </summary>
        public BitField()
        {
            _bits = new BitString();
            _fields = new OrderedDictionary();
        }

        /// <summary>
        /// Adds a named field to the BitField
        /// </summary>
        /// <param name="name">The name of the field.  Case doesn't matter.</param>
        /// <param name="length">The length of the field</param>
        public void AddField(String name, Int32 length)
        {
            BitFieldDefinition bfd = new BitFieldDefinition {Length = length, Offset = _bits.Length};

            _fields.Add(name.ToUpper(), bfd);
            _bits += new BitString(length);
        }

        /// <summary>
        /// Clear out all the named fields
        /// </summary>
        public void ClearFields()
        {
            _fields.Clear();
            _bits = new BitString();
        }

        /// <summary>
        /// After defining your named fields, you must call this
        /// method to initialize the BitString.  This initializes
        /// a BitString with all 0's that has the defined named
        /// fields.
        /// </summary>
        public void InitBitField()
        {
            Int32 length = 0;

            // Determine the length of the BitField that is described by the fields
            for (Int32 i = 0; i < _fields.Count; i++)
            {
                length += ((BitFieldDefinition) _fields[i]).Length;
            }

            _bits = new BitString(length);
        }

        /// <summary>
        /// After defining your named fields, you must call this
        /// method to initialize the BitString.  This initializes
        /// a BitString with the bits in the byte buffer.  If the
        /// byte buffer is less than the length of the total named
        /// fields then it is padded with 0's
        /// </summary>
        /// <param name="buffer">A byte array used to initialize the BitString</param>
        public void InitBitField(Byte[] buffer)
        {
            Int32 length = 0;

            // Determine the length of the BitField that is described by the fields
            for (Int32 i = 0; i < _fields.Count; i++)
            {
                length += ((BitFieldDefinition) _fields[i]).Length;
            }

            if (length > buffer.Length * 8)
            {
                Int32 extraBits = length - buffer.Length * 8;

                _bits = new BitString(buffer) + new String('0', extraBits);
                return;
            }

            _bits = new BitString(buffer, length);
        }

        /// <summary>
        /// After defining your named fields, you must call this
        /// method to initialize the BitString.  This initializes
        /// a BitString with the bits in the uint buffer.  If the
        /// uint buffer is less than the length of the total named
        /// fields then it is padded with 0's
        /// </summary>
        /// <param name="buffer">A uint array used to initialze the BitString</param>
        public void InitBitField(UInt32[] buffer)
        {
            Int32 length = 0;

            // Determine the length of the BitField that is described by the fields
            for (Int32 i = 0; i < _fields.Count; i++)
            {
                length += ((BitFieldDefinition) _fields[i]).Length;
            }

            if (length > buffer.Length * 32)
            {
                Int32 extraBits = length - buffer.Length * 32;

                _bits = new BitString(buffer) + new String('0', extraBits);
                return;
            }

            _bits = new BitString(buffer, length);
        }

        /// <summary>
        /// After defining your named fields, you must call this
        /// method to initialize the BitString.  This initializes
        /// a BitString with the bits in another BitString.  If the
        /// input BitString is less than the length of the total named
        /// fields then it is padded with 0's
        /// </summary>
        /// <param name="buffer">A BitString used to initialize the BitString</param>
        public void InitBitField(BitString buffer)
        {
            Int32 length = 0;

            // Determine the length of the BitField that is described by the fields
            for (Int32 i = 0; i < _fields.Count; i++)
            {
                length += ((BitFieldDefinition) _fields[i]).Length;
            }

            if (length > buffer.Length)
            {
                _bits = new BitString(buffer + new String('0', length - buffer.Length));
                return;
            }

            _bits = new BitString(buffer);
        }

        /// <summary>
        /// After defining your named fields, you must call this
        /// method to initialize the BitString.  This initializes
        /// a BitString with the 1's and 0's in the input string.  If the
        /// input string is less than the length of the total named
        /// fields then it is padded with 0's
        /// </summary>
        /// <param name="buffer">A string of 1's and 0's used to initialize the BitString</param>
        public void InitBitField(String buffer)
        {
            Int32 length = 0;

            // Determine the length of the BitField that is described by the fields
            for (Int32 i = 0; i < _fields.Count; i++)
            {
                length += ((BitFieldDefinition) _fields[i]).Length;
            }

            if (length > buffer.Length)
            {
                _bits = new BitString(buffer + new String('0', length - buffer.Length));
                return;
            }

            _bits = new BitString(buffer);
        }


        /// <summary>
        /// Sets the named field's value to a string of 1's and 0's.  Note that the string of 1's and 0's
        /// should be the same length as defined when you called AddField().  If it is longer then it is
        /// truncated.  If it is shorter then it will be right padded with 0's
        /// </summary>
        /// <param name="field">The name of the field to set</param>
        /// <param name="value">A string of 1's and 0's</param>
        public void SetValue(String field, String value)
        {
            String key = field.ToUpper();

            if (!_fields.Contains(key))
            {
                return;
            }

            BitFieldDefinition bfd = (BitFieldDefinition) _fields[key];

            _bits[bfd.Offset, bfd.Length] = new BitString(value);
        }

        /// <summary>
        /// Sets the named field's value to a BitString.  Note that the BitString should be the same length
        /// as defined when you called AddField().  If it is longer then it is truncated.  If it is shorter
        /// then it will be right padded with 0's
        /// </summary>
        /// <param name="field">The name of the field to set</param>
        /// <param name="value">A BitString</param>
        public void SetValue(String field, BitString value)
        {
            String key = field.ToUpper();

            if (!_fields.Contains(key))
            {
                return;
            }

            BitFieldDefinition bfd = (BitFieldDefinition) _fields[key];

            _bits[bfd.Offset, bfd.Length] = value;
        }

        /// <summary>
        /// Array indexing for accessing the name fields of the BitField.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BitString this[String name]
        {
            get
            {
                String key = name.ToUpper();

                if (!_fields.Contains(key))
                {
                    return new BitString();
                }

                BitFieldDefinition bfd = (BitFieldDefinition) _fields[key];

                return _bits[bfd.Offset, bfd.Length];

            }

            set
            {
                String key = name.ToUpper();

                if (!_fields.Contains(key))
                {
                    return;
                }

                BitFieldDefinition bfd = (BitFieldDefinition) _fields[key];

                _bits[bfd.Offset, bfd.Length] = value;
            }
        }

        /// <summary>
        /// Return the buffer as an array of bytes.  This basically means we transform
        /// the buffer (which is represented by an array of uints) to an array of bytes
        /// </summary>
        /// <returns></returns>
        public Byte[] GetBytes()
        {
            return _bits.GetBytes();
        }

        /// <summary>
        /// Return a string representation of the BitField
        /// </summary>
        /// <returns>A string representation of the BitField</returns>
        public override String ToString()
        {
            return _bits.ToString();
        }
    }
}