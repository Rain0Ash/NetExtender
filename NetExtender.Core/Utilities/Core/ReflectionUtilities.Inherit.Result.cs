// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Immutable.Dictionaries;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        public static ReflectionInheritResult Get(this ImmutableDictionary<Type, ReflectionInheritResult> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return source.TryGetValue(type, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
        }
        
        public static ReflectionInheritResult Get(this ImmutableDictionary<Assembly, ReflectionInheritResult> source, Assembly assembly)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return source.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
        }
        
        public static ReflectionInheritResult Get(this ImmutableDictionary<Type, ImmutableDictionary<Assembly, ReflectionInheritResult>> source, Assembly assembly, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return source.TryGetValue(type, out ImmutableDictionary<Assembly, ReflectionInheritResult>? result) ? result.Get(assembly) : ReflectionInheritResult.Empty;
        }
    }
    
    public sealed class Inherit : IImmutableDictionary<Type, ReflectionInheritResult>
    {
        public readonly struct Result : IImmutableDictionary<Type, ReflectionInheritResult>
        {
            public static implicit operator Inherit(Result value)
            {
                return value.Type;
            }
            
            public static implicit operator AttributeInherit(Result value)
            {
                return value.Attribute;
            }
            
            public Inherit Type { get; }
            public AttributeInherit Attribute { get; }
            
            public IImmutableDictionary<Type,IImmutableDictionary<Assembly,ReflectionInheritResult>> Assembly
            {
                get
                {
                    return Type.Assembly;
                }
            }

            public Int32 Count
            {
                get
                {
                    return Type.Count;
                }
            }
            
            public IEnumerable<Type> Keys
            {
                get
                {
                    return Type.Keys;
                }
            }
            
            public IEnumerable<ReflectionInheritResult> Values
            {
                get
                {
                    return Type.Values;
                }
            }
            
            public Result(Inherit type, AttributeInherit attribute)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            }
            
            public Boolean Contains(KeyValuePair<Type, ReflectionInheritResult> pair)
            {
                return Type.Contains(pair);
            }
            
            public Boolean ContainsKey(Type key)
            {
                return Type.ContainsKey(key);
            }
            
            public Boolean TryGetKey(Type equalKey, out Type actualKey)
            {
                return Type.TryGetKey(equalKey, out actualKey);
            }
            
            public Boolean TryGetValue(Type key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
            {
                return Type.TryGetValue(key, out value);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> Add(Type key, ReflectionInheritResult value)
            {
                return Type.Add(key, value);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> AddRange(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> pairs)
            {
                return Type.AddRange(pairs);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> SetItem(Type key, ReflectionInheritResult value)
            {
                return Type.SetItem(key, value);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> SetItems(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> items)
            {
                return Type.SetItems(items);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> Remove(Type key)
            {
                return Type.Remove(key);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> RemoveRange(IEnumerable<Type> keys)
            {
                return Type.RemoveRange(keys);
            }
            
            public IImmutableDictionary<Type, ReflectionInheritResult> Clear()
            {
                return Type.Clear();
            }
            
            public IEnumerator<KeyValuePair<Type, ReflectionInheritResult>> GetEnumerator()
            {
                return Type.GetEnumerator();
            }
            
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable) Type).GetEnumerator();
            }
            
            public ReflectionInheritResult this[Type key]
            {
                get
                {
                    return Type[key];
                }
            }
            
            public ReflectionInheritResult this[Assembly assembly, Type key]
            {
                get
                {
                    return Type[assembly, key];
                }
            }
        }
        
        private ImmutableDictionary<Type, ReflectionInheritResult> Type { get; }
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> Assembly { get; }
        
        public Int32 Count
        {
            get
            {
                return Type.Count;
            }
        }
        
        public IEnumerable<Type> Keys
        {
            get
            {
                return Type.Keys;
            }
        }
        
        public IEnumerable<ReflectionInheritResult> Values
        {
            get
            {
                return Type.Values;
            }
        }

        internal Inherit(ConcurrentDictionary<Type, ConcurrentHashSet<Type>> type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            Type = Builder(type);
            Assembly = Type.ToImmutableDictionary(static pair => pair.Key, Assemblies);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static ImmutableDictionary<Type, ReflectionInheritResult> Builder(ConcurrentDictionary<Type, ConcurrentHashSet<Type>> concurrent)
        {
            ImmutableDictionary<Type, ReflectionInheritResult>.Builder builder = ImmutableDictionary.CreateBuilder<Type, ReflectionInheritResult>();
            
            lock (concurrent)
            {
                foreach ((Type type, ConcurrentHashSet<Type> set) in concurrent)
                {
                    lock (set)
                    {
                        builder[type] = Set(set);
                    }
                }
            }
            
            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReflectionInheritResult Set(ConcurrentHashSet<Type> set)
        {
            return ReflectionInheritResult.Create(set);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReflectionInheritResult Set(KeyValuePair<Type, ConcurrentHashSet<Type>> pair)
        {
            return ReflectionInheritResult.Create(pair.Value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IImmutableDictionary<Assembly, ReflectionInheritResult> Assemblies(KeyValuePair<Type, ReflectionInheritResult> pair)
        {
            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ReflectionInheritResult Set(KeyValuePair<Assembly, ConcurrentHashSet<Type>> pair)
            {
                return ReflectionInheritResult.Create(pair.Value);
            }
            
            ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>> assemblies = new ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>>();
            
            foreach (Type type in pair.Value)
            {
                assemblies.GetOrAdd(type.Assembly, static _ => new ConcurrentHashSet<Type>()).Add(type);
            }
            
            return assemblies.ToImmutableDictionary(static pair => pair.Key, Set);
        }
        
        public Boolean Contains(KeyValuePair<Type, ReflectionInheritResult> pair)
        {
            return Type.Contains(pair);
        }
        
        public Boolean ContainsKey(Type key)
        {
            return Type.ContainsKey(key);
        }
        
        public Boolean TryGetKey(Type equalKey, out Type actualKey)
        {
            return Type.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(Type key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
        {
            return Type.TryGetValue(key, out value);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Add(Type key, ReflectionInheritResult value)
        {
            return Type.Add(key, value);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> AddRange(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> pairs)
        {
            return Type.AddRange(pairs);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> SetItem(Type key, ReflectionInheritResult value)
        {
            return Type.SetItem(key, value);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> SetItems(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> items)
        {
            return Type.SetItems(items);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Remove(Type key)
        {
            return Type.Remove(key);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> RemoveRange(IEnumerable<Type> keys)
        {
            return Type.RemoveRange(keys);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Clear()
        {
            return Type.Clear();
        }
        
        public IEnumerator<KeyValuePair<Type, ReflectionInheritResult>> GetEnumerator()
        {
            return Type.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Type).GetEnumerator();
        }
        
        public ReflectionInheritResult this[Type key]
        {
            get
            {
                return Type.TryGetValue(key, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
        
        public ReflectionInheritResult this[Assembly assembly, Type key]
        {
            get
            {
                return Assembly.TryGetValue(key, out IImmutableDictionary<Assembly, ReflectionInheritResult>? immutable) && immutable.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }
    
    public sealed class AttributeInherit : IImmutableDictionary<Type, ReflectionInheritResult>, IImmutableDictionary<Attribute, ReflectionInheritResult>
    {
        private IImmutableDictionary<Type, ReflectionInheritResult> Type { get; }
        private IImmutableDictionary<Attribute, ReflectionInheritResult> Attribute { get; }

        public AttributeAssemblyInherit Assembly { get; }
        
        public Int32 Count
        {
            get
            {
                return Type.Count;
            }
        }
        
        Int32 IReadOnlyCollection<KeyValuePair<Attribute, ReflectionInheritResult>>.Count
        {
            get
            {
                return Attribute.Count;
            }
        }
        
        public IEnumerable<Type> Keys
        {
            get
            {
                return Type.Keys;
            }
        }
        
        IEnumerable<Attribute> IReadOnlyDictionary<Attribute, ReflectionInheritResult>.Keys
        {
            get
            {
                return Attribute.Keys;
            }
        }
        
        public IEnumerable<ReflectionInheritResult> Values
        {
            get
            {
                return Type.Values;
            }
        }
        
        IEnumerable<ReflectionInheritResult> IReadOnlyDictionary<Attribute, ReflectionInheritResult>.Values
        {
            get
            {
                return Attribute.Values;
            }
        }
        
        internal AttributeInherit(ConcurrentDictionary<Type, ConcurrentHashSet<Type>> type, ConcurrentDictionary<Attribute, ConcurrentHashSet<Type>> attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            Type = Builder(type);
            Attribute = Builder(attribute);
            Assembly = new AttributeAssemblyInherit(Type, Attribute);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static ImmutableDictionary<Type, ReflectionInheritResult> Builder(ConcurrentDictionary<Type, ConcurrentHashSet<Type>> concurrent)
        {
            ImmutableDictionary<Type, ReflectionInheritResult>.Builder builder = ImmutableDictionary.CreateBuilder<Type, ReflectionInheritResult>();
            
            lock (concurrent)
            {
                foreach ((Type type, ConcurrentHashSet<Type> set) in concurrent)
                {
                    lock (set)
                    {
                        builder[type] = Set(set);
                    }
                }
            }
            
            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static ImmutableDictionary<Attribute, ReflectionInheritResult> Builder(ConcurrentDictionary<Attribute, ConcurrentHashSet<Type>> concurrent)
        {
            ImmutableDictionary<Attribute, ReflectionInheritResult>.Builder builder = ImmutableDictionary.CreateBuilder<Attribute, ReflectionInheritResult>();
            
            lock (concurrent)
            {
                foreach ((Attribute attribute, ConcurrentHashSet<Type> set) in concurrent)
                {
                    lock (set)
                    {
                        builder[attribute] = Set(set);
                    }
                }
            }
            
            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReflectionInheritResult Set(ConcurrentHashSet<Type> set)
        {
            return ReflectionInheritResult.Create(set);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReflectionInheritResult Set(KeyValuePair<Type, ConcurrentHashSet<Type>> pair)
        {
            return ReflectionInheritResult.Create(pair.Value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReflectionInheritResult Set(KeyValuePair<Attribute, ConcurrentHashSet<Type>> pair)
        {
            return ReflectionInheritResult.Create(pair.Value);
        }
        
        public Boolean Contains(KeyValuePair<Type, ReflectionInheritResult> pair)
        {
            return Type.Contains(pair);
        }
        
        public Boolean Contains(KeyValuePair<Attribute, ReflectionInheritResult> pair)
        {
            return Attribute.Contains(pair);
        }
        
        public Boolean ContainsKey(Type key)
        {
            return Type.ContainsKey(key);
        }
        
        public Boolean ContainsKey(Attribute key)
        {
            return Attribute.ContainsKey(key);
        }
        
        public Boolean TryGetKey(Type equalKey, out Type actualKey)
        {
            return Type.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetKey(Attribute equalKey, out Attribute actualKey)
        {
            return Attribute.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(Type key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
        {
            return Type.TryGetValue(key, out value);
        }
        
        public Boolean TryGetValue(Attribute key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
        {
            return Attribute.TryGetValue(key, out value);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Add(Type key, ReflectionInheritResult value)
        {
            return Type.Add(key, value);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> Add(Attribute key, ReflectionInheritResult value)
        {
            return Attribute.Add(key, value);
        }

        public IImmutableDictionary<Type, ReflectionInheritResult> AddRange(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> pairs)
        {
            return Type.AddRange(pairs);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> AddRange(IEnumerable<KeyValuePair<Attribute, ReflectionInheritResult>> pairs)
        {
            return Attribute.AddRange(pairs);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> SetItem(Type key, ReflectionInheritResult value)
        {
            return Type.SetItem(key, value);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> SetItem(Attribute key, ReflectionInheritResult value)
        {
            return Attribute.SetItem(key, value);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> SetItems(IEnumerable<KeyValuePair<Type, ReflectionInheritResult>> items)
        {
            return Type.SetItems(items);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> SetItems(IEnumerable<KeyValuePair<Attribute, ReflectionInheritResult>> items)
        {
            return Attribute.SetItems(items);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Remove(Type key)
        {
            return Type.Remove(key);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> Remove(Attribute key)
        {
            return Attribute.Remove(key);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> RemoveRange(IEnumerable<Type> keys)
        {
            return Type.RemoveRange(keys);
        }
        
        public IImmutableDictionary<Attribute, ReflectionInheritResult> RemoveRange(IEnumerable<Attribute> keys)
        {
            return Attribute.RemoveRange(keys);
        }
        
        public IImmutableDictionary<Type, ReflectionInheritResult> Clear()
        {
            return Type.Clear();
        }
        
        IImmutableDictionary<Attribute, ReflectionInheritResult> IImmutableDictionary<Attribute, ReflectionInheritResult>.Clear()
        {
            return Attribute.Clear();
        }
        
        public IEnumerator<KeyValuePair<Type, ReflectionInheritResult>> GetEnumerator()
        {
            return Type.GetEnumerator();
        }
        
        IEnumerator<KeyValuePair<Attribute, ReflectionInheritResult>> IEnumerable<KeyValuePair<Attribute, ReflectionInheritResult>>.GetEnumerator()
        {
            return Attribute.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Type).GetEnumerator();
        }
        
        public ReflectionInheritResult this[Type key]
        {
            get
            {
                return Type.TryGetValue(key, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
        
        public ReflectionInheritResult this[Attribute key]
        {
            get
            {
                return Attribute.TryGetValue(key, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }
    
    public sealed class AttributeAssemblyInherit : IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>>, IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>
    {
        private IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> Type { get; }
        private IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> Attribute { get; }
        
        public Int32 Count
        {
            get
            {
                return Type.Count;
            }
        }
        
        Int32 IReadOnlyCollection<KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>>.Count
        {
            get
            {
                return Attribute.Count;
            }
        }
        
        public IEnumerable<Type> Keys
        {
            get
            {
                return Type.Keys;
            }
        }
        
        IEnumerable<Attribute> IReadOnlyDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>.Keys
        {
            get
            {
                return Attribute.Keys;
            }
        }
        
        public IEnumerable<IImmutableDictionary<Assembly, ReflectionInheritResult>> Values
        {
            get
            {
                return Type.Values;
            }
        }
        
        IEnumerable<IImmutableDictionary<Assembly, ReflectionInheritResult>> IReadOnlyDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>.Values
        {
            get
            {
                return Attribute.Values;
            }
        }
        
        internal AttributeAssemblyInherit(IImmutableDictionary<Type, ReflectionInheritResult> type, IImmutableDictionary<Attribute, ReflectionInheritResult> attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            Type = type.ToImmutableDictionary(static pair => pair.Key, Assemblies);
            Attribute = attribute.ToImmutableDictionary(static pair => pair.Key, Assemblies);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IImmutableDictionary<Assembly, ReflectionInheritResult> Assemblies(KeyValuePair<Type, ReflectionInheritResult> pair)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ReflectionInheritResult Set(KeyValuePair<Assembly, ConcurrentHashSet<Type>> pair)
            {
                return ReflectionInheritResult.Create(pair.Value);
            }
            
            ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>> assemblies = new ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>>();
            
            foreach (Type type in pair.Value)
            {
                assemblies.GetOrAdd(type.Assembly, static _ => new ConcurrentHashSet<Type>()).Add(type);
            }
            
            return assemblies.ToImmutableDictionary(static pair => pair.Key, Set);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IImmutableDictionary<Assembly, ReflectionInheritResult> Assemblies(KeyValuePair<Attribute, ReflectionInheritResult> pair)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ReflectionInheritResult Set(KeyValuePair<Assembly, ConcurrentHashSet<Type>> pair)
            {
                return ReflectionInheritResult.Create(pair.Value);
            }
            
            ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>> assemblies = new ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>>();
            
            foreach (Type type in pair.Value)
            {
                assemblies.GetOrAdd(type.Assembly, static _ => new ConcurrentHashSet<Type>()).Add(type);
            }
            
            return assemblies.ToImmutableDictionary(static pair => pair.Key, Set);
        }
        
        public Boolean Contains(KeyValuePair<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> pair)
        {
            return Type.Contains(pair);
        }
        
        public Boolean Contains(KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> pair)
        {
            return Attribute.Contains(pair);
        }
        
        public Boolean ContainsKey(Type key)
        {
            return Type.ContainsKey(key);
        }
        
        public Boolean ContainsKey(Attribute key)
        {
            return Attribute.ContainsKey(key);
        }
        
        public Boolean TryGetKey(Type equalKey, out Type actualKey)
        {
            return Type.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetKey(Attribute equalKey, out Attribute actualKey)
        {
            return Attribute.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(Type key, [MaybeNullWhen(false)] out IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Type.TryGetValue(key, out value);
        }
        
        public Boolean TryGetValue(Attribute key, [MaybeNullWhen(false)] out IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Attribute.TryGetValue(key, out value);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> Add(Type key, IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Type.Add(key, value);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> Add(Attribute key, IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Attribute.Add(key, value);
        }

        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> AddRange(IEnumerable<KeyValuePair<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>>> pairs)
        {
            return Type.AddRange(pairs);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> AddRange(IEnumerable<KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>> pairs)
        {
            return Attribute.AddRange(pairs);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> SetItem(Type key, IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Type.SetItem(key, value);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> SetItem(Attribute key, IImmutableDictionary<Assembly, ReflectionInheritResult> value)
        {
            return Attribute.SetItem(key, value);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> SetItems(IEnumerable<KeyValuePair<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>>> items)
        {
            return Type.SetItems(items);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> SetItems(IEnumerable<KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>> items)
        {
            return Attribute.SetItems(items);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> Remove(Type key)
        {
            return Type.Remove(key);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> Remove(Attribute key)
        {
            return Attribute.Remove(key);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> RemoveRange(IEnumerable<Type> keys)
        {
            return Type.RemoveRange(keys);
        }
        
        public IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> RemoveRange(IEnumerable<Attribute> keys)
        {
            return Attribute.RemoveRange(keys);
        }
        
        public IImmutableDictionary<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>> Clear()
        {
            return Type.Clear();
        }
        
        IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>> IImmutableDictionary<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>.Clear()
        {
            return Attribute.Clear();
        }
        
        public IEnumerator<KeyValuePair<Type, IImmutableDictionary<Assembly, ReflectionInheritResult>>> GetEnumerator()
        {
            return Type.GetEnumerator();
        }
        
        IEnumerator<KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>> IEnumerable<KeyValuePair<Attribute, IImmutableDictionary<Assembly, ReflectionInheritResult>>>.GetEnumerator()
        {
            return Attribute.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Type).GetEnumerator();
        }
        
        public IImmutableDictionary<Assembly, ReflectionInheritResult> this[Type key]
        {
            get
            {
                return Type.TryGetValue(key, out IImmutableDictionary<Assembly, ReflectionInheritResult>? result) ? result : ImmutableDictionary<Assembly, ReflectionInheritResult>.Empty;
            }
        }
        
        public IImmutableDictionary<Assembly, ReflectionInheritResult> this[Attribute key]
        {
            get
            {
                return Attribute.TryGetValue(key, out IImmutableDictionary<Assembly, ReflectionInheritResult>? result) ? result : ImmutableMultiDictionary<Assembly, ReflectionInheritResult>.Empty;
            }
        }
        
        public ReflectionInheritResult this[Assembly assembly, Type key]
        {
            get
            {
                return Type.TryGetValue(key, out IImmutableDictionary<Assembly, ReflectionInheritResult>? immutable) && immutable.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
        
        public ReflectionInheritResult this[Assembly assembly, Attribute key]
        {
            get
            {
                return Attribute.TryGetValue(key, out IImmutableDictionary<Assembly, ReflectionInheritResult>? immutable) && immutable.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }
}