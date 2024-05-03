// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Linq;

namespace NetExtender.Types.Network
{
    internal static class FormUrlEncodedJson
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JObject? Parse(IEnumerable<KeyValuePair<String?, String?>> source)
        {
            return Parse(source, Int32.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JObject? Parse(IEnumerable<KeyValuePair<String?, String?>> source, Int32 depth)
        {
            return ParseInternal(source, depth, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(IEnumerable<KeyValuePair<String?, String?>> source, [MaybeNullWhen(false)] out JObject value)
        {
            return TryParse(source, Int32.MaxValue, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(IEnumerable<KeyValuePair<String?, String?>> source, Int32 depth, [MaybeNullWhen(false)] out JObject value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ParseInternal(source, depth, false) is not { } result)
            {
                value = default;
                return false;
            }

            value = result;
            return true;
        }

        // ReSharper disable once CognitiveComplexity
        private static JObject? ParseInternal(IEnumerable<KeyValuePair<String?, String?>> source, Int32 depth, Boolean @throw)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            JObject jobject = new JObject();
            foreach ((String? key, String? value) in source)
            {
                if (key is not null)
                {
                    String[]? path = GetPath(key, depth, @throw);
                    if (path is null || !Insert(jobject, path, value, @throw))
                    {
                        return null;
                    }

                    continue;
                }

                if (String.IsNullOrEmpty(value))
                {
                    return !@throw ? null : throw new ArgumentNullException(nameof(value));
                }

                if (!Insert(jobject, new[] { value }, null, @throw))
                {
                    return null;
                }
            }

            FixContiguousArrays(jobject);
            return jobject;
        }

        private static String? GetIndex(JObject jobject, Boolean @throw)
        {
            if (jobject is null)
            {
                throw new ArgumentNullException(nameof(jobject));
            }

            Int32 num = -1;
            if (jobject.Count <= 0)
            {
                return 0.ToString(CultureInfo.InvariantCulture);
            }

            ICollection<String> keys = ((IDictionary<String, JToken>) jobject).Keys;
            foreach (String key in keys)
            {
                if (!Int32.TryParse(key, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out Int32 result) || result <= num)
                {
                    return !@throw ? null : throw new ArgumentException($"Mismatched types at node '{key}'.", nameof(key));
                }

                num = result;
            }

            return (++num).ToString(CultureInfo.InvariantCulture);
        }

        private static String[]? GetPath(String key, Int32 depth, Boolean @throw)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return new[] { String.Empty };
            }

            if (!ValidateQueryString(key, @throw))
            {
                return null;
            }

            String[] path = key.Split('[');
            for (Int32 index = 0; index < path.Length; ++index)
            {
                if (path[index].EndsWith("]", StringComparison.Ordinal))
                {
                    path[index] = path[index].Substring(0, path[index].Length - 1);
                }
            }

            if (path.Length < depth)
            {
                return path;
            }

            return !@throw ? null : throw new InvalidOperationException($"The maximum read depth ({depth}) has been exceeded because the form url-encoded data being read has more levels of nesting than is allowed.");
        }

        private static String BuildPath(String[] path, Int32 index)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            StringBuilder builder = new StringBuilder(path[0]);
            for (Int32 i = 1; i <= index; ++i)
            {
                builder.Append(CultureInfo.InvariantCulture, $"[{path[i]}]");
            }

            return builder.ToString();
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean ValidateQueryString(String key, Boolean @throw)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Boolean flag = false;
            for (Int32 index = 0; index < key.Length; ++index)
            {
                switch (key[index])
                {
                    case '[':
                    {
                        if (!flag)
                        {
                            flag = true;
                            break;
                        }

                        if (@throw)
                        {
                            throw new InvalidOperationException($"Nested bracket is not valid for 'application/x-www-form-urlencoded' data at position {index}.");
                        }

                        return false;
                    }
                    case ']':
                    {
                        if (flag)
                        {
                            flag = false;
                            break;
                        }

                        if (@throw)
                        {
                            throw new InvalidOperationException($"There is an unmatched opened bracket for the 'application/x-www-form-urlencoded' at position {index}.");
                        }

                        return false;
                    }
                }
            }

            if (!flag)
            {
                return true;
            }

            return !@throw ? false : throw new InvalidOperationException($"Nested bracket is not valid for 'application/x-www-form-urlencoded' data at position {key.LastIndexOf('[')}.");
        }

        private static Boolean AddToObject(JObject jobject, String[] path, String? value, Boolean @throw)
        {
            if (jobject is null)
            {
                throw new ArgumentNullException(nameof(jobject));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            String key = path[^1];
            IDictionary<String, JToken> dictionary = (IDictionary<String, JToken>) jobject;
            if (dictionary.ContainsKey(key))
            {
                jobject[key] = value;
                return true;
            }

            JToken? item = jobject[key];
            if (item is null || item.Type == JTokenType.Null)
            {
                return !@throw ? false : throw new ArgumentException($"Mismatched types at node '{BuildPath(path, path.Length - 1)}'.", nameof(jobject));
            }

            if (path.Length != 1)
            {
                return !@throw ? false : throw new ArgumentException($"Traditional style array without '[]' is not supported with nested object at location {BuildPath(path, path.Length - 1)}.");
            }
            
            if (item.Type == JTokenType.String)
            {
                jobject[key] = new JObject
                {
                    { "0", item.ToObject<String>() },
                    { "1", value }
                };
                
                return true;
            }

            if (item is not JObject json)
            {
                return true;
            }

            String? index = GetIndex(json, @throw);
            if (index is null)
            {
                return false;
            }

            json.Add(index, value);
            return true;
        }

        private static Boolean AddToArray(JObject parent, String[] path, String? value, Boolean @throw)
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            String property = path[^2];
            if (parent[property] is not JObject jobject)
            {
                return !@throw ? false : throw new ArgumentException($"Mismatched types at node '{BuildPath(path, path.Length - 1)}'.", nameof(parent));
            }

            String? index = GetIndex(jobject, @throw);
            if (index is null)
            {
                return false;
            }

            jobject.Add(index, (JToken) value);
            return true;
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean Insert(JObject root, String[] path, String? value, Boolean @throw)
        {
            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            JObject? jobject = root;
            JObject? parent = null;
            for (Int32 i = 0; i < path.Length - 1; ++i)
            {
                if (String.IsNullOrEmpty(path[i]))
                {
                    return !@throw ? false : throw new ArgumentException($"Invalid array at node '{BuildPath(path, i)}'.", nameof(path));
                }

                if (jobject is null)
                {
                    break;
                }

                IDictionary<String, JToken> dictionary = (IDictionary<String, JToken>) jobject;
                if (!dictionary.ContainsKey(path[i]))
                {
                    jobject[path[i]] = new JObject();
                }
                else if (jobject[path[i]] is null || jobject[path[i]] is JValue)
                {
                    return !@throw ? false : throw new ArgumentException($"Mismatched types at node '{BuildPath(path, i)}'.", nameof(path));
                }

                parent = jobject;
                jobject = jobject[path[i]] as JObject;
            }

            if (parent is not null && String.IsNullOrEmpty(path[^1]) && path.Length > 1)
            {
                return AddToArray(parent, path, value, @throw);
            }

            if (jobject is not null)
            {
                return value is not null && AddToObject(jobject, path, value, @throw);
            }

            return !@throw ? false : throw new ArgumentException($"Mismatched types at node '{BuildPath(path, path.Length - 1)}'.", nameof(path));
        }

        private static void FixContiguousArrays(JToken token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            void Array(JArray array)
            {
                if (array is null)
                {
                    throw new ArgumentNullException(nameof(array));
                }

                for (Int32 index = 0; index < array.Count; ++index)
                {
                    array[index] = FixSingleContiguousArray(array[index]);
                    FixContiguousArrays(array[index]);
                }
            }

            void Object(JObject @object)
            {
                if (@object.Count <= 0)
                {
                    return;
                }

                ICollection<String> keys = ((IDictionary<String, JToken>) @object).Keys;
                using List<String>.Enumerator enumerator = new List<String>(keys).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    String current = enumerator.Current;
                    JToken? item = @object[current];
                    if (item is null)
                    {
                        continue;
                    }

                    @object[current] = FixSingleContiguousArray(item);
                    FixContiguousArrays(item);
                }
            }

            switch (token)
            {
                case JArray jarray:
                {
                    Array(jarray);
                    return;
                }
                case JObject jobject:
                {
                    Object(jobject);
                    return;
                }
            }
        }

        private static JToken FixSingleContiguousArray(JToken token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (token is not JObject { Count: > 0 } jobject || !TryBecomeArray(new List<String>(((IDictionary<String, JToken>) jobject).Keys), out List<String>? keys))
            {
                return token;
            }

            JArray jarray = new JArray();
            
            foreach (JToken? item in keys.Select(property => jobject[property]))
            {
                jarray.Add(item!);
            }

            return jarray;
        }

        private static Boolean TryBecomeArray(List<String> keys, [MaybeNullWhen(false)] out List<String> result)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            List<ArrayCandidate> source = new List<ArrayCandidate>();
            Boolean flag = true;
            foreach (String key in keys)
            {
                if (!Int32.TryParse(key, NumberStyles.None, CultureInfo.InvariantCulture, out Int32 value))
                {
                    flag = false;
                    break;
                }

                String @string = value.ToString(CultureInfo.InvariantCulture);
                if (!@string.Equals(key, StringComparison.Ordinal))
                {
                    flag = false;
                    break;
                }

                source.Add(new ArrayCandidate(value, @string));
            }

            if (!flag)
            {
                result = default;
                return false;
            }

            source.Sort((x, y) => x.Key - y.Key);
            if (source.Where((array, index) => array.Key != index).Any())
            {
                result = default;
                return false;
            }

            result = new List<String>(source.Select(array => array.Value));
            return true;
        }

        private record ArrayCandidate(Int32 Key, String Value);
    }
}