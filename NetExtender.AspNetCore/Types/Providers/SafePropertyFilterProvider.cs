using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace NetExtender.AspNetCore.Types.Providers
{
    public sealed class PropertyDataMemberFilterProvider : IBindingMetadataProvider, IDisplayMetadataProvider
    {
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Type root = context.Key.ModelType;
            context.BindingMetadata.PropertyFilterProvider ??= new Safe(root);
        }

        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
        }

        private sealed class Safe : IPropertyFilterProvider
        {
            private Type Root { get; }

            public Safe(Type root)
            {
                Root = root;
            }

            public Func<ModelMetadata, Boolean> PropertyFilter { get; } = static metadata =>
            {
                if (metadata.PropertyGetter is null && metadata.PropertySetter is null)
                {
                    return false;
                }

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                PropertyInfo? property = metadata.ContainerType?.GetProperty(metadata.PropertyName ?? String.Empty, binding);

                if (property is null)
                {
                    return true;
                }

                if (property.GetIndexParameters().Length != 0)
                {
                    return false;
                }

                if (property.IsDefined(typeof(IgnoreDataMemberAttribute), true))
                {
                    return false;
                }

                Type type = property.PropertyType;
                Type? container = metadata.ContainerType;

                if (container is null)
                {
                    return true;
                }

                if (type == container)
                {
                    return false;
                }

                if (type.IsAssignableFrom(container))
                {
                    return false;
                }

                return !container.IsAssignableFrom(type);
            };
        }
    }

    public sealed class SafePropertyFilterProvider : IBindingMetadataProvider, IDisplayMetadataProvider
    {
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Type root = context.Key.ModelType;
            context.BindingMetadata.PropertyFilterProvider ??= new Safe(root);
        }

        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
        }

        private sealed class Safe : IPropertyFilterProvider
        {
            private Type Root { get; }

            public Safe(Type root)
            {
                Root = root;
            }

            public Func<ModelMetadata, Boolean> PropertyFilter { get; } = static metadata =>
            {
                if (metadata.PropertyGetter is null && metadata.PropertySetter is null)
                {
                    return false;
                }

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                PropertyInfo? property = metadata.ContainerType?.GetProperty(metadata.PropertyName ?? String.Empty, binding);

                if (property is null)
                {
                    return true;
                }

                if (property.GetIndexParameters().Length != 0)
                {
                    return false;
                }

                if (property.IsDefined(typeof(IgnoreDataMemberAttribute), true))
                {
                    return false;
                }

                if (property.IsDefined(typeof(JsonIgnoreAttribute), true))
                {
                    return false;
                }

                if (property.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>(true) is { Condition: System.Text.Json.Serialization.JsonIgnoreCondition.Always })
                {
                    return false;
                }

                Type type = property.PropertyType;
                Type? container = metadata.ContainerType;

                if (container is null)
                {
                    return true;
                }

                if (type == container)
                {
                    return false;
                }

                if (type.IsAssignableFrom(container))
                {
                    return false;
                }

                return !container.IsAssignableFrom(type);
            };
        }
    }
}