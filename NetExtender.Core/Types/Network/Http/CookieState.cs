// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using NetExtender.Interfaces;

namespace NetExtender.Types.Network
{
    public class CookieState : ICloneable<CookieState>
    {
        private static ImmutableHashSet<Char> InvalidHeader { get; } = ImmutableHashSet<Char>.Empty.Union("()<>@,;:\\\"/[]?={}");
        
        private readonly String? _key;
        public String Key
        {
            get
            {
                return _key ?? String.Empty;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (String.IsNullOrEmpty(value) || value.Any(static character => character < '!' || character > '~' || InvalidHeader.Contains(character)))
                {
                    throw new ArgumentException("The specified value is not a valid cookie key.", nameof(value));
                }
                
                _key = value;
            }
        }

        public String? Value
        {
            get
            {
                return Values.Count > 0 ? Values.AllKeys[0] : null;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (Values.Count <= 0)
                {
                    Values.Add(value, String.Empty);
                    return;
                }

                Values.AllKeys[0] = value;
            }
        }
        
        public NameValueCollection Values { get; } = HttpUtility.ParseQueryString(String.Empty);

        public CookieState(String key)
            : this(key, String.Empty)
        {
        }

        public CookieState(String key, String value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public CookieState(String key, NameValueCollection values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Values.Add(values);
        }

        private CookieState(CookieState source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Key = source.Key;
            Values.Add(source.Values);
        }

        public override String ToString()
        {
            return Key + "=" + Values;
        }

        public virtual CookieState Clone()
        {
            return new CookieState(this);
        }
        
        public String? this[String key]
        {
            get
            {
                return Values[key];
            }
            set
            {
                Values[key] = value;
            }
        }
    }
}