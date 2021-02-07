// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NetExtender.Utils.Types;

namespace NetExtender.NewtonSoft
{
    public class PropertyContractResolver : DefaultContractResolver
    {
        private IDictionary<Type, ISet<String>> Ignore { get; }
        private IDictionary<Type, IDictionary<String, String>> Rename { get; }
        private IDictionary<Type, IDictionary<String, Int32>> Order { get; }

        protected DefaultContractResolver Resolver { get; }

        protected delegate JsonProperty CreatePropertyHandler(MemberInfo info, MemberSerialization serialization);

        protected CreatePropertyHandler ResolverCreatePropertyHandler { get; }

        private CreatePropertyHandler CreateHandler(DefaultContractResolver resolver)
        {
            if (resolver is null)
            {
                return base.CreateProperty;
            }

            return Resolver.GetType()
                .GetMethod(nameof(CreateProperty), new[]{typeof(MemberInfo), typeof(MemberSerialization)})?
                .CreateDelegate<CreatePropertyHandler>() ?? base.CreateProperty;
        }
        
        public PropertyContractResolver()
            : this(null)
        {
        }

        public PropertyContractResolver(DefaultContractResolver resolver)
        {
            Ignore = new Dictionary<Type, ISet<String>>();
            Rename = new Dictionary<Type, IDictionary<String, String>>();
            Order = new Dictionary<Type, IDictionary<String, Int32>>();

            ResolverCreatePropertyHandler = CreateHandler(Resolver ??= resolver);
        }

        public PropertyContractResolver RenameProperty([NotNull] Type type, [NotNull] String property, [NotNull] String name)
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
        
        public PropertyContractResolver IgnoreProperty([NotNull] Type type, [NotNull] String property)
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
        
        public PropertyContractResolver IgnoreProperty([NotNull] Type type, [NotNull] params String[] properties)
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

        public PropertyContractResolver OrderProperty([NotNull] Type type, [NotNull] String property, Int32 order)
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

        protected override JsonProperty CreateProperty([NotNull] MemberInfo member, MemberSerialization serialization)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            JsonProperty property = ResolverCreatePropertyHandler(member, serialization);

            Type type = property.DeclaringType ?? throw new InvalidOperationException();
            
            if (IsIgnored(type, property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
                property.Ignored = true;
            }

            if (IsRenamed(type, property.PropertyName, out String name))
            {
                property.PropertyName = name;
            }

            if (IsOrdered(type, property.PropertyName, out Int32 order))
            {
                property.Order = order;
            }

            return property;
        }

        protected virtual Boolean IsIgnored([NotNull] Type type, String property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Ignore.TryGetValue(type, out ISet<String> ignore) && ignore.Contains(property);
        }

        protected virtual Boolean IsRenamed([NotNull] Type type, String property, out String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (Rename.TryGetValue(type, out IDictionary<String, String> rename) && rename.TryGetValue(property, out name))
            {
                return true;
            }

            name = default;
            return false;
        }

        protected virtual Boolean IsOrdered([NotNull] Type type, String property, out Int32 order)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (Order.TryGetValue(type, out IDictionary<String, Int32> ordering) && ordering.TryGetValue(property, out order))
            {
                return true;
            }

            order = default;
            return false;
        }
    }
}