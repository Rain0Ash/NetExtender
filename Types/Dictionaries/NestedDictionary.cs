// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Utils.Types;
using DynamicData.Annotations;
using Newtonsoft.Json;

namespace NetExtender.Types.Dictionaries
{
    [Serializable]
    [JsonObject]
    public class NestedDictionary<TKey, TValue> : Dictionary<TKey, NestedDictionary<TKey, TValue>>
    {
        [JsonConstructor]
        public NestedDictionary()
        {
        }

        public NestedDictionary([NotNull] IDictionary<TKey, NestedDictionary<TKey, TValue>> dictionary)
            : base(dictionary)
        {
        }

        public NestedDictionary([NotNull] IDictionary<TKey, NestedDictionary<TKey, TValue>> dictionary, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        public NestedDictionary([NotNull] IEnumerable<KeyValuePair<TKey, NestedDictionary<TKey, TValue>>> collection)
            : base(collection)
        {
        }

        public NestedDictionary([NotNull] IEnumerable<KeyValuePair<TKey, NestedDictionary<TKey, TValue>>> collection, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(collection, comparer)
        {
        }

        public NestedDictionary([CanBeNull] IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public NestedDictionary(Int32 capacity)
            : base(capacity)
        {
        }

        public NestedDictionary(Int32 capacity, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }

        protected NestedDictionary([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [JsonProperty]
        public TValue Value { set; get; }

        public new Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections)
        {
            return GetChild(key, sections) is not null;
        }

        public Boolean ContainsValue(TValue value, Int32 depth = -1)
        {
            if (depth < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(depth));
            }
            
            foreach (NestedDictionary<TKey, TValue> dictionary in Values)
            {
                if (value.IsDefaultOrEquals(dictionary.Value))
                {
                    return true;
                }

                if (depth != 0)
                {
                    return dictionary.ContainsValue(value, depth > 0 ? depth - 1 : -1);
                }
            }

            return false;
        }

        public NestedDictionary<TKey, TValue> GetChild(TKey key)
        {
            return ContainsKey(key) ? this[key] : null;
        }
        
        public NestedDictionary<TKey, TValue> GetChild(TKey key, params TKey[] sections)
        {
            NestedDictionary<TKey, TValue> dictionary = GetChildSection(sections);

            if (dictionary is null)
            {
                return null;
            }

            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }

        public NestedDictionary<TKey, TValue> GetChildSection(IEnumerable<TKey> sections)
        {
            NestedDictionary<TKey, TValue> dictionary = this;
            
            foreach (TKey section in sections)
            {
                if (!dictionary.ContainsKey(section))
                {
                    return null;
                }

                dictionary = dictionary[section];
            }

            return dictionary;
        }
        
        public NestedDictionary<TKey, TValue> GetChildSection(params TKey[] sections)
        {
            return GetChildSection(sections.AsEnumerable());
        }
        
        public Boolean ContainValue()
        {
            return Value.IsNotDefault();
        }

        public TValue GetValue()
        {
            return Value;
        }

        public TValue GetValue(TKey key)
        {
            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }
            
            return this[key].Value;
        }

        public TValue GetValue(TKey key, params TKey[] sections)
        {
            return (GetChild(key, sections) ?? throw new KeyNotFoundException()).Value;
        }

        public new Boolean Remove(TKey key)
        {
            return base.Remove(key);
        }
        
        public new Boolean Remove(TKey key, out NestedDictionary<TKey, TValue> value)
        {
            return base.Remove(key, out value);
        }

        public Boolean Remove(TKey key, params TKey[] sections)
        {
            return Remove(key, sections, out _);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out NestedDictionary<TKey, TValue> value)
        {
            NestedDictionary<TKey, TValue> child = GetChildSection(sections);

            if (child is not null)
            {
                return child.Remove(key, out value);
            }

            value = default;
            return false;
        }
        
        public Boolean Remove(TKey key, out NestedDictionary<TKey, TValue> value, params TKey[] sections)
        {
            return Remove(key, sections, out value);
        }

        public void RemoveEmpty()
        {
            foreach (TKey key in Keys)
            {
                RemoveEmpty(key);
            }
        }
        
        public void RemoveEmpty(TKey key)
        {
            NestedDictionary<TKey, TValue> dictionary = GetChild(key);
            
            if (dictionary is null)
            {
                return;
            }

            if (dictionary.Count <= 0 && dictionary.Value.IsDefault())
            {
                Remove(key);
            }
            else if (dictionary.Count > 0)
            {
                foreach ((TKey dictkey, NestedDictionary<TKey, TValue> dict) in dictionary)
                {
                    if (dict is null || dict.Count <= 0 && dict.Value.IsDefault())
                    {
                        dictionary.Remove(dictkey);
                    }
                    else if (dict.Count > 0)
                    {
                        dict.RemoveEmpty();
                    }
                }
            }
        }
        
        public void RemoveEmpty(TKey key, params TKey[] sections)
        {
            NestedDictionary<TKey, TValue> dictionary = GetChildSection(sections);
            
            if (dictionary is null)
            {
                return;
            }

            if (dictionary.Count <= 0)
            {
                return;
            }

            dictionary.RemoveEmpty(key);
        }

        public new NestedDictionary<TKey, TValue> this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    base[key] = new NestedDictionary<TKey, TValue>();
                }

                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }

        public NestedDictionary<TKey, TValue> this[TKey key, params TKey[] sections]
        {
            get
            {
                NestedDictionary<TKey, TValue> dict = this;

                if (sections.Length <= 0)
                {
                    return dict[key];
                }

                dict = sections.Aggregate(dict, (current, section) => current[section]);

                return dict[key];
            }
            set
            {
                NestedDictionary<TKey, TValue> dict = this;

                if (sections.Length <= 0)
                {
                    dict[key] = value;
                }

                dict = sections.Aggregate(dict, (current, section) => current[section]);

                dict[key] = value;
            }
        }
    }
}