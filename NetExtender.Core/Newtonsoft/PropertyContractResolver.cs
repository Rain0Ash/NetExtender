// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Newtonsoft
{
    public class PropertyContractResolver : NewtonsoftContractResolverWrapper
    {
        private IDictionary<Type, ISet<String>> Ignore { get; }
        private IDictionary<Type, IDictionary<String, String>> Rename { get; }
        private IDictionary<Type, IDictionary<String, Int32>> Order { get; }

        protected delegate JsonProperty CreatePropertyHandler(MemberInfo info, MemberSerialization serialization);

        public PropertyContractResolver()
            : this(null)
        {
        }

        public PropertyContractResolver(DefaultContractResolver? resolver)
            : base(resolver)
        {
            Ignore = new Dictionary<Type, ISet<String>>();
            Rename = new Dictionary<Type, IDictionary<String, String>>();
            Order = new Dictionary<Type, IDictionary<String, Int32>>();
        }

        public PropertyContractResolver RenameProperty(Type type, String property, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(property))
            {
                throw new ArgumentNullOrEmptyStringException(property, nameof(property));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
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
                throw new ArgumentNullOrEmptyStringException(property, nameof(property));
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
                throw new ArgumentNullOrEmptyStringException(property, nameof(property));
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

            JsonProperty handler = base.CreateProperty(member, serialization);
            Type type = handler.DeclaringType ?? throw new InvalidOperationException();

            if (handler.PropertyName is not { } property)
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

            name = null;
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

            order = 0;
            return false;
        }
    }
}