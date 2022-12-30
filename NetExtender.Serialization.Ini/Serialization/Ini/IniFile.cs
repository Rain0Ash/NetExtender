// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Serialization.Ini
{
    public class IniFile : IDictionary<String, IniSection>, IReadOnlyDictionary<String, IniSection>
    {
        public static IEqualityComparer<String> DefaultComparer { get; } = StringComparer.OrdinalIgnoreCase;

        public IEqualityComparer<String> Comparer { get; }
        private IndexDictionary<String, IniSection> Sections { get; }

        Boolean ICollection<KeyValuePair<String, IniSection>>.IsReadOnly
        {
            get
            {
                return ((IDictionary<String, IniSection>) Sections).IsReadOnly;
            }
        }

        public Int32 Count
        {
            get
            {
                return Sections.Count;
            }
        }

        public ICollection<String> Keys
        {
            get
            {
                return Sections.Keys;
            }
        }

        IEnumerable<String> IReadOnlyDictionary<String, IniSection>.Keys
        {
            get
            {
                return Keys;
            }
        }

        public ICollection<IniSection> Values
        {
            get
            {
                return Sections.Values;
            }
        }

        IEnumerable<IniSection> IReadOnlyDictionary<String, IniSection>.Values
        {
            get
            {
                return Values;
            }
        }

        public IniFile()
            : this(null)
        {
        }

        public IniFile(IEqualityComparer<String>? comparer)
        {
            Comparer = comparer ?? DefaultComparer;
            Sections = new IndexDictionary<String, IniSection>(Comparer);
        }

        public Boolean ContainsKey(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.ContainsKey(key);
        }

        Boolean ICollection<KeyValuePair<String, IniSection>>.Contains(KeyValuePair<String, IniSection> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(item.Key));
            }

            return ((IDictionary<String, IniSection>) Sections).Contains(item);
        }

        public Boolean TryGetValue(String key, [MaybeNullWhen(false)] out IniSection result)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            return Sections.TryGetValue(key, out result);
        }
        
        public Int32 IndexOf(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.IndexOf(key);
        }

        public Int32 IndexOf(String key, Int32 index)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.IndexOf(key, index);
        }

        public Int32 IndexOf(String key, Int32 index, Int32 count)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.IndexOf(key, index, count);
        }

        public Int32 LastIndexOf(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.LastIndexOf(key);
        }

        public Int32 LastIndexOf(String key, Int32 index)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.LastIndexOf(key, index);
        }

        public Int32 LastIndexOf(String key, Int32 index, Int32 count)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            return Sections.LastIndexOf(key, index, count);
        }

        public IniSection Add(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            IniSection value = new IniSection(Comparer);
            Sections.Add(key, value);
            return value;
        }

        public IniSection Add(String key, IniSection value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Equals(value.Comparer, Comparer))
            {
                value = new IniSection(value, Comparer);
            }

            Sections.Add(key, value);
            return value;
        }

        void IDictionary<String, IniSection>.Add(String key, IniSection value)
        {
            Add(key, value);
        }

        public IniSection Add(String key, IDictionary<String, IniValue> values)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return Add(key, new IniSection(values, Comparer));
        }

        void ICollection<KeyValuePair<String, IniSection>>.Add(KeyValuePair<String, IniSection> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(item.Key));
            }

            ((IDictionary<String, IniSection>) Sections).Add(item);
        }
        
        public void Insert(Int32 index, String key, IniSection value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }
            
            Sections.Insert(index, key, value);
        }
        
        public void Sort()
        {
            Sections.Sort();
        }

        public void Sort(Comparison<String> comparison)
        {
            Sections.Sort(comparison);
        }

        public void Sort(IComparer<String>? comparer)
        {
            Sections.Sort(comparer);
        }

        public void Reverse()
        {
            Sections.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Sections.Reverse(index, count);
        }

        public Boolean Remove(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            }

            return Sections.Remove(key);
        }

        Boolean ICollection<KeyValuePair<String, IniSection>>.Remove(KeyValuePair<String, IniSection> item)
        {
            if (String.IsNullOrEmpty(item.Key))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(item.Key));
            }

            return ((IDictionary<String, IniSection>) Sections).Remove(item);
        }
        
        public Boolean RemoveAt(Int32 index)
        {
            return Sections.RemoveAt(index);
        }

        public void Clear()
        {
            Sections.Clear();
        }

        public void Read(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using StringReader reader = new StringReader(value);
            Read(reader);
        }

        public void Read(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = new StreamReader(stream);
            Read(reader);
        }

        public void Read(TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            IniSection? section = null;
            while (reader.ReadLine() is { } line)
            {
                ReadLine(line, ref section);
            }
        }

        public async Task ReadAsync(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using StringReader reader = new StringReader(value);
            await ReadAsync(reader);
        }

        public Task ReadAsync(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamReader reader = new StreamReader(stream);
            return ReadAsync(reader);
        }

        public async Task ReadAsync(TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            IniSection? section = null;
            while (await reader.ReadLineAsync() is { } line)
            {
                ReadLine(line, ref section);
            }
        }

        protected virtual Boolean ReadLine(String? line, ref IniSection? section)
        {
            if (line is null)
            {
                return false;
            }

            String trim = line.TrimStart();

            if (trim.Length <= 0)
            {
                return false;
            }

            if (trim[0] == '[')
            {
                Int32 index = trim.IndexOf(']');
                if (index <= 0)
                {
                    return false;
                }

                String name = trim.Substring(1, index - 1).Trim();
                section = new IniSection(Comparer);
                Sections[name] = section;
                return true;
            }

            if (section is null || trim[0] == ';')
            {
                return false;
            }

            if (!ReadValue(line, out String? key, out IniValue result))
            {
                return false;
            }

            section[key] = result;
            return true;
        }

        protected virtual Boolean ReadValue(String? line, [MaybeNullWhen(false)] out String key, out IniValue value)
        {
            if (line is null)
            {
                key = null;
                value = default;
                return false;
            }

            Int32 index = line.IndexOf('=');
            if (index <= 0)
            {
                key = null;
                value = default;
                return false;
            }

            key = line.Substring(0, index).Trim();
            String result = line.Substring(index + 1);

            value = new IniValue(result);
            return true;
        }

        public String Write()
        {
            using StringWriter writer = new StringWriter();
            Write(writer);
            return writer.ToString();
        }

        public void Write(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = new StreamWriter(stream);
            Write(writer);
        }

        public virtual void Write(TextWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            Int32 index = 0;
            foreach ((String ini, IniSection section) in Sections.Where(section => !section.Value.IsEmpty))
            {
                if (index++ > 0)
                {
                    writer.WriteLine(String.Empty);
                }
                
                writer.WriteLine($"[{ini.Trim()}]");

                foreach ((String key, IniValue value) in section)
                {
                    writer.WriteLine($"{key}={value}");
                }
            }
        }

        public async Task<String> WriteAsync()
        {
            await using StringWriter writer = new StringWriter();
            await WriteAsync(writer);
            return writer.ToString();
        }

        public Task WriteAsync(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using StreamWriter writer = new StreamWriter(stream);
            return WriteAsync(writer);
        }

        public virtual async Task WriteAsync(TextWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            Int32 index = 0;
            foreach ((String ini, IniSection section) in Sections.Where(section => !section.Value.IsEmpty))
            {
                if (index++ > 0)
                {
                    await writer.WriteLineAsync(String.Empty);
                }
                
                await writer.WriteLineAsync($"[{ini.Trim()}]");

                foreach ((String key, IniValue value) in section)
                {
                    await writer.WriteLineAsync($"{key}={value}");
                }
            }
        }

        void ICollection<KeyValuePair<String, IniSection>>.CopyTo(KeyValuePair<String, IniSection>[] array, Int32 arrayIndex)
        {
            ((IDictionary<String, IniSection>) Sections).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<String, IniSection>> GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IniSection this[String key]
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(key));
                }

                if (Sections.TryGetValue(key, out IniSection? result))
                {
                    return result;
                }

                result = new IniSection(Comparer);
                Sections[key] = result;
                return result;
            }
            set
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(key));
                }

                if (!Equals(value.Comparer, Comparer))
                {
                    value = new IniSection(value, Comparer);
                }

                Sections[key] = value;
            }
        }
        
        public IniSection this[Int32 index]
        {
            get
            {
                return Sections.GetValueByIndex(index);
            }
            set
            {
                Sections.TrySetValueByIndex(index, value);
            }
        }
    }
}