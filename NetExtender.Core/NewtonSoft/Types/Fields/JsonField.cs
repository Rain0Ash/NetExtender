// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Fields
{
    public struct JsonField<T> where T : class
    {
        public static implicit operator JsonField<T>(T value)
        {
            return new JsonField<T>
            {
                _object = value,
                HasDefault = true
            };
        }

        public static implicit operator T?(JsonField<T> value)
        {
            return value.Object;
        }

        private Boolean IsMaterialized { get; set; }
        private Boolean HasDefault { get; set; }

        private T? _object;
        public T? Object
        {
            get
            {
                if (IsMaterialized)
                {
                    return _object;
                }

                if (!String.IsNullOrEmpty(_json) && _json != "null")
                {
                    return Object = JsonConvert.DeserializeObject<T>(_json);
                }

                if (!HasDefault)
                {
                    return Object = null!;
                }

                HasDefault = false;
                IsMaterialized = true;
                return _object;
            }
            set
            {
                _object = value;
                IsMaterialized = true;
            }
        }

        private String? _json;
        public String? Json
        {
            get
            {
                if (IsMaterialized)
                {
                    _json = _object is not null ? JsonConvert.SerializeObject(_object, Formatting.None) : null;
                }

                return _json;
            }
            set
            {
                _json = value;
                IsMaterialized = false;
            }
        }
    }
}