// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.JWT.Interfaces
{
    public interface IJWTEncoder
    {
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload);
        public String Encode<TPayload>(JWTKey key, TPayload payload);
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra);
        public String Encode<TPayload>(JWTKey key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra);
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IDictionary<String, Object>? extra);
        public String Encode<TPayload>(JWTKey key, TPayload payload, IDictionary<String, Object>? extra);
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers);
        public String Encode<TPayload>(JWTKey key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers);
    }
}