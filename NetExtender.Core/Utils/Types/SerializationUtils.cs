// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetExtender.Utils.Types
{
    public static class SerializationUtils
    {
        public static Byte[] Serialize(this Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using MemoryStream stream = new MemoryStream();
            
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, value);

            return stream.ToArray();
        }

        public static Object Deserialize(this Byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            using MemoryStream stream = new MemoryStream();
            
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            
            return formatter.Deserialize(stream);
        }

        public static T Deserialize<T>(this Byte[] bytes)
        {
            return (T) Deserialize(bytes);
        }

        public static Boolean TryDeserialize<T>(this Byte[] bytes, out T? value)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            try
            {
                value = Deserialize<T>(bytes);
                return true;
            }
            catch (Exception)
            {
                value = default;
                return false;
            }
        }
    }
}