// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Interfaces;

namespace NetExtender.JWT
{
    public enum JWTSerializerType : Byte
    {
        Unknown,
        TextJson,
        Newtonsoft,
        Other
    }

    public abstract class JWTSerializer : IJWTSerializer
    {
        public abstract JWTSerializerType Type { get; }
        
        public abstract String Serialize<T>(T value);
        public abstract Object Deserialize(Type type, String json);
        public abstract T Deserialize<T>(String json);
    }
}