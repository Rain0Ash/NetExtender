// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft
{
    public class PropertyContractResolver : DefaultContractResolver
    {
        private IDictionary<Type, ISet<String>> Ignore { get; }
        private IDictionary<Type, IDictionary<String, String>> Rename { get; }
        private IDictionary<Type, IDictionary<String, Int32>> Order { get; }

        protected DefaultContractResolver? Resolver { get; }

        protected delegate JsonProperty CreatePropertyHandler(MemberInfo info, MemberSerialization serialization);

        protected CreatePropertyHandler ResolverCreatePropertyHandler { get; }

        private CreatePropertyHandler CreateHandler(DefaultContractResolver? resolver)
        {
            if (resolver is null)
            {
                return base.CreateProperty;
            }

            return Resolver?.GetType()
                .GetMethod(nameof(CreateProperty), new[]{typeof(MemberInfo), typeof(MemberSerialization)})?
                .CreateDelegate<CreatePropertyHandler>() ?? base.CreateProperty;
        }

        public PropertyContractResolver()
            : this(null)
        {
        }

        public PropertyContractResolver(DefaultContractResolver? resolver)
        {
            Ignore = new Dictionary<Type, ISet<String>>();
            Rename = new Dictionary<Type, IDictionary<String, String>>();
            Order = new Dictionary<Type, IDictionary<String, Int32>>();

            ResolverCreatePropertyHandler = CreateHandler(Resolver ??= resolver);
        }

        public PropertyContractResolver RenameProperty(Type type, String property, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(property))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(property));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(name));
            }

            if (!Rename.ContainsKey(type))
            {
                Rename[type] = new Dictionary<String, String>();
            }

            Rename[type][property] = name;

            return this;
        }

        public PropertyContractResolver IgnoreProperty(Type type, String property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(property))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(property));
            }

            if (!Ignore.ContainsKey(type))
            {
                Ignore[type] = new HashSet<String>();
            }

            Ignore[type].Add(property);

            return this;
        }

        public PropertyContractResolver IgnoreProperty(Type type, params String?[] properties)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (properties.Length <= 0)
            {
                return this;
            }

            if (!Ignore.ContainsKey(type))
            {
                Ignore[type] = new HashSet<String>();
            }

            Ignore[type].AddRange(properties.WhereNotNullOrEmpty());
            return this;
        }

        public PropertyContractResolver OrderProperty(Type type, String property, Int32 order)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(property))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(property));
            }

            if (!Order.ContainsKey(type))
            {
                Order[type] = new Dictionary<String, Int32>();
            }

            Order[type][property] = order;
            return this;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            JsonProperty handler = ResolverCreatePropertyHandler(member, serialization);

            Type type = handler.DeclaringType ?? throw new InvalidOperationException();

            String? property = handler.PropertyName;

            if (property is null)
            {
                return handler;
            }

            if (IsIgnored(type, property))
            {
                handler.ShouldSerialize = _ => false;
                handler.Ignored = true;
            }

            if (IsRenamed(type, property, out String? name))
            {
                handler.PropertyName = name;
            }

            if (IsOrdered(type, property, out Int32 order))
            {
                handler.Order = order;
            }

            return handler;
        }

        protected virtual Boolean IsIgnored(Type type, String property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Ignore.TryGetValue(type, out ISet<String>? ignore) && ignore.Contains(property);
        }

        protected virtual Boolean IsRenamed(Type type, String property, out String? name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (Rename.TryGetValue(type, out IDictionary<String, String>? rename) && rename.TryGetValue(property, out name))
            {
                return true;
            }

            name = default;
            return false;
        }

        protected virtual Boolean IsOrdered(Type type, String property, out Int32 order)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (Order.TryGetValue(type, out IDictionary<String, Int32>? ordering) && ordering.TryGetValue(property, out order))
            {
                return true;
            }

            order = default;
            return false;
        }
    }
}