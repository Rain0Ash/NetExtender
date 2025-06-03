using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Environments
{
    public sealed class EnvironmentVariableCollection : IReadOnlyHashDictionary<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>, IReadOnlyDictionary<String, String?>, IDictionary<(EnvironmentVariableTarget, String), String?>, ICloneable<EnvironmentVariableCollection>
    {
        private SyncRoot SyncRoot { get; } = SyncRoot.Create();
        private ConcurrentDictionary<String, String> Process { get; }
        private ConcurrentDictionary<String, String> User { get; }
        private ConcurrentDictionary<String, String> Machine { get; }

        public Int32 Count
        {
            get
            {
                lock (SyncRoot)
                {
                    Int32 count = 0;
                    foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                    {
                        if (TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                        {
                            count += container.Count;
                        }
                    }
                
                    return count;
                }
            }
        }

        Int32 IReadOnlyCollection<KeyValuePair<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>>.Count
        {
            get
            {
                return EnumUtilities.Count<EnvironmentVariableTarget>();
            }
        }

        private readonly Collection _collection;

        public ICollection<(EnvironmentVariableTarget, String)> Keys
        {
            get
            {
                return _collection;
            }
        }

        IEnumerable<String> IReadOnlyDictionary<String, String?>.Keys
        {
            get
            {
                return _collection.Keys();
            }
        }

        IEnumerable<EnvironmentVariableTarget> IReadOnlyDictionary<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>.Keys
        {
            get
            {
                return _collection;
            }
        }

        public ICollection<String?> Values
        {
            get
            {
                return _collection;
            }
        }

        IEnumerable<String?> IReadOnlyDictionary<String, String?>.Values
        {
            get
            {
                return _collection;
            }
        }

        IEnumerable<ConcurrentDictionary<String, String>> IReadOnlyDictionary<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>.Values
        {
            get
            {
                return _collection;
            }
        }

        public IEqualityComparer<String> Comparer
        {
            get
            {
                IEqualityComparer<String> comparer = Process.Comparer;
                return ReferenceEquals(comparer, User.Comparer) && ReferenceEquals(comparer, Machine.Comparer) ? comparer : throw new InvalidOperationException();
            }
        }

        IEqualityComparer<EnvironmentVariableTarget> IReadOnlyHashDictionary<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>.Comparer
        {
            get
            {
                return EqualityComparer<EnvironmentVariableTarget>.Default;
            }
        }

        Boolean ICollection<KeyValuePair<(EnvironmentVariableTarget, String), String?>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public EnvironmentVariableCollection()
            : this(null)
        {
        }

        public EnvironmentVariableCollection(Boolean initialize)
            : this(null, initialize)
        {
        }

        public EnvironmentVariableCollection(IEqualityComparer<String>? comparer)
            : this(comparer, false)
        {
        }

        public EnvironmentVariableCollection(IEqualityComparer<String>? comparer, Boolean initialize)
        {
            comparer ??= StringComparer.Ordinal;
            Process = new ConcurrentDictionary<String, String>(comparer);
            User = new ConcurrentDictionary<String, String>(comparer);
            Machine = new ConcurrentDictionary<String, String>(comparer);
            _collection = new Collection(this);

            if (initialize)
            {
                Refill();
            }
        }

        private EnvironmentVariableCollection(ConcurrentDictionary<String, String> process, ConcurrentDictionary<String, String> user, ConcurrentDictionary<String, String> machine)
        {
            Process = process ?? throw new ArgumentNullException(nameof(process));
            User = user ?? throw new ArgumentNullException(nameof(user));
            Machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _collection = new Collection(this);
        }
        
        public EnvironmentVariableCollection Fill()
        {
            return Fill(static target => EnvironmentUtilities.WrapEnvironmentVariables(Environment.GetEnvironmentVariables(target)));
        }

        public EnvironmentVariableCollection Fill(Func<EnvironmentVariableTarget, IReadOnlyDictionary<String, String?>> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            System.Threading.Tasks.Parallel.ForEach(EnumUtilities.GetValues<EnvironmentVariableTarget>(), target =>
            {
                if (!TryGetValue(target, out ConcurrentDictionary<String, String>? environment))
                {
                    return;
                }

                foreach ((String? variable, String? value) in action.Invoke(target))
                {
                    if (!String.IsNullOrEmpty(variable) && value is not null)
                    {
                        environment.TryAdd(variable, value);
                    }
                }
            });
            
            return this;
        }

        public EnvironmentVariableCollection Refill()
        {
            return Refill(static target => EnvironmentUtilities.WrapEnvironmentVariables(Environment.GetEnvironmentVariables(target)));
        }

        public EnvironmentVariableCollection Refill(Func<EnvironmentVariableTarget, IReadOnlyDictionary<String, String?>> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            System.Threading.Tasks.Parallel.ForEach(EnumUtilities.GetValues<EnvironmentVariableTarget>(), target =>
            {
                if (!TryGetValue(target, out ConcurrentDictionary<String, String>? environment))
                {
                    return;
                }

                environment.Clear();
                foreach ((String? variable, String? value) in action.Invoke(target))
                {
                    if (!String.IsNullOrEmpty(variable) && value is not null)
                    {
                        environment[variable] = value;
                    }
                }
            });
            
            return this;
        }

        Boolean ICollection<KeyValuePair<(EnvironmentVariableTarget, String), String?>>.Contains(KeyValuePair<(EnvironmentVariableTarget, String), String?> item)
        {
            return TryGetValue(item.Key.Item1, item.Key.Item2, out String? value) && value == item.Value;
        }

        public Boolean ContainsKey(EnvironmentVariableTarget target)
        {
            return TryGetValue(target, out _);
        }

        public Boolean ContainsKey(String? variable)
        {
            return !String.IsNullOrEmpty(variable) && TryGetValue(variable, out _);
        }

        public Boolean ContainsKey(EnvironmentVariableTarget target, String? variable)
        {
            return !String.IsNullOrEmpty(variable) && TryGetValue(target, out ConcurrentDictionary<String, String>? container) && container.ContainsKey(variable);
        }

        Boolean IDictionary<(EnvironmentVariableTarget, String), String?>.ContainsKey((EnvironmentVariableTarget, String?) key)
        {
            return ContainsKey(key.Item1, key.Item2);
        }

        public Boolean TryGetValue(EnvironmentVariableTarget target, [MaybeNullWhen(false)] out ConcurrentDictionary<String, String> value)
        {
            return (value = target switch
            {
                EnvironmentVariableTarget.Process => Process,
                EnvironmentVariableTarget.User => User,
                EnvironmentVariableTarget.Machine => Machine,
                _ => null
            }) is not null;
        }

        public Boolean TryGetValue(String? variable, [MaybeNullWhen(false)] out String value)
        {
            value = null;
            if (String.IsNullOrEmpty(variable))
            {
                return false;
            }
            
            foreach ((_, ConcurrentDictionary<String, String> container) in this)
            {
                if (!container.TryGetValue(variable, out String? result))
                {
                    continue;
                }

                value = result;
                return true;
            }

            return false;
        }

        public Boolean TryGetValue(EnvironmentVariableTarget target, String? variable, [MaybeNullWhen(false)] out String value)
        {
            value = null;
            return variable is not null && TryGetValue(target, out ConcurrentDictionary<String, String>? container) && container.TryGetValue(variable, out value);
        }

        Boolean IDictionary<(EnvironmentVariableTarget, String), String?>.TryGetValue((EnvironmentVariableTarget, String?) key, [MaybeNullWhen(false)] out String value)
        {
            return TryGetValue(key.Item1, key.Item2, out value);
        }

        public Boolean TryAdd(EnvironmentVariableTarget target, String? variable, String? value)
        {
            if (String.IsNullOrEmpty(variable))
            {
                return false;
            }

            if (!TryGetValue(target, out ConcurrentDictionary<String, String>? container))
            {
                return false;
            }

            return value is not null ? container.TryAdd(variable, value) : container.TryRemove(variable, out _);
        }

        void ICollection<KeyValuePair<(EnvironmentVariableTarget, String), String?>>.Add(KeyValuePair<(EnvironmentVariableTarget, String), String?> item)
        {
            TryAdd(item.Key.Item1, item.Key.Item2, item.Value);
        }

        void IDictionary<(EnvironmentVariableTarget, String), String?>.Add((EnvironmentVariableTarget, String) key, String? value)
        {
            TryAdd(key.Item1, key.Item2, value);
        }

        public Boolean TryRemove(EnvironmentVariableTarget target, String? variable)
        {
            return !String.IsNullOrEmpty(variable) && TryGetValue(target, out ConcurrentDictionary<String, String>? container) && container.TryRemove(variable, out _);
        }

        public Boolean TryRemove(EnvironmentVariableTarget target, KeyValuePair<String?, String?> item)
        {
            return !String.IsNullOrEmpty(item.Key) && TryGetValue(target, out ConcurrentDictionary<String, String>? container) && container.TryRemove(item!);
        }

        Boolean ICollection<KeyValuePair<(EnvironmentVariableTarget, String), String?>>.Remove(KeyValuePair<(EnvironmentVariableTarget, String), String?> item)
        {
            (EnvironmentVariableTarget target, String variable) = item.Key;
            return TryRemove(target, new KeyValuePair<String?, String?>(variable, item.Value));
        }

        Boolean IDictionary<(EnvironmentVariableTarget, String), String?>.Remove((EnvironmentVariableTarget, String) key)
        {
            return TryRemove(key.Item1, key.Item2);
        }

        public void Clear()
        {
            foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
            {
                if (TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                {
                    container.Clear();
                }
            }
        }

        public void CopyTo(KeyValuePair<(EnvironmentVariableTarget, String), String?>[] array, Int32 index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            lock (SyncRoot)
            {
                if (array.Length - index < Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(array), array, null);
                }

                foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                {
                    if (!TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                    {
                        continue;
                    }
                    
                    foreach ((String variable, String value) in container)
                    {
                        array[index++] = new KeyValuePair<(EnvironmentVariableTarget, String), String?>((target, variable), value);
                    }
                }
            }
        }

        public EnvironmentVariableCollection Clone()
        {
            ConcurrentDictionary<String, String> process = new ConcurrentDictionary<String, String>(Process, Process.Comparer);
            ConcurrentDictionary<String, String> user = new ConcurrentDictionary<String, String>(User, User.Comparer);
            ConcurrentDictionary<String, String> machine = new ConcurrentDictionary<String, String>(Machine, Machine.Comparer);
            return new EnvironmentVariableCollection(process, user, machine);
        }

        public IEnumerator<KeyValuePair<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>> GetEnumerator()
        {
            foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
            {
                if (TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                {
                    yield return new KeyValuePair<EnvironmentVariableTarget, ConcurrentDictionary<String, String>>(target, container);
                }
            }
        }

        IEnumerator<KeyValuePair<String, String?>> IEnumerable<KeyValuePair<String, String?>>.GetEnumerator()
        {
            foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
            {
                if (!TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                {
                    continue;
                }

                foreach ((String variable, String value) in container)
                {
                    yield return new KeyValuePair<String, String?>(variable, value);
                }
            }
        }

        IEnumerator<KeyValuePair<(EnvironmentVariableTarget, String), String?>> IEnumerable<KeyValuePair<(EnvironmentVariableTarget, String), String?>>.GetEnumerator()
        {
            foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
            {
                if (!TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                {
                    continue;
                }

                foreach ((String variable, String value) in container)
                {
                    yield return new KeyValuePair<(EnvironmentVariableTarget, String), String?>((target, variable), value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public String? this[String? variable]
        {
            get
            {
                if (String.IsNullOrEmpty(variable))
                {
                    return null;
                }
                
                foreach ((_, ConcurrentDictionary<String, String> value) in this)
                {
                    if (value.TryGetValue(variable, out String? result))
                    {
                        return result;
                    }
                }

                return null;
            }
        }

        public String? this[EnvironmentVariableTarget target, String? variable]
        {
            get
            {
                return !String.IsNullOrEmpty(variable) && this[target].TryGetValue(variable, out String? result) ? result : null;
            }
            set
            {
                if (String.IsNullOrEmpty(variable))
                {
                    return;
                }
                
                ConcurrentDictionary<String, String> container = this[target];
                
                if (value is null)
                {
                    container.TryRemove(variable, out _);
                    return;
                }

                container[variable] = value;
            }
        }

        public ConcurrentDictionary<String, String> this[EnvironmentVariableTarget target]
        {
            get
            {
                return TryGetValue(target, out ConcurrentDictionary<String, String>? container) ? container : throw new KeyNotFoundException($"The given key '{target}' was not present in the dictionary.");
            }
        }

        String? IDictionary<(EnvironmentVariableTarget, String), String?>.this[(EnvironmentVariableTarget, String?) key]
        {
            get
            {
                return this[key.Item1, key.Item2];
            }
            set
            {
                this[key.Item1, key.Item2] = value;
            }
        }

        private sealed class Collection : ICollection<(EnvironmentVariableTarget, String)>, ICollection<String?>, IEnumerable<KeyValuePair<(EnvironmentVariableTarget, String), String>>, IEnumerable<EnvironmentVariableTarget>, IEnumerable<ConcurrentDictionary<String, String>>
        {
            private EnvironmentVariableCollection Internal { get; }

            public Int32 Count
            {
                get
                {
                    lock (Internal.SyncRoot)
                    {
                        Int32 count = 0;
                        foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                        {
                            if (Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                            {
                                count += container.Count;
                            }
                        }
                
                        return count;
                    }
                }
            }

            Int32 ICollection<String?>.Count
            {
                get
                {
                    lock (Internal.SyncRoot)
                    {
                        Int32 count = 0;
                        foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                        {
                            if (Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                            {
                                count += container.Values.Count;
                            }
                        }
                
                        return count;
                    }
                }
            }

            Boolean ICollection<(EnvironmentVariableTarget, String)>.IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            Boolean ICollection<String?>.IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public Collection(EnvironmentVariableCollection collection)
            {
                Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            Boolean ICollection<(EnvironmentVariableTarget, String)>.Contains((EnvironmentVariableTarget, String) item)
            {
                return Internal.TryGetValue(item.Item1, out ConcurrentDictionary<String, String>? container) && container.ContainsKey(item.Item2);
            }

            Boolean ICollection<String?>.Contains(String? item)
            {
                if (item is null)
                {
                    return false;
                }
                
                lock (Internal.SyncRoot)
                {
                    foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                    {
                        if (Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container) && container.Values.Contains(item))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            void ICollection<(EnvironmentVariableTarget, String)>.Add((EnvironmentVariableTarget, String) item)
            {
                throw new NotSupportedException();
            }

            void ICollection<String?>.Add(String? item)
            {
                throw new NotSupportedException();
            }

            Boolean ICollection<(EnvironmentVariableTarget, String)>.Remove((EnvironmentVariableTarget, String) item)
            {
                throw new NotSupportedException();
            }

            Boolean ICollection<String?>.Remove(String? item)
            {
                throw new NotSupportedException();
            }

            void ICollection<(EnvironmentVariableTarget, String)>.Clear()
            {
                throw new NotSupportedException();
            }

            void ICollection<String?>.Clear()
            {
                throw new NotSupportedException();
            }

            void ICollection<(EnvironmentVariableTarget, String)>.CopyTo((EnvironmentVariableTarget, String)[] array, Int32 index)
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                lock (Internal.SyncRoot)
                {
                    if (array.Length - index < Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(array), array, null);
                    }

                    foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                    {
                        if (!Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                        {
                            continue;
                        }
                        
                        foreach (String? variable in container.Keys)
                        {
                            array[index++] = (target, variable);
                        }
                    }
                }
            }

            void ICollection<String?>.CopyTo(String?[] array, Int32 index)
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                lock (Internal.SyncRoot)
                {
                    if (array.Length - index < Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(array), array, null);
                    }
                    
                    foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                    {
                        if (!Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                        {
                            continue;
                        }
                        
                        foreach (String? value in container.Values)
                        {
                            array[index++] = value;
                        }
                    }
                }
            }

            public IEnumerable<String> Keys()
            {
                foreach (((_, String key), _) in this)
                {
                    yield return key;
                }
            }

            public IEnumerator<String> GetKeysEnumerator()
            {
                return Keys().GetEnumerator();
            }

            public IEnumerator<KeyValuePair<(EnvironmentVariableTarget, String), String>> GetEnumerator()
            {
                foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                {
                    if (!Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                    {
                        continue;
                    }
                    
                    foreach ((String? variable, String? value) in container)
                    {
                        yield return new KeyValuePair<(EnvironmentVariableTarget, String), String>((target, variable), value);
                    }
                }
            }

            IEnumerator<(EnvironmentVariableTarget, String)> IEnumerable<(EnvironmentVariableTarget, String)>.GetEnumerator()
            {
                foreach (((EnvironmentVariableTarget, String) key, _) in this)
                {
                    yield return key;
                }
            }

            IEnumerator<String> IEnumerable<String?>.GetEnumerator()
            {
                foreach ((_, String? value) in this)
                {
                    yield return value;
                }
            }

            IEnumerator<EnvironmentVariableTarget> IEnumerable<EnvironmentVariableTarget>.GetEnumerator()
            {
                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                {
                    if (Internal.ContainsKey(target))
                    {
                        yield return target;
                    }
                }
            }

            IEnumerator<ConcurrentDictionary<String, String>> IEnumerable<ConcurrentDictionary<String, String>>.GetEnumerator()
            {
                foreach (EnvironmentVariableTarget target in EnumUtilities.GetValues<EnvironmentVariableTarget>())
                {
                    if (Internal.TryGetValue(target, out ConcurrentDictionary<String, String>? container))
                    {
                        yield return container;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}