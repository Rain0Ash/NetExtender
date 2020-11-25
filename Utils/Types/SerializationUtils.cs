// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetExtender.Utils.Types
{
    public static class SerializationUtils
    {
        public static Byte[] Serialize(this Object obj)
        {
            if(obj is null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public static Object Deserialize(this Byte[] bytes)
        {
            using MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(bytes, 0, bytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            Object obj = bf.Deserialize(ms);

            return obj;
        }

        public static T Deserialize<T>(this Byte[] bytes)
        {
            return (T)Deserialize(bytes);
        }

        public static Boolean TryDeserialize<T>(this Byte[] bytes, out T value)
        {
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