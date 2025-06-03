// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.JWT.Interfaces
{
    public interface IJWTSerializer
    {
        public JWTSerializerType Type { get; }
        
        public String Serialize<T>(T value);
        public Object Deserialize(Type type, String json);
        public T Deserialize<T>(String json);
    }
}