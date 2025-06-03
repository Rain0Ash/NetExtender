// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.JWT.Interfaces
{
    public interface IJWTDecoder
    {
        public Exception? TryValidate(JWTToken jwt, ReadOnlySpan<Byte> key);
        public Exception? TryValidate(JWTToken jwt, JWTKey key);
        public Exception? TryValidate(JWTToken jwt, JWTKeys keys);

        public String DecodeHeader(JWTToken jwt);
        public Object DecodeHeader(Type type, JWTToken jwt);
        public T DecodeHeader<T>(JWTToken jwt);
        public String Decode(JWTToken jwt);
        public String Decode(JWTToken jwt, Boolean verify);
        public String Decode(JWTToken jwt, ReadOnlySpan<Byte> key);
        public String Decode(JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify);
        public String Decode(JWTToken jwt, JWTKey key);
        public String Decode(JWTToken jwt, JWTKey key, Boolean verify);
        public String Decode(JWTToken jwt, JWTKeys keys);
        public String Decode(JWTToken jwt, JWTKeys keys, Boolean verify);
        public Object Decode(Type type, JWTToken jwt);
        public Object Decode(Type type, JWTToken jwt, Boolean verify);
        public Object Decode(Type type, JWTToken jwt, ReadOnlySpan<Byte> key);
        public Object Decode(Type type, JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify);
        public Object Decode(Type type, JWTToken jwt, JWTKey key);
        public Object Decode(Type type, JWTToken jwt, JWTKey key, Boolean verify);
        public Object Decode(Type type, JWTToken jwt, JWTKeys keys);
        public Object Decode(Type type, JWTToken jwt, JWTKeys keys, Boolean verify);
        public T Decode<T>(JWTToken jwt);
        public T Decode<T>(JWTToken jwt, Boolean verify);
        public T Decode<T>(JWTToken jwt, ReadOnlySpan<Byte> key);
        public T Decode<T>(JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify);
        public T Decode<T>(JWTToken jwt, JWTKey key);
        public T Decode<T>(JWTToken jwt, JWTKey key, Boolean verify);
        public T Decode<T>(JWTToken jwt, JWTKeys keys);
        public T Decode<T>(JWTToken jwt, JWTKeys keys, Boolean verify);
    }
}
