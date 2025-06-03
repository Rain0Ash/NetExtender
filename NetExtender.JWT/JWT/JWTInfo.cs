// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    public sealed record JWTInfo
    {
        public Boolean HasHeader
        {
            get
            {
                return _header is { Count: > 0 };
            }
        }

        private IDictionary<String, Object>? _header;
        public IDictionary<String, Object> Header
        {
            get
            {
                return _header ??= new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public Boolean HasPayload
        {
            get
            {
                return _payload is { Count: > 0 };
            }
        }

        private IDictionary<String, Object>? _payload;
        public IDictionary<String, Object> Payload
        {
            get
            {
                return _payload ??= new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public JWTInfo()
            : this(null, null)
        {
        }

        public JWTInfo(String token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (token.Count('.') != 3)
            {
                throw new JWTFormatException(nameof(token));
            }
        }

        public JWTInfo(IDictionary<String, Object>? payload)
            : this(null, payload)
        {
        }

        public JWTInfo(IDictionary<String, Object>? header, IDictionary<String, Object>? payload)
        {
            _header = header;
            _payload = payload;
        }
    }
}